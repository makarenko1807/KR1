using KR1.Entities;
using KR1.Service.Base;
using KR1.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KR1.Service
{
    // Хрестики-нулики
    public class GameService : IGameService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGameHistoryRepository _historyRepository;
        private readonly IAchievementRepository _achievementRepository;

        private GameState _gameState;
        private string _player1;      // Той, хто зайшов у систему
        private string _player2;      // Або Bot, або реальний гравець
        private GameMode _mode;
        public GameMode Mode => _mode;

        public char CurrentPlayer => _gameState.CurrentPlayer;

        public GameService(
            IUserRepository userRepository,
            IGameHistoryRepository historyRepository,
            IAchievementRepository achievementRepository)
        {
            _userRepository = userRepository;
            _historyRepository = historyRepository;
            _achievementRepository = achievementRepository;

            _gameState = new GameState();
        }

        //  Почати нову гру (режим + другий гравець)
               public void StartNewGame(string username)
        {
            // За замовчуванням: Player vs Bot
            StartNewGame(username, GameMode.PlayerVsBot, null);
        }

        public void StartNewGame(string username, GameMode mode, string? opponent = null)
        {
            _player1 = username;
            _mode = mode;

            _player2 = mode == GameMode.PlayerVsBot ? "Bot" : opponent ?? "Unknown";

            _gameState = new GameState
            {
                Board = new char[3, 3],
                CurrentPlayer = 'X',
                IsFinished = false
            };

            // заповнюємо поле пробілами
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    _gameState.Board[i, j] = ' ';
        
        }


        //  Отримати поле
        public char[,] GetBoard()
        {
            return _gameState.Board;
        }

        //  Зробити хід
        public bool MakeMove(int row, int col)
        {
            var board = _gameState.Board;

            if (_gameState.IsFinished)
                return false;

            if (row < 0 || row > 2 || col < 0 || col > 2)
                return false;

            // Клітинка зайнята
            if (board[row, col] != ' ')
                return false;

            // Хід гравця
            board[row, col] = _gameState.CurrentPlayer;


            return true;
        }


        public bool MakeBotMove()
        {
            if (_gameState.IsFinished)
                return false;

            var board = _gameState.Board;
            Random rnd = new Random();

            while (true)
            {
                int row = rnd.Next(0, 3);
                int col = rnd.Next(0, 3);

                if (board[row, col] == ' ')
                {
                    board[row, col] = _gameState.CurrentPlayer;
                    return true;
                }
            }
        }




        //  Перевірка статусу гри
        public GameResult ProcessGameState()
        {
            char justMoved = _gameState.CurrentPlayer;

            // Перевірка виграшу
            if (HasPlayerWon(justMoved))
            {
                _gameState.IsFinished = true;

                string winner = justMoved == 'X' ? _player1 : _player2;
                string loser = justMoved == 'X' ? _player2 : _player1;

                SaveHistory(winner, loser, "Win");

                UpdateRating(winner, +25);
                UpdateRating(loser, -10);

                GiveAchievements(winner);

                return justMoved == 'X' ? GameResult.WinX : GameResult.WinO;

            }

            // Перевірка нічиєї
            if (IsBoardFull())
            {
                _gameState.IsFinished = true;
                SaveHistory(_player1, _player2, "Draw");
                return GameResult.Draw;
            }

            // Гра продовжується – переключення гравців
            _gameState.CurrentPlayer = justMoved == 'X' ? 'O' : 'X';
            return GameResult.InProgress;
        }

        // Допоміжні методи гри
        private bool IsBoardFull()
        {
            foreach (var c in _gameState.Board)
                if (c == ' ')
                    return false;
            return true;
        }

        public string GetCurrentPlayerName()
        {
            return _gameState.CurrentPlayer == 'X' ? _player1 : _player2;
        }

        private bool HasPlayerWon(char p)
        {
            // рядки та стовпці
            for (int i = 0; i < 3; i++)
            {
                if (_gameState.Board[i, 0] == p &&
                    _gameState.Board[i, 1] == p &&
                    _gameState.Board[i, 2] == p)
                    return true;

                if (_gameState.Board[0, i] == p &&
                    _gameState.Board[1, i] == p &&
                    _gameState.Board[2, i] == p)
                    return true;
            }

            // діагоналі
            if (_gameState.Board[0, 0] == p &&
                _gameState.Board[1, 1] == p &&
                _gameState.Board[2, 2] == p)
                return true;

            if (_gameState.Board[0, 2] == p &&
                _gameState.Board[1, 1] == p &&
                _gameState.Board[2, 0] == p)
                return true;

            return false;
        }

        //  ЗБЕРЕЖЕННЯ ІСТОРІЇ
        private void SaveHistory(string winner, string loser, string result)
        {
            _historyRepository.Add(new GameRecord(
                winner,
                loser,
                result,
                DateTime.Now
            ));
        }

        //  ОНОВЛЕННЯ РЕЙТИНГУ
        private void UpdateRating(string username, int delta)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null) return;

            user.Rating += delta;
            _userRepository.Update(user);
        }

        //  ДОСЯГНЕННЯ
        private void GiveAchievements(string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null) return;

            
            if (!user.Achievements.Contains((int)AchievementType.FirstWin))
            {
                user.Achievements.Add((int)AchievementType.FirstWin);
                _achievementRepository.Add(new Achievement(username, (int)AchievementType.FirstWin));
            }

            _userRepository.Update(user);
        }


    }
}

    

