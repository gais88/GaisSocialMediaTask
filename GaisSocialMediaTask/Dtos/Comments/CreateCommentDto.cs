using GaisSocialMediaTask.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace GaisSocialMediaTask.Api.Dtos.Comments
{
	public class CreateCommentDto
	{
		[Required, MaxLength(250)]
		public string Content { get; set; } = string.Empty;
		[Required]
		public int PostId { get; set; }

	}
}
