using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseProject
{
    class Outside:Location
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
                if(Hot)
                return "It's very hot here.";
                return base.Description;
            }
        }
    }
    class OutsideWithDoor : Outside, IHasExteriorDoor
    {
        public OutsideWithDoor(string name, bool hot) : base(name, hot)
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
