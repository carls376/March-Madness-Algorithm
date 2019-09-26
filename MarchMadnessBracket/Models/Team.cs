namespace MarchMadnessBracketGenerator.Models
{
    public class Team
    {
        public string Name { get; set; }
        public int Seed { get; set; }
        public float AdjEM { get; set; }
        public float SoS { get; set; }
        public int WinsPerLastTenGames { get; set; }
        public bool DidWinConfTitle { get; set; }
        public bool DidWinConfChamp { get; set; }
        public int StarPlayers { get; set; }
    }
}
