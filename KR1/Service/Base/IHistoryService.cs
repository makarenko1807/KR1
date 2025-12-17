using KR1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Service.Base
{
    public interface IHistoryService
    {
        void AddRecord(GameRecord record);
        IEnumerable<GameRecord> GetHistory(string username);
    }
}
