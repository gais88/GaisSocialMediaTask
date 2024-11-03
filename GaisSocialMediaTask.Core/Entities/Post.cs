using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaisSocialMediaTask.Core.Entities
{
	public class Post:AuditEntity
	{
		[Required,MaxLength(50)]
		public string Title { get; set; } = string.Empty;
		[Required,MaxLength(500)]
		public string Content { get; set; } = string.Empty;
		public string? ImageUrl { get; set; } = string.Empty;
		public string AppUserId { get; set; } = string.Empty;
		public AppUser AppUser { get; set; } = default!;
        public int Like { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
