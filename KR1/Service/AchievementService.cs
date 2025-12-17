using KR1.Entities;
using KR1.Repository.Base;
using KR1.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace KR1.Service
{ //Логіка роботи з досягненнями
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _achievementRepository;

        public AchievementService(IAchievementRepository achievementRepository)
        {
            _achievementRepository = achievementRepository;
        }

        public IEnumerable<Achievement> GetForUser(string username)
        {
            return _achievementRepository.GetForUser(username);
        }

        public void AddAchievement(string username, AchievementType type)
        {
            var achievement = new Achievement(username, (int)type);
            _achievementRepository.Add(achievement);
        }
    }
}
