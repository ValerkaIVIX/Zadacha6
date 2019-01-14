using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//using Form1.cs;
using System.Windows.Forms;

namespace Brezinheim
{
    public class Drawing
    {
        public Point p1, p2;
        public Drawing() {
            p1.X = 0;
            p2.X = 0;
            p1.Y = 0;
            p2.Y = 0;
        }

        public void Swap(ref int one, ref int two)
        {
            int buff = one;
            one = two;
            two = buff;
        }

        

      
        
    }
}
