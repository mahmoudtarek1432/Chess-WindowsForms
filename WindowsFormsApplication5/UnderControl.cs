using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace WindowsFormsApplication5
{
    public class UnderControl //class that store the info about the pieces that is under control
    {
        
        int X; // x position of a piece
        int Y; // y position of a piece

        // contains the locations of the pieces that have control
        public List<Controlling> Controllers = new List<Controlling>();

        public UnderControl(int X, int Y) //used for undercontrol list in controlling class
        {
            this.X = X;
            this.Y = Y;
        }

        public UnderControl()
        {
           
            X = 10; //10 stands for an empty cell as the length of the array is 8
            Y = 10;
        }

        public int AccessX
        {
            set
            {
                X = value;
            }

            get
            {
                return X;
            }
        }

        public int AccessY
        {
            set
            {
                Y = value;
            }

            get
            {
                return Y;
            }
        }

    

        // insert the tiles under control and thier controllers
        public void undercontrol_register(Controlling[,] controlling, UnderControl[,] undercontrol, int CtrltileX, int CtrltileY, int underCtrltileX, int underCtrltileY)
        {
            // register a new controller
            undercontrol[underCtrltileX, underCtrltileY].Controllers.Add(new Controlling(CtrltileX, CtrltileY));

            // register a new piece that is undercontrol
            controlling[CtrltileX, CtrltileY].undercontrol.Add(new UnderControl(underCtrltileX, underCtrltileY));
        }

        // delete the controllers from the under control tiles 
        public void undercontrol_delete(Controlling[,] controlling, UnderControl[,] undercontrol, int CtrltileX, int CtrltileY, int underCtrltileX, int underCtrltileY)
        {
            for (int i = 0; i < undercontrol[underCtrltileX, underCtrltileY].Controllers.Count; i++)
            {

                if (undercontrol[underCtrltileX, underCtrltileY].Controllers[i].AccessX == CtrltileX && undercontrol[underCtrltileX, underCtrltileY].Controllers[i].AccessY == CtrltileY)
                {
                    undercontrol[underCtrltileX, underCtrltileY].Controllers.RemoveAt(i);

                }
            }

            for (int i = 0; i < controlling[CtrltileX, CtrltileY].undercontrol.Count; i++)
            {

                if (controlling[CtrltileX, CtrltileY].undercontrol[i].AccessX == underCtrltileX && controlling[CtrltileX, CtrltileY].undercontrol[i].AccessY == underCtrltileY)
                {
                    controlling[CtrltileX, CtrltileY].undercontrol.RemoveAt(i);

                }
            }
        }

        // after changing location 
        public void onChangeLocation_continue(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY)
        {

            for (int j = 0; j < undercontrol[UnderControlX, UnderControlY].Controllers.Count; j++)
            {

                //continue path function
                // for each controller that has territory in under controlled tile
                // continue.path
                undercontrol[UnderControlX, UnderControlY].continue_path(controlling, undercontrol, boardvalue, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, undercontrol[UnderControlX, UnderControlY].Controllers[j].AccessX, undercontrol[UnderControlX, UnderControlY].Controllers[j].AccessY);

            }

        }

        // Before moving check if the king will be exposed
        public bool beforeChangelocation_CheckInvalid(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, Button[,] boardtile, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY, ref bool kingChecked)
        {
            int controllercolor;
            int piececolor;
            bool controlled = false;
            for (int j = 0; j < undercontrol[UnderControlX, UnderControlY].Controllers.Count; j++)
            {

                //continue path function
                // for each controller that has territory in under controlled tile
                // continue.path
                controllercolor = (boardvalue[undercontrol[UnderControlX, UnderControlY].Controllers[j].AccessX, undercontrol[UnderControlX, UnderControlY].Controllers[j].AccessY] - 20 > 0) ? 20 : 10;
                piececolor = (boardvalue[UnderControlX, UnderControlY] - 20 > 0) ? 20 : 10;

                if (controllercolor != piececolor)
                {
                    controlled = undercontrol[UnderControlX, UnderControlY].forKing_pawnCheckPath(controlling, undercontrol, boardvalue, boardtile, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, undercontrol[UnderControlX, UnderControlY].Controllers[j].AccessX, undercontrol[UnderControlX, UnderControlY].Controllers[j].AccessY, ref kingChecked, true, false);
                }
                
                if (controlled == true) { return controlled; }
            }
            return false;

        }

        // Before moving check if the king will be exposed
        public bool checkforCheck(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, Button[,] boardtile, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY, ref bool kingChecked)
        {
            
            int controllercolor;
            int piececolor;
            bool controlled = false;
            for (int j = 0; j < undercontrol[UnderControlX, UnderControlY].Controllers.Count; j++)
            {

                //continue path function
                // for each controller that has territory in under controlled tile
                // continue.path
                controllercolor = (boardvalue[undercontrol[UnderControlX, UnderControlY].Controllers[j].AccessX, undercontrol[UnderControlX, UnderControlY].Controllers[j].AccessY] - 20 > 0) ? 20 : 10;
                piececolor = (boardvalue[UnderControlX, UnderControlY] - 20 > 0) ? 20 : 10;
                
                if (controllercolor == piececolor)
                {
                   
                    controlled = undercontrol[UnderControlX, UnderControlY].forKing_pawnCheckPath(controlling, undercontrol, boardvalue, boardtile, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, undercontrol[UnderControlX, UnderControlY].Controllers[j].AccessX, undercontrol[UnderControlX, UnderControlY].Controllers[j].AccessY, ref kingChecked, false, true);
                }

                if (controlled == true) {

                    return controlled; }
            }
            return false;

        }


        //after moving in a territorized tile
        public void onChangeLocation_RevertTerritory(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY)
        {

            for (int j = 0; j < undercontrol[UnderControlX, UnderControlY].Controllers.Count; j++)
            {

                //block_path function
                // for each controller that has territory in under controlled tile
                undercontrol[UnderControlX, UnderControlY].block_path(controlling, undercontrol, boardvalue, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, undercontrol[UnderControlX, UnderControlY].Controllers[j].AccessX, undercontrol[UnderControlX, UnderControlY].Controllers[j].AccessY);

            }

        }

        //when a piece move on  a tile with an enemy pawn standing on it
        //clear the piece control over the tiles and update territory
        public void onChangeLocation_CaptureEnemyPawn(Controlling[,] controlling, UnderControl[,] undercontrol, Territory territory, int[,] boardvalue, int tileToX, int tileToY, int PieceValue)
        {
            // case captured is a solider
            if (boardvalue[tileToX, tileToY] == 11 || boardvalue[tileToX, tileToY] == 21)
            {
                territory.solider_removeTerritory(territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, PieceValue);
            }

            //case capture is a knight
            else if (boardvalue[tileToX, tileToY] == 12 || boardvalue[tileToX, tileToY] == 22)
            {
                territory.knight_removeTerritory(territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, PieceValue);
            }

            //case capture is a bishop
            else if (boardvalue[tileToX, tileToY] == 13 || boardvalue[tileToX, tileToY] == 23)
            {
                territory.Diagonal_RemoveCurrent_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, PieceValue);
            }

            //case capture is a rook
            else if (boardvalue[tileToX, tileToY] == 14 || boardvalue[tileToX, tileToY] == 24)
            {
                territory.straight__RemovePiece_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, PieceValue);
            }

            else if (boardvalue[tileToX, tileToY] == 15 || boardvalue[tileToX, tileToY] == 25)
            {
                territory.Diagonal_RemoveCurrent_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, PieceValue);
                territory.straight__RemovePiece_Territory(controlling, undercontrol, boardvalue, territory.WhiteTerritory, territory.BlackTerritory, tileToX, tileToY, PieceValue);
            }
        }


        //roll over the path of the controlling pawn to check if the controlling pawn's territory will ocupy 
        //the moving piece king 
        public bool forKing_pawnCheckPath(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, Button[,] boardtile, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY, int ControllerX, int ControllerY, ref bool kingChecked, bool checkinvalid, bool checkforcheck)
        {
            // case that the controller was a Bishop
            if (boardvalue[ControllerX, ControllerY] == 23 || boardvalue[ControllerX, ControllerY] == 13)
            {
                
                return Diagonal_Check_forKing(controlling, undercontrol, boardvalue, boardtile, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, ControllerX, ControllerY, ref kingChecked, checkinvalid, checkforcheck);
            }

            // case that the controller was a Rook
            else if (boardvalue[ControllerX, ControllerY] == 24 || boardvalue[ControllerX, ControllerY] == 14)
            {
                return straight_Check_forking(controlling, undercontrol, boardvalue, boardtile, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, ControllerX, ControllerY, ref kingChecked, checkinvalid, checkforcheck);
            }

            // case that the controller was a queen
            else if (boardvalue[ControllerX, ControllerY] == 25 || boardvalue[ControllerX, ControllerY] == 15)
            {
                bool checkforDiagonal ,checkforstraight;
                checkforDiagonal = Diagonal_Check_forKing(controlling, undercontrol, boardvalue, boardtile, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, ControllerX, ControllerY, ref kingChecked, checkinvalid, checkforcheck);
                checkforstraight = straight_Check_forking(controlling, undercontrol, boardvalue, boardtile, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, ControllerX, ControllerY, ref kingChecked, checkinvalid, checkforcheck);

                if (checkforDiagonal == true || checkforstraight == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


        // when a piece moves and it was under another piece's territory
        // continue_path function is responsible for assigning the controlling pieces
        // new territory | used for liner moving pieces: Bishop, Rook, Queen
        public void continue_path(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY, int ControllerX, int ControllerY)
        {

            // case that the controller was a Bishop
            if (boardvalue[ControllerX, ControllerY] == 23 || boardvalue[ControllerX, ControllerY] == 13)
            {

                Diagonal_Continue_path(controlling, undercontrol, boardvalue, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, ControllerX, ControllerY);
            }

            // case that the controller was a Rook
            else if (boardvalue[ControllerX, ControllerY] == 24 || boardvalue[ControllerX, ControllerY] == 14)
            {
                straight_continue_path(controlling, undercontrol, boardvalue, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, ControllerX, ControllerY);
            }

            // case that the controller was a queen
            else if (boardvalue[ControllerX, ControllerY] == 25 || boardvalue[ControllerX, ControllerY] == 15)
            {
                Diagonal_Continue_path(controlling, undercontrol, boardvalue, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, ControllerX, ControllerY);
                straight_continue_path(controlling, undercontrol, boardvalue, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, ControllerX, ControllerY);
            }
        }

        // when a piece moves to a tile that is under control
        // the function checks the location of the controlling piece and the moving piece to determin the trajectory of the piece
        // then the tiles under control that lies after the moving piece shall be deleted
        // also decreasing the territory by 1
        public void block_path(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY, int ControllerX, int ControllerY)
        {
            // case that the controller was a Bishop
            if (boardvalue[ControllerX, ControllerY] == 23 || boardvalue[ControllerX, ControllerY] == 13)
            {

                diagonal_BlockPath(controlling, undercontrol, boardvalue, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, ControllerX, ControllerY);
            }

            // case that the controller was a Rook
            else if (boardvalue[ControllerX, ControllerY] == 24 || boardvalue[ControllerX, ControllerY] == 14)
            {
                straight_BlockPath(controlling, undercontrol, boardvalue, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, ControllerX, ControllerY);
            }

            // case that the controller was a queen
            else if (boardvalue[ControllerX, ControllerY] == 25 || boardvalue[ControllerX, ControllerY] == 15)
            {

                diagonal_BlockPath(controlling, undercontrol, boardvalue, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, ControllerX, ControllerY);
                straight_BlockPath(controlling, undercontrol, boardvalue, whiteTerritory, blackTerritory, UnderControlX, UnderControlY, ControllerX, ControllerY);
            }
        }



        //case if a piece has moved from a tile with a territory value > 0
        //and the piece value is 3 - bishop and queen
        private void Diagonal_Continue_path(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY, int ControllerX, int ControllerY)
        {
            int i = 1;
            bool flag = true;
            while (flag)
            {

                //condition checks if the under control piece is in diagonal with the controlling piece
                if (Math.Abs(UnderControlX - ControllerX) == Math.Abs(UnderControlY - ControllerY))
                {
                    // the controller was in the upper right side of the undercontrol piece | +,+ from the undercontrol piece
                    if (UnderControlX > ControllerX && UnderControlY > ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX + i >= 0 && UnderControlX + i <= 7 && UnderControlY + i >= 0 && UnderControlY + i <= 7)
                        {
                            //black piece case
                            if (boardvalue[ControllerX, ControllerY] == 13 || boardvalue[ControllerX, ControllerY] == 15)
                            {
                                blackTerritory[UnderControlX + i, UnderControlY + i] += 1;
                            }

                            else if (boardvalue[ControllerX, ControllerY] == 23 || boardvalue[ControllerX, ControllerY] == 25)
                            {
                                whiteTerritory[UnderControlX + i, UnderControlY + i] += 1;
                            }

                            undercontrol_register(controlling, undercontrol, ControllerX, ControllerY, UnderControlX + i, UnderControlY + i);


                            if (boardvalue[UnderControlX + i, UnderControlY + i] > 0)
                            {
                                flag = false;
                            }
                        }

                        //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }

                    // the controller was in the upper left side of the undercontrol piece | +,- from the undercontrol piece
                    if (UnderControlX > ControllerX && UnderControlY < ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX + i >= 0 && UnderControlX + i <= 7 && UnderControlY - i >= 0 && UnderControlY - i <= 7)
                        {
                            //black piece case
                            if (boardvalue[ControllerX, ControllerY] == 13 || boardvalue[ControllerX, ControllerY] == 15)
                            {
                                blackTerritory[UnderControlX + i, UnderControlY - i] += 1;
                            }

                            //white piece case
                            else if (boardvalue[ControllerX, ControllerY] == 23 || boardvalue[ControllerX, ControllerY] == 25)
                            {
                                whiteTerritory[UnderControlX + i, UnderControlY - i] += 1;
                            }

                            undercontrol_register(controlling, undercontrol, ControllerX, ControllerY, UnderControlX + i, UnderControlY - i);


                            if (boardvalue[UnderControlX + i, UnderControlY - i] > 0)
                            {
                                flag = false;
                            }
                        }
                        //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }

                    // the controller was in the down right side of the undercontrol piece | -,- from the undercontrol piece
                    if (UnderControlX < ControllerX && UnderControlY < ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX - i >= 0 && UnderControlX - i <= 7 && UnderControlY - i >= 0 && UnderControlY - i <= 7)
                        {
                            //black piece case
                            if (boardvalue[ControllerX, ControllerY] == 13 || boardvalue[ControllerX, ControllerY] == 15)
                            {
                                blackTerritory[UnderControlX - i, UnderControlY - i] += 1;
                            }

                            //white piece case
                            else if (boardvalue[ControllerX, ControllerY] == 23 || boardvalue[ControllerX, ControllerY] == 25)
                            {
                                whiteTerritory[UnderControlX - i, UnderControlY - i] += 1;
                            }

                            undercontrol_register(controlling, undercontrol, ControllerX, ControllerY, UnderControlX - i, UnderControlY - i);

                            if (boardvalue[UnderControlX - i, UnderControlY - i] > 0)
                            {
                                flag = false;
                            }
                        }

                        //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }

                    // the controller was in the down left side of the undercontrol piece | -,+ from the undercontrol piece
                    if (UnderControlX < ControllerX && UnderControlY > ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX - i >= 0 && UnderControlX - i <= 7 && UnderControlY + i >= 0 && UnderControlY + i <= 7)
                        {
                            
                            //black piece case
                            if (boardvalue[ControllerX, ControllerY] == 13 || boardvalue[ControllerX, ControllerY] == 15)
                            {
                                blackTerritory[UnderControlX - i, UnderControlY + i] += 1;
                            }

                            //white piece case
                            else if (boardvalue[ControllerX, ControllerY] == 23 || boardvalue[ControllerX, ControllerY] == 25)
                            {
                                whiteTerritory[UnderControlX - i, UnderControlY + i] += 1;
                            }

                            undercontrol_register(controlling, undercontrol, ControllerX, ControllerY, UnderControlX - i, UnderControlY + i);

                            if (boardvalue[UnderControlX - i, UnderControlY + i] > 0)
                            {
                                flag = false;
                            }
                        }

                            //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }
                }

                //used for the case if a pawn stepped on a tile controlled by a queen
                // if the piece moved on a tile controlled over a straight trajectory
                //the diagonal trajctory would itrate infinitly and vice versa
                else
                {
                    flag = false;
                }

                i++;
            }
        }


        //case if a piece has moved from a tile with a territory value > 0
        //and the piece value is 3 - bishop and queen
        private void diagonal_BlockPath(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY, int ControllerX, int ControllerY)
        {
            int i = 1;
            bool flag = true;
            while (flag)
            {
                
                //condition checks if the under control piece is in diagonal with the controlling piece
                if (Math.Abs(UnderControlX - ControllerX) == Math.Abs(UnderControlY - ControllerY))
                {
                    // the controller was in the upper right side of the undercontrol piece | +,+ from the undercontrol piece
                    if (UnderControlX > ControllerX && UnderControlY > ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX + i >= 0 && UnderControlX + i <= 7 && UnderControlY + i >= 0 && UnderControlY + i <= 7)
                        {
                            //black piece case
                            if (boardvalue[ControllerX, ControllerY] == 13 || boardvalue[ControllerX, ControllerY] == 15)
                            {
                                blackTerritory[UnderControlX + i, UnderControlY + i] -= 1;
                            }

                            else if (boardvalue[ControllerX, ControllerY] == 23 || boardvalue[ControllerX, ControllerY] == 25)
                            {
                                whiteTerritory[UnderControlX + i, UnderControlY + i] -= 1;
                            }

                            undercontrol_delete(controlling, undercontrol, ControllerX, ControllerY, UnderControlX + i, UnderControlY + i);


                            if (boardvalue[UnderControlX + i, UnderControlY + i] > 0)
                            {
                                flag = false;
                            }
                        }

                        //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }

                    // the controller was in the upper left side of the undercontrol piece | +,- from the undercontrol piece
                    if (UnderControlX > ControllerX && UnderControlY < ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX + i >= 0 && UnderControlX + i <= 7 && UnderControlY - i >= 0 && UnderControlY - i <= 7)
                        {
                            //black piece case
                            if (boardvalue[ControllerX, ControllerY] == 13 || boardvalue[ControllerX, ControllerY] == 15)
                            {
                                blackTerritory[UnderControlX + i, UnderControlY - i] -= 1;
                            }

                            //white piece case
                            else if (boardvalue[ControllerX, ControllerY] == 23 || boardvalue[ControllerX, ControllerY] == 25)
                            {
                                whiteTerritory[UnderControlX + i, UnderControlY - i] -= 1;
                            }

                            undercontrol_delete(controlling, undercontrol, ControllerX, ControllerY, UnderControlX + i, UnderControlY - i);


                            if (boardvalue[UnderControlX + i, UnderControlY - i] > 0)
                            {
                                flag = false;
                            }
                        }
                        //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }

                    // the controller was in the down right side of the undercontrol piece | -,- from the undercontrol piece
                    if (UnderControlX < ControllerX && UnderControlY < ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX - i >= 0 && UnderControlX - i <= 7 && UnderControlY - i >= 0 && UnderControlY - i <= 7)
                        {
                            //black piece case
                            if (boardvalue[ControllerX, ControllerY] == 13 || boardvalue[ControllerX, ControllerY] == 15)
                            {
                                blackTerritory[UnderControlX - i, UnderControlY - i] -= 1;
                            }

                            //white piece case
                            else if (boardvalue[ControllerX, ControllerY] == 23 || boardvalue[ControllerX, ControllerY] == 25)
                            {
                                whiteTerritory[UnderControlX - i, UnderControlY - i] -= 1;
                            }

                            undercontrol_delete(controlling, undercontrol, ControllerX, ControllerY, UnderControlX - i, UnderControlY - i);

                            if (boardvalue[UnderControlX - i, UnderControlY - i] > 0)
                            {
                                flag = false;
                            }
                        }

                        //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }

                    // the controller was in the down left side of the undercontrol piece | -,+ from the undercontrol piece
                    if (UnderControlX < ControllerX && UnderControlY > ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX - i >= 0 && UnderControlX - i <= 7 && UnderControlY + i >= 0 && UnderControlY + i <= 7)
                        {

                            //black piece case
                            if (boardvalue[ControllerX, ControllerY] == 13 || boardvalue[ControllerX, ControllerY] == 15)
                            {
                                blackTerritory[UnderControlX - i, UnderControlY + i] -= 1;
                            }

                            //white piece case
                            else if (boardvalue[ControllerX, ControllerY] == 23 || boardvalue[ControllerX, ControllerY] == 25)
                            {
                                whiteTerritory[UnderControlX - i, UnderControlY + i] -= 1;
                            }

                            undercontrol_delete(controlling, undercontrol, ControllerX, ControllerY, UnderControlX - i, UnderControlY + i);

                            if (boardvalue[UnderControlX - i, UnderControlY + i] > 0)
                            {
                                flag = false;
                            }
                        }

                            //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }
                }
                //used for the case if a pawn stepped on a tile controlled by a queen
                // if the piece moved on a tile controlled over a straight trajectory
                //the diagonal trajctory would itrate infinitly and vice versa
                else
                {
                    flag = false;
                }
                i++;
            }
        }


        //the function checks before moving a piece if its undercontrol by a bishop or a queen 
        //weather the move will be exposeing the king for a check
        private bool Diagonal_Check_forKing(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, Button[,] boardtile, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY, int ControllerX, int ControllerY,ref bool kingChecked, bool checkinvalid, bool checkforcheck)
        {
            int controllercolor = (boardvalue[ControllerX, ControllerY] - 20 > 0) ? 20 : 10;
            int piecevalue = (boardvalue[UnderControlX, UnderControlY] - 20 > 0)? 20 : 10; // assign weather the piece is black or white
            bool PawnMoveInvalid = false;
            int i = 1; // begin after the piece by one tile
            
            bool flag = true;
            while (flag)
            {

                //condition checks if the under control piece is in diagonal with the controlling piece
                if (Math.Abs(UnderControlX - ControllerX) == Math.Abs(UnderControlY - ControllerY))
                {
                    // the controller was in the upper right side of the undercontrol piece | +,+ from the undercontrol piece
                    if (UnderControlX > ControllerX && UnderControlY > ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX + i >= 0 && UnderControlX + i <= 7 && UnderControlY + i >= 0 && UnderControlY + i <= 7)
                        {

                            if (boardvalue[UnderControlX + i, UnderControlY + i] > 0)
                            {
                                if (checkinvalid&& boardvalue[UnderControlX + i, UnderControlY + i] == 16 //if the trajectory occupied the king
                                    ||
                                    checkinvalid && boardvalue[UnderControlX + i, UnderControlY + i] == 26
                                    ||
                                    checkforcheck && boardvalue[UnderControlX + i, UnderControlY + i] == 16
                                    ||
                                    checkforcheck && boardvalue[UnderControlX + i, UnderControlY + i] == 16)
                                {
                                    if ((boardvalue[UnderControlX + i, UnderControlY + i] == 26 && piecevalue == 10) || (boardvalue[UnderControlX + i, UnderControlY + i] == 16 && piecevalue == 20))
                                    {
                                        kingChecked = true;
                                        boardtile[UnderControlX + i, UnderControlY + i].BackColor = Color.Red;

                                        if (kingChecked == true && checkMate(whiteTerritory, blackTerritory, boardvalue, UnderControlX + i, UnderControlY + i))
                                        {

                                            int winner = ((piecevalue - 10 == 0) ? 1 : 2);
                                            MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                        }

                                    }

                                    PawnMoveInvalid = true;
                                }

                                if (boardvalue[UnderControlX + i, UnderControlY + i] > 0)
                                {
                                    flag = false;
                                }
                            }
                        }

                        //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }
                    
                    // the controller was in the upper left side of the undercontrol piece | +,- from the undercontrol piece
                    if (UnderControlX > ControllerX && UnderControlY < ControllerY)
                    {
                        
                        //exception out of bound handle
                        if (UnderControlX + i >= 0 && UnderControlX + i <= 7 && UnderControlY - i >= 0 && UnderControlY - i <= 7)
                        {
                            

                            if (boardvalue[UnderControlX + i, UnderControlY - i] > 0)
                            {
                                 
                                if( checkinvalid&& boardvalue[UnderControlX + i, UnderControlY - i] == 16 //if the trajectory occupied the king
                                    ||
                                    checkinvalid && boardvalue[UnderControlX + i, UnderControlY - i] == 26
                                    ||
                                    checkforcheck && boardvalue[UnderControlX + i, UnderControlY - i] == 16
                                    ||
                                    checkforcheck && boardvalue[UnderControlX + i, UnderControlY - i] == 16)
                                {
                                    if ((boardvalue[UnderControlX + i, UnderControlY - i] == 26 && piecevalue == 10) || (boardvalue[UnderControlX + i, UnderControlY - i] == 16 && piecevalue == 20))
                                    {
                                        kingChecked = true;
                                        boardtile[UnderControlX + i, UnderControlY - i].BackColor = Color.Red;

                                        if (kingChecked == true && checkMate(whiteTerritory, blackTerritory, boardvalue, UnderControlX + i, UnderControlY - i))
                                        {

                                            int winner = ((piecevalue - 10 == 0) ? 1 : 2);
                                            MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                        }

                                    }

                                    PawnMoveInvalid = true;
                                }

                                if (boardvalue[UnderControlX + i, UnderControlY - i] > 0)
                                {
                                    flag = false;
                                }
                            }
                        }
                        //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }

                    // the controller was in the down right side of the undercontrol piece | -,- from the undercontrol piece
                    if (UnderControlX < ControllerX && UnderControlY < ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX - i >= 0 && UnderControlX - i <= 7 && UnderControlY - i >= 0 && UnderControlY - i <= 7)
                        {
                            if (checkinvalid && boardvalue[UnderControlX - i, UnderControlY - i] == 16 //if the trajectory occupied the king
                                ||
                                checkinvalid && boardvalue[UnderControlX - i, UnderControlY - i] == 26
                                ||
                                checkforcheck && boardvalue[UnderControlX - i, UnderControlY - i] == 16
                                ||
                                checkforcheck && boardvalue[UnderControlX - i, UnderControlY - i] == 16)
                            {

                                if ((boardvalue[UnderControlX - i, UnderControlY - i] == 26 && piecevalue == 10) || (boardvalue[UnderControlX - i, UnderControlY - i] == 16 && piecevalue == 20))
                                {
                                    kingChecked = true;
                                    boardtile[UnderControlX - i, UnderControlY - i].BackColor = Color.Red;

                                    if (kingChecked == true && checkMate(whiteTerritory, blackTerritory, boardvalue, UnderControlX - i, UnderControlY - i))
                                    {

                                        int winner = ((piecevalue - 10 == 0) ? 1 : 2);
                                        MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                    }

                                }

                                PawnMoveInvalid = true;
                            }

                            if (boardvalue[UnderControlX - i, UnderControlY - i] > 0)
                            {
                                flag = false;
                            }
                        }

                        //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }

                    // the controller was in the down left side of the undercontrol piece | -,+ from the undercontrol piece
                    if (UnderControlX < ControllerX && UnderControlY > ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX - i >= 0 && UnderControlX - i <= 7 && UnderControlY + i >= 0 && UnderControlY + i <= 7)
                        {
                            if (checkinvalid && boardvalue[UnderControlX - i, UnderControlY + i] == 16 //if the trajectory occupied the king
                                ||
                                checkinvalid && boardvalue[UnderControlX - i, UnderControlY + i] == 26
                                ||
                                checkforcheck && boardvalue[UnderControlX - i, UnderControlY + i] == 16
                                ||
                                checkforcheck && boardvalue[UnderControlX - i, UnderControlY + i] == 16)
                            {

                                if ((boardvalue[UnderControlX - i, UnderControlY + i] == 26 && piecevalue == 10) || (boardvalue[UnderControlX - i, UnderControlY + i] == 16 && piecevalue == 20))
                                {
                                    kingChecked = true;
                                    boardtile[UnderControlX - i, UnderControlY + i].BackColor = Color.Red;

                                    if (kingChecked == true && checkMate(whiteTerritory, blackTerritory, boardvalue, UnderControlX - i, UnderControlY + i))
                                    {

                                        int winner = ((piecevalue - 10 == 0) ? 1 : 2);
                                        MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                    }

                                }

                                PawnMoveInvalid = true;
                            }

                            if (boardvalue[UnderControlX - i, UnderControlY + i] > 0)
                            {
                                flag = false;
                            }
                        }

                            //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }
                }

                //used for the case if a pawn stepped on a tile controlled by a queen
                // if the piece moved on a tile controlled over a straight trajectory
                //the diagonal trajctory would itrate infinitly and vice versa
                else
                {
                    flag = false;
                }

                i++;
            }
           
            return PawnMoveInvalid; //if the value == true the pawn may not move
        }




        //case if a piece has moved from a tile with a territory value > 0
        //and the pawn type is rook or queen
        public void straight_continue_path(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY, int ControllerX, int ControllerY)
        {

            int i = 1; //start at 1 as the under controlled piece tile is already at territory
            bool flag = true;
            while (flag)
            {

                //condition checks if the under control piece is in a straight line with the controlling piece
                if (Math.Abs(UnderControlX - ControllerX) != 0 && (UnderControlY - ControllerY) == 0 || Math.Abs(UnderControlY - ControllerY) != 0 && UnderControlX - ControllerX == 0)
                {
                    // the controller was in the upper side of the undercontrol piece | +, from the undercontrol piece
                    if (UnderControlX > ControllerX && UnderControlY == ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX + i >= 0 && UnderControlX + i <= 7 && UnderControlY >= 0 && UnderControlY <= 7)
                        {
                            
                            //black piece case
                            if (boardvalue[ControllerX, ControllerY] == 14 || boardvalue[ControllerX, ControllerY] == 15)
                            {
                                blackTerritory[UnderControlX + i, UnderControlY] += 1;
                            }

                            else if (boardvalue[ControllerX, ControllerY] == 24 || boardvalue[ControllerX, ControllerY] == 25)
                            {
                                whiteTerritory[UnderControlX + i, UnderControlY] += 1;
                            }

                            undercontrol_register(controlling, undercontrol, ControllerX, ControllerY, UnderControlX + i, UnderControlY);


                            if (boardvalue[UnderControlX + i, UnderControlY] > 0)
                            {
                                flag = false;
                            }
                        }

                        //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }


                    //condition checks if the under control piece is in a straight line with the controlling piece
                    if (Math.Abs(UnderControlX - ControllerX) != 0 && (UnderControlY - ControllerY) == 0 || Math.Abs(UnderControlY - ControllerY) != 0 && UnderControlX - ControllerX == 0)
                    {
                        // the controller was in the right side of the undercontrol piece | ,- from the undercontrol piece
                        if (UnderControlX == ControllerX && UnderControlY < ControllerY)
                        {
                            //exception out of bound handle
                            if (UnderControlX >= 0 && UnderControlX <= 7 && UnderControlY - i >= 0 && UnderControlY - i <= 7)
                            {
                                //black piece case
                                if (boardvalue[ControllerX, ControllerY] == 14 || boardvalue[ControllerX, ControllerY] == 15)
                                {
                                    blackTerritory[UnderControlX, UnderControlY - i] += 1;
                                }

                                else if (boardvalue[ControllerX, ControllerY] == 24 || boardvalue[ControllerX, ControllerY] == 25)
                                {
                                    whiteTerritory[UnderControlX, UnderControlY - i] += 1;
                                }

                                undercontrol_register(controlling, undercontrol, ControllerX, ControllerY, UnderControlX, UnderControlY - i);


                                if (boardvalue[UnderControlX, UnderControlY - i] > 0)
                                {
                                    flag = false;
                                }
                            }

                            //case the no pieces were found on this side
                            else
                            {
                                flag = false;
                            }
                        }
                    }

                    //condition checks if the under control piece is in a straight line with the controlling piece
                    if (Math.Abs(UnderControlX - ControllerX) != 0 && (UnderControlY - ControllerY) == 0 || Math.Abs(UnderControlY - ControllerY) != 0 && UnderControlX - ControllerX == 0)
                    {
                        // the controller was in the left side of the undercontrol piece | ,+ from the undercontrol piece
                        if (UnderControlX == ControllerX && UnderControlY > ControllerY)
                        {
                            //exception out of bound handle
                            if (UnderControlX >= 0 && UnderControlX <= 7 && UnderControlY + i >= 0 && UnderControlY + i <= 7)
                            {
                                //black piece case
                                if (boardvalue[ControllerX, ControllerY] == 14 || boardvalue[ControllerX, ControllerY] == 15)
                                {
                                    blackTerritory[UnderControlX, UnderControlY + i] += 1;
                                }

                                else if (boardvalue[ControllerX, ControllerY] == 24 || boardvalue[ControllerX, ControllerY] == 25)
                                {
                                    whiteTerritory[UnderControlX, UnderControlY + i] += 1;
                                }

                                undercontrol_register(controlling, undercontrol, ControllerX, ControllerY, UnderControlX, UnderControlY + i);


                                if (boardvalue[UnderControlX, UnderControlY + i] > 0)
                                {
                                    flag = false;
                                }
                            }

                            //case the no pieces were found on this side
                            else
                            {
                                flag = false;
                            }
                        }
                    }

                    //condition checks if the under control piece is in a straight line with the controlling piece
                    if (Math.Abs(UnderControlX - ControllerX) != 0 && (UnderControlY - ControllerY) == 0 || Math.Abs(UnderControlY - ControllerY) != 0 && UnderControlX - ControllerX == 0)
                    {
                        // the controller was in the down side of the undercontrol piece | -, from the undercontrol piece
                        if (UnderControlX < ControllerX && UnderControlY == ControllerY)
                        {
                            //exception out of bound handle
                            if (UnderControlX - i >= 0 && UnderControlX - i <= 7 && UnderControlY >= 0 && UnderControlY <= 7)
                            {
                                
                                //black piece case
                                if (boardvalue[ControllerX, ControllerY] == 14 || boardvalue[ControllerX, ControllerY] == 15)
                                {
                                    blackTerritory[UnderControlX - i, UnderControlY] += 1;
                                }

                                else if (boardvalue[ControllerX, ControllerY] == 24 || boardvalue[ControllerX, ControllerY] == 25)
                                {
                                    whiteTerritory[UnderControlX - i, UnderControlY] += 1;
                                }
                                
                                undercontrol_register(controlling, undercontrol, ControllerX, ControllerY, UnderControlX - i, UnderControlY);


                                if (boardvalue[UnderControlX - i, UnderControlY] > 0)
                                {
                                    flag = false;
                                }
                            }
                            //case the no pieces were found on this side
                            else
                            {
                                flag = false;
                            }
                        }
                    }
                }
                //used for the case if a pawn stepped on a tile controlled by a queen
                // if the piece moved on a tile controlled over a straight trajectory
                //the diagonal trajctory would itrate infinitly and vice versa
                else
                {
                    flag = false;
                }
                i++;
            }
        }


        //case if a piece has moved from a tile with a territory value > 0
        //and the pawn type is rook or queen
        public void straight_BlockPath(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY, int ControllerX, int ControllerY)
        {

            int i = 1; //start at 1 as the under controlled piece tile is already at territory
            bool flag = true;
            while (flag)
            {

                //condition checks if the under control piece is in a straight line with the controlling piece
                if (Math.Abs(UnderControlX - ControllerX) != 0 && (UnderControlY - ControllerY) == 0 || Math.Abs(UnderControlY - ControllerY) != 0 && UnderControlX - ControllerX == 0)
                {
                    // the controller was in the upper side of the undercontrol piece | +, from the undercontrol piece
                    if (UnderControlX > ControllerX && UnderControlY == ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX + i >= 0 && UnderControlX + i <= 7 && UnderControlY >= 0 && UnderControlY <= 7)
                        {

                            //black piece case
                            if (boardvalue[ControllerX, ControllerY] == 14 || boardvalue[ControllerX, ControllerY] == 15)
                            {
                                blackTerritory[UnderControlX + i, UnderControlY] -= 1;
                            }

                            else if (boardvalue[ControllerX, ControllerY] == 24 || boardvalue[ControllerX, ControllerY] == 25)
                            {
                                whiteTerritory[UnderControlX + i, UnderControlY] -= 1;
                            }

                            undercontrol_delete(controlling, undercontrol, ControllerX, ControllerY, UnderControlX + i, UnderControlY);


                            if (boardvalue[UnderControlX + i, UnderControlY] > 0)
                            {
                                flag = false;
                            }
                        }

                        //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }


                    //condition checks if the under control piece is in a straight line with the controlling piece
                    if (Math.Abs(UnderControlX - ControllerX) != 0 && (UnderControlY - ControllerY) == 0 || Math.Abs(UnderControlY - ControllerY) != 0 && UnderControlX - ControllerX == 0)
                    {
                        // the controller was in the right side of the undercontrol piece | ,- from the undercontrol piece
                        if (UnderControlX == ControllerX && UnderControlY < ControllerY)
                        {
                            //exception out of bound handle
                            if (UnderControlX >= 0 && UnderControlX <= 7 && UnderControlY - i >= 0 && UnderControlY - i <= 7)
                            {
                                //black piece case
                                if (boardvalue[ControllerX, ControllerY] == 14 || boardvalue[ControllerX, ControllerY] == 15)
                                {
                                    blackTerritory[UnderControlX, UnderControlY - i] -= 1;
                                }

                                else if (boardvalue[ControllerX, ControllerY] == 24 || boardvalue[ControllerX, ControllerY] == 25)
                                {
                                    whiteTerritory[UnderControlX, UnderControlY - i] -= 1;
                                }

                                undercontrol_delete(controlling, undercontrol, ControllerX, ControllerY, UnderControlX, UnderControlY - i);


                                if (boardvalue[UnderControlX, UnderControlY - i] > 0)
                                {
                                    flag = false;
                                }
                            }

                            //case the no pieces were found on this side
                            else
                            {
                                flag = false;
                            }
                        }
                    }

                    //condition checks if the under control piece is in a straight line with the controlling piece
                    if (Math.Abs(UnderControlX - ControllerX) != 0 && (UnderControlY - ControllerY) == 0 || Math.Abs(UnderControlY - ControllerY) != 0 && UnderControlX - ControllerX == 0)
                    {
                        // the controller was in the left side of the undercontrol piece | ,+ from the undercontrol piece
                        if (UnderControlX == ControllerX && UnderControlY > ControllerY)
                        {
                            //exception out of bound handle
                            if (UnderControlX >= 0 && UnderControlX <= 7 && UnderControlY + i >= 0 && UnderControlY + i <= 7)
                            {
                                //black piece case
                                if (boardvalue[ControllerX, ControllerY] == 14 || boardvalue[ControllerX, ControllerY] == 15)
                                {
                                    blackTerritory[UnderControlX, UnderControlY + i] -= 1;
                                }

                                else if (boardvalue[ControllerX, ControllerY] == 24 || boardvalue[ControllerX, ControllerY] == 25)
                                {
                                    whiteTerritory[UnderControlX, UnderControlY + i] -= 1;
                                }

                                undercontrol_delete(controlling, undercontrol, ControllerX, ControllerY, UnderControlX, UnderControlY + i);


                                if (boardvalue[UnderControlX, UnderControlY + i] > 0)
                                {
                                    flag = false;
                                }
                            }

                            //case the no pieces were found on this side
                            else
                            {
                                flag = false;
                            }
                        }
                    }

                    //condition checks if the under control piece is in a straight line with the controlling piece
                    if (Math.Abs(UnderControlX - ControllerX) != 0 && (UnderControlY - ControllerY) == 0 || Math.Abs(UnderControlY - ControllerY) != 0 && UnderControlX - ControllerX == 0)
                    {
                        // the controller was in the down side of the undercontrol piece | -, from the undercontrol piece
                        if (UnderControlX < ControllerX && UnderControlY == ControllerY)
                        {
                            //exception out of bound handle
                            if (UnderControlX - i >= 0 && UnderControlX - i <= 7 && UnderControlY >= 0 && UnderControlY <= 7)
                            {

                                //black piece case
                                if (boardvalue[ControllerX, ControllerY] == 14 || boardvalue[ControllerX, ControllerY] == 15)
                                {
                                    blackTerritory[UnderControlX - i, UnderControlY] -= 1;
                                }

                                else if (boardvalue[ControllerX, ControllerY] == 24 || boardvalue[ControllerX, ControllerY] == 25)
                                {
                                    whiteTerritory[UnderControlX - i, UnderControlY] -= 1;
                                }

                                undercontrol_delete(controlling, undercontrol, ControllerX, ControllerY, UnderControlX - i, UnderControlY);


                                if (boardvalue[UnderControlX - i, UnderControlY] > 0)
                                {
                                    flag = false;
                                }
                            }
                            //case the no pieces were found on this side
                            else
                            {
                                flag = false;
                            }
                        }
                    }
                }

                 //used for the case if a pawn stepped on a tile controlled by a queen
                // if the piece moved on a tile controlled over a straight trajectory
                //the diagonal trajctory would itrate infinitly and vice versa
                else
                {
                    flag = false;
                }
                i++;
            }
        }




        public bool straight_Check_forking(Controlling[,] controlling, UnderControl[,] undercontrol, int[,] boardvalue, Button [,] boardtile, int[,] whiteTerritory, int[,] blackTerritory, int UnderControlX, int UnderControlY, int ControllerX, int ControllerY, ref bool kingChecked, bool checkinvalid, bool checkforcheck)
        {
            int piecevalue = (boardvalue[UnderControlX, UnderControlY] - 20 > 0)? 20 : 10;
            bool PawnMoveInvalid = false;
            int i = 1; //start at 1 as the under controlled piece tile is already at territory
            bool flag = true;
            while (flag)
            {

                //condition checks if the under control piece is in a straight line with the controlling piece
                if (Math.Abs(UnderControlX - ControllerX) != 0 && (UnderControlY - ControllerY) == 0 || Math.Abs(UnderControlY - ControllerY) != 0 && UnderControlX - ControllerX == 0)
                {
                    // the controller was in the upper side of the undercontrol piece | +, from the undercontrol piece
                    if (UnderControlX > ControllerX && UnderControlY == ControllerY)
                    {
                        //exception out of bound handle
                        if (UnderControlX + i >= 0 && UnderControlX + i <= 7 && UnderControlY >= 0 && UnderControlY <= 7)
                        {
                            if (checkinvalid && boardvalue[UnderControlX + i, UnderControlY] == 16 //if the trajectory occupied the king
                                ||
                                checkinvalid && boardvalue[UnderControlX + i, UnderControlY] == 26
                                ||
                                checkforcheck && boardvalue[UnderControlX + i, UnderControlY] == 16
                                ||
                                checkforcheck && boardvalue[UnderControlX + i, UnderControlY] == 16)
                            {

                                if ((boardvalue[UnderControlX + i, UnderControlY] == 26 && piecevalue == 10) || (boardvalue[UnderControlX + i, UnderControlY] == 16 && piecevalue == 20))
                                {
                                    kingChecked = true;
                                    boardtile[UnderControlX + i, UnderControlY].BackColor = Color.Red;

                                    if (kingChecked == true && checkMate(whiteTerritory, blackTerritory, boardvalue, UnderControlX + i, UnderControlY))
                                    {

                                        int winner = ((piecevalue - 10 == 0) ? 1 : 2);
                                        MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                    }

                                }

                                PawnMoveInvalid = true;
                            }

                            if (boardvalue[UnderControlX + i, UnderControlY] > 0)
                            {
                                flag = false;
                            }
                        }

                        //case the no pieces were found on this side
                        else
                        {
                            flag = false;
                        }
                    }


                    //condition checks if the under control piece is in a straight line with the controlling piece
                    if (Math.Abs(UnderControlX - ControllerX) != 0 && (UnderControlY - ControllerY) == 0 || Math.Abs(UnderControlY - ControllerY) != 0 && UnderControlX - ControllerX == 0)
                    {
                        // the controller was in the right side of the undercontrol piece | ,- from the undercontrol piece
                        if (UnderControlX == ControllerX && UnderControlY < ControllerY)
                        {
                            //exception out of bound handle
                            if (UnderControlX >= 0 && UnderControlX <= 7 && UnderControlY - i >= 0 && UnderControlY - i <= 7)
                            {
                                if (checkinvalid && boardvalue[UnderControlX, UnderControlY - i] == 16 //if the trajectory occupied the king
                                    ||
                                    checkinvalid && boardvalue[UnderControlX, UnderControlY - i] == 26
                                    ||
                                    checkforcheck && boardvalue[UnderControlX, UnderControlY - i] == 16
                                    ||
                                    checkforcheck && boardvalue[UnderControlX, UnderControlY - i] == 16)
                                {

                                    if ((boardvalue[UnderControlX, UnderControlY - i] == 26 && piecevalue == 10) || (boardvalue[UnderControlX, UnderControlY - i] == 16 && piecevalue == 20))
                                    {
                                        kingChecked = true;
                                        boardtile[UnderControlX, UnderControlY - i].BackColor = Color.Red;

                                        if (kingChecked == true && checkMate(whiteTerritory, blackTerritory, boardvalue, UnderControlX, UnderControlY - i))
                                        {

                                            int winner = ((piecevalue - 10 == 0) ? 1 : 2);
                                            MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                        }

                                    }

                                    PawnMoveInvalid = true;
                                }

                                if (boardvalue[UnderControlX, UnderControlY - i] > 0)
                                {
                                    flag = false;
                                }
                            }

                            //case the no pieces were found on this side
                            else
                            {
                                flag = false;
                            }
                        }
                    }

                    //condition checks if the under control piece is in a straight line with the controlling piece
                    if (Math.Abs(UnderControlX - ControllerX) != 0 && (UnderControlY - ControllerY) == 0 || Math.Abs(UnderControlY - ControllerY) != 0 && UnderControlX - ControllerX == 0)
                    {
                        // the controller was in the left side of the undercontrol piece | ,+ from the undercontrol piece
                        if (UnderControlX == ControllerX && UnderControlY > ControllerY)
                        {
                            //exception out of bound handle
                            if (UnderControlX >= 0 && UnderControlX <= 7 && UnderControlY + i >= 0 && UnderControlY + i <= 7)
                            {
                                if (checkinvalid && boardvalue[UnderControlX , UnderControlY + i] == 16 //if the trajectory occupied the king
                                    ||
                                    checkinvalid && boardvalue[UnderControlX , UnderControlY + i] == 26
                                    ||
                                    checkforcheck && boardvalue[UnderControlX, UnderControlY + i] == 16
                                    ||
                                    checkforcheck && boardvalue[UnderControlX , UnderControlY + i] == 16)
                                {

                                    if ((boardvalue[UnderControlX, UnderControlY + i] == 26 && piecevalue == 10) || (boardvalue[UnderControlX, UnderControlY + i] == 16 && piecevalue == 20))
                                    {
                                        kingChecked = true;
                                        boardtile[UnderControlX, UnderControlY + i].BackColor = Color.Red;

                                        if (kingChecked == true && checkMate(whiteTerritory, blackTerritory, boardvalue, UnderControlX, UnderControlY + i))
                                        {

                                            int winner = ((piecevalue - 10 == 0) ? 1 : 2);
                                            MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                        }

                                    }


                                    PawnMoveInvalid = true;
                                }

                                if (boardvalue[UnderControlX, UnderControlY + i] > 0)
                                {
                                    flag = false;
                                }
                            }

                            //case the no pieces were found on this side
                            else
                            {
                                flag = false;
                            }
                        }
                    }

                    //condition checks if the under control piece is in a straight line with the controlling piece
                    if (Math.Abs(UnderControlX - ControllerX) != 0 && (UnderControlY - ControllerY) == 0 || Math.Abs(UnderControlY - ControllerY) != 0 && UnderControlX - ControllerX == 0)
                    {
                        // the controller was in the down side of the undercontrol piece | -, from the undercontrol piece
                        if (UnderControlX < ControllerX && UnderControlY == ControllerY)
                        {
                            //exception out of bound handle
                            if (UnderControlX - i >= 0 && UnderControlX - i <= 7 && UnderControlY >= 0 && UnderControlY <= 7)
                            {
                                if (checkinvalid && boardvalue[UnderControlX - i, UnderControlY ] == 16 //if the trajectory occupied the king
                                    ||
                                    checkinvalid && boardvalue[UnderControlX - i, UnderControlY ] == 26
                                    ||
                                    checkforcheck && boardvalue[UnderControlX - i, UnderControlY ] == 16
                                    ||
                                    checkforcheck && boardvalue[UnderControlX - i, UnderControlY ] == 16)
                                {

                                    if ((boardvalue[UnderControlX - i, UnderControlY] == 26 && piecevalue == 10) || (boardvalue[UnderControlX - i, UnderControlY] == 16 && piecevalue == 20))
                                    {
                                        kingChecked = true;
                                        boardtile[UnderControlX - i, UnderControlY].BackColor = Color.Red;

                                        if (kingChecked == true && checkMate(whiteTerritory, blackTerritory, boardvalue, UnderControlX - i, UnderControlY))
                                        {

                                            int winner = ((piecevalue - 10 == 0) ? 1 : 2);
                                            MessageBox.Show("          Check Mate \n" + " the winner is: Player " + winner);
                                        }

                                    }

                                    PawnMoveInvalid = true;
                                }

                                if (boardvalue[UnderControlX - i, UnderControlY] > 0)
                                {
                                    flag = false;
                                }
                            }
                            //case the no pieces were found on this side
                            else
                            {
                                flag = false;
                            }
                        }
                    }
                }
                //used for the case if a pawn stepped on a tile controlled by a queen
                // if the piece moved on a tile controlled over a straight trajectory
                //the diagonal trajctory would itrate infinitly and vice versa
                else
                {
                    flag = false;
                }
                i++;
            }
            
            return PawnMoveInvalid;
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
