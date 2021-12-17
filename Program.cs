using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koelkast
{
    class Program
    {
        static void Main(string[] args)
        {
            List<uint> muurlist = new List<uint>();
            string[] inputarr;
            

            inputarr = Console.ReadLine().Split(' ');
            uint b = UInt32.Parse(inputarr[0]);
            uint h = UInt32.Parse(inputarr[1]);
            string m = inputarr[2];
            uint cc = 0;
            uint einddoel;
            

            List<uint> wallist = new List<uint>();

            for(uint i = h; i > 0; i--)
            {
                string plat = Console.ReadLine();
                uint width = 0;

                foreach (char x in plat)
                {
                    if (x == '!')
                    {
                        cc += (width << 24) + (i << 16);
                    }
                    else if(x == '?')
                    {
                        einddoel = (width << 8) + (i);
                    }
                    else if(x == '+')
                    {
                        cc += (width << 8) + (i);
                    }
                    else if(x != '.')
                    {
                        wallist.Add((width << 8) + (i));
                    }

                    width += 1;
                }
                
            }
            
            Console.ReadKey();

        }

        public static void pathfinder(uint cc, List<uint> wallist, uint einddoel)
        {
            Queue<uint> myqu = new Queue<uint>();
            myqu.Append(cc);
            List<uint> pcc = new List<uint>();
            pcc.Add(cc);
            while(myqu.Count != 0)
            {
                uint northcc = direction(cc, 1, wallist, einddoel);
                if (pcc.Contains(northcc) == false)
                {
                    pcc.Add(northcc);
                    myqu.Append(northcc);
                }
                
            }
        }

        public static uint direction(uint cc,uint direction, List<uint> wallist, uint einddoel)
        {
            switch(direction)
            {
                case 1:
                    uint northcc = cc + (1 << 16);
                    var coen = ((northcc >> 16) << 16);
                    if ( northcc - coen == einddoel)
                    {
                        //bereikt
                    }
                    else if(wallist.Contains(coen) == true)
                    {
                        //niet in qu doen
                    }
                    else if (wallist.Contains(northcc - coen) == true)
                    {
                        //niet in qu doen
                    }
                    else
                    {
                        return northcc;
                    }

                    break;
            }
        }
       
    }
}
