using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using MarchMadnessBracketGenerator.Models;

namespace MarchMadnessBracketGenerator.Data
{
    public class BracketGenerator : IBracketGenerator
    {
        public void GenerateBracket(List<Matchup> matchups, List<Team> winners)
        {
            if (matchups.Count != winners.Count)
            {
                throw new Exception("Matchups count and winners count should be equal");
            }

            var results = new List<object>();
            for (var i = 0; i < matchups.Count; i++)
            {
                results.Add(new
                {
                    TeamOne = matchups[i].TeamOne.Name,
                    TeamTwo = matchups[i].TeamTwo.Name,
                    Winner = winners[i].Name
                });
            }

            using (var writer = new StreamWriter("BracketResults.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(results);
            }
        }
    }
}
