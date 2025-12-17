using KR1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR1.Service.Base
{
    // логіка гри
    public interface IGameService
    {
        void StartNewGame(string username);
        void StartNewGame(string username, GameMode mode, string? opponent = null);

        bool MakeMove(int row, int col);

        char[,] GetBoard();

        char CurrentPlayer { get; }

        GameResult ProcessGameState();
        bool MakeBotMove();
        GameMode Mode { get; }
        string GetCurrentPlayerName();


    }
}

        

