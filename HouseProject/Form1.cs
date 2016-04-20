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
        int Moves;
        Location currentLocation;
        Opponent opponent;

        RoomWithDoor livingRoom;
        RoomWithDoor kitchen;
        RoomWithHidingPlace diningRoom;
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
            opponent = new Opponent(frontYard);
            ResetGame(false);            
        }

        private void ResetGame(bool displayMessage)
        {
            if (displayMessage)
            {
                MessageBox.Show("You found me in " + Moves + "moves!");
                IHidingPlace foundLocation = currentLocation as IHidingPlace;
                description.Text = "You found your opponent in " + Moves + "moves! He was hiding in " + foundLocation.HidingPlaceName + ".";
            }
            Moves = 0;
            hide.Visible = true;
            goHere.Visible = false;
            check.Visible = false;
            goThroughTheDoor.Visible = false;
            exits.Visible = false;
        }

        public void CreateObjects()
        {
            stairs = new Room("Stairs", "wooden bannister");
            upstairsHallway = new RoomWithHidingPlace("Upstairs Hallway", "picture of a dog", "in the closet");
            masterBedroom = new RoomWithHidingPlace("Master Bedroom", "a large bed", "under the bed");
            secondBedroom = new RoomWithHidingPlace("Second Bedroom", "a small bed", "under the bed");
            bathroom = new RoomWithHidingPlace("Bathroom", "a sink an a toilet", "in the shower");
            livingRoom = new RoomWithDoor("Living Room", "antique carpet", "an oak door with a brass knob", "under the carpet");
            diningRoom = new RoomWithHidingPlace("Dining Room", "crystal chandelier", "on the chandelier");
            kitchen = new RoomWithDoor("Kitchen", "stainless steel appliances", "a screen door", "on the fridge");

            frontYard = new OutsideWithDoor("Front Yard", false, "a heavy-looking oak door");
            backYard = new OutsideWithDoor("Back Yard", true, "a screen door");
            garden = new OutsideWithHidingPlace("Garden", false, "in the shed");
            driveway = new OutsideWithHidingPlace("Driveway", false, "behind the garage");

            stairs.Exits = new Location[] { livingRoom, upstairsHallway };
            upstairsHallway.Exits = new Location[] { masterBedroom, secondBedroom, bathroom, stairs };
            bathroom.Exits = new Location[] { upstairsHallway };
            masterBedroom.Exits = new Location[] { upstairsHallway };
            secondBedroom.Exits = new Location[] { upstairsHallway };
            diningRoom.Exits = new Location[] { livingRoom, kitchen };
            livingRoom.Exits = new Location[] { frontYard, stairs };            
            kitchen.Exits = new Location[] { diningRoom };
            frontYard.Exits = new Location[] { backYard, garden, driveway };
            backYard.Exits = new Location[] { kitchen, frontYard, driveway };
            garden.Exits = new Location[] { frontYard, backYard };
            driveway.Exits = new Location[] { frontYard, backYard };

            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;
            kitchen.DoorLocation = diningRoom;
            backYard.DoorLocation = kitchen;
        }

        private void hide_Click(object sender, EventArgs e)
        {
            hide.Visible = false;
            for (int i = 0; i < 10; i++)
            {
                opponent.Move();
                description.Text = i + "...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
            }
            description.Text = "\nReady or not, here I come!";
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);

            goHere.Visible = true;
            exits.Visible = true;
            MoveToANewLocation(livingRoom);
        }

        private void check_Click(object sender, EventArgs e)
        {
            Moves++;
            if (opponent.Check(currentLocation))
                ResetGame(true);
            else RedrawForm();
        }

        private void MoveToANewLocation(Location newLocation)
        {
            Moves++;
            currentLocation = newLocation;
            RedrawForm();
        }

        private void RedrawForm()
        {
            exits.Items.Clear();
            for (int i = 0; i < currentLocation.Exits.Length; i++)
                exits.Items.Add(currentLocation.Exits[i].Name);
            exits.SelectedIndex = 0;
            description.Text = currentLocation.Description + "\r\n(move #" + Moves + ")";
            if (currentLocation is IHidingPlace)
            {
                IHidingPlace hidingLocation = currentLocation as IHidingPlace;                
                check.Text = "Check " + hidingLocation.HidingPlaceName;
                check.Visible = true;
            }
            else check.Visible = false;

            if (currentLocation is IHasExteriorDoor)
                goThroughTheDoor.Visible = true;
            else goThroughTheDoor.Visible = false;
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
