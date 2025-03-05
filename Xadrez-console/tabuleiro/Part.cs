using tabuleiro;

namespace tabuleiro {
    abstract class Part {

        public Position position { get; set; }
        public Color color { get; set; }
        public int moves { get; set; }
        public Board board { get; protected set; }

        public Part(Board board, Color color) {
            this.position = null;
            this.board = board;
            this.color = color;
            this.moves = 0;
        }

        public void increaseMoves () {
            moves++;
        }

        public void decreaseMoves() {
            moves--;
        }

        public abstract bool[,] possibleMovements();

        public bool existPossibleMovements() {
            bool[,] mat = possibleMovements();
            for (int i = 0; i < board.line; i++) {
                for (int j = 0; j < board.row; j++) {
                    if (mat[i, j]) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool canMoveTo(Position pos) {
            return possibleMovements()[pos.line, pos.row];
        }
    }
}
