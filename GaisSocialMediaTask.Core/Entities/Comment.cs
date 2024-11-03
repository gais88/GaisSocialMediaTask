using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaisSocialMediaTask.Core.Entities
{
	public class Comment:AuditEntity
	{
		[Required,MaxLength(250)]
        public string Content { get; set; } = string.Empty;
		[Required]
        public int PostId { get; set; }
		public Post Post { get; set; } = default!;
		[Required]
        public string AppUserId { get; set; }= string.Empty;
		public AppUser AppUser { get; set; } = default!;
	}
}
