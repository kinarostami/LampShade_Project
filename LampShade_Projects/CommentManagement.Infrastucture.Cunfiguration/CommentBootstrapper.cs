using CommentManagement.Application;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastucture.EFCore;
using CommentManagement.Infrastucture.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Infrastucture.Configuration
{
    public class CommentBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentApplication, CommentApplication>();

            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
