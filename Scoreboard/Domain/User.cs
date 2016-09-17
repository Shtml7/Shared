using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Scoreboard
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string imageUrl { get; set; }
		public int wins { get; set; }
		public int losses { get; set; }


		public override int GetHashCode()
		{
			return id;
		}

		public bool Equals(User obj)
		{
			return obj != null && obj.id == this.id;
		}
    }

}
