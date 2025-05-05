using BookSTOER.Model;

namespace BookSTOER.repos
{
    public interface IuserRepos
    {
        public List<User> GetUsers();
        public User GetUser(int id);

        public LoginResult UpdateRole(int id , string role);
        public User RemoveUser(int id);
    }
}
