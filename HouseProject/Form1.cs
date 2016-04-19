using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HouseProject
{
    public partial class Form1 : Form
    {
        Location currentLocation;
        Opponent opponent;

        RoomWithDoor livingRoom;
        RoomWithDoor kitchen;
        Room diningRoom;
        Room stairs;
        RoomWithHidingPlace upstairsHallway, masterBedroom, secondBedroom, bathroom;

        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;
        OutsideWithHidingPlace garden;
        OutsideWithHidingPlace driveway;
        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            MoveToANewLocation(livingRoom);
        }
        public void CreateObjects()
        {
            stairs = new Room("Stairs", "wooden bannister");
            upstairsHallway = new RoomWithHidingPlace("Upstairs Hallway", "picture of a dog", "in the closet");
            masterBedroom = new RoomWithHidingPlace("Master Bedroom", "a large bed", "under the bed");
            secondBedroom = new RoomWithHidingPlace("Second Bedroom", "a small bed", "under the bed");
            bathroom = new RoomWithHidingPlace("Bathroom", "a sink an a toilet", "in the shower");
            livingRoom = new RoomWithDoor("Living Room", "antique carpet", "an oak door with a brass knob", "under the carpet");
            diningRoom = new Room("Dining Room", "crystal chandelier");
            kitchen = new RoomWithDoor("Kitchen", "stainless steel appliances", "a screen door", "on the fridge");

            frontYard = new OutsideWithDoor("Front Yard", false, "an oak door with a brass knob");
            backYard = new OutsideWithDoor("Back Yard", true, "a screen door");
            garden = new OutsideWithHidingPlace("Garden", false, "in the shed");
            driveway = new OutsideWithHidingPlace("Driveway", false, "behind the garage");

            stairs.Exits = new Location[] { livingRoom, upstairsHallway };
            upstairsHallway.Exits = new Location[] { masterBedroom, secondBedroom, bathroom, livingRoom };

            diningRoom.Exits = new Location[] { livingRoom, kitchen };
            livingRoom.Exits = new Location[] { frontYard };
            frontYard.Exits = new Location[] { backYard, garden };
            kitchen.Exits = new Location[] { diningRoom };
            backYard.Exits = new Location[] { kitchen, frontYard };
            garden.Exits = new Location[] { frontYard, backYard };

            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;
            kitchen.DoorLocation = diningRoom;
            backYard.DoorLocation = kitchen;

            opponent = new Opponent(livingRoom);
        }

        private void hide_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                opponent.Move();
            }
            hide.Visible = false;
            goHere.Visible = true;
            exits.Visible = true;
        }

        private void check_Click(object sender, EventArgs e)
        {

        }

        private void MoveToANewLocation(Location newLocation)
        {
            currentLocation = newLocation;
            exits.Items.Clear();
            for (int i = 0; i < currentLocation.Exits.Length; i++)
                exits.Items.Add(currentLocation.Exits[i].Name);
            exits.SelectedIndex = 0;
            description.Text = currentLocation.Description;

            if (currentLocation is IHasExteriorDoor)
                goThroughTheDoor.Visible = true;
            else goThroughTheDoor.Visible = false;

            if (currentLocation is IHidingPlace)
            {
                IHidingPlace hidingLocation = currentLocation as IHidingPlace;
                check.Visible = true;
                check.Text = "Check " + hidingLocation.HidingPlace;
            }
            else check.Visible = false;
        }

        private void goHere_Click(object sender, EventArgs e)
        {
            MoveToANewLocation(currentLocation.Exits[exits.SelectedIndex]);
        }

        private void goThroughTheDoor_Click(object sender, EventArgs e)
        {
            IHasExteriorDoor hasDoor = currentLocation as IHasExteriorDoor;
            MoveToANewLocation(hasDoor.DoorLocation);
        }
    }
}
