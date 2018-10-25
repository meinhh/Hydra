using System;
using System.Linq;
using Hydra.Data;
using Hydra.Models;

namespace Hydra.DAL
{
    public class UserDataAccess
    {
        private readonly HydraContext _context;

        public UserDataAccess(HydraContext context)
        {
            _context = context;
        }

        public void AddUser(string id, string name, Gender gender)
        {
             _context.User.Add(new User
            {
                ID = id,
                Name = name,
                Gender = gender
            });

            _context.SaveChanges();
        }

        public bool IsUserExist(string id)
        {
            return _context.User.Any(u => u.ID == id);
        }

        public User GetUser(string id) 
        {
            return _context.User.FirstOrDefault(u => u.ID == id);
        }
    }
}
