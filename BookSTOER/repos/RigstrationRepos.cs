using Azure.Core;
using BookSTOER.Data;
using BookSTOER.Model;
using BookSTOER.NewFolder.repos;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;

namespace BookSTOER.repos
{
    public class RigstrationRepos : IRigstrationRepos
    {
        private readonly dbcontext dbcontext;

        public RigstrationRepos(dbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public LoginResult Rigstration(Rigstration Rigstration)
        {

            if (dbcontext.Set<User>().Any(x => x.Email == Rigstration.Email))
            {
                return new LoginResult { Success = false, Message = "Rigstration data is missing." };
            }
            using var stream = new MemoryStream();
            if (Rigstration.image != null)
            {
                Rigstration.image.CopyTo(stream);
            }

            var hashPassword = BCrypt.Net.BCrypt.HashPassword(Rigstration.password);
            var user = new User
            {
                Email = Rigstration.Email,
                FullName = Rigstration.FullName,
                Address = Rigstration.Address,
                PhoneNumber = Rigstration.PhoneNumber,
                password = hashPassword,
                image = stream.ToArray(),

            };
            dbcontext.Set<User>().Add(user);
            dbcontext.SaveChanges();
            return new LoginResult
            {
                Success = true,
                Message = "Rigstration successful",
                Data = new
                {
                    user.Id,
                    user.Email,
                    user.FullName,
                    user.Address,
                    user.PhoneNumber,
                    user.image,
                    user.Orders,

                }


            };
        }
    }
}
