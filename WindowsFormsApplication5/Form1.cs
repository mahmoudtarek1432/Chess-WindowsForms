using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        Form3 f3;
        public Form1(Form3 f3)
        {
            this.f3 = f3;
            InitializeComponent();
        }




        public Button[,] BoardTiles; 
        Board board = new Board();
        int turncounter = 1; 
        bool Rewind = false;



        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            this.Size = new Size(70 * 8 + 18, 70 * 8);//form size
            this.AutoSize = true;
            // button config.
            const int TileSize = 70;
            const int gride = 8;
            Color color1 = Color.White;
            Color color2 = Color.Black;
            
            //intialising buttons
            BoardTiles = new Button[gride, gride];

            for (int i = 0; i < gride; i++)
            {
                
                for (int j = 0; j < gride; j++)
                {
                    Button tempbutton = new Button
                    {
                        Size = new Size(TileSize, TileSize), 
                        Location = new Point(TileSize * i, TileSize * j + 32), 
                        BackColor = (i % 2 == 0) ? ((j % 2 == 0) ? color1 : color2) : ((j % 2 == 0) ? color2 : color1) //color of tiles
                    };
                    BoardTiles[j, i] = tempbutton; //insert in array of buttons
                    board.ProcessIcons(board.BoardValue, BoardTiles,j, i);
                    Controls.Add(tempbutton); //display on screen

                }
            }
            

            //Onclick event handler

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board.territory.intializeTerritories(board.controlling, board.undercontrol, board.territory, board.BoardValue, BoardTiles, i, j);
                    BoardTiles[i, j].Click += button_onclick; //add all the buttons under one onclick handler
                    BoardTiles[i, j].Name = i + " " + j;
                    
                }
            }

            this.FormClosing += form_closing;
        }

        





            private void button_onclick (object sender,EventArgs e){ //click event
                var button = (Button)sender; //var button to access the clicked button
                int ButtonX = button.Name[0] - '0'; // convert char to int by subtracting 49 from base ASCII value
                int ButtonY = button.Name[2] - '0';
                
                string text = label1.Text;
               
                if (Rewind == true)// the rewind button was pressed so delete all the saved forms
                {
                    buttonClickafterRewind();
                    Rewind = false;
                }

                board.update(BoardTiles, ButtonX, ButtonY, ref text,this, ref turncounter);
                label1.Text = text;
                
            }

            private void Guide_Click(object sender, EventArgs e)
            {
                board.setgetguide = (board.setgetguide) ? false : true;
                
            }

            private void label1_Click(object sender, EventArgs e)
            {

            }
            





            private void button1_Click(object sender, EventArgs e)
            {
              
                if (turncounter > 1)
                {
                    try
                    {
                        turncounter -= 1;
                        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\wed\Documents\Visual Studio 2012\Projects\CHESS\WindowsFormsApplication5\Chess.accdb");
                        connection.Open();
                        string command = null;
                        for (int i = 0; i < 8; i++)
                        {
                            command = "SELECT [X" + i + "] from turn" + turncounter + " where  arrayNo = 1";
                            OleDbCommand cmd = new OleDbCommand(command, connection);

                            using (var reader = cmd.ExecuteReader())
                            {
                                int J = 0;
                                while (reader.Read())
                                {
                                    board.BoardValue[J, i] = Convert.ToInt16(reader[0]);
                                    board.ProcessIcons(board.BoardValue, BoardTiles, J, i);
                                    if (BoardTiles[J, i].BackColor == Color.Red) // king was checked
                                    {
                                        BoardTiles[J, i].BackColor = (i % 2 == 0) ? (((J) % 2 == 0) ? Color.White : Color.Black) : (((J) % 2 == 0) ? Color.Black : Color.White);
                                        board.kingChecked = false;
                                    }


                                    J++;
                                }
                            }

                            //blackterritory
                            command = "SELECT [X" + i + "] from turn" + turncounter + " where  arrayNo = 2";
                            cmd = new OleDbCommand(command, connection);

                            using (var reader = cmd.ExecuteReader())
                            {
                                int J = 0;
                                while (reader.Read())
                                {

                                    board.territory.BlackTerritory[J, i] = Convert.ToInt16(reader[0]);

                                    J++;
                                }
                            }


                            //white territory
                            command = "SELECT [X" + i + "] from turn" + turncounter + " where  arrayNo = 3";
                            cmd = new OleDbCommand(command, connection);

                            using (var reader = cmd.ExecuteReader())
                            {
                                int J = 0;
                                while (reader.Read())
                                {

                                    board.territory.WhiteTerritory[J, i] = Convert.ToInt16(reader[0]);

                                    J++;
                                }
                            }
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("unable to retrive data, refresh the database by closing the program successfuly");
                    }
                    board.turn = (board.turn % 2 == 1) ? 2 : 1;
                    label1.Text = (board.turn == 1)? "Player 1: Black       Turn: " + turncounter : "Player 2: White       Turn: " + turncounter+"";
                    Rewind = true;
                }
            }


            public void form_closing(object sender, EventArgs e)
            {
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\wed\Documents\Visual Studio 2012\Projects\CHESS\WindowsFormsApplication5\Chess.accdb");
                OleDbCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                DataTable chesstable = null;
                string []restriction = {null,null,null,"table"};
                connection.Open();
                chesstable = connection.GetSchema("Tables", restriction);
                int numberOfTables = chesstable.Rows.Count;
                
                
                for (int i = numberOfTables ; i > 1; i--)
                {
                    string command = "Drop TABLE Turn"+i+"";
                    cmd.CommandText = command;
                    
                        cmd.ExecuteNonQuery();
                }
                connection.Close();

                f3.Close();
            }

            public void buttonClickafterRewind()
            {
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\wed\Documents\Visual Studio 2012\Projects\CHESS\WindowsFormsApplication5\Chess.accdb");
                OleDbCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                DataTable chesstable = null;
                string[] restriction = { null, null, null, "table" };
                connection.Open();
                chesstable = connection.GetSchema("Tables", restriction);
                int numberOfTables = chesstable.Rows.Count;

                //the first table is turn2 so numoftables+1
                for (int i = numberOfTables ; i > turncounter; i--)
                {
                    string command = "Drop TABLE Turn" + i + "";
                    cmd.CommandText = command;

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }

  
        
    }
}
