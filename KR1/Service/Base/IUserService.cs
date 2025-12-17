using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KR1.Entities;


namespace KR1.Service.Base
{
    // сервіс для реєстрації
    public interface IUserService
    {
        bool Register(string username, string password, out string message);
        User? Login(string username, string password, out string message);
        int GetRating(string username);
        void UpdateRating(string username, int delta);
    }
} 
