using System.Data;
using Azure.Core;
using BookSTOER.Data;
using BookSTOER.Model;
using Microsoft.EntityFrameworkCore;

namespace BookSTOER.repos
{
    public class UserRepos : IuserRepos
    {
        private readonly dbcontext dbcontext;

        public UserRepos(dbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public User GetUser(int id)
        {
            var s = dbcontext.Set<User>().FirstOrDefault(u => u.Id == id);

            return s;
        }

        public List<User> GetUsers()
        {
            var all = dbcontext.Set<User>().ToList();
            dbcontext.SaveChangesAsync();
            return all;
        }

        public User RemoveUser(int id)
        {
            var se = dbcontext.Set<User>().FirstOrDefault();
            dbcontext.Set<User>().Remove(se);
            dbcontext.SaveChanges();
            return se;
        }

        public LoginResult UpdateRole(int id, string role)
        {
            var user = dbcontext.Set<User>().FirstOrDefault(u => u.Id == id);
            user.Role = role;
            if (user == null)
            {
                return new LoginResult { Success = false, Message = "Not Find User" };
            }


            dbcontext.SaveChanges();


            return new LoginResult
            {
                Success = true,
                Message = "Update successful",
                Data = new
                {
                    user.Id,
                    user.Role,

                }

            };
        }
    }
}
