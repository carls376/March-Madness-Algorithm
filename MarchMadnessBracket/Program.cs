using System;
using System.Linq;
using MarchMadnessBracketGenerator.Data;
using MarchMadnessBracketGenerator.Services;

namespace MarchMadnessBracketGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ITeamsRepository teamsRepo = new TeamsRepository();
            IBracketGenerator bracketGenerator = new BracketGenerator();
            IMatchupService matchupService = new MatchupService();
            IWinnersService winnersService = new WinnersService();

            var roundOf64Teams = teamsRepo.GetTeams();

            var roundOf64Matchups = matchupService.GetMatchups(roundOf64Teams);
            ValidateMatchupCount(32, roundOf64Matchups.Count);
            var roundOf64Winners = winnersService.PredictWinners(roundOf64Matchups);

            var roundOf32Matchups = matchupService.GetMatchups(roundOf64Winners);
            ValidateMatchupCount(16, roundOf32Matchups.Count);
            var roundOf32Winners = winnersService.PredictWinners(roundOf32Matchups);

            var sweetSixteenMatchups = matchupService.GetMatchups(roundOf32Winners);
            ValidateMatchupCount(8, sweetSixteenMatchups.Count);
            var sweetSixteenWinners = winnersService.PredictWinners(sweetSixteenMatchups);

            var eliteEightMatchups = matchupService.GetMatchups(sweetSixteenWinners);
            ValidateMatchupCount(4, eliteEightMatchups.Count);
            var eliteEightWinners = winnersService.PredictWinners(eliteEightMatchups);

            var finalFourMatchups = matchupService.GetMatchups(eliteEightWinners);
            ValidateMatchupCount(2, finalFourMatchups.Count);
            var finalFourWinners = winnersService.PredictWinners(finalFourMatchups);

            var championshipMatchup = matchupService.GetMatchups(finalFourWinners);
            ValidateMatchupCount(1, championshipMatchup.Count);
            var championshipWinner = winnersService.PredictWinners(championshipMatchup);

            var allMatchups = roundOf64Matchups.Concat(roundOf32Matchups).Concat(sweetSixteenMatchups)
                .Concat(eliteEightMatchups).Concat(finalFourMatchups).Concat(championshipMatchup).ToList();

            var allWinners = roundOf64Winners.Concat(roundOf32Winners).Concat(sweetSixteenWinners)
                .Concat(eliteEightWinners).Concat(finalFourWinners).Concat(championshipWinner).ToList();

            bracketGenerator.GenerateBracket(allMatchups, allWinners);

            Console.WriteLine("Your bracket has been generated. Press [enter] to exit.");
            Console.ReadLine();
        }

        static private void ValidateMatchupCount(int expected, int actual)
        {
            if (expected != actual)
            {
                // throw error
                throw new Exception("Matchup count is incorrect");
            }
        }
    }
}
