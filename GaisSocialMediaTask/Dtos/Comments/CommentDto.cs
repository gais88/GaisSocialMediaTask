using GaisSocialMediaTask.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace GaisSocialMediaTask.Api.Dtos.Comments
{
	public class CommentDto
	{
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
		public int PostId { get; set; }
		
		public string AppUserId { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
    }
}
