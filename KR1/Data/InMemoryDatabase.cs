using KR1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Data
{ //Зберігає тимчасові дані програми у памʼяті:

    public class InMemoryDatabase
    {
        public List<User> Users { get; set; } = new List<User>();
        public List<GameRecord> GameHistory { get; set; } = new List<GameRecord>();
        public List<Achievement> Achievements { get; set; } = new List<Achievement>();
    }

}
