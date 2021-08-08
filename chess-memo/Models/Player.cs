using System;
using System.Collections.Generic;

#nullable disable

namespace chess_memo.Models
{
    public partial class Player
    {
        public Player()
        {
            Configs = new HashSet<Config>();
            Scores = new HashSet<Score>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public Guid? Salt { get; set; }
        public Guid? Token { get; set; }

        public virtual ICollection<Config> Configs { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
