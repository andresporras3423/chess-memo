using System;
using System.Collections.Generic;

#nullable disable

namespace chess_memo.Models
{
    public partial class Difficulty
    {
        public Difficulty()
        {
            Configs = new HashSet<Config>();
            Scores = new HashSet<Score>();
        }

        public int Id { get; set; }
        public string DifficultyName { get; set; }
        public int MinPieces { get; set; }
        public int MaxPieces { get; set; }

        public virtual ICollection<Config> Configs { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
