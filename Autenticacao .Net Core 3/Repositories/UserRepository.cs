using System.Collections.Generic;
using System.Linq;
using Autenticacao_.Net_Core_3.Models;

namespace Autenticacao_.Net_Core_3.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User {Id = 1, Username = "batman", Password = "batman", Role = "manager"});
            users.Add(new User{ Id = 2, Username = "roben", Password= "roben", Role = "employee"});
            return users.FirstOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}