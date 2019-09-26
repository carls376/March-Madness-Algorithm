using System.Linq;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using MarchMadnessBracketGenerator.Models;

namespace MarchMadnessBracketGenerator.Data
{
    public class TeamsRepository : ITeamsRepository
    {
        public List<Team> GetTeams()
        {
            using (var reader = new StreamReader("TeamData.csv"))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<Team>();
                return records.ToList();
            }
        }
    }
}
