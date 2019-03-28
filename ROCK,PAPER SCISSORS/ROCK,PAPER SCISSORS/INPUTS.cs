using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROCK_PAPER_SCISSORS
{
     internal class INPUTS
    {
        //LOAD AVALABLE KEYBOARD FUNCTIONS//
        private static Hashtable keytable = new Hashtable();

        //CHECK IF BUTTON IS PRESSED//
        public static bool KeyPressed(Keys key)
        {
            if (keytable [key] == null )
            {
                return false;
            }
            return (bool)keytable[key];
        }
        //DETECT IF KEYBOARD IS USED//
        public static void ChangeState(Keys key,bool state)
        {
            keytable[key] = state;
        }
    }
}
