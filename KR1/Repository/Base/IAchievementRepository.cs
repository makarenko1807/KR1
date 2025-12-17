using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KR1.Entities;

namespace KR1.Repository.Base
{
    public interface IAchievementRepository
    {
        IEnumerable<Achievement> GetForUser(string username);
        void Add(Achievement achievement);
    }
}
