using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;

namespace WindowsFormsApplication5
{
    public class Board : Pawns
    {
        public int[,] BoardValue; //pieces locations
        public UnderControl[,] undercontrol; //contains the pieces that are in the territory of another pieces
        public Controlling[,] controlling; //contains the pieces that has territoy over another pieces
        
        //true == didn't move, false == moved and wont be able to castle
        //castling [0] == king, [1] == right bishop, [2] == left bishop
        bool[] castlingWhite;
        bool[] castlingBlack;
        public int turn; //counts the turns
        public bool kingChecked; // check weather the king status is checked
        bool onclick; //bool for a button clicked, once == move/ twice == cancel move
        int ButtonX, ButtonY;// 2d Array index for X and Y
        bool guide;// bool to control the pawn guide
        public Territory territory;

        public Board()
        {
            //Array value 0 == NULL
            //Array value 1 == SOLIDER
            //Array value 2 == KNIGHT
            //Array value 3 == BISHOP
            //Array value 4 == ROOK
            //Array value 5 == QUEEN
            //Array value 6 == KING
            // +10 for player1
            // +20 for player2
            BoardValue = new int[,]{ //DEFUALT_MAPPING
                { 24, 22, 23, 26, 25, 23, 22, 24},
                { 21, 21, 21, 21, 21, 21, 21, 21},
                {  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0},
                {  0,  0,  0,  0,  0,  0,  0,  0},
                { 11, 11, 11, 11, 11, 11, 11, 11},
                { 14, 12, 13, 16, 15, 13, 12, 14}};

            onclick = false;
            guide = true;
            turn = 1;
            kingChecked = false;

            //castling [0] == king, [1] == right bishop, [2] == left bishop
            castlingBlack = new bool[] { true, true, true };
            castlingWhite = new bool[] { true, true, true };

            territory = new Territory();
            undercontrol = new UnderControl[8, 8];
            controlling = new Controlling[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    undercontrol[i, j] = new UnderControl();
                    controlling[i, j] = new Controlling();
                }
            }
        }


        public bool setgetguide { set; get; }

        public void update(Button[,] BoardTiles, int ButtonX, int ButtonY, ref string labeltext, Form1 form, ref int turncounter)
        {
            if (onclick == true && this.ButtonX == ButtonX && this.ButtonY == ButtonY)//if the same button clicked twice case
            {
                ProcessPawns(castlingWhite, castlingBlack, controlling, undercontrol, territory, BoardValue, BoardTiles, this.ButtonX, this.ButtonY, ButtonX, ButtonY, ref turn, ref kingChecked,ref turncounter);
                onclick = false;

            }

            else if (onclick == false)//new tile is clicked 
            {
                if (BoardValue[ButtonX, ButtonY] - 10 > 0 && BoardValue[ButtonX, ButtonY] - 10 < 10 && turn == 1        //condition that check if the tile pressed is on the right turn
                     || BoardValue[ButtonX, ButtonY] - 20 > 0 && BoardValue[ButtonX, ButtonY] - 20 < 10 && turn == 2)
                {
                    this.ButtonX = ButtonX;
                    this.ButtonY = ButtonY;
                    ProcessPawns(castlingWhite, castlingBlack, controlling, undercontrol, territory, BoardValue, BoardTiles, this.ButtonX, this.ButtonY, ButtonX, ButtonY, ref turn, ref kingChecked,ref turncounter);
                    onclick = true;
                }
            }

            else if (onclick == true) //button click on diffrent tile
            {
                if ((BoardValue[this.ButtonX, this.ButtonY] - 10 > 0 && BoardValue[this.ButtonX, this.ButtonY] - 10 < 10 && turn == 1
                    && (BoardValue[ButtonX, ButtonY] - 20 > 0 && BoardValue[ButtonX, ButtonY] - 20 < 10 || BoardValue[ButtonX, ButtonY] == 0))
                    || (BoardValue[this.ButtonX, this.ButtonY] - 20 > 0 && BoardValue[this.ButtonX, this.ButtonY] - 20 < 10 && turn == 2
                    && (BoardValue[ButtonX, ButtonY] - 10 > 0 && BoardValue[ButtonX, ButtonY] - 10 < 10) || BoardValue[ButtonX, ButtonY] == 0))
                {


                    int change = turn;
                    ProcessPawns(castlingWhite, castlingBlack, controlling, undercontrol, territory, BoardValue, BoardTiles, this.ButtonX, this.ButtonY, ButtonX, ButtonY, ref turn, ref kingChecked,ref turncounter); //change locations

                    changePawn(form, BoardValue, ButtonX, ButtonY);
                    ProcessIcons(BoardValue, BoardTiles, this.ButtonX, this.ButtonY);
                    ProcessIcons(BoardValue, BoardTiles, ButtonX, ButtonY);
                    this.ButtonX = ButtonX;
                    this.ButtonY = ButtonY;
                    onclick = false;

                   /*string text = "";
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            text += ", " + territory.BlackTerritory[i, j];
                            if (j == 7)
                            {
                                text += "\n";
                            }
                        }
                    }

                    MessageBox.Show(text);
                    */

                    if (change != turn)
                    {
                        labeltext = (turn == 1) ? "Player 1: Black       Turn: " + turncounter : "Player 2: White       Turn: " + turncounter;
                        
                    }
                }
            }
        }

        public void ProcessPawns(bool[] castlingWhite, bool[] castlingBlack, Controlling[,] controlling, UnderControl[,] undercontrol, Territory territory, int[,] BoardValue, Button[,] BoardTiles, int TileAtX, int TileAtY, int TileToX, int TileToY, ref int turn, ref bool kingChecked,ref int turncounter) //after a completing a turn checks posible outcome (design wise)
        {
            if (turn == 1)
            {
                // the king is not checkd
                if (kingChecked == false)
                {
                    //black pieces 11~16
                    if (BoardValue[TileAtX, TileAtY] - 10 == 1) { solider(controlling, undercontrol, territory, BoardValue, BoardTiles, TileAtX, TileAtY, TileToX, TileToY, ref turn, onclick, guide, ref kingChecked, ref turncounter); }
                    else if (BoardValue[TileAtX, TileAtY] - 10 == 2) { knight(controlling, undercontrol, territory, BoardValue, BoardTiles, TileAtX, TileAtY, TileToX, TileToY, ref turn, onclick, guide, ref kingChecked, ref turncounter); }
                    else if (BoardValue[TileAtX, TileAtY] - 10 == 3) { bishop(controlling, undercontrol, territory, BoardValue, BoardTiles, TileAtX, TileAtY, TileToX, TileToY, ref turn, onclick, guide, ref kingChecked,  ref turncounter); }
                    else if (BoardValue[TileAtX, TileAtY] - 10 == 4) { rook(controlling, undercontrol, territory, castlingWhite, castlingBlack, BoardValue, BoardTiles, TileAtX, TileAtY, TileToX, TileToY, ref turn, onclick, guide, ref kingChecked, ref turncounter); }
                    else if (BoardValue[TileAtX, TileAtY] - 10 == 5) { queen(controlling, undercontrol, territory, castlingWhite, castlingBlack, BoardValue, BoardTiles, TileAtX, TileAtY, TileToX, TileToY, ref turn, onclick, guide, ref kingChecked, ref turncounter); }
                }

                // the king is checked- player can only move the king
                if (kingChecked == false || kingChecked == true  && !(checkMate(territory.WhiteTerritory, territory.BlackTerritory, BoardValue, TileAtX, TileAtY)) )
                {
                    //MessageBox.Show(" " + !(checkMate(territory.WhiteTerritory, territory.BlackTerritory, BoardValue, TileAtX, TileAtY)));
                    if (BoardValue[TileAtX, TileAtY] - 10 == 6) { king(castlingWhite, castlingBlack, controlling, undercontrol, territory, BoardValue, BoardTiles, TileAtX, TileAtY, TileToX, TileToY, ref turn, onclick, guide, ref kingChecked, ref turncounter); }
                }
            }

            if (turn == 2)
            {
                // the king is not checkd
                if (kingChecked == false)
                {
                    //white pieces 21~26
                    if (BoardValue[TileAtX, TileAtY] - 20 == 1) { solider(controlling, undercontrol, territory, BoardValue, BoardTiles, TileAtX, TileAtY, TileToX, TileToY, ref turn, onclick, guide, ref kingChecked, ref turncounter); }
                    else if (BoardValue[TileAtX, TileAtY] - 20 == 2) { knight(controlling, undercontrol, territory, BoardValue, BoardTiles, TileAtX, TileAtY, TileToX, TileToY, ref turn, onclick, guide, ref kingChecked, ref turncounter); }
                    else if (BoardValue[TileAtX, TileAtY] - 20 == 3) { bishop(controlling, undercontrol, territory, BoardValue, BoardTiles, TileAtX, TileAtY, TileToX, TileToY, ref turn, onclick, guide, ref kingChecked, ref turncounter); }
                    else if (BoardValue[TileAtX, TileAtY] - 20 == 4) { rook(controlling, undercontrol, territory, castlingWhite, castlingBlack, BoardValue, BoardTiles, TileAtX, TileAtY, TileToX, TileToY, ref turn, onclick, guide, ref kingChecked, ref turncounter); }
                    else if (BoardValue[TileAtX, TileAtY] - 20 == 5) { queen(controlling, undercontrol, territory, castlingWhite, castlingBlack, BoardValue, BoardTiles, TileAtX, TileAtY, TileToX, TileToY, ref turn, onclick, guide, ref kingChecked, ref turncounter); }
                }

                // the king is checked- player can only move the king
                if (kingChecked == false  || kingChecked == true && !(checkMate(territory.WhiteTerritory, territory.BlackTerritory, BoardValue, TileAtX, TileAtY)))
                {

                    if (BoardValue[TileAtX, TileAtY] - 20 == 6) { king(castlingWhite, castlingBlack, controlling, undercontrol, territory, BoardValue, BoardTiles, TileAtX, TileAtY, TileToX, TileToY, ref turn, onclick, guide, ref kingChecked, ref turncounter); }
                }
            }
        }


        public void ProcessIcons(int[,] BoardValue, Button[,] BoardTiles, int TileX, int TileY) //assign icon on tile
        {
            //default to remove icon
            if (BoardValue[TileX, TileY] == 0) { BoardTiles[TileX, TileY].BackgroundImage = null; }

                //black pieces 11~16
            else if (BoardValue[TileX, TileY] - 10 == 1) { BoardTiles[TileX, TileY].BackgroundImage = Properties.Resources.invertedsolider2; }
            else if (BoardValue[TileX, TileY] - 10 == 2) { BoardTiles[TileX, TileY].BackgroundImage = Properties.Resources.invertedknight2; }
            else if (BoardValue[TileX, TileY] - 10 == 3) { BoardTiles[TileX, TileY].BackgroundImage = Properties.Resources.invertedbishop2png; }
            else if (BoardValue[TileX, TileY] - 10 == 4) { BoardTiles[TileX, TileY].BackgroundImage = Properties.Resources.invertedrook2; }
            else if (BoardValue[TileX, TileY] - 10 == 5) { BoardTiles[TileX, TileY].BackgroundImage = Properties.Resources.invertedqueen2; }
            else if (BoardValue[TileX, TileY] - 10 == 6) { BoardTiles[TileX, TileY].BackgroundImage = Properties.Resources.invertedking2; }

                //white pieces 21~26
            else if (BoardValue[TileX, TileY] - 20 == 1) { BoardTiles[TileX, TileY].BackgroundImage = Properties.Resources.solider2; }
            else if (BoardValue[TileX, TileY] - 20 == 2) { BoardTiles[TileX, TileY].BackgroundImage = Properties.Resources.knight2; }
            else if (BoardValue[TileX, TileY] - 20 == 3) { BoardTiles[TileX, TileY].BackgroundImage = Properties.Resources.bishop2png; }
            else if (BoardValue[TileX, TileY] - 20 == 4) { BoardTiles[TileX, TileY].BackgroundImage = Properties.Resources.rook2; }
            else if (BoardValue[TileX, TileY] - 20 == 5) { BoardTiles[TileX, TileY].BackgroundImage = Properties.Resources.queen2; }
            else if (BoardValue[TileX, TileY] - 20 == 6) { BoardTiles[TileX, TileY].BackgroundImage = Properties.Resources.king2; }
        }

        public void changePawn(Form1 form, int[,] boardvalue, int tiletoX, int tiletoY) //when a soldier pawn reach the other player side, open the promotion form
        {
            //player 1
            if (tiletoX == 0 && boardvalue[tiletoX, tiletoY] == 11)
            {

                Form2 fm = new Form2(10, this, form, tiletoX, tiletoY);
                fm.Show();

            }

            else if (tiletoX == 7 && boardvalue[tiletoX, tiletoY] == 21)
            {

                Form2 fm = new Form2(20, this, form, tiletoX, tiletoY);
                fm.Show();
            }
        }

        public bool checkMate(int [,] whiteTerritory, int [,] blackTerritory, int[,] BoardValue, int tileX, int tileY)
        {
            int piecevalue = (BoardValue[tileX, tileY] - 20 > 0) ? 20 : 10;
            bool Checkmate = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //exception out of bound handle
                    if (tileX + i - 1 >= 0 && tileX + i - 1 <= 7 && tileY + j - 1 >= 0 && tileY + j - 1 <= 7)
                    {
                        if ((BoardValue[tileX + i - 1, tileY + j - 1] - piecevalue < 10 && BoardValue[tileX + i - 1, tileY + j - 1] - piecevalue >= 0) // if the pawn from the same player
                            ||
                            !(BoardValue[tileX + i - 1, tileY + j - 1] - piecevalue < 10 && BoardValue[tileX + i - 1, tileY + j - 1] - piecevalue >= 0)
                            && (piecevalue == 10 && whiteTerritory[tileX + i - 1, tileY + j - 1] > 0) || (piecevalue == 20 && blackTerritory[tileX + i - 1, tileY + j - 1] > 0)) // or if the pawn for the enemy and is defended
                        {
                            Checkmate = true;
                        }
                        else
                        {
                            Checkmate = false;
                            return Checkmate;
                        }
                    }
                    
                }
            }
            //MessageBox.Show("1");
            return Checkmate;
        }   
    }
}