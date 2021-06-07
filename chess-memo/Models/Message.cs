using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chess_memo.Models
{
    [Keyless]
    public partial class Message
    {
        public string responseMessage { get; set; }
    }
}
