using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.domain
{
    class Team
    {
        private long id { get; set; }
        private User player1 { get; set; }
        private User player2 { get; set; }
        private int score { get; set; }
    }
}
