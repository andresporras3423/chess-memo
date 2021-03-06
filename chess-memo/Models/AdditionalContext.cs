using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chess_memo.Models
{
    public partial class chessmemoContext : DbContext
    {
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<UniquePosition> UniquePositions { get; set; }

        public virtual DbSet<RankingScore> RankingScores { get; set; }
    }
}
