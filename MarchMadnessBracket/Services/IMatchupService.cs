using MarchMadnessBracketGenerator.Models;
using System.Collections.Generic;

namespace MarchMadnessBracketGenerator.Services
{
    public interface IMatchupService
    {
        List<Matchup> GetMatchups(List<Team> teams);
    }
}
