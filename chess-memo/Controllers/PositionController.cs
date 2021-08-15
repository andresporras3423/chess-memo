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
    public class PositionController : BaseController
    {
        [AuthorizationFilter]
        [HttpGet]
        public List<UniquePosition> Get()
        {
            using (var context = new chessmemoContext())
            {
                Dictionary<string, string> body = read_body();
                var nId = new SqlParameter("@nId", Int32.Parse(HttpContext.Request.Headers["id"].ToString()));
               List<UniquePosition> msn = context.Set<UniquePosition>().FromSqlRaw("EXECUTE dbo.selectPositions @nId", parameters: new[] { nId }).AsEnumerable().ToList<UniquePosition>();
                return msn;
            }
        }
    }
}
