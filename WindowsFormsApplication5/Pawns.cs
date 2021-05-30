using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data.OleDb;
using System.Data;

namespace WindowsFormsApplication5
{
    public class Pawns
    {
        // class for handeling movment of Pawns on board
        //!!! onclick attribute when passed as true means that the piece has been highlighted,
        //!!! when passed as false means that after a piece has been highlighted another tile was clicked,
        //!!! hence Pawn class handles if a move is leagl or no 
        // for future refrence 

        public void solider(Controlling [,] controlling, UnderControl[,] undercontrol, Territory territory, int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int tileToX, int tileToY, ref int turn, bool onclick, bool guide,ref bool kingChecked, ref int turncounter)
        {

            //check if the piece will expose the king or not
            if(undercontrol[tileToX, tileToY].beforeChangelocation_CheckInvalid(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked))
            {
                MessageBox.Show("invalid move");
            }
            else
            {
                //if onclick == false, the soldier was highlited the guide should work.

                if (guide == true && onclick == false)
                {
                    //player 1

                    if (boardvalue[tileAtX, tileAtY] - 10 == 1)
                    {

                        if (tileAtX == 6) //first location double step
                        {
                            boardtiles[tileAtX - 2, tileAtY].BackColor = Color.FromArgb(200, Color.LimeGreen);
                        }

                        //exception out of bound handle
                        if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY >= 0 && tileAtY <= 7)
                        {
                            //if no enemy pawns infront 
                            if (boardvalue[tileAtX - 1, tileAtY] == 0)
                            {
                                boardtiles[tileAtX - 1, tileAtY].BackColor = Color.FromArgb(200, Color.LimeGreen);
                            }
                        }
                        //exception out of bound handle
                        if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY + 1 >= 0 && tileAtY + 1 <= 7)
                        {
                            //enemy pawn - right tile
                            if (boardvalue[tileAtX - 1, tileAtY + 1] - 20 > 0)
                            {
                                boardtiles[tileAtX - 1, tileAtY + 1].BackColor = Color.FromArgb(200, Color.LimeGreen);
                            }
                        }

                        //exception out of bound handle
                        if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY - 1 >= 0 && tileAtY - 1 <= 7)
                        {
                            //enemy pawn - left tile
                            if (boardvalue[tileAtX - 1, tileAtY - 1] - 20 > 0)
                            {
                                boardtiles[tileAtX - 1, tileAtY - 1].BackColor = Color.FromArgb(200, Color.LimeGreen);
                            }
                        }
                    }

                    //player 2
                    if (boardvalue[tileAtX, tileAtY] - 20 == 1)
                    {
                        //first double step
                        if (tileAtX == 1)
                        {
                            boardtiles[tileAtX + 2, tileAtY].BackColor = Color.FromArgb(200, Color.LimeGreen);
                        }

                        //exception out of bound handle
                        if (tileAtX + 1 >= 0 && tileAtX + 1 <= 7 && tileAtY >= 0 && tileAtY <= 7)
                        {
                            if (boardvalue[tileAtX + 1, tileAtY] == 0)
                            {
                                boardtiles[tileAtX + 1, tileAtY].BackColor = Color.FromArgb(200, Color.LimeGreen);
                            }
                        }

                        //exception out of bound handle
                        if (tileAtX + 1 >= 0 && tileAtX + 1 <= 7 && tileAtY + 1 >= 0 && tileAtY + 1 <= 7)
                        {

                            //enemy pawn - right tile
                            if (boardvalue[tileAtX + 1, tileAtY + 1] - 10 > 0 && boardvalue[tileAtX + 1, tileAtY + 1] - 10 < 10)
                            {

                                boardtiles[tileAtX + 1, tileAtY + 1].BackColor = Color.FromArgb(200, Color.LimeGreen);
                            }
                        }

                        //exception out of bound handle
                        if (tileAtX + 1 >= 0 && tileAtX + 1 <= 7 && tileAtY - 1 >= 0 && tileAtY - 1 <= 7)
                        {

                            //enemy pawn - left tile
                            if (boardvalue[tileAtX + 1, tileAtY - 1] - 10 > 0 && boardvalue[tileAtX + 1, tileAtY - 1] - 10 < 10)
                            {

                                boardtiles[tileAtX + 1, tileAtY - 1].BackColor = Color.FromArgb(200, Color.LimeGreen);
                            }
                        }
                    }
                }


                //onclick true, handle double click on same tile (remove highlight)
                if (guide == true && onclick == true)
                {
                    Color color1 = Color.White;
                    Color color2 = Color.Black;
                    if (boardvalue[tileAtX, tileAtY] - 10 == 1) //player 1 remove highlight
                    {
                        if (tileAtX == 6) //first location double step
                        {
                            boardtiles[tileAtX - 2, tileAtY].BackColor = ((tileAtX - 2) % 2 == 0 && tileAtY % 2 == 0 || (tileAtX - 2) % 2 == 1 && tileAtY % 2 == 1) ? color1 : color2;
                        }

                        //exception out of bound handle
                        if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY >= 0 && tileAtY <= 7)
                        {
                            if (boardvalue[tileAtX - 1, tileAtY] == 0)
                            {
                                boardtiles[tileAtX - 1, tileAtY].BackColor = ((tileAtX - 1) % 2 == 0 && tileAtY % 2 == 0 || (tileAtX - 1) % 2 == 1 && tileAtY % 2 == 1) ? color1 : color2;
                            }
                        }

                        //exception out of bound handle
                        if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY + 1 >= 0 && tileAtY + 1 <= 7)
                        {
                            //enemy pawn - right tile
                            if (boardvalue[tileAtX - 1, tileAtY + 1] - 20 > 0)
                            {
                                boardtiles[tileAtX - 1, tileAtY + 1].BackColor = ((tileAtX - 1) % 2 == 0 && (tileAtY + 1) % 2 == 0 || (tileAtX - 1) % 2 == 1 && (tileAtY + 1) % 2 == 1) ? color1 : color2;
                            }
                        }

                        //exception out of bound handle
                        if (tileAtX - 1 >= 0 && tileAtX - 1 <= 7 && tileAtY - 1 >= 0 && tileAtY - 1 <= 7)
                        {
                            //enemy pawn - left tile
                            if (boardvalue[tileAtX - 1, tileAtY - 1] - 20 > 0)
                            {
                                boardtiles[tileAtX - 1, tileAtY - 1].BackColor = ((tileAtX - 1) % 2 == 0) ? (((tileAtY - 1) % 2 == 0) ? color1 : color2) : (((tileAtY - 1) % 2 == 0) ? color2 : color1);
                            }
                        }
                    }

                    //player 2 remove highlight
                    if (boardvalue[tileAtX, tileAtY] - 20 == 1)
                    {

                        if (tileAtX == 1) //first location double step
                        {
                            boardtiles[tileAtX + 2, tileAtY].BackColor = ((tileAtX + 2) % 2 == 0 && tileAtY % 2 == 0 || (tileAtX + 2) % 2 == 1 && tileAtY % 2 == 1) ? color1 : color2;
                        }

                        //exception out of bound handle
                        if (tileAtX + 1 >= 0 && tileAtX + 1 <= 7 && tileAtY >= 0 && tileAtY <= 7)
                        {
                            if (boardvalue[tileAtX + 1, tileAtY] == 0)
                            {
                                boardtiles[tileAtX + 1, tileAtY].BackColor = ((tileAtX - 1) % 2 == 0) ? ((tileAtY % 2 == 0) ? color1 : color2) : ((tileAtY % 2 == 0) ? color2 : color1);
                            }
                        }

                        //exception out of bound handle
                        if (tileAtX + 1 >= 0 && tileAtX + 1 <= 7 && tileAtY + 1 >= 0 && tileAtY + 1 <= 7)
                        {
                            //enemy pawn - right tile
                            if (boardvalue[tileAtX + 1, tileAtY + 1] - 10 > 0 && boardvalue[tileAtX + 1, tileAtY + 1] - 10 < 10)
                            {
                                boardtiles[tileAtX + 1, tileAtY + 1].BackColor = ((tileAtX - 1) % 2 == 0) ? (((tileAtY + 1) % 2 == 0) ? color1 : color2) : (((tileAtY + 1) % 2 == 0) ? color2 : color1);
                            }
                        }

                        //exception out of bound handle
                        if (tileAtX + 1 >= 0 && tileAtX + 1 <= 7 && tileAtY - 1 >= 0 && tileAtY - 1 <= 7)
                        {
                            //enemy pawn - left tile
                            if (boardvalue[tileAtX + 1, tileAtY - 1] - 10 > 0 && boardvalue[tileAtX + 1, tileAtY - 1] - 10 < 10)
                            {
                                boardtiles[tileAtX + 1, tileAtY - 1].BackColor = ((tileAtX + 1) % 2 == 0) ? (((tileAtY - 1) % 2 == 0) ? color1 : color2) : (((tileAtY - 1) % 2 == 0) ? color2 : color1);
                            }
                        }
                    }
                }

            }




            //moveing pawn case
            if (onclick == true && tileAtX != tileToX || tileAtY != tileToY)
            {
            
                //PLAYER 1: value 11~16
                if (tileAtX == 6 && boardvalue[tileAtX, tileAtY] - 10 == 1)
                {
                    if (boardvalue[tileToX, tileToY] == 0 && Math.Abs(tileAtX - tileToX) == 2 && tileAtY - tileToY == 0) //solider move forward, case to tile is empty
                    {
                        //delete and remove territory off the tiles lying after the ToTile
                        undercontrol[tileToX, tileToY].onChangeLocation_RevertTerritory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY);

                        territory.solider_ChangeTerritory(territory.WhiteTerritory, territory.BlackTerritory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 10, ref kingChecked); // update soldier territory
                        //if a pawn is under territory of an ally pawn, check after moving if the king will be checked
                        undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked);
                        boardvalue[tileAtX, tileAtY] = 0;
                        boardvalue[tileToX, tileToY] = 11;//checks for pawn to KBRQ change after reaching the edge 

                        //update the tiles lying after the ATtile to extend the controlling pieces territory
                        undercontrol[tileToX, tileToY].onChangeLocation_continue(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY);
                        undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, ref kingChecked);
                        turn = (turn % 2 == 1) ? 2 : 1;
                        turncounter += 1;
                        saveStatus(boardvalue, territory,ref turncounter);
                    }
                }

                    if (boardvalue[tileAtX, tileAtY] - 10 == 1)
                        if (boardvalue[tileToX, tileToY] == 0 && Math.Abs(tileAtX - tileToX) == 1 && tileAtY - tileToY == 0) //solider move forward, case to tile is empty
                        {
                            //delete and remove territory off the tiles lying after the ToTile
                            undercontrol[tileToX, tileToY].onChangeLocation_RevertTerritory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY);

                            territory.solider_ChangeTerritory(territory.WhiteTerritory, territory.BlackTerritory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 10, ref kingChecked); // update soldier territory
                            //if a pawn is under territory of an ally pawn, check after moving if the king will be checked
                            undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked);

                            boardvalue[tileAtX, tileAtY] = 0;
                            boardvalue[tileToX, tileToY] = 11;
                            //checks for pawn to KBRQ change after reaching the edge 

                            //update the tiles lying after the ATtile to extend the controlling pieces territory
                            undercontrol[tileToX, tileToY].onChangeLocation_continue(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY);
                            undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, ref kingChecked);

                            turn = (turn % 2 == 1) ? 2 : 1;
                            turncounter += 1;
                            saveStatus(boardvalue, territory, ref turncounter);
                        }

                    if (boardvalue[tileToX, tileToY] > 20 && tileAtX - tileToX == 1 && Math.Abs(tileAtY - tileToY) == 1) //if statment == the totile is an enemy tile
                    {
                        territory.solider_ChangeTerritory(territory.WhiteTerritory, territory.BlackTerritory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 10, ref kingChecked); // update soldier territory

                        //delete and remove territory off the tiles lying after the ToTile
                        undercontrol[tileToX, tileToY].onChangeLocation_RevertTerritory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY);

                        //case if the Totile has an enemy pawn
                        undercontrol[tileToX, tileToY].onChangeLocation_CaptureEnemyPawn(controlling, undercontrol, territory, boardvalue, tileToX, tileToY, 10);
                        //if a pawn is under territory of an ally pawn, check after moving if the king will be checked
                        undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked);

                        boardvalue[tileAtX, tileAtY] = 0;
                        boardvalue[tileToX, tileToY] = 11;
                        //checks for pawn to KBRQ change after reaching the edge

                        //update the tiles lying after the ATtile to extend the controlling pieces territory
                        undercontrol[tileToX, tileToY].onChangeLocation_continue(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY);

                        turn = (turn % 2 == 1) ? 2 : 1;
                        turncounter += 1;
                        saveStatus(boardvalue, territory, ref turncounter);
                    }

                    //PLAYER 2: value 21~26

                    if (tileAtX == 1 && boardvalue[tileAtX, tileAtY] - 20 == 1)
                    {
                        if (boardvalue[tileToX, tileToY] == 0 && Math.Abs(tileAtX - tileToX) == 2 && tileAtY - tileToY == 0) //solider move forward, case to tile is empty
                        {
                            //delete and remove territory off the tiles lying after the ToTile
                            undercontrol[tileToX, tileToY].onChangeLocation_RevertTerritory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY);

                            territory.solider_ChangeTerritory(territory.WhiteTerritory, territory.BlackTerritory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 20, ref kingChecked); // update soldier territory
                            //if a pawn is under territory of an ally pawn, check after moving if the king will be checked
                            undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked);

                            boardvalue[tileAtX, tileAtY] = 0;
                            boardvalue[tileToX, tileToY] = 21;//checks for pawn to KBRQ change after reaching the edge 

                            //update the tiles lying after the ATtile to extend the controlling pieces territory
                            undercontrol[tileToX, tileToY].onChangeLocation_continue(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY);

                            turn = (turn % 2 == 1) ? 2 : 1;
                            turncounter += 1;
                            saveStatus(boardvalue, territory, ref turncounter);
                        }
                    }

                    if (boardvalue[tileAtX, tileAtY] - 20 == 1)
                        if (boardvalue[tileToX, tileToY] == 0 && tileAtX - tileToX == -1 && tileAtY - tileToY == 0) //solider move forward, case to tile is empty
                        {
                            //delete and remove territory off the tiles lying after the ToTile
                            undercontrol[tileToX, tileToY].onChangeLocation_RevertTerritory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY);

                            territory.solider_ChangeTerritory(territory.WhiteTerritory, territory.BlackTerritory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 20, ref kingChecked); // update soldier territory
                            //if a pawn is under territory of an ally pawn, check after moving if the king will be checked
                            undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked);

                            boardvalue[tileAtX, tileAtY] = 0;
                            boardvalue[tileToX, tileToY] = 21;
                            //checks for pawn to KBRQ change after reaching the edge 

                            //update the tiles lying after the ATtile to extend the controlling pieces territory
                            undercontrol[tileToX, tileToY].onChangeLocation_continue(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY);

                            turn = (turn % 2 == 1) ? 2 : 1;
                            turncounter += 1;
                            saveStatus(boardvalue, territory, ref turncounter);
                        }

                    if (boardvalue[tileToX, tileToY] - 10 > 0 && boardvalue[tileToX, tileToY] - 10 < 10 && tileAtX - tileToX == -1 && Math.Abs(tileAtY - tileToY) == 1) //if statment == the totile is an enemy tile
                    {
                        //delete and remove territory off the tiles lying after the ToTile
                        undercontrol[tileToX, tileToY].onChangeLocation_RevertTerritory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY);

                        territory.solider_ChangeTerritory(territory.WhiteTerritory, territory.BlackTerritory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 20, ref kingChecked); // update soldier territory
                        //checks for pawn to KBRQ change after reaching the edge 

                        //case if the Totile has an enemy pawn, remove the captured pieces territory
                        //before moving
                        undercontrol[tileToX, tileToY].onChangeLocation_CaptureEnemyPawn(controlling, undercontrol, territory, boardvalue, tileToX, tileToY, 10);
                        //if a pawn is under territory of an ally pawn, check after moving if the king will be checked
                        undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked);

                        boardvalue[tileAtX, tileAtY] = 0;
                        boardvalue[tileToX, tileToY] = 21;

                        //update the tiles lying after the ATtile to extend the controlling pieces territory
                        undercontrol[tileToX, tileToY].onChangeLocation_continue(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY);

                        
                        turn = (turn % 2 == 1) ? 2 : 1;
                        turncounter += 1;
                        saveStatus(boardvalue, territory, ref turncounter);
                    
                }
            }
        }
            





        //knight tile processing
        public void knight(Controlling[,] controlling, UnderControl[,] undercontrol, Territory territory, int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int tileToX, int tileToY, ref int turn, bool onclick, bool guide,ref bool kingchecked, ref int turncounter)
        {
            //check if the piece will expose the king or not
            if(undercontrol[tileToX, tileToY].beforeChangelocation_CheckInvalid(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingchecked))
            {
                MessageBox.Show("invalid move");
            }
            else
            {

            //if onclick == false tile with knight was highlighted
            if (guide == true && onclick == false)
            {

                //player 1 - black
                if (boardvalue[tileAtX, tileAtY] - 10 == 2)
                {
                    //UPPER RIGHT
                    knight_highlight(boardvalue, boardtiles, tileAtX - 2, tileAtY + 1, 20);

                    //UPPER LEFT
                    knight_highlight(boardvalue, boardtiles, tileAtX - 2, tileAtY - 1, 20);

                    //LEFT UP
                    knight_highlight(boardvalue, boardtiles, tileAtX - 1, tileAtY - 2, 20);

                    //LEFT DOWN
                    knight_highlight(boardvalue, boardtiles, tileAtX + 1, tileAtY - 2, 20);

                    //RIGHT UP
                    knight_highlight(boardvalue, boardtiles, tileAtX - 1, tileAtY + 2, 20);

                    //RIGHT DOWN
                    knight_highlight(boardvalue, boardtiles, tileAtX + 1, tileAtY + 2, 20);

                    //DOWN RIGHT
                    knight_highlight(boardvalue, boardtiles, tileAtX + 2, tileAtY + 1, 20);

                    //DOWN LEFT
                    knight_highlight(boardvalue, boardtiles, tileAtX + 2, tileAtY - 1, 20);
                }


                //player 2 - white
                if (boardvalue[tileAtX, tileAtY] - 20 == 2)
                {
                    //UPPER RIGHT
                    knight_highlight(boardvalue, boardtiles, tileAtX - 2, tileAtY + 1, 10);

                    //UPPER LEFT
                    knight_highlight(boardvalue, boardtiles, tileAtX - 2, tileAtY - 1, 10);

                    //LEFT UP
                    knight_highlight(boardvalue, boardtiles, tileAtX - 1, tileAtY - 2, 10);

                    //LEFT DOWN
                    knight_highlight(boardvalue, boardtiles, tileAtX + 1, tileAtY - 2, 10);

                    //RIGHT UP
                    knight_highlight(boardvalue, boardtiles, tileAtX - 1, tileAtY + 2, 10);

                    //RIGHT DOWN
                    knight_highlight(boardvalue, boardtiles, tileAtX + 1, tileAtY + 2, 10);

                    //DOWN RIGHT
                    knight_highlight(boardvalue, boardtiles, tileAtX + 2, tileAtY + 1, 10);

                    //DOWN LEFT
                    knight_highlight(boardvalue, boardtiles, tileAtX + 2, tileAtY - 1, 10);
                }
            }

            // reverting tiles color after bring highlighted
            if (guide == true && onclick == true)
            {

                // player 1 - black
                if (boardvalue[tileAtX, tileAtY] - 10 == 2)
                {
                    //UPPER RIGHT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 2, tileAtY + 1, 20);

                    //UPPER LEFT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 2, tileAtY - 1, 20);

                    //LEFT UP
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 1, tileAtY - 2, 20);

                    //LEFT DOWN
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 1, tileAtY - 2, 20);

                    //RIGHT UP
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 1, tileAtY + 2, 20);

                    //RIGHT DOWN
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 1, tileAtY + 2, 20);

                    //DOWN RIGHT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 2, tileAtY + 1, 20);

                    //DOWN LEFT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 2, tileAtY - 1, 20);
                }

                // player 2 - white
                if (boardvalue[tileAtX, tileAtY] - 20 == 2)
                {
                    //UPPER RIGHT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 2, tileAtY + 1, 10);

                    //UPPER LEFT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 2, tileAtY - 1, 10);

                    //LEFT UP
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 1, tileAtY - 2, 10);

                    //LEFT DOWN
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 1, tileAtY - 2, 10);

                    //RIGHT UP
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 1, tileAtY + 2, 10);

                    //RIGHT DOWN
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 1, tileAtY + 2, 10);

                    //DOWN RIGHT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 2, tileAtY + 1, 10);

                    //DOWN LEFT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 2, tileAtY - 1, 10);
                }
            }

            //movment case
            if (onclick == true && tileAtX != tileToX && tileAtY != tileToY)
            {

                //player 1 value 11~16
                if (boardvalue[tileAtX, tileAtY] - 10 == 2)
                {
                    territory.knight_ChangeTerritory(territory.WhiteTerritory, territory.BlackTerritory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 10, ref kingchecked);
                    knight_move(controlling, undercontrol, territory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 10, ref turn, ref turncounter, ref kingchecked);
                }

                //player 2 value 21~26
                if (boardvalue[tileAtX, tileAtY] - 20 == 2)
                {
                    territory.knight_ChangeTerritory(territory.WhiteTerritory, territory.BlackTerritory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 20, ref kingchecked);
                    knight_move(controlling, undercontrol, territory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 20, ref turn, ref turncounter, ref kingchecked);
                }
            }
        }
    }


        public void bishop(Controlling[,] controlling, UnderControl[,] undercontrol, Territory territory, int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int tileToX, int tileToY, ref int turn, bool onclick, bool guide, ref bool kingChecked, ref int turncounter)
        {
            //check if the piece will expose the king or not
            if(undercontrol[tileToX, tileToY].beforeChangelocation_CheckInvalid(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked))
            {
                MessageBox.Show("invalid move");
            }

            else
            {
                //if onclick == false tile with a bishop was highlighted
                if (guide == true && onclick == false)
                {
                    //PLAYER 1 - Black || 10
                    if (boardvalue[tileAtX, tileAtY] - 10 == 3)
                    {
                        diagonal_highlight(boardvalue, boardtiles, tileAtX, tileAtY, 10, 20);
                    }

                    //PLAYER 2 - white || 20
                    if (boardvalue[tileAtX, tileAtY] - 20 == 3)
                    {
                        diagonal_highlight(boardvalue, boardtiles, tileAtX, tileAtY, 20, 10);
                    }
                }

                // reverting tiles color after bring highlighted
                if (guide == true && onclick == true)
                {
                    //PLAYER 1 - Black || 10
                    if (boardvalue[tileAtX, tileAtY] - 10 == 3)
                    {
                        diagonal_RevertTile(boardvalue, boardtiles, tileAtX, tileAtY, 10, 20);
                    }

                    //PLAYER 2 - white || 20
                    if (boardvalue[tileAtX, tileAtY] - 20 == 3)
                    {
                        diagonal_RevertTile(boardvalue, boardtiles, tileAtX, tileAtY, 20, 10);
                    }
                }

                //movment case
                if (onclick == true && tileAtX != tileToX && tileAtY != tileToY)
                {
                    //player 1 value 11~16
                    if (boardvalue[tileAtX, tileAtY] - 10 == 3)
                    {
                   

                        territory.Diagonal_RemoveCurrent_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY,10);
                        diagonal_move(controlling, undercontrol, territory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 10, 3, ref turn, ref turncounter, ref kingChecked);
                        territory.Diagonal_UpdateCurrent_Territory(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory,tileToX, tileToY, 10, ref kingChecked);
                    }

                    //player 2 value 21~26
                    if (boardvalue[tileAtX, tileAtY] - 20 == 3)
                    {

                        territory.Diagonal_RemoveCurrent_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, 20);
                        diagonal_move(controlling, undercontrol, territory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 20, 3, ref turn, ref turncounter, ref kingChecked);
                        territory.Diagonal_UpdateCurrent_Territory(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, 20, ref kingChecked);
                    }
                }
            }
        }

        public void rook(Controlling[,] controlling, UnderControl[,] undercontrol, Territory territory, bool[] castlingWhite, bool[] castlingBlack, int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int tileToX, int tileToY, ref int turn, bool onclick, bool guide, ref bool kingChecked, ref int turncounter)
        {
            //check if the piece will expose the king or not
            if (undercontrol[tileToX, tileToY].beforeChangelocation_CheckInvalid(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked))
            {
                MessageBox.Show("invalid move");
            }
            else
            {

                //onclick == false highlight available moves
                if (onclick == false && guide == true)
                {
                    //player 1 - black 
                    if (boardvalue[tileAtX, tileAtY] - 10 == 4)
                    {
                        HorizontalVertical_highlight(boardvalue, boardtiles, tileAtX, tileAtY, 10, 20);
                    }
                    else if (boardvalue[tileAtX, tileAtY] - 20 == 4)
                    {
                        HorizontalVertical_highlight(boardvalue, boardtiles, tileAtX, tileAtY, 20, 10);
                    }
                }

                // reverting tiles color after bring highlighted
                if (guide == true && onclick == true)
                {
                    //PLAYER 1 - Black || 10
                    if (boardvalue[tileAtX, tileAtY] - 10 == 4)
                    {
                        horizontalVertical_RevertTile(boardvalue, boardtiles, tileAtX, tileAtY, 10, 20);
                    }

                    //PLAYER 2 - white || 20
                    if (boardvalue[tileAtX, tileAtY] - 20 == 4)
                    {
                        horizontalVertical_RevertTile(boardvalue, boardtiles, tileAtX, tileAtY, 20, 10);
                    }
                }

                //onclick == true and the At location differ from the To location
                if (onclick == true && tileAtX == tileToX && tileAtY != tileToY || tileAtY == tileToY && tileAtX != tileToX)
                {
                    //player 1 value 11~16
                    if (boardvalue[tileAtX, tileAtY] - 10 == 4)
                    {
                        //remove piece territory
                        territory.straight__RemovePiece_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, 10);
                        //check if the new location is valid and on valid move update piece location
                        horizonalVertical_move(controlling, undercontrol, territory, castlingWhite, castlingBlack, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 10, 4, ref turn, ref turncounter, ref kingChecked);
                        //update piece territory after changing location
                        territory.straight_UpdatePiece_Territory(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, 10, ref kingChecked);
                    }

                    //player 2 value 21~26
                    if (boardvalue[tileAtX, tileAtY] - 20 == 4)
                    {
                        //remove piece territory
                        territory.straight__RemovePiece_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, 20);
                        //check if the new location is valid and on valid move update piece location
                        horizonalVertical_move(controlling, undercontrol, territory, castlingWhite, castlingBlack, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 20, 4, ref turn, ref turncounter, ref kingChecked);
                        //update piece territory after changing location
                        territory.straight_UpdatePiece_Territory(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, 20, ref kingChecked);
                    }
                }
            }
        }

        // queen function uses the diagonal + horizontalvertical movement
        public void queen(Controlling[,] controlling, UnderControl[,] undercontrol, Territory territory, bool[] castlingWhite, bool[] castlingBlack, int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int tileToX, int tileToY, ref int turn, bool onclick, bool guide, ref bool kingChecked, ref int turncounter)
        {
            //check if the piece will expose the king or not
            if(undercontrol[tileToX, tileToY].beforeChangelocation_CheckInvalid(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked))
            {
                MessageBox.Show("invalid move");
            }
            else
            {
                //onclick == false highlight available moves
                if (onclick == false && guide == true)
                {
                    //player 1 - black 
                    if (boardvalue[tileAtX, tileAtY] - 10 == 5)
                    {
                        diagonal_highlight(boardvalue, boardtiles, tileAtX, tileAtY, 10, 20);
                        HorizontalVertical_highlight(boardvalue, boardtiles, tileAtX, tileAtY, 10, 20);
                    }

                    //player 2 - white
                    else if (boardvalue[tileAtX, tileAtY] - 20 == 5)
                    {
                        diagonal_highlight(boardvalue, boardtiles, tileAtX, tileAtY, 20, 10);
                        HorizontalVertical_highlight(boardvalue, boardtiles, tileAtX, tileAtY, 20, 10);
                    }
                }

                // reverting tiles color after bring highlighted
                if (guide == true && onclick == true)
                {
                    //PLAYER 1 - Black || 10
                    if (boardvalue[tileAtX, tileAtY] - 10 == 5)
                    {
                        diagonal_RevertTile(boardvalue, boardtiles, tileAtX, tileAtY, 10, 20);
                        horizontalVertical_RevertTile(boardvalue, boardtiles, tileAtX, tileAtY, 10, 20);
                    }

                    //PLAYER 2 - white || 20
                    if (boardvalue[tileAtX, tileAtY] - 20 == 5)
                    {
                        diagonal_RevertTile(boardvalue, boardtiles, tileAtX, tileAtY, 20, 10);
                        horizontalVertical_RevertTile(boardvalue, boardtiles, tileAtX, tileAtY, 20, 10);
                    }
                }

                //onclick == true and the At location differ from the To location
                if (onclick == true )
                {
                    //player 1 value 11~16
                    if (boardvalue[tileAtX, tileAtY] - 10 == 5)
                    {
                        //remove piece territory
                        territory.Diagonal_RemoveCurrent_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, 10);
                        //check if the new location is valid and on valid move update piece location
                        diagonal_move(controlling, undercontrol, territory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 10, 5, ref turn, ref turncounter, ref kingChecked);
                        //update piece territory after changing location
                        territory.Diagonal_UpdateCurrent_Territory(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, 10, ref kingChecked);

                        //remove piece territory
                        territory.straight__RemovePiece_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, 10);
                        //check if the new location is valid and on valid move update piece location
                        horizonalVertical_move(controlling, undercontrol, territory, castlingWhite, castlingBlack, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 10, 5, ref turn, ref turncounter, ref kingChecked);
                        //update piece territory after changing location
                        territory.straight_UpdatePiece_Territory(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, 10, ref kingChecked);
                    }               
                
                
                    //player 2 value 21~26
                    if (boardvalue[tileAtX, tileAtY] - 20 == 5)
                    {

                        territory.Diagonal_RemoveCurrent_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, 20);
                        //check if the new location is valid and on valid move update piece location
                        diagonal_move(controlling, undercontrol, territory, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 20, 5, ref turn, ref turncounter, ref kingChecked);
                        //update piece territory after changing location
                        territory.Diagonal_UpdateCurrent_Territory(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, 20, ref kingChecked);

                        //remove piece territory
                        territory.straight__RemovePiece_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, 20);
                        //check if the new location is valid and on valid move update piece location
                        horizonalVertical_move(controlling, undercontrol, territory, castlingWhite, castlingBlack, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 20, 5, ref turn, ref turncounter, ref kingChecked);
                        //update piece territory after changing location
                        territory.straight_UpdatePiece_Territory(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, 20, ref kingChecked);
                    }
                }
            }
        }
        
            
        public void king(bool []castlingWhite, bool []castlingBlack, Controlling[,] controlling, UnderControl[,] undercontrol, Territory territory, int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int tileToX, int tileToY, ref int turn, bool onclick, bool guide ,ref bool kingChecked, ref int turncounter)
        {
          
            //if onclick == false tile with knight was highlighted
            if (guide == true && onclick == false)
            {

                //player 1 - black
                if (boardvalue[tileAtX, tileAtY] - 10 == 6)
                {
                    
                    //UP
                    King_highlight(territory, boardvalue, boardtiles, tileAtX - 1, tileAtY, 10, 20);

                    //UPPER RIGHT
                    King_highlight(territory, boardvalue, boardtiles, tileAtX - 1, tileAtY + 1, 10, 20);

                    //UPPER LEFT
                    King_highlight(territory, boardvalue, boardtiles, tileAtX - 1, tileAtY - 1, 10, 20);

                    //LEFT 
                    King_highlight(territory, boardvalue, boardtiles, tileAtX, tileAtY - 1, 10, 20);

                    //RIGHT
                    King_highlight(territory, boardvalue, boardtiles, tileAtX, tileAtY + 1, 10, 20);

                    //DOWN
                    King_highlight(territory, boardvalue, boardtiles, tileAtX + 1, tileAtY, 10, 20);

                    //DOWN RIGHT
                    King_highlight(territory, boardvalue, boardtiles, tileAtX + 1, tileAtY + 1, 10, 20);

                    //DOWN LEFT
                    King_highlight(territory, boardvalue, boardtiles, tileAtX + 1, tileAtY - 1, 10, 20);

                    //Castling
                    if (castlingBlack[0] == true && territory.WhiteTerritory[tileAtX, tileAtY] == 0) //the king hasn't moved and is not in check
                    {
                        //Right Rook Black
                        if (castlingBlack[1] == true && boardvalue[7, 4] == 0 && territory.WhiteTerritory[7, 4] == 0 //the tile is not under control
                            && boardvalue[7, 5] == 0 && territory.WhiteTerritory[7, 5] == 0
                            && boardvalue[7, 6] == 0 && territory.WhiteTerritory[7, 6] == 0)
                        {
                            King_highlight(territory, boardvalue, boardtiles, 7, 6, 10, 20);
                        }

                        //Left Rook Black
                        if (castlingBlack[2] == true && boardvalue[7, 2] == 0 && territory.WhiteTerritory[7, 2] == 0 //the tile is not under control
                            && boardvalue[7, 1] == 0 && territory.WhiteTerritory[7, 1] == 0)
                        {
                            King_highlight(territory, boardvalue, boardtiles, 7, 1, 10, 20);
                        }
                    }
                }


                //player 2 - white
                if (boardvalue[tileAtX, tileAtY] - 20 == 6)
                {
                    //UP
                    King_highlight(territory, boardvalue, boardtiles, tileAtX - 1, tileAtY, 20, 10);

                    //UPPER RIGHT
                    King_highlight(territory, boardvalue, boardtiles, tileAtX - 1, tileAtY + 1, 20, 10);

                    //UPPER LEFT
                    King_highlight(territory, boardvalue, boardtiles, tileAtX - 1, tileAtY - 1, 20, 10);

                    //LEFT 
                    King_highlight(territory, boardvalue, boardtiles, tileAtX, tileAtY - 1, 20, 10);

                    //RIGHT
                    King_highlight(territory, boardvalue, boardtiles, tileAtX, tileAtY + 1, 20, 10);

                    //DOWN
                    King_highlight(territory, boardvalue, boardtiles, tileAtX + 1, tileAtY, 20, 10);

                    //DOWN RIGHT
                    King_highlight(territory, boardvalue, boardtiles, tileAtX + 1, tileAtY + 1, 20, 10);

                    //DOWN LEFT
                    King_highlight(territory, boardvalue, boardtiles, tileAtX + 1, tileAtY - 1, 20, 10);


                    //Castling
                    if (castlingWhite[0] == true && territory.BlackTerritory[tileAtX, tileAtY] == 0) //the king hasn't moved and is not in check
                    {
                        //Right Rook Black
                        if (castlingWhite[1] == true && boardvalue[0, 4] == 0 && territory.BlackTerritory[0, 4] == 0 //the tile is not under control
                            && boardvalue[0, 5] == 0 && territory.BlackTerritory[0, 5] == 0
                            && boardvalue[0, 6] == 0 && territory.BlackTerritory[0, 6] == 0)
                        {
                            King_highlight(territory, boardvalue, boardtiles, 0, 6, 20, 10);
                        }

                        //Left Rook Black
                        if (castlingWhite[2] == true && boardvalue[0, 2] == 0 && territory.BlackTerritory[0, 2] == 0 //the tile is not under control
                            && boardvalue[0, 1] == 0 && territory.BlackTerritory[0, 1] == 0)
                        {
                            King_highlight(territory, boardvalue, boardtiles, 0, 1, 20, 10);
                        }
                    }
                }
            }



            // reverting tiles color after bring highlighted
            if (guide == true && onclick == true)
            {
                //player 1 - black
                if (boardvalue[tileAtX, tileAtY] - 10 == 6)
                {
                    //UP
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 1, tileAtY, 20);

                    //UPPER RIGHT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 1, tileAtY + 1, 20);

                    //UPPER LEFT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 1, tileAtY - 1, 20);

                    //LEFT 
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX, tileAtY - 1, 20);

                    //RIGHT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX, tileAtY + 1, 20);

                    //DOWN
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 1, tileAtY, 20);

                    //DOWN RIGHT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 1, tileAtY + 1, 20);

                    //DOWN LEFT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 1, tileAtY - 1, 20);

                    //castling
                    //right castle
                    if (boardtiles[7, 6].BackColor == Color.FromArgb(200, Color.LimeGreen))
                    {
                        kingknight_RevertTile(boardvalue, boardtiles, 7, 6, 20);
                    }

                    //left castle
                    if (boardtiles[7, 6].BackColor == Color.FromArgb(200, Color.LimeGreen))
                    {
                        kingknight_RevertTile(boardvalue, boardtiles, 7, 6, 20);
                    }
                }


                //player 2 - white
                if (boardvalue[tileAtX, tileAtY] - 20 == 6)
                {
                    //UP
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 1, tileAtY, 10);

                    //UPPER RIGHT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 1, tileAtY + 1, 10);

                    //UPPER LEFT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX - 1, tileAtY - 1, 10);

                    //LEFT 
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX, tileAtY - 1, 10);

                    //RIGHT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX, tileAtY + 1, 10);

                    //DOWN
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 1, tileAtY, 10);

                    //DOWN RIGHT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 1, tileAtY + 1, 10);

                    //DOWN LEFT
                    kingknight_RevertTile(boardvalue, boardtiles, tileAtX + 1, tileAtY - 1, 10);

                    //castling
                    //right castle
                    if (boardtiles[0, 6].BackColor == Color.FromArgb(200, Color.LimeGreen))
                    {
                        kingknight_RevertTile(boardvalue, boardtiles, 0, 6, 20);
                    }

                    //left castle
                    if (boardtiles[0, 1].BackColor == Color.FromArgb(200, Color.LimeGreen))
                    {
                        kingknight_RevertTile(boardvalue, boardtiles, 0, 1, 20);
                    }
                }
            }

            //movment case
            if (onclick == true)
            {
                //determine weather the piece is black or white
                int PieceValue = (boardvalue[tileAtX, tileAtY] - 10 == 6)? 10 : 20;

                // if true process the piece location
                if (IsValid_King(territory, boardvalue, tileToX, tileToY, PieceValue))
                {

                    //player 1 value 11~16
                    if (boardvalue[tileAtX, tileAtY] - 10 == 6)
                    {
                        territory.King_ChangeTerritory(territory.WhiteTerritory, territory.BlackTerritory,boardvalue,boardtiles, tileAtX, tileAtY, tileToX, tileToY, 10, ref kingChecked);
                        king_move(controlling, undercontrol, territory, castlingWhite, castlingBlack, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 10, ref turn, ref kingChecked, ref turncounter);
                    }

                    //player 2 value 21~26
                    if (boardvalue[tileAtX, tileAtY] - 20 == 6)
                    {
                        territory.King_ChangeTerritory(territory.WhiteTerritory, territory.BlackTerritory,boardvalue,boardtiles, tileAtX, tileAtY, tileToX, tileToY, 20,ref kingChecked);
                        king_move(controlling, undercontrol, territory, castlingWhite, castlingBlack, boardvalue, boardtiles, tileAtX, tileAtY, tileToX, tileToY, 20, ref turn, ref kingChecked, ref turncounter);
                    }
                }

                //if the move is Invalid: change the tileTo color to red
                //informing the player that the move is illegal
                else
                {

                }
                

            }
        }


        //green highlight

        //piece color == 10 for black enemy and == 20 for white enemy

        public void knight_highlight(int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int pieceColor)
        {
            //exception out of bound handle
            if (tileAtX >= 0 && tileAtX <= 7 && tileAtY >= 0 && tileAtY <= 7)
            {
                //if the tile is empty or has an enemy
                if (boardvalue[tileAtX, tileAtY] - pieceColor > 0 && boardvalue[tileAtX, tileAtY] - pieceColor < 10 || boardvalue[tileAtX, tileAtY] == 0)
                {
                    boardtiles[tileAtX, tileAtY].BackColor = Color.FromArgb(200, Color.LimeGreen);
                }
            }
        }

        // tile highlighting for diagonal moving pieces - Bishop and Queen
        public void diagonal_highlight(int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int pieceColor, int enemyColor)
        {
            int i = 1; // i is used to roll through the x and y locations
            bool flagUR = true; // flags used to stop the highlighting if a piece is encountered
            bool flagUL = true;
            bool flagDR = true;
            bool flagDL = true;

            // all the tiles are checked by level for preformance
            while (flagUR == true || flagUL == true || flagDR == true || flagDL == true)
            {

                //UPPER RIGHT side || -,+
                if (flagUR)
                {
                    ////exception out of bound handle
                    if (tileAtX - i >= 0 && tileAtX - i <= 7 && tileAtY + i >= 0 && tileAtY + i <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX - i, tileAtY + i] - pieceColor > 0 && boardvalue[tileAtX - i, tileAtY + i] - pieceColor < 10)
                        {
                            flagUR = false;
                        }
                        else
                        {
                            boardtiles[tileAtX - i, tileAtY + i].BackColor = Color.FromArgb(200, Color.LimeGreen);

                            if (boardvalue[tileAtX - i, tileAtY + i] - enemyColor > 0 && boardvalue[tileAtX - i, tileAtY + i] - enemyColor < 10)
                            {
                                flagUR = false;
                            }
                        }

                    }
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
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX - i, tileAtY - i] - pieceColor > 0 && boardvalue[tileAtX - i, tileAtY - i] - pieceColor < 10)
                        {
                            flagUL = false;
                        }

                        else
                        {
                            boardtiles[tileAtX - i, tileAtY - i].BackColor = Color.FromArgb(200, Color.LimeGreen);

                            if (boardvalue[tileAtX - i, tileAtY - i] - enemyColor > 0 && boardvalue[tileAtX - i, tileAtY - i] - enemyColor < 10)
                            {
                                flagUL = false;
                            }
                        }
                    }
                    else
                    {
                        flagUL = false;
                    }
                }

                //DOWN RIGHT side || +,-
                if (flagDR)
                {
                    ////exception out of bound handle
                    if (tileAtX + i >= 0 && tileAtX + i <= 7 && tileAtY - i >= 0 && tileAtY - i <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX + i, tileAtY - i] - pieceColor > 0 && boardvalue[tileAtX + i, tileAtY - i] - pieceColor < 10)
                        {
                            flagDR = false;
                        }

                        else
                        {
                            boardtiles[tileAtX + i, tileAtY - i].BackColor = Color.FromArgb(200, Color.LimeGreen);

                            if (boardvalue[tileAtX + i, tileAtY - i] - enemyColor > 0 && boardvalue[tileAtX + i, tileAtY - i] - enemyColor < 10)
                            {
                                flagDR = false;
                            }
                        }
                    }

                    else
                    {
                        flagDR = false;
                    }
                }

                //DOWN LEFT side || +,+
                if (flagDL)
                {
                    ////exception out of bound handle
                    if (tileAtX + i >= 0 && tileAtX + i <= 7 && tileAtY + i >= 0 && tileAtY + i <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX + i, tileAtY + i] - pieceColor > 0 && boardvalue[tileAtX + i, tileAtY + i] - pieceColor < 10)
                        {
                            flagDL = false;
                        }

                        else
                        {
                            boardtiles[tileAtX + i, tileAtY + i].BackColor = Color.FromArgb(200, Color.LimeGreen);

                            if (boardvalue[tileAtX + i, tileAtY + i] - enemyColor > 0 && boardvalue[tileAtX + i, tileAtY + i] - enemyColor < 10)
                            {
                                flagDL = false;
                            }
                        }
                    }

                    else
                    {
                        flagDL = false;
                    }
                }


                i++;
            }
        }


        public void HorizontalVertical_highlight(int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int pieceColor, int enemyColor)
        {
            int i = 1; // i is used to roll through the x and y locations
            bool flagU = true; // flags used to stop the highlighting if a piece is encountered
            bool flagR = true;
            bool flagD = true;
            bool flagL = true;

            while (flagU == true || flagR == true || flagD == true || flagL == true)
            {

                //UPPER side || -,0
                if (flagU)
                {
                    ////exception out of bound handle
                    if (tileAtX - i >= 0 && tileAtX - i <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX - i, tileAtY] - pieceColor > 0 && boardvalue[tileAtX - i, tileAtY] - pieceColor < 10)
                        {
                            flagU = false;
                        }
                        else
                        {
                            boardtiles[tileAtX - i, tileAtY].BackColor = Color.FromArgb(200, Color.LimeGreen);

                            if (boardvalue[tileAtX - i, tileAtY] - enemyColor > 0 && boardvalue[tileAtX - i, tileAtY] - enemyColor < 10)
                            {
                                flagU = false;
                            }
                        }

                    }
                    else
                    {
                        flagU = false;
                    }
                }

                //RIGHT side || 0,+
                if (flagR)
                {
                    ////exception out of bound handle
                    if (tileAtX >= 0 && tileAtX <= 7 && tileAtY + i >= 0 && tileAtY + i <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX, tileAtY + i] - pieceColor > 0 && boardvalue[tileAtX, tileAtY + i] - pieceColor < 10)
                        {
                            flagR = false;
                        }

                        else
                        {
                            boardtiles[tileAtX, tileAtY + i].BackColor = Color.FromArgb(200, Color.LimeGreen);

                            if (boardvalue[tileAtX, tileAtY + i] - enemyColor > 0 && boardvalue[tileAtX, tileAtY + i] - enemyColor < 10)
                            {
                                flagR = false;
                            }
                        }
                    }
                    else
                    {
                        flagR = false;
                    }
                }

                //DOWN side || +,0
                if (flagD)
                {
                    ////exception out of bound handle
                    if (tileAtX + i >= 0 && tileAtX + i <= 7 && tileAtY >= 0 && tileAtY <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX + i, tileAtY] - pieceColor > 0 && boardvalue[tileAtX + i, tileAtY] - pieceColor < 10)
                        {
                            flagD = false;
                        }

                        else
                        {
                            boardtiles[tileAtX + i, tileAtY].BackColor = Color.FromArgb(200, Color.LimeGreen);

                            if (boardvalue[tileAtX + i, tileAtY] - enemyColor > 0 && boardvalue[tileAtX + i, tileAtY] - enemyColor < 10)
                            {
                                flagD = false;
                            }
                        }
                    }

                    else
                    {
                        flagD = false;
                    }
                }

                //LEFT side || 0,-
                if (flagL)
                {
                    ////exception out of bound handle
                    if (tileAtX >= 0 && tileAtX <= 7 && tileAtY - i >= 0 && tileAtY - i <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX, tileAtY - i] - pieceColor > 0 && boardvalue[tileAtX, tileAtY - i] - pieceColor < 10)
                        {
                            flagL = false;
                        }

                        else
                        {
                            boardtiles[tileAtX, tileAtY - i].BackColor = Color.FromArgb(200, Color.LimeGreen);

                            if (boardvalue[tileAtX, tileAtY - i] - enemyColor > 0 && boardvalue[tileAtX, tileAtY - i] - enemyColor < 10)
                            {
                                flagL = false;
                            }
                        }
                    }

                    else
                    {
                        flagL = false;
                    }
                }


                i++;
            }
        }



        public void King_highlight(Territory territory, int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int piecevalue, int pieceColor)
        {
            
            //exception out of bound handle
            if (tileAtX >= 0 && tileAtX <= 7 && tileAtY >= 0 && tileAtY <= 7)
            {
                //if the tile was undercontrol
            if (piecevalue == 10 && territory.WhiteTerritory[tileAtX, tileAtY] > 0 || piecevalue == 20 && territory.BlackTerritory[tileAtX, tileAtY] > 0)
            {
                return;
            }
                //if the tile is empty or has an enemy
                if (boardvalue[tileAtX, tileAtY] - pieceColor > 0 && boardvalue[tileAtX, tileAtY] - pieceColor < 10 || boardvalue[tileAtX, tileAtY] == 0)
                {
                    boardtiles[tileAtX, tileAtY].BackColor = Color.FromArgb(200, Color.LimeGreen);
                }
            }
        }




        //black and white

        //Revert colors to Black or White after being highlighted
        public void kingknight_RevertTile(int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int enemyValue) //enemy value is the piece value + color value
        {
            Color color1 = Color.White;
            Color color2 = Color.Black;
            //exception out of bound handle
            if (tileAtX >= 0 && tileAtX <= 7 && tileAtY >= 0 && tileAtY <= 7)
            {
                if (boardvalue[tileAtX, tileAtY] == 0 || boardvalue[tileAtX, tileAtY] - enemyValue > 0 && boardvalue[tileAtX, tileAtY] - enemyValue < 10)
                {
                    boardtiles[tileAtX, tileAtY].BackColor = ((tileAtX) % 2 == 0 && tileAtY % 2 == 0 || (tileAtX) % 2 == 1 && tileAtY % 2 == 1) ? color1 : color2;
                }
            }
        }


        // tile highlighting for diagonal moving pieces - Bishop and Queen
        public void diagonal_RevertTile(int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int pieceColor, int enemyColor)
        {
            int i = 1; // i is used to roll through the x and y locations
            bool flagUR = true; // flags used to stop the highlighting if a piece is encountered
            bool flagUL = true;
            bool flagDR = true;
            bool flagDL = true;

            Color color1 = Color.White;
            Color color2 = Color.Black;
            while (flagUR == true || flagUL == true || flagDR == true || flagDL == true)
            {

                //UPPER RIGHT side || -,+
                if (flagUR)
                {
                    ////exception out of bound handle
                    if (tileAtX - i >= 0 && tileAtX - i <= 7 && tileAtY + i >= 0 && tileAtY + i <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX - i, tileAtY + i] - pieceColor > 0 && boardvalue[tileAtX - i, tileAtY + i] - pieceColor < 10)
                        {
                            flagUR = false;
                        }

                        else
                        {
                            boardtiles[tileAtX - i, tileAtY + i].BackColor = ((tileAtX - i) % 2 == 0 && (tileAtY + i) % 2 == 0 || (tileAtX - i) % 2 == 1 && (tileAtY + i) % 2 == 1) ? color1 : color2;

                            if (boardvalue[tileAtX - i, tileAtY + i] - enemyColor > 0 && boardvalue[tileAtX - i, tileAtY + i] - enemyColor < 10)
                            {
                                flagUR = false;
                            }
                        }
                    }
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
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX - i, tileAtY - i] - pieceColor > 0 && boardvalue[tileAtX - i, tileAtY - i] - pieceColor < 10)
                        {
                            flagUL = false;
                        }

                        else
                        {
                            boardtiles[tileAtX - i, tileAtY - i].BackColor = ((tileAtX - i) % 2 == 0 && (tileAtY - i) % 2 == 0 || (tileAtX - i) % 2 == 1 && (tileAtY - i) % 2 == 1) ? color1 : color2;

                            //an enemy encountered on a highlighted tile
                            if (boardvalue[tileAtX - i, tileAtY - i] - enemyColor > 0 && boardvalue[tileAtX - i, tileAtY - i] - enemyColor < 10)
                            {
                                flagUL = false;
                            }
                        }
                    }
                    else
                    {
                        flagUL = false;
                    }
                }

                //DOWN RIGHT side || +,-
                if (flagDR)
                {
                    ////exception out of bound handle
                    if (tileAtX + i >= 0 && tileAtX + i <= 7 && tileAtY - i >= 0 && tileAtY - i <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX + i, tileAtY - i] - pieceColor > 0 && boardvalue[tileAtX + i, tileAtY - i] - pieceColor < 10)
                        {
                            flagDR = false;
                        }

                        else
                        {
                            boardtiles[tileAtX + i, tileAtY - i].BackColor = ((tileAtX + i) % 2 == 0 && (tileAtY - i) % 2 == 0 || (tileAtX + i) % 2 == 1 && (tileAtY - i) % 2 == 1) ? color1 : color2;

                            if (boardvalue[tileAtX + i, tileAtY - i] - enemyColor > 0 && boardvalue[tileAtX + i, tileAtY - i] - enemyColor < 10)
                            {
                                flagDR = false;
                            }
                        }
                    }
                    else
                    {
                        flagDR = false;
                    }
                }

                //DOWN LEFT side || +,+
                if (flagDL)
                {
                    ////exception out of bound handle
                    if (tileAtX + i >= 0 && tileAtX + i <= 7 && tileAtY + i >= 0 && tileAtY + i <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX + i, tileAtY + i] - pieceColor > 0 && boardvalue[tileAtX + i, tileAtY + i] - pieceColor < 10)
                        {
                            flagDL = false;
                        }

                        else
                        {
                            boardtiles[tileAtX + i, tileAtY + i].BackColor = ((tileAtX + i) % 2 == 0 && (tileAtY + i) % 2 == 0 || (tileAtX + i) % 2 == 1 && (tileAtY + i) % 2 == 1) ? color1 : color2;

                            if (boardvalue[tileAtX + i, tileAtY + i] - enemyColor > 0 && boardvalue[tileAtX + i, tileAtY + i] - enemyColor < 10)
                            {
                                flagDL = false;
                            }
                        }
                    }
                    else
                    {
                        flagDL = false;
                    }
                }

                i++;
            }
        }



        // tile highlighting for diagonal moving pieces - Bishop and Queen
        public void horizontalVertical_RevertTile(int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int pieceColor, int enemyColor)
        {
            int i = 1; // i is used to roll through the x and y locations
            bool flagU = true; // flags used to stop the highlighting if a piece is encountered
            bool flagR = true;
            bool flagD = true;
            bool flagL = true;

            Color color1 = Color.White;
            Color color2 = Color.Black;
            while (flagU == true || flagR == true || flagD == true || flagL == true)
            {

                //UPPER side || -,0
                if (flagU)
                {
                    ////exception out of bound handle
                    if (tileAtX - i >= 0 && tileAtX - i <= 7 && tileAtY >= 0 && tileAtY <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX - i, tileAtY] - pieceColor > 0 && boardvalue[tileAtX - i, tileAtY] - pieceColor < 10)
                        {
                            flagU = false;
                        }

                        else
                        {
                            boardtiles[tileAtX - i, tileAtY].BackColor = ((tileAtX - i) % 2 == 0 && (tileAtY) % 2 == 0 || (tileAtX - i) % 2 == 1 && (tileAtY) % 2 == 1) ? color1 : color2;

                            if (boardvalue[tileAtX - i, tileAtY] - enemyColor > 0 && boardvalue[tileAtX - i, tileAtY] - enemyColor < 10)
                            {
                                flagU = false;
                            }
                        }
                    }
                    else
                    {
                        flagU = false;
                    }
                }

                //Right side || 0,+
                if (flagR)
                {
                    ////exception out of bound handle
                    if (tileAtX >= 0 && tileAtX <= 7 && tileAtY + i >= 0 && tileAtY + i <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX, tileAtY + i] - pieceColor > 0 && boardvalue[tileAtX, tileAtY + i] - pieceColor < 10)
                        {
                            flagR = false;
                        }

                        else
                        {
                            boardtiles[tileAtX, tileAtY + i].BackColor = ((tileAtX) % 2 == 0 && (tileAtY + i) % 2 == 0 || (tileAtX) % 2 == 1 && (tileAtY + i) % 2 == 1) ? color1 : color2;

                            //an enemy encountered on a highlighted tile
                            if (boardvalue[tileAtX, tileAtY + i] - enemyColor > 0 && boardvalue[tileAtX, tileAtY + i] - enemyColor < 10)
                            {
                                flagR = false;
                            }
                        }
                    }
                    else
                    {
                        flagR = false;
                    }
                }

                //DOWN side || +,0
                if (flagD)
                {
                    ////exception out of bound handle
                    if (tileAtX + i >= 0 && tileAtX + i <= 7 && tileAtY >= 0 && tileAtY <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX + i, tileAtY] - pieceColor > 0 && boardvalue[tileAtX + i, tileAtY] - pieceColor < 10)
                        {
                            flagD = false;
                        }

                        else
                        {
                            boardtiles[tileAtX + i, tileAtY].BackColor = ((tileAtX + i) % 2 == 0 && (tileAtY) % 2 == 0 || (tileAtX + i) % 2 == 1 && (tileAtY) % 2 == 1) ? color1 : color2;

                            if (boardvalue[tileAtX + i, tileAtY] - enemyColor > 0 && boardvalue[tileAtX + i, tileAtY] - enemyColor < 10)
                            {
                                flagD = false;
                            }
                        }
                    }
                    else
                    {
                        flagD = false;
                    }
                }

                //LEFT side || 0,-
                if (flagL)
                {
                    ////exception out of bound handle
                    if (tileAtX >= 0 && tileAtX <= 7 && tileAtY - i >= 0 && tileAtY - i <= 7)
                    {
                        // if the tile has a piece of the same color stop brfore it
                        if (boardvalue[tileAtX, tileAtY - i] - pieceColor > 0 && boardvalue[tileAtX, tileAtY - i] - pieceColor < 10)
                        {
                            flagL = false;
                        }

                        else
                        {
                            boardtiles[tileAtX, tileAtY - i].BackColor = ((tileAtX) % 2 == 0 && (tileAtY - i) % 2 == 0 || (tileAtX) % 2 == 1 && (tileAtY - i) % 2 == 1) ? color1 : color2;

                            if (boardvalue[tileAtX, tileAtY - i] - enemyColor > 0 && boardvalue[tileAtX, tileAtY - i] - enemyColor < 10)
                            {
                                flagL = false;
                            }
                        }
                    }
                    else
                    {
                        flagL = false;
                    }
                }

                i++;
            }
        }


        //movment processing knight
        public void knight_move(Controlling[,] controlling, UnderControl[,] undercontrol, Territory territory, int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int tileToX, int tileToY, int pieceValue, ref int turn, ref int turncounter, ref bool kingChecked) //we use piece value here to elemeinat the same colored pieces so we wont move over them
        {
            //if NOT the totile is of the same side
            if (!(boardvalue[tileToX, tileToY] - pieceValue < 10 && boardvalue[tileToX, tileToY] - pieceValue >= 0))
            {

                if (Math.Abs(tileAtX - tileToX) == 2 && Math.Abs(tileAtY - tileToY) == 1 || Math.Abs(tileAtX - tileToX) == 1 && Math.Abs(tileAtY - tileToY) == 2) // conditon to check the L pattern
                {
                    //if the Totile had an enemy, remove the captured enemy territory
                    if (boardvalue[tileToX, tileToY] != 0)
                    {
                        undercontrol[tileToX, tileToY].onChangeLocation_CaptureEnemyPawn(controlling, undercontrol, territory, boardvalue, tileToX, tileToY,  (pieceValue == 10)? 20 : 10);
                    }

                    //delete and remove territory off the tiles lying after the ToTile
                    undercontrol[tileToX, tileToY].onChangeLocation_RevertTerritory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY);
                    //if a pawn is under territory of an ally pawn, check after moving if the king will be checked
                    undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked);

                    boardvalue[tileAtX, tileAtY] = 0;
                    boardvalue[tileToX, tileToY] = pieceValue + 2;

                    
                    //update the tiles lying after the ATtile to extend the controlling pieces territory
                    undercontrol[tileToX, tileToY].onChangeLocation_continue(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY);
                    undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, ref kingChecked);
                    

                    turn = (turn % 2 == 1) ? 2 : 1;
                    turncounter = turncounter + 1;
                    saveStatus(boardvalue, territory, ref turncounter);
                }
            }
        }

        //function for diagonal movment used for bishops and queens
        //piece value is the value of piece, EX: queen == 5 bishop == 4.
        public void diagonal_move(Controlling [,] controlling, UnderControl [,] undercontrol, Territory territory, int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int tileToX, int tileToY, int pieceColor, int pieceValue,ref int turn, ref int turncounter, ref bool kingChecked)
        {
            // pieceColor is the color value. Ex: black == 10 and white == 20, 
            if (!(boardvalue[tileToX, tileToY] - pieceColor < 10 && boardvalue[tileToX, tileToY] - pieceColor >= 0))
            {
                //if the diffrence between the piece location X and the go to location X  == the piece location Y and the go to location Y: the movment is a diagonal
                if (Math.Abs(tileAtX - tileToX) == Math.Abs(tileAtY - tileToY))
                {
                    bool flag = true;
                    for (int i = 1; i < Math.Abs(tileAtX - tileToX); i++) //check all the tiles between the At location and the To location if a piece was inbetween dont move
                    {
                        // UPPER Right || -,+
                        if (tileAtX > tileToX && tileAtY < tileToY)
                        {
                            ////exception out of bound handle
                            if (tileAtX - i >= 0 && tileAtX - i <= 7 && tileAtY + i >= 0 && tileAtY + i <= 7)
                            {
                                if (boardvalue[tileAtX - i, tileAtY + i] > 0) // a piece  was incountered midway.
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }

                        // UPPER Left || -,-
                        if (tileAtX > tileToX && tileAtY > tileToY)
                        {
                            ////exception out of bound handle
                            if (tileAtX - i >= 0 && tileAtX - i <= 7 && tileAtY - i >= 0 && tileAtY - i <= 7)
                            {
                                if (boardvalue[tileAtX - i, tileAtY - i] > 0) // a piece  was incountered midway.
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }

                        // DOWN Left || +,-
                        if (tileAtX < tileToX && tileAtY > tileToY)
                        {
                            ////exception out of bound handle
                            if (tileAtX + i >= 0 && tileAtX + i <= 7 && tileAtY - i >= 0 && tileAtY - i <= 7)
                            {
                                if (boardvalue[tileAtX + i, tileAtY - i] > 0) // a piece  was incountered midway
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }

                        // DOWN Right || +,+
                        if (tileAtX < tileToX && tileAtY < tileToY)
                        {
                            ////exception out of bound handle
                            if (tileAtX + i >= 0 && tileAtX + i <= 7 && tileAtY + i >= 0 && tileAtY + i <= 7)
                            {
                                if (boardvalue[tileAtX + i, tileAtY + i] > 0) // a piece  was incountered midway
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                    }

                    if (flag == true) 
                    {
                        //if the Totile had an enemy, remove the captured enemy territory
                        if (boardvalue[tileToX, tileToY] != 0)
                        {
                            undercontrol[tileToX, tileToY].onChangeLocation_CaptureEnemyPawn(controlling, undercontrol, territory, boardvalue, tileToX, tileToY,  (pieceValue == 10)? 20 : 10);
                        }

                        //delete and remove territory off the tiles lying after the ToTile
                        undercontrol[tileToX, tileToY].onChangeLocation_RevertTerritory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY);
                        //if a pawn is under territory of an ally pawn, check after moving if the king will be checked
                        undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked);

                        boardvalue[tileAtX, tileAtY] = 0;
                        boardvalue[tileToX, tileToY] = pieceValue + pieceColor;
                        turn = (turn % 2 == 1) ? 2 : 1;
                        turncounter += 1;
                        
                        //update the tiles lying after the ATtile to extend the controlling pieces territory
                        undercontrol[tileToX, tileToY].onChangeLocation_continue(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY);
                        
                        saveStatus(boardvalue, territory, ref turncounter);
                    }
                }
            }
        }



        //function for diagonal movment used for Rooks and Queens
        //piece value is the value of piece, EX: queen == 5 Rook == 4.
        public void horizonalVertical_move(Controlling[,] controlling, UnderControl[,] undercontrol, Territory territory, bool[] castlingWhite, bool[] castlingBlack, int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int tileToX, int tileToY, int pieceColor, int pieceValue, ref int turn, ref int turncounter, ref bool kingChecked)
        {
            // pieceColor is the color value. Ex: black == 10 and white == 20, 
            if (!(boardvalue[tileToX, tileToY] - pieceColor < 10 && boardvalue[tileToX, tileToY] - pieceColor >= 0))
            {
                //check if the movment is vertical or horizontal by subtracting the Y and the X on both sides, if one side's diffrence is 0 and the other side's is > 0 then it is a Hori-ver movment
                if (tileAtX == tileToX && Math.Abs(tileAtY - tileToY) > 0 || tileAtY == tileToY && Math.Abs(tileAtX - tileToX) > 0)
                {
                    bool flag = true;
                    int distance = (Math.Abs(tileAtX - tileToX) > 0) ? Math.Abs(tileAtX - tileToX) : Math.Abs(tileAtY - tileToY); //choose the movment in horizontal or vertical

                    for (int i = 1; i < distance; i++) //check all the tiles between the At location and the To location if a piece was inbetween dont move
                    {
                        // UPPER || -,0
                        if (tileAtX > tileToX && tileAtY == tileToY)
                        {
                            ////exception out of bound handle
                            if (tileAtX - i >= 0 && tileAtX - i <= 7 && tileAtY >= 0 && tileAtY <= 7)
                            {
                                if (boardvalue[tileAtX - i, tileAtY] > 0) // a piece  was incountered midway.
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }

                        // Right || 0,+
                        if (tileAtX == tileToX && tileAtY < tileToY)
                        {
                            ////exception out of bound handle
                            if (tileAtX >= 0 && tileAtX <= 7 && tileAtY + i >= 0 && tileAtY + i <= 7)
                            {
                                if (boardvalue[tileAtX, tileAtY + i] > 0) // a piece  was incountered midway.
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }

                        // DOWN Left || +,0
                        if (tileAtX < tileToX && tileAtY > tileToY)
                        {
                            ////exception out of bound handle
                            if (tileAtX + i >= 0 && tileAtX + i <= 7 && tileAtY >= 0 && tileAtY <= 7)
                            {
                                if (boardvalue[tileAtX + i, tileAtY - i] > 0) // a piece  was incountered midway
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }

                        // DOWN Right || 0,-
                        if (tileAtX == tileToX && tileAtY > tileToY)
                        {
                            ////exception out of bound handle
                            if (tileAtX >= 0 && tileAtX <= 7 && tileAtY - i >= 0 && tileAtY - i <= 7)
                            {
                                if (boardvalue[tileAtX, tileAtY - i] > 0) // a piece  was incountered midway
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                    }

                    if (flag == true)
                    {
                        if(boardvalue[tileAtX, tileAtY] == 14 || boardvalue[tileAtX, tileAtY] == 24){
                            // the rook was black and at starting position 
                            if (tileAtX == 7 && tileAtY == 7) //right
                            {
                                castlingBlack[1] = false;
                            }
                            else if (tileAtX == 7 && tileAtY == 0)//Left
                            {
                                castlingBlack[2] = false;
                            }
                            // the rook was white and at starting position
                            else if (tileAtX == 0 && tileAtY == 0)//right
                            {
                                castlingWhite[1] = false;
                            }
                            else if (tileAtX == 0 && tileAtY == 7)//Left
                            {
                                castlingWhite[2] = false;
                            }
                        }

                        //if the Totile had an enemy, remove the captured enemy territory
                        if (boardvalue[tileToX, tileToY] != 0)
                        {
                            undercontrol[tileToX, tileToY].onChangeLocation_CaptureEnemyPawn(controlling, undercontrol, territory, boardvalue, tileToX, tileToY, (pieceValue == 10)? 20 : 10); // piece value should be the opposite
                        }
                        //delete and remove territory off the tiles lying after the ToTile
                        undercontrol[tileToX, tileToY].onChangeLocation_RevertTerritory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY);
                        //if a pawn is under territory of an ally pawn, check after moving if the king will be checked
                        undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked);

                        boardvalue[tileAtX, tileAtY] = 0;
                        boardvalue[tileToX, tileToY] = pieceValue + pieceColor;

                        
                        //update the tiles lying after the ATtile to extend the controlling pieces territory
                        undercontrol[tileToX, tileToY].onChangeLocation_continue(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY);

                        
                        turn = (turn % 2 == 1) ? 2 : 1;
                        turncounter += 1;
                        saveStatus(boardvalue, territory, ref turncounter);
                    }
                }
            }
        }


        //movment processing king
        public void king_move(Controlling[,] controlling, UnderControl[,] undercontrol, Territory territory, bool[] castlingWhite, bool[] castlingBlack, int[,] boardvalue, Button[,] boardtiles, int tileAtX, int tileAtY, int tileToX, int tileToY, int pieceValue, ref int turn, ref bool kingChecked, ref int turncounter) //we use piece value here to elemeinat the same colored pieces so we wont move over them
        {
            if (!(boardvalue[tileToX, tileToY] - pieceValue < 10 && boardvalue[tileToX, tileToY] - pieceValue >= 0))
            {
                //check if the To location is within the king's range
                if (Math.Abs(tileAtX - tileToX) == 1 && (Math.Abs(tileAtY - tileToY) == 0 || Math.Abs(tileAtY - tileToY) == 1)
                    || Math.Abs(tileAtY - tileToY) == 1 && (Math.Abs(tileAtX - tileToX) == 0 || Math.Abs(tileAtX - tileToX) == 0))// conditon to check the L pattern
                {
                    //the black king move from beginning position
                    if (tileAtX == 7 && tileAtY == 3)
                    {
                        castlingBlack[0] = false;
                    }
                    else if (tileAtX == 0 && tileAtY == 3)
                    {
                        castlingWhite[0] = false;
                    }

                    //if the Totile had an enemy, remove the captured enemy territory
                    if (boardvalue[tileToX, tileToY] != 0)
                    {
                        undercontrol[tileToX, tileToY].onChangeLocation_CaptureEnemyPawn(controlling, undercontrol, territory, boardvalue, tileToX, tileToY,  (pieceValue == 10)? 20 : 10);
                    }
                    //delete and remove territory off the tiles lying after the ToTile
                    undercontrol[tileToX, tileToY].onChangeLocation_RevertTerritory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY);
                    //if a pawn is under territory of an ally pawn, check after moving if the king will be checked
                    undercontrol[tileAtX, tileAtY].checkforCheck(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY, ref kingChecked);

                    boardvalue[tileAtX, tileAtY] = 0;
                    boardvalue[tileToX, tileToY] = pieceValue + 6;

                    
                    //update the tiles lying after the ATtile to extend the controlling pieces territory
                    undercontrol[tileToX, tileToY].onChangeLocation_continue(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileAtX, tileAtY);


                    turn = (turn % 2 == 1) ? 2 : 1;
                    turncounter += 1;

                    saveStatus(boardvalue, territory, ref turncounter);
                    if (kingChecked == true) //king was checked
                    {
                        kingChecked = false;
                        boardtiles[tileAtX, tileAtY].BackColor = (tileAtX  % 2 == 0 && (tileAtY) % 2 == 0 || (tileAtX ) % 2 == 1 && (tileAtY) % 2 == 1) ? Color.White : Color.Black;
                    }

                }

                else if ((castlingBlack[0] == true && tileToX == 7 || castlingWhite[0] == true && tileToX == 0) && (tileToY == 6 || tileToY == 1))
                {
                    Board iconprocess = new Board();

                    //Castling
                    if (castlingBlack[0] == true && territory.WhiteTerritory[tileAtX, tileAtY] == 0) //the king hasn't moved and is not in check
                    {
                        MessageBox.Show((pieceValue + 4) + "");
                        //Right Rook Black
                        if (castlingBlack[1] == true && boardvalue[7, 4] == 0 && territory.WhiteTerritory[7, 4] == 0 //the tile is not under control
                            && boardvalue[7, 5] == 0 && territory.WhiteTerritory[7, 5] == 0
                            && boardvalue[7, 6] == 0 && territory.WhiteTerritory[7, 6] == 0
                            && tileToX == 7 && tileToY == 6)
                        {
                            territory.straight__RemovePiece_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, 7, 7, 10);

                            boardvalue[tileAtX, tileAtY] = 0;
                            boardvalue[tileToX, tileToY] = pieceValue + 6;
                            //rook

                            boardvalue[7, 7] = 0;
                            boardvalue[7, 5] = pieceValue + 4;
                            territory.straight_UpdatePiece_Territory(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, 7, 5, 10, ref kingChecked);

                            iconprocess.ProcessIcons(boardvalue, boardtiles, 7, 7); 
                            iconprocess.ProcessIcons(boardvalue, boardtiles, 7, 5); 
                        }

                        //Left Rook Black
                        if (castlingBlack[2] == true && boardvalue[7, 2] == 0 && territory.WhiteTerritory[7, 2] == 0 //the tile is not under control
                            && boardvalue[7, 1] == 0 && territory.WhiteTerritory[7, 1] == 0
                            && tileToX == 7 && tileToY == 1)
                        {
                            territory.straight__RemovePiece_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, 7, 0, 10);

                            boardvalue[tileAtX, tileAtY] = 0;
                            boardvalue[tileToX, tileToY] = pieceValue + 6;

                            boardvalue[7, 0] = 0;
                            boardvalue[7, 2] = pieceValue + 4;
                            territory.straight_UpdatePiece_Territory(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, 7, 2, 10, ref kingChecked);

                            iconprocess.ProcessIcons(boardvalue, boardtiles, 7, 0);
                            iconprocess.ProcessIcons(boardvalue, boardtiles, 7, 2);
                        }

                        // white castling
                        if (castlingWhite[0] == true && territory.BlackTerritory[tileAtX, tileAtY] == 0) //the king hasn't moved and is not in check
                        {
                            //Right Rook white
                            if (castlingWhite[1] == true && boardvalue[0, 4] == 0 && territory.BlackTerritory[0, 4] == 0 //the tile is not under control
                                && boardvalue[0, 5] == 0 && territory.BlackTerritory[0, 5] == 0
                                && boardvalue[0, 6] == 0 && territory.BlackTerritory[0, 6] == 0
                                && tileToX == 0  && tileToY == 6)
                            {
                                territory.straight__RemovePiece_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, 0, 7, 10);


                                boardvalue[tileAtX, tileAtY] = 0;
                                boardvalue[tileToX, tileToY] = pieceValue + 6;

                                boardvalue[0, 7] = 0;
                                boardvalue[0, 5] = pieceValue + 4;
                                territory.straight_UpdatePiece_Territory(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, 0, 5, 10, ref kingChecked);

                                iconprocess.ProcessIcons(boardvalue, boardtiles, 0, 7);
                                iconprocess.ProcessIcons(boardvalue, boardtiles, 0, 5);
                            }

                            //Left Rook white
                            if (castlingWhite[2] == true && boardvalue[0, 2] == 0 && territory.BlackTerritory[0, 2] == 0 //the tile is not under control
                                && boardvalue[0, 1] == 0 && territory.BlackTerritory[0, 1] == 0
                                && tileToX == 0 && tileToY == 1)
                            {
                                territory.straight__RemovePiece_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, 7, 7, 10);


                                boardvalue[tileAtX, tileAtY] = 0;
                                boardvalue[tileToX, tileToY] = pieceValue + 6;

                                boardvalue[0, 0] = 0;
                                boardvalue[0, 2] = pieceValue + 4;

                                territory.straight_UpdatePiece_Territory(controlling, undercontrol, boardvalue, boardtiles, territory.WhiteTerritory, territory.BlackTerritory, 7, 5, 10, ref kingChecked);

                                iconprocess.ProcessIcons(boardvalue, boardtiles, 0, 0);
                                iconprocess.ProcessIcons(boardvalue, boardtiles, 0, 2);
                            }
                        }
                        turn = (turn % 2 == 1) ? 2 : 1;
                        turncounter += 1;
                        saveStatus(boardvalue, territory, ref turncounter);
                    }
                }
            }

        }

        //checks if the king move is valid
        public bool IsValid_King(Territory territory, int[,] boardvalue, int tileToX, int tileToY, int piecevalue)
        {
            //player1 - black
            if (piecevalue == 10)
            {
                // if a white piece has territory over the TO location
                if (territory.WhiteTerritory[tileToX, tileToY] > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (piecevalue == 20)
            {
                // if a black piece has territory over the TO location
                if (territory.BlackTerritory[tileToX, tileToY] > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void saveStatus(int [,] BoardValue, Territory territory,ref int turncounter)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\wed\Documents\Visual Studio 2012\Projects\CHESS\WindowsFormsApplication5\Chess.accdb");
                connection.Open();
                OleDbCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                //cmd.CommandText = "Drop Table Turn2";
                //cmd.ExecuteNonQuery();
                //make a new table                                     //array no1: boardvalue array no2: blackterritory array no3: whiteterritory
                cmd.CommandText = "CREATE TABLE Turn" + turncounter + " ( [arrayNo] number, [X0] number, [X1] number, [X2] number, [X3] number, [X4] number, [X5] number, [X6] number, [X7] number)";
                cmd.ExecuteNonQuery();

                string command;
                for (int i = 0; i < 8; i++)
                {
                    command = "insert into Turn" + turncounter + " values('" + "1" + "'";
                    for (int j = 0; j < 8; j++)
                    {
                        command += ",'" + BoardValue[i, j] + "'";
                    }
                    command += ")";
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();
                }

                for (int i = 0; i < 8; i++)
                {
                    command = "insert into Turn" + turncounter + " values('" + "2" + "'";
                    for (int j = 0; j < 8; j++)
                    {
                        command += ",'" + territory.BlackTerritory[i, j] + "'";
                    }
                    command += ")";
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();
                }

                for (int i = 0; i < 8; i++)
                {
                    command = "insert into Turn" + turncounter + " values('" + "3" + "'";
                    for (int j = 0; j < 8; j++)
                    {
                        command += ",'" + territory.WhiteTerritory[i, j] + "'";
                    }
                    command += ")";
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();
                }


                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("refresh the table data base by closing and reopening the program");
            }

        }
    }
}


//continue...
//not much is remaining *^* <3
//the next step should be implementing the IsValid function for the king - Done!!
//which should check if the selected move is valid or not
//constraining the king movment by the territory array 
//and handeling piece's territory after promotion
//remaining functions:
//
//the castllingrule() - Done!!!
//King Check conditions() - Done!!!!
//Checkmate () - Done!!!!!!

//new conditions-
//force king into check condition upon extending a piece territory- done!!!!!!
// make a piece move unvalid if its king will be undercontrol - done!!!!!!!

//database save map state and retrive state- done!!!!!!!

//ability to defend the king

//last task for project !!
// implement the check at exposing the enemy king and add
/*
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
*/
// to check for check()