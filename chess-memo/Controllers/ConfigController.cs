using chess_memo.Filters;
using chess_memo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chess_memo.Controllers
{
    public class ConfigController : BaseController
    {
        [AuthorizationFilter]
        [HttpGet]
        public Dictionary<string, string> Get()
        {
            using (var context = new chessmemoContext())
            {
                try
                {
                    Dictionary<string, string> body = read_body();
                    Config playerConfig = context.Configs.Where(x => x.PlayerId == Int32.Parse(HttpContext.Request.Headers["id"].ToString())).FirstOrDefault();
                    return new Dictionary<string, string> { { "DifficultyId", $"{playerConfig.DifficultyId}"},
                    { "Questions", $"{playerConfig.Questions}" }};
                    ;
                }
                catch (Exception ex)
                {
                    return new Dictionary<string, string> { { "error_message" , ex.Message } };
                }
            }
        }

        [AuthorizationFilter]
        [HttpPut]
        public string Put()
        {
            using (var context = new chessmemoContext())
            {
                try
                {
                    Dictionary<string, string> body = read_body();
                    Config playerConfig = context.Configs.Where(x => x.PlayerId == Int32.Parse(HttpContext.Request.Headers["id"].ToString())).FirstOrDefault();
                    playerConfig.DifficultyId = Int32.Parse(body["DifficultyId"]);
                    playerConfig.Questions = Int32.Parse(body["Questions"]);
                    context.SaveChanges();
                    return "success";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                } 
            }
        }
    }
}
