using KR1.Entities;
using KR1.Repository.Base;
using KR1.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Service
{
    // виведення історії
    public class HistoryService : IHistoryService
    {
        private readonly IGameHistoryRepository _historyRepository;

        public HistoryService(IGameHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public void AddRecord(GameRecord record)
        {
            _historyRepository.Add(record);
        }

        public IEnumerable<GameRecord> GetHistory(string username)
        {
            return _historyRepository.GetByPlayer(username);
        }
    }
}