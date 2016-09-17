using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.domain
{
    public class Team
    {
        public long id { get; set; }
        public User player1 { get; set; }
        public User player2 { get; set; }
        public int score { get; set; }
    }
}
