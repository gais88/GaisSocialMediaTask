using GaisSocialMediaTask.Api.Dtos.Comments;
using GaisSocialMediaTask.Core.Entities;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.ComponentModel.DataAnnotations;

namespace GaisSocialMediaTask.Api.Dtos.Posts
{
	public class PostListDto
	{
        public int Id { get; set; }
		
		public string Title { get; set; } = string.Empty;
		public int Like { get; set; }
		public string Content { get; set; } = string.Empty;
		public string? ImageUrl { get; set; } = string.Empty;
		public string AppUserId { get; set; } = string.Empty;
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;

        public ICollection<CommentDto>? commentDtos  { get; set; }
    }
}
