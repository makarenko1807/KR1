using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Entities
{
    // Запис однієї завершеної гри.
    public class GameRecord
    {
        public string Player { get; set; }
        public string Opponent { get; set; }
        public string Result { get; set; }      // "Win", "Lose", "Draw"
        public DateTime Date { get; set; }
        public string Winner { get; set; }


        public GameRecord(string player, string opponent, string result, DateTime date )
        {
            Player = player;
            Opponent = opponent;
            Result = result;
            Date = DateTime.Now;
        }
    }
}
