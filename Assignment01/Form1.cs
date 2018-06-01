using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Daiana Arantes
//writen jan 2018

namespace Assignment01
{
    public partial class Form1 : System.Windows.Forms.Form
    {

        Button[,] seats = new Button[5, 3];
        string name = null;
        string[] waitingList = new string[10]; 

        //Method to creat the chart
        private void CreateSeatChart ()
        {
            //Method to creat the chart
            seats[0, 0] = buttonSeatA0;
            seats[0, 1] = buttonSeatA1;
            seats[0, 2] = buttonSeatA2;
            seats[1, 0] = buttonSeatB0;
            seats[1, 1] = buttonSeatB1;
            seats[1, 2] = buttonSeatB2;
            seats[2, 0] = buttonSeatC0;
            seats[2, 1] = buttonSeatC1;
            seats[2, 2] = buttonSeatC2;
            seats[3, 0] = buttonSeatD0;
            seats[3, 1] = buttonSeatD1;
            seats[3, 2] = buttonSeatD2;
            seats[4, 0] = buttonSeatE0;
            seats[4, 1] = buttonSeatE1;
            seats[4, 2] = buttonSeatE2;
        }

        
        private bool AreSeatsAvailable ()
        {
            //Method to verify with there are seats available
            for (int y = 0; y <= seats.GetUpperBound(0); y++)
            {
                for (int x = 0; x <= seats.GetUpperBound(1); x++)
                {
                    if(seats[y,x].Text.Equals("EMPTY"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        private void ReorderWaitingListArray ()
        {
            //Method that put the second name of the waiting list to first
            for (int i = 1; i < waitingList.Length; i++)
            {
                if(waitingList [i] == null)
                {
                    break;
                }
                else
                {
                    waitingList[i - 1] = waitingList[i];
                    waitingList[i] = null;
                }
            }
        }
        
        private void ShowWaitList()
        {
            //Method that prints the names on the waiting list
            richTextBoxWaitingList.Text = "";
            foreach (var item in waitingList)
            {
                if (item != null)
                {
                    richTextBoxWaitingList.Text += item + "\n";
                }
            }
        }

        private void AddToWaitList()
        {
            for (int i = 0; i < waitingList.Length; i++)
            {
                //if wating list is not full, add to waiting list
                if (waitingList[i] == null)
                {
                    waitingList[i] = name;
                    richTextBoxMessage.Text = "You were add to waiting list!";
                    break;
                }
                //if waiting list is full, show message
                else
                {
                    richTextBoxMessage.Text = "The waiting list is full!";
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //When the form is loaded, Method CreatSeatChat is called
            CreateSeatChart();
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            //when button show all is clicked, 
            //the groupBoxChart is turn to visible
            groupBoxChart.Visible = true;
        }

        private void buttonBook_Click(object sender, EventArgs e)
        {
            name = textBoxName.Text;

            //verifies if name is not empty
            if(textBoxName.Text.Equals(""))
            {
                richTextBoxMessage.Text = "Please insert your name!";
            }
            //verify if row and column are not empty
            else if(listBoxRow.SelectedIndex == -1 ||
                listBoxColumn.SelectedIndex == -1)
            {
                richTextBoxMessage.Text = "Please insert row and column!";
            }
            //if there is no seats available, name is added to waiting list 
            else if (!AreSeatsAvailable())
            {
                AddToWaitList();
            }
            //if there are seats available, the 
            //name is put to the position selected
            else if (seats[listBoxRow.SelectedIndex,
                listBoxColumn.SelectedIndex].Text.Equals("EMPTY"))
            {
                seats[listBoxRow.SelectedIndex, 
                    listBoxColumn.SelectedIndex].Text = name;
                richTextBoxMessage.Text = "Your Reservation was booked!";
            }
            //if the seat is not empty, a message is shown
            else
            {
                richTextBoxMessage.Text = "This Seat is not "+
                    "available, please choose another one!";
            }
        }

        private void buttonFillAll_Click(object sender, EventArgs e)
        {
            richTextBoxMessage.Text = "Test";
            //fill all the positions of the chart
            for (int y = 0; y <= seats.GetUpperBound(0); y++)
            {
                for (int x = 0; x <= seats.GetUpperBound(1); x++)
                {
                    if (seats[y, x].Text.Equals("EMPTY"))
                    {
                        seats[y, x].Text = "Test";
                    }
                }
            }
        }

        private void buttonAddWaitList_Click(object sender, EventArgs e)
        {
            name = textBoxName.Text;

            //check if the name is not empty
            if (textBoxName.Text.Equals(""))
            {
                richTextBoxMessage.Text = "Please insert your name!";
            }

            //Add to waiting list if there is no seats available
            else if (!AreSeatsAvailable())
            {
                AddToWaitList();
            }
            //Inform if there are seats available
            else
            {
                richTextBoxMessage.Text = "There are seats available!";
            }
        }

        private void buttonShowWaitList_Click(object sender, EventArgs e)
        {
            //Call method ShowWaitList
            ShowWaitList();
        }

        private void buttonStatus_Click(object sender, EventArgs e)
        {
            //If row and column are empty, so the seat is available
            if (seats[listBoxRow.SelectedIndex,
                listBoxColumn.SelectedIndex].Text.Equals("EMPTY"))
            {
                textBoxStatus.Text = "Available";
            }
            //If row and column are not empty, so the seat is not available
            else
            {
                textBoxStatus.Text = "Not Available";
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //Verify if row and column are selected
            if (listBoxRow.SelectedIndex == -1 ||
                listBoxColumn.SelectedIndex == -1)
            {
                richTextBoxMessage.Text = "Please insert row" +
                    " and column to cancel!";
            }
            //If seat is not empty, than turn it empty
            else if (!seats[listBoxRow.SelectedIndex,
                listBoxColumn.SelectedIndex].Text.Equals("EMPTY"))
            {
                seats[listBoxRow.SelectedIndex, 
                    listBoxColumn.SelectedIndex].Text = "EMPTY";
                richTextBoxMessage.Text = "Your reservation was cancelled!";

                //If waiting list is not empty, than replace seat
                //cancelled dor the first name of the list
                if (waitingList[0] != null)
                {
                    seats[listBoxRow.SelectedIndex,
                        listBoxColumn.SelectedIndex].Text = waitingList[0];
                    richTextBoxMessage.Text += "\n" + waitingList[0] +
                        " was added to chart!";
                    waitingList[0] = null;
                    ReorderWaitingListArray();
                    ShowWaitList();
                }
            }
            //Verify if the seat is empty
            else
            {
                richTextBoxMessage.Text = "This seat is empty!";
            }
        }
    }
}
