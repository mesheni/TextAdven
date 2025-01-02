namespace TextAdven
{
    using Pastel;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Xml.Serialization;
    using TextAdven.Logic;
    using TextAdven.PlayChar;

    public class Program
    {
        public static void Main(string[] args)
        {
            // Начальное сообщение
            Console.WriteLine("Добро пожаловать в текстовое приключение!");
            Console.WriteLine($"Напишите {"помощь".Pastel(Color.Aqua)}, чтобы увидеть список доступных команд.");

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
                else if (input.StartsWith("говорить "))
                {
                    string characterName = input.Substring(8).Trim();
                    Character character = world.Locations[player.CurrentLocation].Characters.FirstOrDefault(c => c.Name.ToLower().Contains(characterName.ToLower()));
                    if (character != null)
                    {
                        character.Talk();
                    }
                    else
                    {
                        Console.WriteLine("Здесь нет такого персонажа.");
                    }
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
            Console.WriteLine($"  {"помощь".Pastel(Color.Aqua)} - показать этот список команд");
            Console.WriteLine($"  {"осмотреться".Pastel(Color.Aqua)} - осмотреть текущую локацию");
            Console.WriteLine($"  {"идти".Pastel(Color.Aqua)} {"<направление>".Pastel(Color.Bisque)} - перейти в другую локацию");
            Console.WriteLine($"  {"говорить".Pastel(Color.Aqua)} {"<персонаж>".Pastel(Color.Bisque)} - поговорить с персонажем");
            Console.WriteLine($"  {"инвентарь".Pastel(Color.Aqua)} - посмотреть содержимое инвентаря");
            Console.WriteLine($"  {"сохранить".Pastel(Color.Aqua)} - сохранить текущую игру");
            Console.WriteLine($"  {"выход".Pastel(Color.Aqua)} - завершить игру");
            // Добавьте описание других команд здесь
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
