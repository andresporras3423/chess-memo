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
        public RankingScore Post()
        {
            try
            {
                using (var context = new chessmemoContext())
                {
                    Dictionary<string, string> body = read_body();
                    var nId = new SqlParameter("@nId", Int32.Parse(HttpContext.Session.GetString("id")));
                    var nDifficultyId = new SqlParameter("@nDifficultyId", Int32.Parse(body["nDifficultyId"]));
                    var nQuestions = new SqlParameter("@nQuestions", Int32.Parse(body["nQuestions"]));
                    var nCorrects = new SqlParameter("@nCorrects", Int32.Parse(body["nCorrects"]));
                    var nSeconds = new SqlParameter("@nSeconds", Int32.Parse(body["nSeconds"]));
                    var nDateTime = new SqlParameter("@nDateTime", DateTime.Now);
                    RankingScore currentScore = 
                        (RankingScore)context.RankingScores.FromSqlRaw("EXECUTE dbo.addScore @nId, @nDifficultyId, @nQuestions, @nCorrects, @nSeconds, @nDateTime", 
                        parameters: new[] { nId, nDifficultyId, nQuestions, nCorrects, nSeconds, nDateTime }).AsEnumerable().FirstOrDefault();
                    return currentScore;
                }
            }
            catch (Exception ex)
            {
                return  new RankingScore { responseMessage = ex.Message } ;
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
                    return new List<RankingScore> { new RankingScore { responseMessage = ex.Message } };
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
                return new List<RankingScore> { new RankingScore { responseMessage = ex.Message } };
            }
        }

        [Route("recent_scores")]
        [AuthorizationFilter]
        [HttpGet]
        public List<RankingScore> Recent_scores()
        {
            try
            {
                using (var context = new chessmemoContext())
                {
                    Dictionary<string, string> body = read_body();
                    var nDifficultyId = new SqlParameter("@nDifficultyId", body["nDifficultyId"]);
                    var nQuestions = new SqlParameter("@nQuestions", body["nQuestions"]);
                    var nPlayerId = new SqlParameter("@nPlayerId", HttpContext.Session.GetString("id"));
                    List<RankingScore> top10 = context.Set<RankingScore>().FromSqlRaw("EXECUTE dbo.getRecentPersonalScores @nDifficultyId, @nQuestions, @nPlayerId", parameters: new[] { nDifficultyId, nQuestions, nPlayerId }).ToList<RankingScore>();
                    return top10;
                }
            }
            catch (Exception ex)
            {
                return new List<RankingScore> { new RankingScore { responseMessage = ex.Message } };
            }
        }
    }
}
