using System.Collections.Generic;
using MarchMadnessBracketGenerator.Models;

namespace MarchMadnessBracketGenerator.Services
{
    public class MatchupService : IMatchupService
    {
        public List<Matchup> GetMatchups(List<Team> teams)
        {
            var matchups = new List<Matchup>(teams.Count / 2);

            var furthestMatchupDistance = teams.Count / 4;
            var curMatchupDistance = furthestMatchupDistance;
            var curRegion = 0;

            if (furthestMatchupDistance > 1)
            {
                while (curRegion <= 3)
                {
                    var teamOneIndex = (curRegion * furthestMatchupDistance) + (furthestMatchupDistance - curMatchupDistance);
                    var teamTwoIndex = (curRegion * furthestMatchupDistance) + curMatchupDistance - 1;

                    matchups.Add(new Matchup()
                    {
                        TeamOne = teams[teamOneIndex],
                        TeamTwo = teams[teamTwoIndex]
                    });

                    if (teamTwoIndex - teamOneIndex == 1)
                    {
                        curRegion++;
                        curMatchupDistance = furthestMatchupDistance;
                        continue;
                    }
                    curMatchupDistance--;
                }
            }
            else
            {
                // we are in the final four or championship at this point - regions no longer exist
                for (var i = 0; i < teams.Count / 2; i++)
                {
                    matchups.Add(new Matchup()
                    {
                        TeamOne = teams[i * 2],
                        TeamTwo = teams[(i * 2) + 1]
                    });
                }
            }

            return matchups;
        }
    }
}
