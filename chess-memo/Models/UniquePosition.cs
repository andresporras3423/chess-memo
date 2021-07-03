using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chess_memo.Models
{
    [Keyless]
    public class UniquePosition
    {
        public string board { get; set; }
        public bool black_long_castling { get; set; }
        public bool black_short_castling { get; set; }
        public bool white_long_castling { get; set; }
        public bool white_short_castling { get; set; }
        public string last_move { get; set; }
        public int available_moves { get; set; }
    }
}
