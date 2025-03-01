using System;
using tabuleiro;
using xadrez;

namespace Xadrez_console {
    class Program {
        static void Main(string[] args) {
            ChessMatch match = new ChessMatch();

            while (!match.finished) {
                try {
                    Console.Clear();
                    Screen.printboard(match.board);
                    Console.WriteLine();

                    Console.Write("Origin: ");
                    Position origin = Screen.readChessPosition().toPosition();
                    Console.Write("Destiny: ");
                    Position destiny = Screen.readChessPosition().toPosition();

                    match.movePart(origin, destiny);

                } catch (BoardException e) {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }


        }
    }
}
