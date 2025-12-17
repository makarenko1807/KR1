using KR1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Service.Base
{
    public interface IAchievementService
    {
        IEnumerable<Achievement> GetForUser(string username);
        void AddAchievement(string username, AchievementType type);
    }
}
