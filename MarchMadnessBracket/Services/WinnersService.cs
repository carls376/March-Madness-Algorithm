using System;
using System.Collections.Generic;
using System.Linq;
using MarchMadnessBracketGenerator.Models;

namespace MarchMadnessBracketGenerator.Services
{
    public class WinnersService : IWinnersService
    {
        private const int MaxUpsetsRoundOf64 = 5;
        private const int MaxUpsetsRoundOf32 = 5;
        private const int MaxUpsetsSweetSixteen = 2;
        private const int MaxUpsetsEliteEight = 1;
        private const int MaxUpsetsFinalFour = 0;
        private const int MaxUpsetsChampionship = 0;

        private Random _rand;

        public WinnersService()
        {
            _rand = new Random();
        }

        public List<Team> PredictWinners(List<Matchup> matchups)
        {
            var winners = new Team[matchups.Count];

            var potentialUpsets = new List<PotentialUpset>();

            for (var i = 0; i < matchups.Count; i++)
            {
                var teamOne = matchups[i].TeamOne;
                var teamTwo = matchups[i].TeamTwo;

                var teamOneScore = CalculateScoreForTeam(teamOne);
                var teamTwoScore = CalculateScoreForTeam(teamTwo);
                if (teamOne.Seed < teamTwo.Seed - 1)
                {
                    // potential upset is teamTwo over teamOne
                    var scoreDiff = teamOneScore - teamTwoScore;
                    potentialUpsets.Add(new PotentialUpset()
                    {
                        LowSeed = teamOne,
                        HighSeed = teamTwo,
                        ScoreDiff = scoreDiff,
                        MatchupIndex = i
                    });
                }
                else if (teamTwo.Seed < teamOne.Seed - 1)
                {
                    // potential upset is teamOne over teamTwo
                    var scoreDiff = teamTwoScore - teamOneScore;
                    potentialUpsets.Add(new PotentialUpset()
                    {
                        LowSeed = teamTwo,
                        HighSeed = teamOne,
                        ScoreDiff = scoreDiff,
                        MatchupIndex = i
                    });
                }
                else
                {
                    // teams are either the same seed or 1 seed apart - not a potential upset
                    if (teamOneScore > teamTwoScore)
                    {
                        // teamOne wins
                        winners[i] = teamOne;
                    }
                    else if (teamTwoScore > teamOneScore)
                    {
                        // teamTwo wins
                        winners[i] = teamTwo;
                    }
                    else
                    {
                        // if the scores are equal, choose randomly
                        if (_rand.Next(2) == 0)
                            winners[i] = teamOne;
                        else
                            winners[i] = teamTwo;
                    }
                }
                
            }

            // Sort the list by score diffs in ascending order
            var sortedListByScoreDiffs = potentialUpsets.OrderBy(x => x.ScoreDiff).ToList();

            // Determine how many upsets we can have according to the round we're in
            var maxUpsetsAllowed = GetMaxUpsetsAllowed(matchups.Count);

            // Predict the upsets
            for (var i = 0; i < sortedListByScoreDiffs.Count; i++)
            {
                var potentialUpset = sortedListByScoreDiffs[i];
                if (i < maxUpsetsAllowed)
                {
                    // upset!!!
                    winners[potentialUpset.MatchupIndex] = potentialUpset.HighSeed;
                }
                else
                {
                    // not an upset
                    winners[potentialUpset.MatchupIndex] = potentialUpset.LowSeed;
                }
            }

            return new List<Team>(winners);
        }

        private float CalculateScoreForTeam(Team team)
        {
            var winsPerLastTenGamesPoints = (float)team.WinsPerLastTenGames;
            var confTitlePoints = team.DidWinConfTitle ? 3f : 0f;
            var confChampPoints = team.DidWinConfChamp ? 3f : 0f;
            var starPlayerPoints = (float)team.StarPlayers * 3;
            var randomPoints = (float)_rand.Next(5);

            return team.AdjEM + team.SoS + winsPerLastTenGamesPoints
                + confTitlePoints + confChampPoints + starPlayerPoints + randomPoints;
        }

        private int GetMaxUpsetsAllowed(int matchupCount)
        {
            if (matchupCount == 32)
                return MaxUpsetsRoundOf64;

            if (matchupCount == 16)
                return MaxUpsetsRoundOf32;

            if (matchupCount == 8)
                return MaxUpsetsSweetSixteen;

            if (matchupCount == 4)
                return MaxUpsetsEliteEight;

            if (matchupCount == 2)
                return MaxUpsetsFinalFour;

            if (matchupCount == 1)
                return MaxUpsetsChampionship;

            // throw error
            throw new Exception("Matchup count is incorrect");
        }
    }
}
