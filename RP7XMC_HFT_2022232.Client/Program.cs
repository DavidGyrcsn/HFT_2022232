using System;

namespace RP7XMC_HFT_2022232.Client
{
    internal class Program
    {
        static RestService brandrest;
        static void Main(string[] args)
        {
            brandrest = new RestService("http://localhost:2810", "brand");
        }
    }
}
