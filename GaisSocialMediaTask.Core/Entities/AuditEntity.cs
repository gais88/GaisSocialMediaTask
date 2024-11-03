using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaisSocialMediaTask.Core.Entities
{
	public class AuditEntity
	{
        public int Id { get; set; }
        public DateTime? CreatedOn { get; set; }

		public long? CreatedBy { get; set; }

		public DateTime? ModifiedOn { get; set; }

		public long? ModifiedBy { get; set; }

		
	}
}
