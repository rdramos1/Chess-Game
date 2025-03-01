using System;
using tabuleiro;
using xadrez;

namespace Xadrez_console {
    class Program {
        static void Main(string[] args) {
            Board board = new Board(8, 8);

            board.PutPart(new Tower(board, Color.Black), new Position(0, 0));

            

            Screen.printboard(board);

        }
    }
}
