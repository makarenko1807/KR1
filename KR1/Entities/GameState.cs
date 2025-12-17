using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Entities
{
    //дані про поточну гру
    public class GameState
    {
        public char[,] Board { get; set; }
        public char CurrentPlayer { get; set; } = 'X';
        public bool IsFinished { get; set; } = false;

       
    }
}
