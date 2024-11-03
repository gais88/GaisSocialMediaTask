using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaisSocialMediaTask.Data.Repositories
{
	public interface IUnitOfWork
	{
		public IAppUserRepository AppUserRepository { get; }
		public IPostRepository PostRepository { get; }
		public ICommentRepository CommentRepository { get; }
		Task<bool> SaveAsync();
	}
}
