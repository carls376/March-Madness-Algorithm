using MarchMadnessBracketGenerator.Models;
using System.Collections.Generic;

namespace MarchMadnessBracketGenerator.Data
{
    public interface ITeamsRepository
    {
        List<Team> GetTeams();
    }
}
