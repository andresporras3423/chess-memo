using System;
using System.Collections.Generic;

#nullable disable

namespace chess_memo.Models
{
    public partial class Score
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int DifficultyId { get; set; }
        public int Questions { get; set; }
        public int Corrects { get; set; }
        public int Seconds { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Difficulty Difficulty { get; set; }
        public virtual Player Player { get; set; }
    }
}
