namespace Stregsystem.Models
{
    public class UserFactory
    {
        private int _id = 1;
        public User CreateUser(string[] firstnames, string lastname, string username, string email)
        {
            return new User(firstnames, lastname, username, email, _id++);
        }
    }
}
