using AutoMapper;
using GaisSocialMediaTask.Api.Dtos.Comments;
using GaisSocialMediaTask.Core.Entities;

namespace GaisSocialMediaTask.Api.Profiles
{
	public class CommentProfile:Profile
	{
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>()
				.ForMember(viewModel => viewModel.FirstName, model => model.MapFrom(model => model.AppUser.FirstName))
				.ForMember(viewModel => viewModel.LastName, model => model.MapFrom(model => model.AppUser.LastName))
				.ReverseMap();
            CreateMap<Comment, CreateCommentDto>().ReverseMap();
        }
    }
}
