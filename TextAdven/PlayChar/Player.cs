namespace TextAdven.PlayChar
{
    [Serializable]
    public class Player
    {
        public string CurrentLocation { get; set; } = "начало";
        public List<string> Inventory { get; set; } = new List<string>();

        public void ShowInventory()
        {
            if (Inventory.Count == 0)
            {
                Console.WriteLine("Ваш инвентарь пуст.");
            }
            else
            {
                Console.WriteLine("В вашем инвентаре:");
                foreach (string item in Inventory)
                {
                    Console.WriteLine("  " + item);
                }
            }
        }
    }
}
