using KR1.Service.Base;
using KR1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KR1.Repository.Base;

namespace KR1.Service
{
    // Робота з користувачами. Взаємодіє з репозиторієм
        public class UserService : IUserService
        {
            private readonly IUserRepository _userRepository;

            public UserService(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public bool Register(string username, string password, out string message)
            {
                message = "";

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    message = "Логін або пароль не можуть бути порожніми.";
                    return false;
                }

                if (_userRepository.GetByUsername(username) != null)
                {
                    message = "Користувач вже існує.";
                    return false;
                }

                string hash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
                var user = new User(username, password);

                _userRepository.Add(user);
                message = "Реєстрація успішна!";
                return true;
            }

            public User? Login(string username, string password, out string message)
            {
                message = "";
                var user = _userRepository.GetByUsername(username);

                if (user == null)
                {
                    message = "Користувача не знайдено.";
                    return null;
                }

                if (!user.CheckPassword(password))
                {
                    message = "Невірний пароль.";
                    return null;
                }

                message = "Вхід виконано успішно!";
                return user;
            }

            public int GetRating(string username)
            {
                return _userRepository.GetByUsername(username)?.Rating ?? 0;
            }

            public void UpdateRating(string username, int delta)
            {
                var user = _userRepository.GetByUsername(username);
                if (user == null)
                    return;

                user.Rating += delta;
                _userRepository.Update(user);
            }
        }
    }






