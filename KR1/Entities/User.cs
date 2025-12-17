using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Entities
{
    // Представляє зареєстрованого користувача системи.
    // Зберігаємо логін, хешований пароль, рейтинг та досягнення.
    public class User
    {
            public string Username { get; set; }
            public string PasswordHash { get; set; }

            public int Rating { get; set; } = 1000;

            public List<int> Achievements { get; set; } = new List<int>();
            


        public User(string username, string password)
            {
                Username = username;
                PasswordHash = HashPassword(password);
            }
        private string HashPassword(string password)
        {
            
           
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        // Перевірка пароля
        public bool CheckPassword(string password)
        {
            return PasswordHash == HashPassword(password);
        }

    }
}
