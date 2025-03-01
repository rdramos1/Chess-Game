using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace Xadrez_console {
    class Program {
        static void Main(string[] args) {
            Position P;
            P = new Position(3, 4);
            Console.WriteLine("Position: " + P);
        }
    }
}
