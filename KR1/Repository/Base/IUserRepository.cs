using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KR1.Entities;

namespace KR1.Repository.Base
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        void Add(User user);
        void Update(User user);
        IEnumerable<User> GetAll();
    }
}
