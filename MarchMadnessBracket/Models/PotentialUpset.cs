namespace MarchMadnessBracketGenerator.Models
{
    public class PotentialUpset
    {
        public Team LowSeed { get; set; }
        public Team HighSeed { get; set; }
        public float ScoreDiff { get; set; }
        public int MatchupIndex { get; set; }
    }
}
