using KR1.Data;
using KR1.Entities;
using KR1.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Repository
{ //Працює з користувачами
    public class UserRepository : IUserRepository
    {
        private readonly InMemoryDatabase _db;

        public UserRepository(InMemoryDatabase db)
        {
            _db = db;
        }

        public User? GetByUsername(string username)
        {
            return _db.Users.FirstOrDefault(u => u.Username == username);
        }

        public void Add(User user)
        {
            _db.Users.Add(user);
        }

        public void Update(User user)
        {
            
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }
    }
    
}