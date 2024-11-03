using AutoMapper;
using GaisSocialMediaTask.Api.Dtos.Comments;
using GaisSocialMediaTask.Api.Dtos.Posts;
using GaisSocialMediaTask.Api.Extensions;
using GaisSocialMediaTask.Api.Responses;
using GaisSocialMediaTask.Core.Entities;
using GaisSocialMediaTask.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace GaisSocialMediaTask.Api.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CommentController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		[HttpPost("Comment")]
		public async Task<ActionResult<CommentDto>> AddCommentToPost([FromBody] CreateCommentDto model)
		{
			var user = await _unitOfWork.AppUserRepository.GetByIdAsync(User.GetUserId());
			if (user is null)
			{
				return Unauthorized(new DataResponse(false, "User Not Found"));
			}
			var comment = _mapper.Map<Comment>(model);
			comment.AppUserId = User.GetUserId();

			await _unitOfWork.CommentRepository.AddAsync(comment);	
			if (await _unitOfWork.SaveAsync())
			{
				var result = _mapper.Map<CommentDto>(comment);
				return Ok(new DataResponse<CommentDto>(true, result, "add comment Was Successflly"));
			}

			return BadRequest(new DataResponse(false, "Failed to Add Comment!"));
		}
	}
}
