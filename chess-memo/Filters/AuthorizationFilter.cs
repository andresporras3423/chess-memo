using chess_memo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace chess_memo.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
                using (var connect = new chessmemoContext())
            {
                var id = context.HttpContext.Request.Headers["id"].ToString();
                var token = context.HttpContext.Request.Headers["token"].ToString();
                if(id==null || id=="undefined"  || token == null && token=="undefined")
                {
                    context.Result = new UnauthorizedResult();
                }
                else
                {
                    var nId = new SqlParameter("@nId", Int32.Parse(id));
                    var nToken = new SqlParameter("@nToken", token);
                    var msn = connect.Messages.FromSqlRaw("EXECUTE dbo.isPlayerLogged @nId, @nToken", parameters: new[] { nId, nToken }).AsEnumerable().FirstOrDefault();
                    if (msn.responseMessage != "1") context.Result = new UnauthorizedResult();
                }
            }
        }
     
    }
}
