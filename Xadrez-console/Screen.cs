using tabuleiro;

namespace Xadrez_console {
    class Screen {

        public static void printboard(Board board) {

            for (int i = 0; i < board.line; i++) {
                for (int j = 0; j < board.row; j++) {
                    if (board.part(i, j) == null) {
                        Console.Write("- ");
                    }
                    else {
                        Console.Write(board.part(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
