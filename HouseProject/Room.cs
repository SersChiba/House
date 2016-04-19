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

    class RoomWithDoor : RoomWithHidingPlace, IHasExteriorDoor
    {

        public RoomWithDoor(string name, string decoration, string doorDescription, string hidingPlace) : base(name, decoration, hidingPlace)
        {
            DoorDescription = doorDescription;
        }

        public string DoorDescription { get; private set; }

        public Location DoorLocation { get; set; }
        public override string Description { get { return base.Description + " You see " + DoorDescription + "."; } }
    }

    class RoomWithHidingPlace : Room, IHidingPlace
    {
        public RoomWithHidingPlace(string name, string decoration, string hidingPlace) : base(name, decoration)
        {
            HidingPlace = hidingPlace;
        }
        public string HidingPlace { get; }
    }
}
