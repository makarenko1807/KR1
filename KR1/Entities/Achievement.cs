using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KR1.Entities
{
    public class Achievement
    { // Представляє конкретне отримане досягнення користувачем.
        public Guid Id { get; set; } = Guid.NewGuid();

        // Хто отримав досягнення
        public string Username { get; set; }
        // Посилання на тип досягнення  
        public int AchievementTypeId { get; set; }

        // Час, коли було отримано досягнення
        public DateTime EarnedAt { get; set; } = DateTime.Now;

        public Achievement() { }

        public Achievement(string username, int achievementTypeId)
        {
            Username = username;
            AchievementTypeId = achievementTypeId;
        }
    }
}
