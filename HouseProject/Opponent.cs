using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseProject
{
    class Opponent
    {
        private Location myLocation;
        private Random random;
        public Opponent(Location startingLocation)
        {
            myLocation = startingLocation;
            random = new Random();
        }

        public void Move()
        {
            //if(myLocation is RoomWithDoor)
            //    if(random.Next(2)==1)

        }
        public bool Check(Location location)
        {
            if (location == myLocation)
                return true;
            return false;
        }
    }
}
