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
    public class LoginController : BaseController
    {
        [HttpPost]
        public string Post()
        {
            using (var context = new chessmemoContext())
            {
                Dictionary<string, string> body = read_body();
                var nEmail = new SqlParameter("@nEmail", body["email"]);
                var nPassword = new SqlParameter("@nPassword", body["password"]);
                var msn = context.Messages.FromSqlRaw("EXECUTE dbo.loginPlayer @nEmail, @nPassword", parameters: new[] { nEmail, nPassword }).AsEnumerable().FirstOrDefault();
                if(Regex.Match(msn.responseMessage, @"[1-9]").Success) HttpContext.Session.SetString("id", msn.responseMessage);
                return msn.responseMessage;
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (HttpContext.Session.GetString("id") != null) return StatusCode(200);
            return StatusCode(403);
        }
    }
}
