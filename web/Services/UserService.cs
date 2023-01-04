using web.Entities;
using web.Data;
using web.Helpers;
using web.Models;
using AutoMapper;
using BCrypt.Net;

namespace web.Services
{
    public interface IUserService 
    {
        public void Register(RegisterModel model);
        public AuthResponse Authenticate(AuthRequest model);
        public List<User> GetUsers();
        public User GetUser(int id);
        public void RemoveUser(int id);
        public void UpdateUser(User user);
    }

    public class UserService : IUserService 
    {
        private DatabaseContext context;
        private IJwtUtils jwtUtils;
        public IMapper mapper;
        public UserService(DatabaseContext context, IMapper mapper, IJwtUtils jwtUtils) 
        {
            this.context = context;
            this.mapper = mapper;
            this.jwtUtils = jwtUtils;
        }
        public void Register(RegisterModel model) {
            if (context.Users.Any(u => u.Email == model.Email)) {
                throw new ApplicationException("Email already exists!");
            }
            var user = mapper.Map<User>(model);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            user.CreatedAt = DateTime.Now;
            context.Users.Add(user);
            context.SaveChanges();
        }
        public AuthResponse Authenticate(AuthRequest model) {
            var user = context.Users.SingleOrDefault(u => u.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                throw new ApplicationException("Email or password incorrect!");
            }

            var response = mapper.Map<AuthResponse>(user);
            response.Token = jwtUtils.GenerateToken(user);
            response.ExpirationDate = DateTime.Now.AddMinutes(15);
            return response;
        }

        public List<User> GetUsers() {
            return context.Users.ToList();
        }

        public User GetUser(int id) {
            return context.Users.SingleOrDefault<User>(u => u.Id == id);
        }
        
        public void RemoveUser(int id) {
            var user = context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null) {
                throw new ApplicationException("User with this ID couldn't be found!");
            } 
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var entity = context.Users.Find(user.Id);
            if (entity == null)
            {
                throw new ApplicationException("User with this ID couldn't be found!");
            }
            context.Entry(entity).CurrentValues.SetValues(user);
            context.SaveChanges();
        }
    }
}