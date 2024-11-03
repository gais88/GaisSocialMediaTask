using AutoMapper;
using GaisSocialMediaTask.Api.Dtos.Posts;
using GaisSocialMediaTask.Api.Errors;
using GaisSocialMediaTask.Api.Extensions;
using GaisSocialMediaTask.Api.Responses;
using GaisSocialMediaTask.Api.Settings;
using GaisSocialMediaTask.Core.Entities;
using GaisSocialMediaTask.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace GaisSocialMediaTask.Api.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly string _imagesPath;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public PostController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
			_imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}"; ;
		}
		
		[HttpGet("Feed")]
		[ResponseCache(Duration =60, Location = ResponseCacheLocation.Any)]
		public async Task<ActionResult<PostListDto[]>> GetAll()
		{
			var result = await _unitOfWork.PostRepository.GetAllAsync();
			//to do 
			//// refactor images section
	       var request = HttpContext.Request;
			foreach (var item in result)
			{
				
				var baseUrl = $"{request.Scheme}://{request.Host}:{request.PathBase.ToUriComponent()}";
				if (!string.IsNullOrEmpty(item.ImageUrl))
				{
					item.ImageUrl = $"{baseUrl}{FileSettings.ImagesPath}/{item.ImageUrl}" ;
				}
			}
			if (result == null)
			{
				return NotFound(new DataResponse(false, "No Posts Found!"));
			}
			
			var dto =  _mapper.Map<List<PostListDto>>(result);
			return Ok(new ListDataResponse<PostListDto>(result.Count, dto));
		}
		[HttpPost("Add")]
		public async Task<ActionResult<PostDto>> CreatePost([FromForm]CreatePostDto model)
		{
			var user = await _unitOfWork.AppUserRepository.GetByIdAsync(User.GetUserId());
			if (user is null)
			{
				return Unauthorized(new DataResponse(false, "User Not Found"));
			}
			var image = await SaveImage(model.Image);
			var post = _mapper.Map<Post>(model);
			post.ImageUrl = image;
			// save image 

			post.AppUserId = User.GetUserId();
			await _unitOfWork.PostRepository.AddAsync(post);

			if (await _unitOfWork.SaveAsync())
			{

				var result = _mapper.Map<PostDto>(post);
				return Ok(new DataResponse<PostDto>(true,result,"Create Post Was Successflly"));
			}

			return BadRequest(new DataResponse(false, "Failed to Add Post!"));
		}
		[HttpPost("Like")]
		public async Task<ActionResult<bool>> AddLikeToPos(int postId)
		{
			if (postId == 0) return BadRequest(new DataResponse(false, "postId must be Not Empty"));

			var user = await _unitOfWork.AppUserRepository.GetByIdAsync(User.GetUserId());
			if (user is null)
			{
				return Unauthorized(new DataResponse(false, "User Not Found"));
			}
			if (await _unitOfWork.PostRepository.AddLike(postId))
			{
				return Ok(new DataResponse(true));
			}
			return BadRequest(new DataResponse(false, "Failed to like Post!"));
		}


		#region Helper Methods 
		private async Task<string> SaveImage(IFormFile? cover)
		{
			if (cover != null)
			{
				var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

				var path = Path.Combine(_imagesPath, coverName);

				using var stream = System.IO.File.Create(path);
				await cover.CopyToAsync(stream);

				return coverName;

			}
			return string.Empty;
		}
		#endregion
	}
}
