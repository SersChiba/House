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
        public Room(string name, string decoration) : base(name)
        {
            this.decoration = decoration;
        }
        public override string Description
        {
            get
            {
                return "You see the " + decoration + ".";
            }
        }
    }

    class RoomWithDoor : Room, IHasExteriorDoor
    {

        public RoomWithDoor(string name, string decoration, string doorDescription) : base(name, decoration)
        {
            DoorDescription = doorDescription;
        }

        public string DoorDescription { get; private set; }

        public Location DoorLocation { get; set; }
        public override string Description { get { return base.Description + " You see " + DoorDescription + "."; } }
    }
}
