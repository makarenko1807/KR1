using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Entities
{ // Результат ходу або завершення гри.
    public enum GameResult
    {
        InProgress,
        WinX,
        WinO,
        Draw
    }
}
