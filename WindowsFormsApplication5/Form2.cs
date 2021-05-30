using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form2 : Form
    {
        int  store;
        int piecevalue;
        Board boardlocation; 
        Form1 formlocation;
        int X,Y;
        public Form2(int piecevalue, Board board, Form1 form, int X, int Y) //pass the board and form location for changeing the value
        {
            InitializeComponent();
            this.piecevalue = piecevalue;
            boardlocation = board;
            formlocation = form;
            this.X =X;
            this.Y = Y;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormClosed += formclose; //form onclose event handler intialization
        }

        public void formclose(object sender, EventArgs e) { //onclose event handeler 

            boardlocation.ProcessIcons(boardlocation.BoardValue,formlocation.BoardTiles, X, Y); // when the form closed and a piece is selected, change the icon
        }

        public void button1_Click(object sender, EventArgs e) // change the board value on submit
        {
            if (radioButton1.Checked)
            {
                store = 2 + piecevalue;
            }
            else if (radioButton2.Checked)
            {
                store = 3 + piecevalue;
            }
            else if (radioButton3.Checked)
            {
                store = 4 + piecevalue;
            }
            else if (radioButton4.Checked)
            {
                store = 5 + piecevalue;
            }

            boardlocation.BoardValue[X, Y] = store;
            
            this.Close();
        }
    }
}
