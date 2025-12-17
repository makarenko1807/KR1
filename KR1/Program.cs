using KR1.Data;
using KR1.Entities;
using KR1.Repository;
using KR1.Repository.Base;
using KR1.Service;
using KR1.Service.Base;


internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        //  СТВОРЕННЯ БАЗИ 
        var db = new InMemoryDatabase();

        IUserRepository userRepo = new UserRepository(db);
        IGameHistoryRepository historyRepo = new GameHistoryRepository(db);
        IAchievementRepository achievementRepo = new AchievementRepository(db);

        //   СЕРВІСИ
        IUserService userService = new UserService(userRepo);
        IHistoryService historyService = new HistoryService(historyRepo);
        IAchievementService achievementService = new AchievementService(achievementRepo);
        IGameService gameService = new GameService(userRepo, historyRepo, achievementRepo);

        User? currentUser = null;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== МЕНЮ ===");
            Console.WriteLine("1. Реєстрація");
            Console.WriteLine("2. Вхід");
            Console.WriteLine("3. Вийти з програми");
            Console.Write("Ваш вибір: ");

            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Логін: ");
                string username = Console.ReadLine()!;
                Console.Write("Пароль: ");
                string password = Console.ReadLine()!;

                if (userService.Register(username, password, out string msg))
                    Console.WriteLine($"✔ {msg}");
                else
                    Console.WriteLine($"✖ {msg}");

                Console.ReadKey();
            }
            else if (choice == "2")
            {
                Console.Write("Логін: ");
                string username = Console.ReadLine()!;
                Console.Write("Пароль: ");
                string password = Console.ReadLine()!;

                currentUser = userService.Login(username, password, out string msg);
                Console.WriteLine(msg);

                if (currentUser != null)
                {
                    Console.ReadKey();
                    UserMenu(currentUser, gameService, userService, historyService, achievementService);
                }
                else
                {
                    Console.ReadKey();
                }
            }
            else if (choice == "3")
            {
                break;
            }
        }
    }
    // МЕНЮ КОРИСТУВАЧА
    static void UserMenu(User user, IGameService gameService, IUserService userService, IHistoryService historyService, IAchievementService achievementService)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== Користувач: {user.Username} ===");
            Console.WriteLine("1. Перевірити рейтинг");
            Console.WriteLine("2. Почати гру");
            Console.WriteLine("3. Переглянути історію ігор");
            Console.WriteLine("4. Переглянути досягнення");
            Console.WriteLine("5. Вийти з акаунту");
            Console.Write("Ваш вибір: ");

            var choice = Console.ReadLine();

            if (choice == "1")
            {
                int rating = userService.GetRating(user.Username);
                Console.WriteLine($"Ваш рейтинг: {rating}");
                Console.ReadKey();
            }
            else if (choice == "2")
            {
                Console.Clear();
                Console.WriteLine("Оберіть режим гри:");
                Console.WriteLine("1. Гравець vs Бот");
                Console.WriteLine("2. Гравець vs Гравець");
                Console.Write("Ваш вибір: ");
                var modeChoice = Console.ReadLine();

                if (modeChoice == "1")
                {
                    // старт гри з ботом
                    gameService.StartNewGame(user.Username, GameMode.PlayerVsBot);
                    PlayGame(user, gameService);
                }
                else if (modeChoice == "2")
                {
                    Console.Write("Введіть ім'я другого гравця: ");
                    string opponent = Console.ReadLine()!;

                    gameService.StartNewGame(user.Username, GameMode.PlayerVsPlayer, opponent);
                    PlayGame(user, gameService);
                }
            }
            else if (choice == "3")
            {
                var history = historyService.GetHistory(user.Username);
                Console.WriteLine("=== ІСТОРІЯ ІГОР ===");
                foreach (var h in history)
                    Console.WriteLine($"{h.Date}: {h.Player} vs {h.Opponent} — {h.Result}");
                Console.ReadKey();
            }
            else if (choice == "4")
            {
                var achievements = achievementService.GetForUser(user.Username);

                Console.WriteLine("=== ДОСЯГНЕННЯ ===");
                foreach (var a in achievements)
                    Console.WriteLine($"{(AchievementType)a.AchievementTypeId} — {a.EarnedAt}");

                Console.ReadKey();
            }

            else if (choice == "5")
            {
                break;
            }
        }
    }

    // ЛОГІКА ПРОЦЕСУ ГРИ
    static void PlayGame(User currentUser, IGameService gameService)
    {
        while (true)
        {
            Console.Clear();
            var board = gameService.GetBoard();

            Console.WriteLine("=== ГРА ХРЕСТИКИ-НУЛИКИ ===");
            Console.WriteLine($"Хід робить: {gameService.GetCurrentPlayerName()} ({gameService.CurrentPlayer})\n");

            // Вивести поле
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(" " + board[i, 0] + " | " + board[i, 1] + " | " + board[i, 2]);
                if (i < 2) Console.WriteLine("---+---+---");
            }

            // 1) ХІД ГРАВЦЯ (X або O)
            if (gameService.Mode == GameMode.PlayerVsPlayer ||
                (gameService.Mode == GameMode.PlayerVsBot && gameService.CurrentPlayer == 'X'))
            {
                Console.Write("\nРядок (0-2): ");
                int row = int.Parse(Console.ReadLine()!);
                Console.Write("Колонка (0-2): ");
                int col = int.Parse(Console.ReadLine()!);

                if (!gameService.MakeMove(row, col))
                {
                    Console.WriteLine("Неможливий хід!");
                    Console.ReadKey();
                    continue;
                }
            }
            else
            {
                // 2) ХІД БОТА
                Console.WriteLine("\nБот думає...");
                Thread.Sleep(700);
                gameService.MakeBotMove();
            }

            // 3) Перевірка результату гри
            GameResult result = gameService.ProcessGameState();

            if (result != GameResult.InProgress)
            {
                Console.Clear();

                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(" " + board[i, 0] + " | " + board[i, 1] + " | " + board[i, 2]);
                    if (i < 2) Console.WriteLine("---+---+---");
                }

                Console.WriteLine();

                if (result == GameResult.WinX) Console.WriteLine("Переміг X!");
                else if (result == GameResult.WinO) Console.WriteLine("Переміг O!");
                else Console.WriteLine("Нічия!");

                Console.ReadKey();
                break;
            }
        }
    }
}

