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
    public class LoginController : BaseController
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
                    var msn = context.Messages.FromSqlRaw("EXECUTE dbo.loginPlayer @nEmail, @nPassword", parameters: new[] { nEmail, nPassword }).AsEnumerable().FirstOrDefault();
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
        public IActionResult Get()
        {
            return StatusCode(200);
        }

        [AuthorizationFilter]
        [HttpDelete]
        public IActionResult Delete()
        {
            using (var connect = new chessmemoContext())
            {
                var id = HttpContext.Request.Headers["id"].ToString();
                var nId = new SqlParameter("@nId", Int32.Parse(id));
                var msn = connect.Messages.FromSqlRaw("EXECUTE dbo.destroyLogin @nId", parameters: new[] { nId }).AsEnumerable().FirstOrDefault();
            }
            return StatusCode(200);
        }
    }
}
