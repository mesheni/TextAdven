namespace TextAdven.Logic
{
    public class Locations
    {
        public string Description { get; set; }
        public Dictionary<string, string> Exits { get; set; }
        // Добавьте другие свойства локации здесь, например, предметы

        public Locations(string description, Dictionary<string, string> exits)
        {
            Description = description;
            Exits = exits;
        }
    }
}
