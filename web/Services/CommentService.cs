using web.Entities;
using web.Data;
using web.Helpers;
using AutoMapper;

namespace web.Services
{
    public interface ICommentService 
    {
        public void CreateComment(Comment comment);
        public void DeleteComment(int id);
        public void UpdateComment(Comment comment);
    }

    public class CommentService : ICommentService 
    {
        private DatabaseContext context;
        private IJwtUtils jwtUtils;
        public IMapper mapper;
        public CommentService(DatabaseContext context, IMapper mapper, IJwtUtils jwtUtils) 
        {
            this.context = context;
            this.mapper = mapper;
            this.jwtUtils = jwtUtils;
        }

        public void CreateComment(Comment comment) 
        {
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            var comment = context.Comments.SingleOrDefault(c => c.Id == id);
            if (comment == null)
            {
                throw new ApplicationException("Comment with this id doesn't exist!");
            }
            context.Comments.Remove(comment);
            context.SaveChanges();
        }

        public void UpdateComment(Comment comment)
        {
            var entity = context.Comments.Find(comment.Id);
            if (entity == null) 
            {
                throw new ApplicationException("Comment with this id doesn't exist!");
            }
           context.Entry(entity).CurrentValues.SetValues(comment);
           context.SaveChanges();
        }
    }
}