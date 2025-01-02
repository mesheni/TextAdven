namespace TextAdven
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using TextAdven.Character;
    using TextAdven.Logic;




    public class Program
    {
        public static void Main(string[] args)
        {
            // Начальное сообщение
            Console.WriteLine("Добро пожаловать в текстовое приключение!");
            Console.WriteLine("Напишите 'помощь', чтобы увидеть список доступных команд.");

            // Инициализация игрока и мира
            Player player = LoadGame() ?? new Player(); // Загрузка или создание нового игрока
            World world = new World();


            // Игровой цикл
            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine().ToLower().Trim(); // Удаление лишних пробелов

                // Обработка команд
                if (input == "помощь")
                {
                    ShowHelp();
                }
                else if (input == "осмотреться")
                {
                    world.LookAround(player.CurrentLocation);
                }
                else if (input.StartsWith("идти "))
                {
                    string direction = input.Substring(4).Trim(); // Удаление лишних пробелов
                    world.Move(player, direction);
                }
                else if (input == "инвентарь")
                {
                    player.ShowInventory();
                }
                // Добавьте другие команды здесь
                else if (input == "выход")
                {
                    break;
                }
                else if (input == "сохранить")
                {
                    SaveGame(player);
                }
                else
                {
                    Console.WriteLine("Неизвестная команда.");
                }
            }

            Console.WriteLine("Игра окончена.");
        }

        // Функция для отображения списка команд
        static void ShowHelp()
        {
            Console.WriteLine("Доступные команды:");
            Console.WriteLine("  помощь - показать этот список команд");
            Console.WriteLine("  осмотреться - осмотреть текущую локацию");
            Console.WriteLine("  идти <направление> - перейти в другую локацию");
            Console.WriteLine("  инвентарь - посмотреть содержимое инвентаря");
            Console.WriteLine("  сохранить - сохранить текущую игру");
            // Добавьте описание других команд здесь
            Console.WriteLine("  выход - завершить игру");
        }

        static void SaveGame(Player player, string filename = "savegame.xml")
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Player));
            using (TextWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, player);
            }
            Console.WriteLine("Игра сохранена.");
        }

        static Player LoadGame(string filename = "savegame.xml")
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Player));
                using (TextReader reader = new StreamReader(filename))
                {
                    return (Player)serializer.Deserialize(reader);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл сохранения не найден.");
                return null;
            }
        }
    }
}
