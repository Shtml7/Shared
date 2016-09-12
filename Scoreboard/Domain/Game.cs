using System;

namespace Scoreboard.domain
{
    public class Game
    {
        public long id { get; set; }
        public User owner { get; set; }
        public Boolean isActive { get; set; }
        public Team team1 { get; set; }
        public Team team2 { get; set; }
        
    }
}
