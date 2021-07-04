using chess_memo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chess_memo.Controllers
{
    public class ScoreController : BaseController
    {
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
    }
}
