using tabuleiro;
using xadrez;

namespace Xadrez_console {
    class Screen {

        public static void printboard(Board board) {

            for (int i = 0; i < board.line; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.row; j++) {
                    if (board.part(i, j) == null) {
                        Console.Write("- ");
                    }
                    else {
                        printPart(board.part(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printPart(Part part) {

            if (part.color == Color.White) {
                Console.Write(part);
            } else {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(part);
                Console.ForegroundColor = aux;
            }
        }

        public static ChessPosition readChessPosition() {
            string s = Console.ReadLine();
            char row = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(row, line);
        }

    }
}
