using System;
using System.Collections.Generic;

#nullable disable

namespace chess_memo.Models
{
    public partial class Config
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int DifficultyId { get; set; }
        public int Questions { get; set; }

        public virtual Difficulty Difficulty { get; set; }
        public virtual Player Player { get; set; }
    }
}
