using chess_memo.Filters;
using chess_memo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chess_memo.Controllers
{
    //[AuthorizationFilter]
    public class PlayerController : BaseController
    {
        [HttpPost]
        public string Post()
        {
            try
            {
                using (var context = new chessmemoContext())
                {
                    Dictionary<string, string> body = read_body();
                    var nEmail = new SqlParameter("@nEmail", body["email"]);
                    var nPassword = new SqlParameter("@nPassword", body["password"]);
                    var msn = context.Messages.FromSqlRaw("EXECUTE dbo.addPlayer @nEmail, @nPassword", parameters: new[] { nEmail, nPassword }).AsEnumerable().FirstOrDefault();
                    if (int.TryParse(msn.responseMessage, out int playerId))
                    {
                        context.Configs.Add(new Config { DifficultyId = 1, PlayerId = playerId, Questions = 10 });
                        context.SaveChanges();
                    }
                    return msn.responseMessage;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [AuthorizationFilter]
        [HttpGet]
        public string Get()
        {
            return "hello world";
        }
    }
}
