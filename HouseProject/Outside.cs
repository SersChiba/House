using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseProject
{
    class Outside : Location
    {
        private bool hot;
        public bool Hot { get { return hot; } }
        public Outside(string name, bool hot) : base(name)
        {
            this.hot = hot;
        }
        public override string Description
        {
            get
            {
                if (Hot)
                    return "It's very hot here.";
                return base.Description;
            }
        }
    }
    class OutsideWithDoor : Outside, IHasExteriorDoor
    {
        public OutsideWithDoor(string name, bool hot, string doorDescription) : base(name, hot)
        {
            DoorDescription = doorDescription;
        }

        public string DoorDescription { get; private set; }

        public Location DoorLocation { get; set; }
    }

    class OutsideWithHidingPlace : Outside, IHidingPlace
    {
        public OutsideWithHidingPlace(string name, bool hot, string hidingPlace) : base(name, hot)
        {
            HidingPlaceName = hidingPlace;
        }
        public string HidingPlaceName { get; private set; }
        public override string Description { get { return base.Description + " Someone could hide " + HidingPlaceName + "."; } }
    }
}
