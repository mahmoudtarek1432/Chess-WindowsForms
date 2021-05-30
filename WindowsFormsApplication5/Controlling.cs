using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication5
{
    public class Controlling
    {       
        int X; // the controlling piece x position
        int Y; // the controlling piece Y position 

        public List <UnderControl> undercontrol = new List<UnderControl>();

        public Controlling(int controllerX, int controllerY)
        {
            this.X = controllerX;
            this.Y = controllerY;
            
        }

        public Controlling()
        {
        
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

        public void controller_changeLocation(Controlling[,] controlling, UnderControl[,] undercontrol, int CtrltileX, int CtrltileY, int underCtrltileX, int underCtrltileY)
        {
            for (int i = 0; i < undercontrol[underCtrltileX, underCtrltileY].Controllers.Count; i++)
            {

                if (undercontrol[underCtrltileX, underCtrltileY].Controllers[i].AccessX == CtrltileX && undercontrol[underCtrltileX, underCtrltileY].Controllers[i].AccessY == CtrltileY)
                {
                    undercontrol[underCtrltileX, underCtrltileY].Controllers.RemoveAt(i);

                }
            }
        }
    }
}
