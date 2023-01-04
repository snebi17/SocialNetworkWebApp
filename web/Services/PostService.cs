using web.Entities;
using web.Data;
using web.Helpers;
using AutoMapper;

namespace web.Services
{
    public interface IPostService 
    {
        public List<Post> GetPosts(int id);
        public Post GetPost(int id);
        public void CreatePost(Post post);
        public void RemovePost(int id);
        public void UpdatePost(Post post);
    }

    public class PostService : IPostService 
    {
        private DatabaseContext context;
        private IJwtUtils jwtUtils;
        public IMapper mapper;
        public PostService(DatabaseContext context, IMapper mapper, IJwtUtils jwtUtils) 
        {
            this.context = context;
            this.mapper = mapper;
            this.jwtUtils = jwtUtils;
        }

        public List<Post> GetPosts(int id) 
        {
            var user = context.Users.SingleOrDefault(u => u.Id == id);
            return user.Posts.ToList();
        }

        public Post GetPost(int id) 
        {
            var post = context.Posts.SingleOrDefault(p => p.Id == id);
            if (post == null) 
            {
                throw new ApplicationException("Post either doesn't have an author or it doesn't exist anymore!");
            }
            return post;
        }
        
        public void CreatePost(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }

        public void RemovePost(int id) 
        {
            var post = context.Posts.SingleOrDefault(p => p.Id == id);
            if (post == null) 
            {
                throw new ApplicationException("Post either doesn't have an author or it doesn't exist anymore!");
            } 
            context.Posts.Remove(post);
            context.SaveChanges();
        }

        public void UpdatePost(Post post) {
            var entity = context.Posts.Find(post.Id);
            if (entity == null) 
            {
                throw new ApplicationException("Post doesn't exist!");
            }
            context.Entry(entity).CurrentValues.SetValues(post);
            context.SaveChanges();
        }
    }
}