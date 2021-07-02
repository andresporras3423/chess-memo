using System;
using System.Collections.Generic;

#nullable disable

namespace chess_memo.Models
{
    public partial class Position
    {
        public int Id { get; set; }
        public string Board { get; set; }
        public int TotalBlackPieces { get; set; }
        public int TotalWhitePieces { get; set; }
        public bool BlackLongCastling { get; set; }
        public bool BlackShortCastling { get; set; }
        public bool WhiteLongCastling { get; set; }
        public bool WhiteShortCastling { get; set; }
        public string LastMove { get; set; }
        public int AvailableMoves { get; set; }
    }
}
