using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseProject
{
    class Room : Location
    {
        private string decoration;
        public string Decoration { get { return decoration; } }
        public Room(string name, string decoration) : base(name)
        {
            this.decoration = decoration;
        }
        public override string Description
        {
            get
            {
                return "You see the " + Decoration;
            }
        }
    }

    class RoomWithDoor : Room, IHasExteriorDoor
    {
        public RoomWithDoor(string name, string decoration) : base(name, decoration)
        {
        }

        public string DoorDescription
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Location DoorLocation
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
