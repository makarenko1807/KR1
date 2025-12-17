using KR1.Data;
using KR1.Entities;
using KR1.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Repository
{ //Працює з історією ігор
    public class GameHistoryRepository : IGameHistoryRepository
    {
        private readonly InMemoryDatabase _db;

        public GameHistoryRepository(InMemoryDatabase db)
        {
            _db = db;
        }

        public void Add(GameRecord record)
        {
            _db.GameHistory.Add(record);
        }

        public IEnumerable<GameRecord> GetByPlayer(string username)
        {
            return _db.GameHistory.Where(r =>r.Player == username || r.Opponent == username);
        }
    }
    
}
    