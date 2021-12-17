using System;
using System.Collections.Generic;

namespace Koelkast
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstInput = Console.ReadLine();
            string[] firstInputs = firstInput.Split(' ');
            uint hoogte = Convert.ToUInt32(firstInputs[1]);
            uint[] coordinates = new uint[300 * 300];
            uint t = 0;
            uint KoenKoelkast = 0;
            uint endPoint = 0;
            bool p;
            if(firstInputs[2] == "P")
            {
                p = true;
            }
            else
            {
                p = false;
            }
            for(uint i= hoogte; i>0;i--)
            {
                string input = Console.ReadLine();
                uint width = 1;
                foreach(char inp in input)
                {
                    uint breedte = width << 8;
                    t++;

                    if(inp == '+')
                    {
                        KoenKoelkast += (i + breedte) << 16;
                    }
                    else if(inp == '!')
                    {
                        KoenKoelkast += i + breedte;
                    }
                    else if(inp == '?')
                    {
                        endPoint = i + breedte;
                    }
                    else if(inp != '.')
                    {
                        coordinates[i + breedte] = 1;
                    }
                    width++;
                }
            }
            Queue<uint> queue = new Queue<uint>();
            queue.Enqueue(KoenKoelkast);
            Dictionary<uint, uint> processed = new Dictionary<uint, uint>();
            long lastValue = -1;
            uint NorthKoen = 1 << 16;
            uint EastKoen = 1 << 24;

            uint NorthKoel = 1;
            uint EastKoel = 1 << 8;

            processed.Add(KoenKoelkast, KoenKoelkast);
            while(queue.Count != 0)
            {
                uint newValue = queue.Dequeue();
                if(((newValue + NorthKoen) >> 16) == (newValue - ((newValue >> 16)<<16)) && coordinates[(newValue - ((newValue >> 16) << 16)) + NorthKoel] != 1 && !processed.ContainsKey(newValue+NorthKoen+NorthKoel)) //check if hit koelkast and if wall next to koelkast in direction X and if state is a dictionary.
                {
                    uint inputValue = newValue + NorthKoen + NorthKoel;
                    processed.Add(inputValue, newValue);
                    queue.Enqueue(inputValue); 
                    if(inputValue - ((inputValue >> 16) << 16) == endPoint)
                    {
                        lastValue = inputValue;
                        break;
                    }
                }
                else if(coordinates[(newValue + NorthKoen) >> 16] != 1 && !processed.ContainsKey(newValue + NorthKoen) && ((newValue + NorthKoen) >> 16) != (newValue - ((newValue >> 16) << 16))) //wall next to koelkast or wall next to you in direction X and if state in dictionary.
                {
                    uint inputValue = newValue + NorthKoen;
                    processed.Add(inputValue, newValue);
                    queue.Enqueue(inputValue);
                }
                if (((newValue - NorthKoen) >> 16) == (newValue - ((newValue >> 16) << 16)) && coordinates[(newValue - ((newValue >> 16) << 16)) - NorthKoel] != 1 && !processed.ContainsKey(newValue - NorthKoen - NorthKoel)) //check if hit koelkast and if wall next to koelkast in direction X and if state is a dictionary.
                {
                    uint inputValue = newValue - NorthKoen - NorthKoel;
                    processed.Add(inputValue, newValue);
                    queue.Enqueue(inputValue);
                    if (inputValue - ((inputValue >> 16) << 16) == endPoint)
                    {
                        lastValue = inputValue;
                        break;
                    }
                }
                else if (coordinates[(newValue - NorthKoen) >> 16] != 1 && !processed.ContainsKey(newValue - NorthKoen) && ((newValue -  NorthKoen) >> 16) != (newValue - ((newValue >> 16) << 16))) //wall next to koelkast or wall next to you in direction X and if state in dictionary.
                {
                    uint inputValue = newValue - NorthKoen;
                    processed.Add(inputValue, newValue);
                    queue.Enqueue(inputValue);
                }
                if (((newValue + EastKoen) >> 16) == (newValue - ((newValue >> 16) << 16)) && coordinates[(newValue - ((newValue >> 16) << 16)) + EastKoel] != 1 && !processed.ContainsKey(newValue + EastKoen + EastKoel)) //check if hit koelkast and if wall next to koelkast in direction X and if state is a dictionary.
                {
                    uint inputValue = newValue + EastKoen + EastKoel;
                    processed.Add(inputValue, newValue);
                    queue.Enqueue(inputValue);
                    if (inputValue - ((inputValue >> 16) << 16) == endPoint)
                    {
                        lastValue = inputValue;
                        break;
                    }
                }
                else if (coordinates[(newValue + EastKoen) >> 16] != 1 && !processed.ContainsKey(newValue + EastKoen) && ((newValue + EastKoen) >> 16) != (newValue - ((newValue >> 16) << 16))) //wall next to koelkast or wall next to you in direction X and if state in dictionary.
                {
                    uint inputValue = newValue + EastKoen;
                    processed.Add(inputValue, newValue);
                    queue.Enqueue(inputValue);
                }
                if (((newValue - EastKoen) >> 16) == (newValue - ((newValue >> 16) << 16)) && coordinates[(newValue - ((newValue >> 16) << 16)) - EastKoel] != 1 && !processed.ContainsKey(newValue - EastKoen - EastKoel)) //check if hit koelkast and if wall next to koelkast in direction X and if state is a dictionary.
                {
                    uint inputValue = newValue - EastKoen - EastKoel;
                    processed.Add(inputValue, newValue);
                    queue.Enqueue(inputValue);
                    if (inputValue - ((inputValue >> 16)<<16) == endPoint)
                    {
                        lastValue = inputValue;
                        break;
                    }
                }
                else if (coordinates[(newValue - EastKoen) >> 16] != 1 && !processed.ContainsKey(newValue - EastKoen) && ((newValue - EastKoen) >> 16) != (newValue - ((newValue >> 16) << 16))) //wall next to koelkast or wall next to you in direction X and if state in dictionary.
                {
                    uint inputValue = newValue - EastKoen;
                    processed.Add(inputValue, newValue);
                    queue.Enqueue(inputValue);
                }
            }
            if(lastValue != -1)
            {
                uint key = Convert.ToUInt32(lastValue);
                uint value;
                string theWay = "";
                int lengthPath = 0;
                while(true)
                {
                    value = processed[key];
                    if (((value >> 16) - ((value >> 24) << 8)) + 1 == (key>>16) - ((key>>24) << 8))
                    {
                        theWay = "N" + theWay;
                    }
                    if (((value >> 16) - ((value >> 24) <<8)) - 1 == (key >> 16) - ((key >> 24)<<8))
                    {
                        theWay = "S" + theWay;
                    }
                    if ((value >> 24) + 1 == key>>24)
                    {
                        theWay = "E" + theWay;
                    }
                    if ((value >> 24) - 1 == key>>24)
                    {
                        theWay = "W" + theWay;
                    }
                    if (key - value == 0)
                    {
                        break;
                    }
                    key = value;
                    lengthPath++;
                }
                Console.WriteLine(lengthPath);
                if (p)
                {
                    Console.WriteLine(theWay);
                }
            }
            else
            {
                Console.WriteLine("No solution");
            }
        }
    }
}
