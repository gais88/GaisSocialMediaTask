using System.ComponentModel.DataAnnotations;

namespace GaisSocialMediaTask.Api.Dtos.Posts
{
	public class CreatePostDto
	{
		[Required, MaxLength(50)]
		public string Title { get; set; } = string.Empty;
		[Required, MaxLength(500)]
		public string Content { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }


    }
}
