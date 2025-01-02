namespace TextAdven.PlayChar
{
    public class Character
    {
        public string Name { get; set; }
        public string Dialogue { get; set; }
        public Dictionary<string, string> Responses { get; set; } = new Dictionary<string, string>();

        public Character(string name, string dialogue)
        {
            Name = name;
            Dialogue = dialogue;
        }

        public void Talk()
        {
            Console.WriteLine($"{Name}: {Dialogue}");
            if (Responses.Count > 0)
            {
                Console.WriteLine("Варианты ответа:");
                int i = 1;
                foreach (var response in Responses)
                {
                    Console.WriteLine($"{i}. {response.Key}");
                    i++;
                }
                Console.Write("> ");
                string playerResponse = Console.ReadLine();
                if (int.TryParse(playerResponse, out int choice) && choice > 0 && choice <= Responses.Count)
                {
                    string key = Responses.Keys.ElementAt(choice - 1);
                    Console.WriteLine($"{Name}: {Responses[key]}");
                }
                else
                {
                    Console.WriteLine("Неверный ответ.");
                }
            }
        }
    }
}
