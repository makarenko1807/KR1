using KR1.Data;
using KR1.Repository.Base;
using KR1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Repository
{ //Працює з досягненнями
    public class AchievementRepository : IAchievementRepository
    {
        private readonly InMemoryDatabase _db;

        public AchievementRepository(InMemoryDatabase db)
        {
            _db = db;
        }

        public IEnumerable<Achievement> GetForUser(string username)
        {
            return _db.Achievements.Where(a => a.Username == username);
        }

        public void Add(Achievement achievement)
        {
            _db.Achievements.Add(achievement);
        }
    }
}
