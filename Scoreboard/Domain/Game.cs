using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.OS;
using Java.IO;

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
