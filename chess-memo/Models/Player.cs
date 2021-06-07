using System;
using System.Collections.Generic;

#nullable disable

namespace chess_memo.Models
{
    public partial class Player
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public Guid? Salt { get; set; }
    }
}
