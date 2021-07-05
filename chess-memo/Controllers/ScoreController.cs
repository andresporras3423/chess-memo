using chess_memo.Filters;
using chess_memo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace chess_memo.Controllers
{
    public class ScoreController : BaseController
    {
        [AuthorizationFilter]
        [HttpPost]
        public Dictionary<string, string> Post()
        {
            using (var context = new chessmemoContext())
            {
                try
                {
                    Dictionary<string, string> body = read_body();
                    context.Scores.Add(new Score { PlayerId = Int32.Parse(HttpContext.Session.GetString("id")),
                        DifficultyId = Int32.Parse(body["DifficultyId"]),
                        Questions = Int32.Parse(body["Questions"]),
                        Corrects = Int32.Parse(body["Corrects"]),
                        Seconds = Int32.Parse(body["Seconds"]),
                        DateTime = DateTime.Now
                    });
                    context.SaveChanges();
                    return new Dictionary<string, string> { { "status", "success"} };
                }
                catch(Exception ex)
                {
                    return new Dictionary<string, string> { { "status", ex.Message} };
                }
            }
        }

        [Route("global_scores")]
        [AuthorizationFilter]
        [HttpGet]
        public List<RankingScore> Global_scores()
        {
                try
                {
                using (var context = new chessmemoContext())
                {
                    Dictionary<string, string> body = read_body();
                    var nDifficultyId = new SqlParameter("@nDifficultyId", body["nDifficultyId"]);
                    var nQuestions = new SqlParameter("@nQuestions", body["nQuestions"]);
                    List<RankingScore> top10 = context.Set<RankingScore>().FromSqlRaw("EXECUTE dbo.getBestGlobalScores @nDifficultyId, @nQuestions", parameters: new[] { nDifficultyId, nQuestions }).ToList<RankingScore>();
                    return top10;
                }
            }
                catch (Exception ex)
                {
                    return new List<RankingScore> { new RankingScore { errorMessage = ex.Message } };
                }
        }

        [Route("personal_scores")]
        [AuthorizationFilter]
        [HttpGet]
        public List<RankingScore> Personal_scores()
        {
            try
            {
                using (var context = new chessmemoContext())
                {
                    Dictionary<string, string> body = read_body();
                    var nDifficultyId = new SqlParameter("@nDifficultyId", body["nDifficultyId"]);
                    var nQuestions = new SqlParameter("@nQuestions", body["nQuestions"]);
                    var nPlayerId = new SqlParameter("@nPlayerId", HttpContext.Session.GetString("id"));
                    List<RankingScore> top10 = context.Set<RankingScore>().FromSqlRaw("EXECUTE dbo.getBestPersonalScores @nDifficultyId, @nQuestions, @nPlayerId", parameters: new[] { nDifficultyId, nQuestions, nPlayerId }).ToList<RankingScore>();
                    return top10;
                }
            }
            catch (Exception ex)
            {
                return new List<RankingScore> { new RankingScore { errorMessage = ex.Message } };
            }
        }
    }
}
