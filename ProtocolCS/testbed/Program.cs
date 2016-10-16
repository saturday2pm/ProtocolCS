using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProtocolCS;
using ProtocolCS.Constants;

namespace testbed
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = Serializer.ToJson(new StartGame());

            ////
            Console.WriteLine(json);
        }
    }
}
