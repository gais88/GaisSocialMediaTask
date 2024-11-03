using GaisSocialMediaTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaisSocialMediaTask.Data.Repositories
{
	public interface IPostRepository:IGenericRepository<Post>
	{
		Task<bool> AddLike(int postId);
	}
}
