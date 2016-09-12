using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard.domain
{
    class Game
    {
        private long id { get; set; }
        private User owner { get; set; }
        private Boolean isActive { get; set; }
        private Team team1 { get; set; }
        private Team team2 { get; set; }


    }
}
