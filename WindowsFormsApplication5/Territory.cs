using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication5
{
    public class Territory 
    {
        public int[,] WhiteTerritory; //the ground represents the tiles that are safe or under control by the Black pieces,
        //marked by the value of 0, if a piece has control over a tile add a value of 1
        public int[,] BlackTerritory; //the ground represents the tiles that are safe or under control by the White pieces, 
        //marked by the value of 0, if a piece has control over a tile add a value of 1
       
        
        public Territory () {
            WhiteTerritory = new int [,]{
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0}};

            BlackTerritory = new int[,]{
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},      
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0},
            { 0, 0, 0, 0, 0, 0, 0, 0}};
        }

        //removes territory of a solider, used when a solider is captured by enemy
        public void solider_removeTerritory(int[,] WhiteTerritory, int[,] BlackTerritory, int tileAtX, int tileAtY, int pieceValue)
        {
            //player 1 - Black
            if (pieceValue == 10)
            {
                // -1 to the tiles 
                //exception out of bound handle
                if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY + 1 >= 0 && tileAtY + 1 <= 7)
                {
                    BlackTerritory[tileAtX - 1, tileAtY + 1] -= 1;
                }

                //exception out of bound handle
                if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY - 1 >= 0 && tileAtY - 1 <= 7)
                {
                    BlackTerritory[tileAtX - 1, tileAtY - 1] -= 1;
                }
            }

            //player 2 - White
            else if (pieceValue == 20)
            {

                // -1 to the tiles 
                //exception out of bound handle
                if (tileAtX + 1 >= 0 && tileAtX + 1 <= 7 && tileAtY + 1 >= 0 && tileAtY + 1 <= 7)
                {
                    WhiteTerritory[tileAtX + 1, tileAtY + 1] -= 1;
                }

                //exception out of bound handle
                if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY - 1 >= 0 && tileAtY - 1 <= 7)
                {
                    WhiteTerritory[tileAtX + 1, tileAtY - 1] -= 1;
                }
            }
        }

        //removes territory of a solider, used when a solider is captured by enemy
        public void solider_updateTerritory(int[,] WhiteTerritory, int[,] BlackTerritory, int tileAtX, int tileAtY, int pieceValue)
        {
            //player 1 - Black
            if (pieceValue == 10)
            {
                // -1 to the tiles 
                //exception out of bound handle
                if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY + 1 >= 0 && tileAtY + 1 <= 7)
                {
                    BlackTerritory[tileAtX - 1, tileAtY + 1] += 1;

                }

                //exception out of bound handle
                if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY - 1 >= 0 && tileAtY - 1 <= 7)
                {
                    BlackTerritory[tileAtX - 1, tileAtY - 1] += 1;
                }
            }

            //player 2 - White
            else if (pieceValue == 20)
            {

                // -1 to the tiles 
                //exception out of bound handle
                if (tileAtX + 1 >= 0 && tileAtX + 1 <= 7 && tileAtY + 1 >= 0 && tileAtY + 1 <= 7)
                {
                    WhiteTerritory[tileAtX + 1, tileAtY + 1] += 1;
                }

                //exception out of bound handle
                if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY - 1 >= 0 && tileAtY - 1 <= 7)
                {
                    WhiteTerritory[tileAtX + 1, tileAtY - 1] += 1;
                }
            }
        }

        //update the piece territory
        public void solider_ChangeTerritory(int[,] WhiteTerritory, int[,] BlackTerritory, int[,] boardvalue, Button[,] boardtile, int tileAtX, int tileAtY, int tileToX, int tileToY, int pieceValue, ref bool kingChecked)
        {
            //player 1 - Black
            if (pieceValue == 10)
            {
                // -1 to the tiles before changing location
                //exception out of bound handle
                if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY + 1 >= 0 && tileAtY + 1 <= 7)
                {
                    BlackTerritory[tileAtX - 1, tileAtY + 1] -= 1;
                }

                //exception out of bound handle
                if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY - 1 >= 0 && tileAtY - 1 <= 7)
                {
                    BlackTerritory[tileAtX - 1, tileAtY - 1] -= 1;
                }

                // +1 to the tiles after changing location
                //exception out of bound handle
                if (tileToX - 1 >= 0 && tileToX - 1 <= 7 && tileToY + 1 >= 0 && tileToY + 1 <= 7)
                {
                    BlackTerritory[tileToX - 1, tileToY + 1] += 1;
                    //if the king was under territory
                    if ((boardvalue[tileToX - 1, tileToY + 1] == 26 && pieceValue == 10) || (boardvalue[tileToX - 1, tileToY + 1] == 16 && pieceValue == 20))
                    {
                        kingChecked = true;
                        boardtile[tileToX - 1, tileToY + 1].BackColor = Color.Red;

                        if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX - 1, tileToY + 1))
                        {

                            int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                            MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                        }
                    }
                }

                //exception out of bound handle
                if (tileToX - 1 >= 0 && tileToX - 1 <= 7 && tileToY - 1 >= 0 && tileToY - 1 <= 7)
                {
                    BlackTerritory[tileToX - 1, tileToY - 1] += 1;
                    //if the king was under territory
                    if ((boardvalue[tileToX - 1, tileToY - 1] == 26 && pieceValue == 10) || (boardvalue[tileToX - 1, tileToY - 1] == 16 && pieceValue == 20))
                    {
                        kingChecked = true;
                        boardtile[tileToX - 1, tileToY - 1].BackColor = Color.Red;

                        if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX - 1, tileToY - 1))
                        {

                            int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                            MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                        }
                    }
                }
            }

            //player 2 - White
            else if (pieceValue == 20)
            {

                // -1 to the tiles before changing location
                //exception out of bound handle
                if (tileAtX + 1 >= 0 && tileAtX + 1 <= 7 && tileAtY + 1 >= 0 && tileAtY + 1 <= 7)
                {
                    WhiteTerritory[tileAtX + 1, tileAtY + 1] -= 1;
                }

                //exception out of bound handle
                if (tileAtX + 1 >= 0 && tileAtX + 1 <= 7 && tileAtY - 1 >= 0 && tileAtY - 1 <= 7)
                {
                    WhiteTerritory[tileAtX + 1, tileAtY - 1] -= 1;
                }

                // +1 to the tiles after changing location
                //exception out of bound handle
                if (tileToX + 1 >= 0 && tileToX + 1 <= 7 && tileToY + 1 >= 0 && tileToY + 1 <= 7)
                {
                    WhiteTerritory[tileToX + 1, tileToY + 1] += 1;
                    //if the king was under territory
                    if ((boardvalue[tileToX + 1, tileToY + 1] == 26 && pieceValue == 10) || (boardvalue[tileToX + 1, tileToY + 1] == 16 && pieceValue == 20))
                    {
                        kingChecked = true;
                        boardtile[tileToX + 1, tileToY + 1].BackColor = Color.Red;

                        if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX + 1, tileToY + 1))
                        {

                            int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                            MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                        }
                    }
                }

                //exception out of bound handle
                if (tileToX - 1 >= 0 && tileToX - 1 <= 7 && tileToY - 1 >= 0 && tileToY - 1 <= 7)
                {
                    WhiteTerritory[tileToX + 1, tileToY - 1] += 1;
                    //if the king was under territory
                    if ((boardvalue[tileToX + 1, tileToY - 1] == 26 && pieceValue == 10) || (boardvalue[tileToX + 1, tileToY - 1] == 16 && pieceValue == 20))
                    {
                        kingChecked = true;
                        boardtile[tileToX + 1, tileToY - 1].BackColor = Color.Red;

                        if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX - 1, tileToY + 1))
                        {

                            int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                            MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                        }
                    }
                }
            }
        }


        //remove the territory of a single tile on upon being captured
        private void Knight_remove_Tile(int[,] WhiteTerritory, int[,] BlackTerritory, int tileAtX, int tileAtY, int pieceValue)
        {
            //player 1 - black
            if(pieceValue == 10)
            {
                // -1 to the tiles before changing location
                //exception out of bound handle
                if (tileAtX >= 0 && tileAtX <= 7 && tileAtY >= 0 && tileAtY <= 7)
                {
                    
                    BlackTerritory[tileAtX, tileAtY] -= 1;
                    
                }
            }

            // player 2 - white
            if (pieceValue == 20)
            {
                // -1 to the tiles before changing location
                //exception out of bound handle
                if (tileAtX >= 0 && tileAtX <= 7 && tileAtY >= 0 && tileAtY <= 7)
                {
                    WhiteTerritory[tileAtX, tileAtY] -= 1;
                }
            }
        }

        //remove the territory of a single tile on upon being captured
        private void Knight_update_Tile(int[,] WhiteTerritory, int[,] BlackTerritory, int tileAtX, int tileAtY, int pieceValue)
        {
            //player 1 - black
            if (pieceValue == 10)
            {
                // -1 to the tiles before changing location
                //exception out of bound handle
                if (tileAtX >= 0 && tileAtX <= 7 && tileAtY >= 0 && tileAtY <= 7)
                {

                    BlackTerritory[tileAtX, tileAtY] += 1;

                }
            }

            // player 2 - white
            if (pieceValue == 20)
            {
                // -1 to the tiles before changing location
                //exception out of bound handle
                if (tileAtX >= 0 && tileAtX <= 7 && tileAtY >= 0 && tileAtY <= 7)
                {
                    WhiteTerritory[tileAtX, tileAtY] += 1;
                }
            }
        }

        //update knight territory
        public void knight_removeTerritory(int[,] WhiteTerritory, int[,] BlackTerritory, int tileAtX, int tileAtY, int pieceValue)
        {
            //used for both sides, the piece value decides the piece color,
            //the function should update the knights terratory

            //UPPER RIGHT
            Knight_remove_Tile(WhiteTerritory, BlackTerritory, tileAtX - 2, tileAtY + 1, pieceValue);

            //UPPER LEFT
            Knight_remove_Tile(WhiteTerritory, BlackTerritory, tileAtX - 2, tileAtY - 1, pieceValue);

            //LEFT UP
            Knight_remove_Tile(WhiteTerritory, BlackTerritory, tileAtX - 1, tileAtY - 2, pieceValue);

            //LEFT DOWN
            Knight_remove_Tile(WhiteTerritory, BlackTerritory, tileAtX + 1, tileAtY - 2, pieceValue);

            //RIGHT UP
            Knight_remove_Tile(WhiteTerritory, BlackTerritory, tileAtX - 1, tileAtY + 2, pieceValue);

            //RIGHT DOWN
            Knight_remove_Tile(WhiteTerritory, BlackTerritory, tileAtX + 1, tileAtY + 2, pieceValue);

            //DOWN RIGHT
            Knight_remove_Tile(WhiteTerritory, BlackTerritory, tileAtX + 2, tileAtY + 1, pieceValue);

            //DOWN LEFT
            Knight_remove_Tile(WhiteTerritory, BlackTerritory, tileAtX + 2, tileAtY - 1, pieceValue);

        }



        //update knight territory
        public void knight_updateTerritory(int[,] WhiteTerritory, int[,] BlackTerritory, int tileAtX, int tileAtY, int pieceValue)
        {
            //used for both sides, the piece value decides the piece color,
            //the function should update the knights terratory

            //UPPER RIGHT
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX - 2, tileAtY + 1, pieceValue);

            //UPPER LEFT
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX - 2, tileAtY - 1, pieceValue);

            //LEFT UP
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX - 1, tileAtY - 2, pieceValue);

            //LEFT DOWN
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX + 1, tileAtY - 2, pieceValue);

            //RIGHT UP
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX - 1, tileAtY + 2, pieceValue);

            //RIGHT DOWN
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX + 1, tileAtY + 2, pieceValue);

            //DOWN RIGHT
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX + 2, tileAtY + 1, pieceValue);

            //DOWN LEFT
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX + 2, tileAtY - 1, pieceValue);

        }



        //update single tile
        private void King_Knight_changeTerritory_Tile(int[,] WhiteTerritory, int[,] BlackTerritory,int[,] boardvalue, Button[,] boardtile, int tileAtX, int tileAtY, int tileToX, int tileToY, int pieceValue, ref bool kingChecked)
        {
            if(pieceValue == 10)
            {
                // -1 to the tiles before changing location
                //exception out of bound handle
                if (tileAtX >= 0 && tileAtX <= 7 && tileAtY >= 0 && tileAtY <= 7)
                {
                    
                    BlackTerritory[tileAtX, tileAtY] -= 1;
                    
                }
                // +1 to the tiles before changing location
                if (tileToX >= 0 && tileToX <= 7 && tileToY >= 0 && tileToY <= 7)
                {
                    
                    BlackTerritory[tileToX, tileToY] += 1;
                    //if the king was under territory
                    if ((boardvalue[tileToX, tileToY] == 26 && pieceValue == 10) || (boardvalue[tileToX, tileToY] == 16 && pieceValue == 20))
                    {
                        kingChecked = true;
                        boardtile[tileToX, tileToY].BackColor = Color.Red;

                        if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX, tileToY))
                        {

                            int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                            MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                        }
                    } 
                }
            }
            if (pieceValue == 20)
            {
                // -1 to the tiles before changing location
                //exception out of bound handle
                if (tileAtX >= 0 && tileAtX <= 7 && tileAtY >= 0 && tileAtY <= 7)
                {
                    WhiteTerritory[tileAtX, tileAtY] -= 1;
                    
                }
                // +1 to the tiles before changing location
                if (tileToX >= 0 && tileToX <= 7 && tileToY >= 0 && tileToY <= 7)
                {
                    WhiteTerritory[tileToX, tileToY] += 1;
                    //if the king was under territory
                    if ((boardvalue[tileToX, tileToY] == 26 && pieceValue == 10) || (boardvalue[tileToX, tileToY] == 16 && pieceValue == 20))
                    {
                        kingChecked = true;
                        boardtile[tileToX, tileToY].BackColor = Color.Red;

                        if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX, tileToY))
                        {

                            int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                            MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                        }
                    }
                }
            }
        }
            

        //update knight territory
        public void knight_ChangeTerritory(int[,] WhiteTerritory, int[,] BlackTerritory,int[,] boardvalue, Button[,] boardtile, int tileAtX, int tileAtY, int tileToX, int tileToY, int pieceValue, ref bool kingChecked)
        {
            //used for both sides, the piece value decides the piece color,
            //the function should update the knights terratory

            //UPPER RIGHT
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory,boardvalue, boardtile, tileAtX - 2, tileAtY + 1, tileToX - 2, tileToY + 1, pieceValue, ref kingChecked);

            //UPPER LEFT
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory, boardvalue, boardtile,  tileAtX - 2, tileAtY - 1, tileToX - 2, tileToY - 1, pieceValue, ref kingChecked);

            //LEFT UP
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory, boardvalue, boardtile,  tileAtX - 1, tileAtY - 2, tileToX - 1, tileToY - 2, pieceValue, ref kingChecked);

            //LEFT DOWN
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory, boardvalue, boardtile, tileAtX + 1, tileAtY - 2, tileToX + 1, tileToY - 2, pieceValue, ref kingChecked);

            //RIGHT UP
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory, boardvalue, boardtile, tileAtX - 1, tileAtY + 2, tileToX - 1, tileToY + 2, pieceValue, ref kingChecked);

            //RIGHT DOWN
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory, boardvalue, boardtile, tileAtX + 1, tileAtY + 2, tileToX + 1, tileToY + 2, pieceValue, ref kingChecked);

            //DOWN RIGHT
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory, boardvalue, boardtile, tileAtX + 2, tileAtY + 1, tileToX + 2, tileToY + 1, pieceValue, ref kingChecked);

            //DOWN LEFT
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory, boardvalue, boardtile, tileAtX + 2, tileAtY - 1, tileToX + 2, tileToY - 1, pieceValue, ref kingChecked);

        }

        // remove the territory of a piece after declaring a move - Bishop, Queen
        public void Diagonal_RemoveCurrent_Territory(Controlling [,] controlling , UnderControl [,] undercontrol, int [,] boardvalue ,int[,] WhiteTerritory, int[,] BlackTerritory, int tileAtX, int tileAtY, int pieceValue)
        {
            int i = 1; // i is used to roll through the x and y locations
            bool flagUR = true; // flags are true while a piece has unremoved territory
            bool flagUL = true;
            bool flagDR = true;
            bool flagDL = true;

            // removes all the tiles under control in the controlling array
            controlling[tileAtX, tileAtY].undercontrol.Clear();


            // all the tiles are checked by level for preformance
            while (flagUR == true || flagUL == true || flagDR == true || flagDL == true)
            {
                
                //UPPER RIGHT side || -,+
                if (flagUR)
                {
                    ////exception out of bound handle
                    if (tileAtX - i >= 0 && tileAtX - i <= 7 && tileAtY + i >= 0 && tileAtY + i <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileAtX - i, tileAtY + i] -= 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileAtX - i, tileAtY + i] -= 1;

                        }

                        //after removing the territory, remove the piece control over the pieces
                        controlling[tileAtX - i, tileAtY + i].controller_changeLocation(controlling, undercontrol, tileAtX, tileAtY, tileAtX - i, tileAtY + i);

                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileAtX - i, tileAtY + i] > 0)
                        {
                            flagUR = false;
                        }
                    }
                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagUR = false;
                    }
                }

                //UPPER LEFT side || -,-
                if (flagUL)
                {
                    ////exception out of bound handle
                    if (tileAtX - i >= 0 && tileAtX - i <= 7 && tileAtY - i >= 0 && tileAtY - i <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            //MessageBox.Show("B"+BlackTerritory[tileAtX - i, tileAtY - i]);
                            BlackTerritory[tileAtX - i, tileAtY - i] -= 1;
                            //MessageBox.Show("A"+BlackTerritory[tileAtX - i, tileAtY - i]);

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileAtX - i, tileAtY - i] -= 1;

                        }
                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileAtX - i, tileAtY - i] > 0)
                        {
                            flagUL = false;
                        }

                        //after removing the territory, remove the piece control over the pieces
                        controlling[tileAtX - i, tileAtY - i].controller_changeLocation(controlling, undercontrol, tileAtX, tileAtY, tileAtX - i, tileAtY - i);
                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagUL = false;
                    }
                }



                //DOWN RIGHT side || +,+

                if (flagDR)
                {
                    ////exception out of bound handle
                    if (tileAtX + i >= 0 && tileAtX + i <= 7 && tileAtY + i >= 0 && tileAtY + i <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileAtX + i, tileAtY + i] -= 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileAtX + i, tileAtY + i] -= 1;

                        }

                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileAtX + i, tileAtY + i] > 0)
                        {
                            flagDR = false;
                        }

                        //after removing the territory, remove the piece control over the pieces
                        controlling[tileAtX + i, tileAtY + i].controller_changeLocation(controlling, undercontrol, tileAtX, tileAtY, tileAtX + i, tileAtY + i);
                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagDR = false;
                    }
                }

                //DOWN LEFT side || +,-
                if (flagDL)
                {
                    ////exception out of bound handle
                    if (tileAtX + i >= 0 && tileAtX + i <= 7 && tileAtY - i >= 0 && tileAtY - i <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileAtX + i, tileAtY - i] -= 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileAtX + i, tileAtY - i] -= 1;

                        }

                        //after removing the territory, remove the piece control over the pieces
                        controlling[tileAtX + i, tileAtY - i].controller_changeLocation(controlling, undercontrol, tileAtX, tileAtY, tileAtX + i, tileAtY - i);

                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileAtX + i, tileAtY - i] > 0)
                        {
                            flagDL = false;
                        }
                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagDL = false;
                    }
                }


                i++;
            }
        }

        // add 1 to the territory of a piece after declaring a move - Bishop, Queen
        public void Diagonal_UpdateCurrent_Territory(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, Button[,] boardtile, int[,] WhiteTerritory, int[,] BlackTerritory, int tileToX, int tileToY, int pieceValue, ref bool kingChecked)
        {
            int i = 1; // i is used to roll through the x and y locations
            bool flagUR = true; // flags are true while a piece has unremoved territory
            bool flagUL = true;
            bool flagDR = true;
            bool flagDL = true;

            //nutralize the the Totile
    


            // all the tiles are checked by level for preformance
            while (flagUR == true || flagUL == true || flagDR == true || flagDL == true)
            {

                //UPPER RIGHT side || -,+
                if (flagUR)
                {
                    ////exception out of bound handle
                    if (tileToX - i >= 0 && tileToX - i <= 7 && tileToY + i >= 0 && tileToY + i <= 7)
                    {
                        // each iteration add to the tile a value of 1
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileToX - i, tileToY + i] += 1;

                        }
                        else if (pieceValue == 20)
                        {

                            WhiteTerritory[tileToX - i, tileToY + i] += 1;

                        }
                        
                        // register the new under control tiles
                        undercontrol[tileToX - i, tileToY + i].undercontrol_register(controlling, undercontrol, tileToX, tileToY, tileToX - i, tileToY + i);


                        // until a tile with a pawn on it is detected.
                        if (boardvalue[tileToX - i, tileToY + i] > 0)
                        {
                            //if the king was under territory
                            if ((boardvalue[tileToX - i, tileToY + i] == 26 && pieceValue == 10) || (boardvalue[tileToX - i, tileToY + i] == 16 && pieceValue == 20))
                            {
                                kingChecked = true;
                                boardtile[tileToX - i, tileToY + i].BackColor = Color.Red;

                                if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX - i, tileToY + i))
                                {

                                    int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                                    MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                }
                            }

                            flagUR = false;
                        }
                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagUR = false;
                    }
                }

                //UPPER LEFT side || -,-
                if (flagUL)
                {
                    ////exception out of bound handle
                    if (tileToX - i >= 0 && tileToX - i <= 7 && tileToY - i >= 0 && tileToY - i <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileToX - i, tileToY - i] += 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileToX - i, tileToY - i] += 1;

                        }

                        // register the new under control tiles
                        undercontrol[tileToX - i, tileToY - i].undercontrol_register(controlling, undercontrol, tileToX, tileToY, tileToX - i, tileToY - i);
                        
                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileToX - i, tileToY - i] > 0)
                        {
                            //if the king was under territory
                            if ((boardvalue[tileToX - i, tileToY - i] == 26 && pieceValue == 10) || (boardvalue[tileToX - i, tileToY - i] == 16 && pieceValue == 20))
                            {
                                kingChecked = true;
                                boardtile[tileToX - i, tileToY - i].BackColor = Color.Red;

                                if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX - i, tileToY - i))
                                {

                                    int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                                    MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                }
                            }

                            flagUL = false;
                        }
                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagUL = false;
                    }
                }


                //DOWN RIGHT side || +,+

                if (flagDR)
                {
                    ////exception out of bound handle
                    if (tileToX + i >= 0 && tileToX + i <= 7 && tileToY + i >= 0 && tileToY + i <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileToX + i, tileToY + i] += 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileToX + i, tileToY + i] += 1;

                        }

                        // register the new under control tiles
                        
                        undercontrol[tileToX + i, tileToY + i].undercontrol_register(controlling, undercontrol, tileToX, tileToY, tileToX + i, tileToY + i);

                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileToX + i, tileToY + i] > 0)
                        {
                            //if the king was under territory
                            if ((boardvalue[tileToX + i, tileToY + i] == 26 && pieceValue == 10) || (boardvalue[tileToX + i, tileToY + i] == 16 && pieceValue == 20))
                            {
                                kingChecked = true;
                                boardtile[tileToX + i, tileToY + i].BackColor = Color.Red;

                                if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX + i, tileToY + i))
                                {

                                    int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                                    MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                }
                            }

                            flagDR = false;
                        }
                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagDR = false;
                    }
                }

                //DOWN LEFT side || +,-
                if (flagDL)
                {
                    ////exception out of bound handle
                    if (tileToX + i >= 0 && tileToX + i <= 7 && tileToY - i >= 0 && tileToY - i <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileToX + i, tileToY - i] += 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileToX + i, tileToY - i] += 1;

                        }

                        // register the new under control tiles
                        undercontrol[tileToX + i, tileToY - i].undercontrol_register(controlling, undercontrol, tileToX, tileToY, tileToX + i, tileToY - i);

                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileToX + i, tileToY - i] > 0)
                        {
                            //if the king was under territory
                            if ((boardvalue[tileToX + i, tileToY - i] == 26 && pieceValue == 10) || (boardvalue[tileToX + i, tileToY - i] == 16 && pieceValue == 20))
                            {
                                kingChecked = true;
                                boardtile[tileToX + i, tileToY - i].BackColor = Color.Red;

                                if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX + i, tileToY - i))
                                {

                                    int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                                    MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                }
                            }
                            flagDL = false;
                        }
                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagDL = false;
                    }
                }

                i++;
            }
        }

        // remove the territory of a piece after declaring a move - Rook, Queen
        public void straight__RemovePiece_Territory(Controlling [,]controlling, UnderControl [,]undercontrol, int[,] boardvalue, int[,] WhiteTerritory, int[,] BlackTerritory, int tileAtX, int tileAtY, int pieceValue)
        {

            int i = 1; // i is used to roll through the x and y locations
            bool flagU = true; // flags are true while a piece has unremoved territory
            bool flagR = true;
            bool flagL = true;
            bool flagD = true;

            // removes all the tiles under control in the controlling array
            controlling[tileAtX, tileAtY].undercontrol.Clear();

            // all the tiles are checked by level for preformance
            while (flagU == true || flagR == true || flagL == true || flagD == true)
            {

                //UPPER side || -,
                if (flagU)
                {
                    ////exception out of bound handle
                    if (tileAtX - i >= 0 && tileAtX - i <= 7 && tileAtY >= 0 && tileAtY <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileAtX - i, tileAtY] -= 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileAtX - i, tileAtY] -= 1;

                        }

                        //after removing the territory, remove the piece control over the pieces
                        controlling[tileAtX - i, tileAtY].controller_changeLocation(controlling, undercontrol, tileAtX, tileAtY, tileAtX - i, tileAtY);

                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileAtX - i, tileAtY] > 0)
                        {
                            flagU = false;
                        }

                    }
                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagU = false;
                    }
                }

                //RIGHT side ||  ,+
                if (flagR)
                {
                    ////exception out of bound handle
                    if (tileAtX >= 0 && tileAtX <= 7 && tileAtY + i >= 0 && tileAtY + i <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileAtX, tileAtY + i] -= 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileAtX, tileAtY + i] -= 1;

                        }
                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileAtX, tileAtY + i] > 0)
                        {
                            flagR = false;
                        }

                        //after removing the territory, remove the piece control over the pieces
                        controlling[tileAtX, tileAtY + i].controller_changeLocation(controlling, undercontrol, tileAtX, tileAtY, tileAtX, tileAtY + i);

                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagR = false;
                    }
                }


                //LEFT side ||  ,-

                if (flagL)
                {
                    ////exception out of bound handle
                    if (tileAtX >= 0 && tileAtX <= 7 && tileAtY - i >= 0 && tileAtY - i <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileAtX, tileAtY - i] -= 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileAtX, tileAtY - i] -= 1;

                        }

                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileAtX, tileAtY - i] > 0)
                        {
                            flagL = false;
                        }

                        //after removing the territory, remove the piece control over the pieces
                        controlling[tileAtX, tileAtY - i].controller_changeLocation(controlling, undercontrol, tileAtX, tileAtY, tileAtX, tileAtY - i);

                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagL = false;
                    }
                }

                //DOWN side || +,
                if (flagD)
                {
                    ////exception out of bound handle
                    if (tileAtX + i >= 0 && tileAtX + i <= 7 && tileAtY >= 0 && tileAtY <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileAtX + i, tileAtY] -= 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileAtX + i, tileAtY] -= 1;

                        }
                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileAtX + i, tileAtY] > 0)
                        {
                            flagD = false;
                        }

                        //after removing the territory, remove the piece control over the pieces
                        controlling[tileAtX + i, tileAtY].controller_changeLocation(controlling, undercontrol, tileAtX, tileAtY, tileAtX + i, tileAtY);

                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagD = false;
                    }
                }
                i++;
            }
        }


        // add 1 to the territory of a piece after declaring a move - Rook, Queen
        // the function takes the location of the destination and then update the piece's territory
        public void straight_UpdatePiece_Territory(Controlling [,] controlling, UnderControl [,] undercontrol, int[,] boardvalue, Button [,]boardtile, int[,] WhiteTerritory, int[,] BlackTerritory, int tileToX, int tileToY, int pieceValue, ref bool kingChecked)
        {
            int i = 1; // i is used to roll through the x and y locations
            bool flagU = true; // flags are true while a piece has unremoved territory
            bool flagR = true;
            bool flagL = true;
            bool flagD = true;

            // all the tiles are checked by level for preformance
            while (flagU == true || flagR == true || flagL == true || flagD == true)
            {

                //UPPER side || -,
                if (flagU)
                {
                    ////exception out of bound handle
                    if (tileToX - i >= 0 && tileToX - i <= 7 && tileToY >= 0 && tileToY <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileToX - i, tileToY] += 1;
                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileToX - i, tileToY] += 1;

                        }
                        
                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileToX - i, tileToY] > 0)
                        {
                            //if the king was under territory
                            if ((boardvalue[tileToX - i, tileToY] == 26 && pieceValue == 10) || (boardvalue[tileToX - i, tileToY] == 16 && pieceValue == 20))
                            {
                                kingChecked = true;
                                boardtile[tileToX - i, tileToY].BackColor = Color.Red;

                                if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX - 1, tileToY))
                                {

                                    int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                                    MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                }
                            }
                            
                            flagU = false;
                        }

                        // register the new under control tiles
                        undercontrol[tileToX - i, tileToY].undercontrol_register(controlling, undercontrol, tileToX, tileToY, tileToX - i, tileToY);

                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagU = false;
                    }
                }

                //RIGHT side ||  ,+
                if (flagR)
                {
                    ////exception out of bound handle
                    if (tileToX >= 0 && tileToX <= 7 && tileToY + i >= 0 && tileToY + i <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileToX, tileToY + i] += 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileToX, tileToY + i] += 1;

                        }
                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileToX, tileToY + i] > 0)
                        {
                            //if the king was under territory
                            if ((boardvalue[tileToX, tileToY + i] == 26 && pieceValue == 10) || (boardvalue[tileToX, tileToY + i] == 16 && pieceValue == 20))
                            {
                                kingChecked = true;
                                boardtile[tileToX, tileToY + i].BackColor = Color.Red;

                                if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX, tileToY + i))
                                {

                                    int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                                    MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                }

                            }
                            flagR = false;
                        }

                        // register the new under control tiles
                        undercontrol[tileToX, tileToY + i].undercontrol_register(controlling, undercontrol, tileToX, tileToY, tileToX, tileToY + i);

                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagR = false;
                    }
                }


                //LEFT side ||  ,-

                if (flagL)
                {
                    ////exception out of bound handle
                    if (tileToX >= 0 && tileToX <= 7 && tileToY - i >= 0 && tileToY - i <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileToX, tileToY - i] += 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileToX, tileToY - i] += 1;

                        }

                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileToX, tileToY - i] > 0)
                        {
                            //if the king was under territory
                            if ((boardvalue[tileToX, tileToY - i] == 26 && pieceValue == 10) || (boardvalue[tileToX, tileToY - i] == 16 && pieceValue == 20))
                            {
                                kingChecked = true;
                                boardtile[tileToX, tileToY - i].BackColor = Color.Red;
                                if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX, tileToY - i))
                                {

                                    int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                                    MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                }

                            }

                            flagL = false;
                        }

                        // register the new under control tiles
                        undercontrol[tileToX, tileToY - i].undercontrol_register(controlling, undercontrol, tileToX, tileToY, tileToX, tileToY - i);

                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagL = false;
                    }
                }

                //DOWN side || +,
                if (flagD)
                {
                    ////exception out of bound handle
                    if (tileToX + i >= 0 && tileToX + i <= 7 && tileToY >= 0 && tileToY <= 7)
                    {
                        // each iteration 
                        if (pieceValue == 10)
                        {
                            BlackTerritory[tileToX + i, tileToY] += 1;

                        }
                        else if (pieceValue == 20)
                        {
                            WhiteTerritory[tileToX + i, tileToY] += 1;

                        }
                        // if a piece was encountered flag = false: will stop iterating in this path
                        if (boardvalue[tileToX + i, tileToY] > 0)
                        {
                            //if the king was under territory
                            if ((boardvalue[tileToX + i, tileToY] == 26 && pieceValue == 10) || (boardvalue[tileToX + i, tileToY] == 16 && pieceValue == 20))
                            {
                                kingChecked = true;
                                boardtile[tileToX + i, tileToY].BackColor = Color.Red;

                                if (kingChecked == true && checkMate(WhiteTerritory, BlackTerritory, boardvalue, tileToX + 1, tileToY))
                                {

                                    int winner = ((pieceValue - 10 == 0) ? 1 : 2);
                                    MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                }
                            }

                            flagD = false;
                        }

                        // register the new under control tiles
                        undercontrol[tileToX + i, tileToY].undercontrol_register(controlling, undercontrol, tileToX, tileToY, tileToX + i, tileToY);

                    }

                    //if the loop has passed the border stop looping in the current path
                    else
                    {
                        flagD = false;
                    }
                }
                i++;
            }
        }

        //update bishop territory
        public void Bishop_ChangeTerritory(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, Button [,] boardtile, int[,] WhiteTerritory, int[,] BlackTerritory, int tileAtX, int tileAtY, int tileToX, int tileToY, int pieceValue, ref bool kingChecked)
        {
            //used for both sides, the piece value decides the piece color,
            //the function should update the knights terratory

            //player 1 - black
            if (pieceValue == 10)
            {
                //remove territory from past location (tileAtX,tileAtY)
                Diagonal_RemoveCurrent_Territory(controlling, undercontrol, boardvalue, WhiteTerritory, BlackTerritory, tileAtX, tileAtY, 10);
                //assign new territory from in the current location (tileToX,tileToY)
                Diagonal_UpdateCurrent_Territory(controlling, undercontrol, boardvalue, boardtile, WhiteTerritory, BlackTerritory, tileToX, tileToY, 10,ref kingChecked);
            }

            //player 2 - white
            else if (pieceValue == 20)
            {
                //remove territory from past location (tileAtX,tileAtY)
                Diagonal_RemoveCurrent_Territory(controlling, undercontrol, boardvalue, WhiteTerritory, BlackTerritory, tileAtX, tileAtY, 20);
                //assign new territory from in the current location (tileToX,tileToY)
                Diagonal_UpdateCurrent_Territory(controlling, undercontrol, boardvalue, boardtile, WhiteTerritory, BlackTerritory, tileToX, tileToY, 20, ref kingChecked);

            }
        }

        public void King_ChangeTerritory(int[,] WhiteTerritory, int[,] BlackTerritory,int[,] boardvalue,Button[,] boardtile, int tileAtX, int tileAtY, int tileToX, int tileToY, int pieceValue, ref bool kingChecked)
        {
            //used for both sides, the piece value decides the piece color,
            //the function should update the King terratory

            //UP
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory,boardvalue,boardtile, tileAtX - 1, tileAtY, tileToX - 1, tileToY, pieceValue, ref kingChecked);

            //UPPER LEFT
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory,boardvalue,boardtile, tileAtX - 1, tileAtY - 1, tileToX - 1, tileToY - 1, pieceValue, ref kingChecked);

            //UPPER RIGHT
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory,boardvalue,boardtile, tileAtX - 1, tileAtY + 1, tileToX - 1, tileToY + 1, pieceValue, ref kingChecked);

            //DOWN
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory,boardvalue,boardtile, tileAtX + 1, tileAtY, tileToX + 1, tileToY, pieceValue, ref kingChecked);

            //DOWN LEFT
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory,boardvalue,boardtile, tileAtX + 1, tileAtY - 1, tileToX + 1, tileToY - 1, pieceValue, ref kingChecked);

            //DOWN RIGHT
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory,boardvalue,boardtile, tileAtX + 1, tileAtY + 1, tileToX + 1, tileToY + 1, pieceValue, ref kingChecked);

            //RIGHT
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory,boardvalue,boardtile, tileAtX, tileAtY + 1, tileToX, tileToY + 1, pieceValue, ref kingChecked);

            //LEFT
            King_Knight_changeTerritory_Tile(WhiteTerritory, BlackTerritory,boardvalue,boardtile, tileAtX, tileAtY - 1, tileToX, tileToY - 1, pieceValue, ref kingChecked);

        }

        public void King_updateTerritory(int[,] WhiteTerritory, int[,] BlackTerritory, int tileAtX, int tileAtY, int pieceValue)
        {
            //used for both sides, the piece value decides the piece color,
            //the function should update the King terratory

            //UP
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX - 1, tileAtY, pieceValue);

            //UPPER LEFT
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX - 1, tileAtY - 1, pieceValue);

            //UPPER RIGHT
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX - 1, tileAtY + 1, pieceValue);

            //DOWN
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX + 1, tileAtY, pieceValue);

            //DOWN LEFT
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX + 1, tileAtY - 1, pieceValue);

            //DOWN RIGHT
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX + 1, tileAtY + 1, pieceValue);

            //RIGHT
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX, tileAtY + 1, pieceValue);

            //LEFT
            Knight_update_Tile(WhiteTerritory, BlackTerritory, tileAtX, tileAtY - 1, pieceValue);

        }

        public bool checkMate(int[,] whiteTerritory, int[,] blackTerritory, int[,] BoardValue, int tileX, int tileY)
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
                        //MessageBox.Show("hey" + (tileX + i - 1) + " " + (tileY + j - 1));
                        if ((BoardValue[tileX + i - 1, tileY + j - 1] - piecevalue < 10 && BoardValue[tileX + i - 1, tileY + j - 1] - piecevalue >= 0) // if the pawn from the same player
                            ||
                            !(BoardValue[tileX + i - 1, tileY + j - 1] - piecevalue < 10 && BoardValue[tileX + i - 1, tileY + j - 1] - piecevalue >= 0)
                            && (piecevalue == 10 && whiteTerritory[tileX + i - 1, tileY + j - 1] > 0) || (piecevalue == 20 && blackTerritory[tileX + i - 1, tileY + j - 1] > 0)) // or if the pawn for the enemy and is defended
                        {
                            Checkmate = true;
                        }
                        else
                        {
                            //MessageBox.Show("hey" + (tileX + i - 1) + " " + (tileY + j - 1));
                            //MessageBox.Show("2");
                            Checkmate = false;

                            return Checkmate;
                        }
                    }
                    
                }
            }
            //MessageBox.Show("1");
            return Checkmate;
        }

        //upon openning the program assign the controlling and undercontrolled tiles and territories
        public void intializeTerritories(Controlling [,]controlling, UnderControl [,] undercontrol, Territory territory, int [,] boardvalue, Button [,]boardtile, int tileX, int tileY)
        {
            bool tempcheck = false;
            int piecevalue = (boardvalue[tileX, tileY] - 20 > 0)? 20 : 10;

            //solider
            if (boardvalue[tileX, tileY] == 11 || boardvalue[tileX, tileY] == 21)
            {
                solider_updateTerritory(territory.WhiteTerritory, territory.BlackTerritory, tileX, tileY, piecevalue);
            }

            //knight
            else if (boardvalue[tileX, tileY] == 12 || boardvalue[tileX, tileY] == 22)
            {
                knight_updateTerritory(territory.WhiteTerritory, territory.BlackTerritory, tileX, tileY, piecevalue);
            }

            //bishop
            else if (boardvalue[tileX, tileY] == 13 || boardvalue[tileX, tileY] == 23)
            {

                Diagonal_UpdateCurrent_Territory(controlling, undercontrol, boardvalue, boardtile, territory.WhiteTerritory, territory.BlackTerritory, tileX, tileY, piecevalue, ref tempcheck);
            }

            //rook
            else if (boardvalue[tileX, tileY] == 14 || boardvalue[tileX, tileY] == 24)
            {

                straight_UpdatePiece_Territory(controlling, undercontrol, boardvalue, boardtile, territory.WhiteTerritory, territory.BlackTerritory, tileX, tileY, piecevalue, ref tempcheck);
            }

            //queen
            else if (boardvalue[tileX, tileY] == 15 || boardvalue[tileX, tileY] == 25)
            {
                Diagonal_UpdateCurrent_Territory( controlling, undercontrol, boardvalue, boardtile, territory.WhiteTerritory, territory.BlackTerritory, tileX, tileY, piecevalue, ref tempcheck);
                straight_UpdatePiece_Territory( controlling, undercontrol, boardvalue, boardtile, territory.WhiteTerritory, territory.BlackTerritory, tileX, tileY, piecevalue, ref tempcheck);
            }
            
            //king
            else if (boardvalue[tileX, tileY] == 16 || boardvalue[tileX, tileY] == 26)
            {
                King_updateTerritory(territory.WhiteTerritory, territory.BlackTerritory, tileX, tileY, piecevalue);
            }
        }
    }
}
//continue...
//the remaining should be to complete the territory functions,
//with a function for the diagonal and horizontal used in RookBishopQueen movement
//which should be responsible for the undercontrol and when another piece either
//from the enemy or the piece's team move away from the piece's trajectory.
//the function will work by getting back the information of the said piece from the moving pawn 
//UnderControl.controling list which shall include all the pieces that the pawn is lying on thier trajectory
//then a comparison between the pawn location and the controlling pieces will lead to an update of thier territory