using System.Collections.Generic;
using System.Linq;
using Assignment4.Core;
using Lecture05.Entities;

namespace Assignment4.Entities
{
    public class UserRepository : IUserRepository

    {
        private readonly IKanbanContext _context;

        public UserRepository(IKanbanContext context)
        {
            _context = context;
        }
        public (Response Response, int UserId) Create(UserCreateDTO user)
        {
            if (GetUserFromEmail(user.Email) != null)
            {
                return (Response.Conflict, -1);
            }

            var entity = new User
            {
                Email = user.Email,
                Name = user.Name,

            };

            _context.Users.Add(entity);
            _context.SaveChanges();

            return (Response.Created, entity.ID);
        }

        public Response Delete(int userId, bool force = false)
        {
            var user = _context.Users.Find(userId);

            if (user.Tasks?.Count > 0)
            {
                if (force)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();

                    return Response.Deleted;
                }
                else
                {
                    return Response.Conflict;
                }
            }
            else
            {
                _context.Users.Remove(user);
                _context.SaveChanges();

                return Response.Deleted;
            }
        }

        public UserDTO Read(int userId)
        {
            var user = from u in _context.Users
                       where u.ID == userId
                       select new UserDTO(
                           u.ID,
                           u.Name,
                           u.Email
                       );

            return user.FirstOrDefault();
        }

        public IReadOnlyCollection<UserDTO> ReadAll()
        {
            var user = from u in _context.Users
                       select new UserDTO(
                           u.ID,
                           u.Name,
                           u.Email
                       );

            return user.ToList().AsReadOnly();
        }

        public Response Update(UserUpdateDTO user)
        {
            var userToBeUpdated = _context.Users.Find(user.Id);

            if (userToBeUpdated == null)
            {
                return Response.NotFound;
            }
            else
            {
                userToBeUpdated.Email = user.Email;
                userToBeUpdated.ID = user.Id;
                userToBeUpdated.Name = user.Name;

                _context.Users.Update(userToBeUpdated);
                _context.SaveChanges();

                return Response.Updated;
            }
        }

        private User GetUserFromEmail(string email)
        {
            return _context.Users.Where(s => s.Email == email).FirstOrDefault();
        }
    }
}
