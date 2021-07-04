using chess_memo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chess_memo.Controllers
{
    public class DifficultyController : BaseController
    {
        [HttpGet]
        public List<Dictionary<string, string>> Get()
        {
            using (var context = new chessmemoContext())
            {
                try
                {
                    Dictionary<string, string> body = read_body();
                    List<Dictionary<string, string>> difficulties = context.Difficulties.Select( d =>
                    new Dictionary<string, string> { { "id", d.Id.ToString() },
                    { "difficultyName", d.DifficultyName },
                    { "minPieces", d.MinPieces.ToString() },
                    { "maxPieces", d.MaxPieces.ToString() }}).ToList<Dictionary<string, string>>();
                    return difficulties;
                }
                catch (Exception ex)
                {
                    return new List<Dictionary<string, string>> { new Dictionary<string, string> { { "error_message", ex.Message } }};
                }
            }
        }
    }
}
