using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chess_memo.Models
{
    [Keyless]
    public partial class RankingScore
    {
        public int difficulty_id { get; set; }
        public int questions { get; set; }
        public int corrects { get; set; }
        public int seconds { get; set; }
        public long ranking { get; set; }
        public long? personalRanking { get; set; }
        public string email { get; set; }
        public string responseMessage { get; set; }
        public DateTime date_time { get; set; }
    }
}
