using MarchMadnessBracketGenerator.Models;
using System.Collections.Generic;

namespace MarchMadnessBracketGenerator.Data
{
    public interface IBracketGenerator
    {
        void GenerateBracket(List<Matchup> matchups, List<Team> winners);
    }
}
