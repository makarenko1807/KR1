using KR1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Repository.Base
{
    public interface IGameHistoryRepository
    {
        void Add(GameRecord record);
        IEnumerable<GameRecord> GetByPlayer(string username);
    }
}
