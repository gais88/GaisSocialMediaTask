using AutoMapper;
using GaisSocialMediaTask.Api.Dtos.Posts;
using GaisSocialMediaTask.Api.Settings;
using GaisSocialMediaTask.Core.Entities;

namespace GaisSocialMediaTask.Api.Profiles
{
	public class PostProfile:Profile
	{
		
		public PostProfile()
        {
			
			CreateMap<Post, PostListDto>()
			   .ForMember(viewModel => viewModel.FirstName, model => model.MapFrom(model => model.AppUser.FirstName))
			   .ForMember(viewModel => viewModel.LastName, model => model.MapFrom(model => model.AppUser.LastName))
			   .ForMember(viewModel => viewModel.commentDtos, model => model.MapFrom(model => model.Comments))
			   .ReverseMap();
			CreateMap<Post,PostDto>()
				.ForMember(viewModel => viewModel.FirstName, model => model.MapFrom(model => model.AppUser.FirstName))
				.ForMember(viewModel => viewModel.LastName, model => model.MapFrom(model => model.AppUser.LastName))
				.ReverseMap();
			CreateMap<Post, CreatePostDto>()
				
				.ReverseMap();
		}
    }
}
