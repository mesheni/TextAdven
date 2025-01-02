using Pastel;
using System.Drawing;
using TextAdven.PlayChar;

namespace TextAdven.Logic
{
    public class World
    {
        public Dictionary<string, Location> Locations { get; set; } = new Dictionary<string, Location>();

        public World()
        {
            // Инициализация локаций
            Locations.Add("начало", new Location("Вы находитесь в начале...", new Dictionary<string, string>() { { "север", "лес" } }));
            Locations.Add("лес", new Location("Вы в темном лесу...", new Dictionary<string, string>() { { "юг", "начало" }, { "восток", "пещера" } }));
            Locations.Add("пещера", new Location("Вы в сырой пещере...", new Dictionary<string, string>() { { "запад", "лес" } }));
            // Добавьте другие локации здесь

            // Добавление персонажей
            Character oldMan = new Character($"{"Старик".Pastel(Color.FromArgb(165, 229, 250))}", "Приветствую, путник. Куда путь держишь?");
            oldMan.Responses.Add("В лес", "Осторожнее там, говорят, в лесу водятся волки.");
            oldMan.Responses.Add("Не знаю", "Хм, странно...");
            Locations["начало"].Characters.Add(oldMan);
        }

        public void LookAround(string locationName)
        {
            if (Locations.ContainsKey(locationName))
            {
                Console.WriteLine(Locations[locationName].Description);
                ShowExits(locationName);
                Locations[locationName].ShowCharacters(); // Показать персонажей
            }
            else
            {
                Console.WriteLine("Вы не можете осмотреться здесь.");
            }
        }

        public void Move(Player player, string direction)
        {
            string currentLocation = player.CurrentLocation;

            if (Locations.ContainsKey(currentLocation) && Locations[currentLocation].Exits.ContainsKey(direction))
            {
                player.CurrentLocation = Locations[currentLocation].Exits[direction];
                Console.WriteLine($"Вы идете на {direction}.");
                LookAround(player.CurrentLocation);
            }
            else
            {
                Console.WriteLine("Вы не можете идти в этом направлении.");
            }
        }

        public void ShowExits(string locationName)
        {
            if (Locations.ContainsKey(locationName) && Locations[locationName].Exits.Count > 0)
            {
                Console.WriteLine("Выходы:");
                foreach (KeyValuePair<string, string> exit in Locations[locationName].Exits)
                {
                    Console.WriteLine("  " + exit.Key);
                }
            }
            else
            {
                Console.WriteLine("Здесь нет выходов.");
            }
        }
    }
}
