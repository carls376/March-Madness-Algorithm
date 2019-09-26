using MarchMadnessBracketGenerator.Models;
using System.Collections.Generic;

namespace MarchMadnessBracketGenerator.Services
{
    public interface IWinnersService
    {
        List<Team> PredictWinners(List<Matchup> matchups);
    }
}
