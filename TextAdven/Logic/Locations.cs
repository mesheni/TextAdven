using TextAdven.PlayChar;

namespace TextAdven.Logic
{
    public class Location
    {
        public string Description { get; set; }
        public Dictionary<string, string> Exits { get; set; }
        public List<Character> Characters { get; set; } = new List<Character>(); // Добавлено

        public Location(string description, Dictionary<string, string> exits)
        {
            Description = description;
            Exits = exits;
        }

        public void ShowCharacters()
        {
            if (Characters.Count > 0)
            {
                Console.WriteLine("Здесь находятся:");
                foreach (var character in Characters)
                {
                    Console.WriteLine($"- {character.Name}");
                }
            }
        }
    }
}
