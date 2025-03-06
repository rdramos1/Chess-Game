using tabuleiro;

namespace xadrez {
    class Pawn : Part {
        public Pawn(Board board, Color color) : base(board, color) {
        }
        public override string ToString() {
            return "T";
        }

        private bool enemyExist(Position pos) {
            Part p = board.part(pos);
            return p != null && p.color != this.color;
        }
        private bool free(Position pos) {
            return board.part(pos) == null;
        }

        private bool CanMove(Position pos) {
            Part p = board.part(pos);
            return p == null || p.color != this.color;
        }

        public override bool[,] possibleMovements() {
            bool[,] mat = new bool[board.line, board.row];
            Position pos = new Position(0, 0);

            if (color == Color.White) {
                pos.setValue(position.line - 1, position.row);
                if (board.ValidPosition(pos) && free(pos)) {
                    mat[pos.line, pos.row] = true;
                }
                pos.setValue(position.line - 2, position.row);
                Position p2 = new Position(position.line - 1, position.row);
                if (board.ValidPosition(pos) && free(pos) && moves == 0) {
                    mat[pos.line, pos.row] = true;
                }
                pos.setValue(position.line - 1, position.row - 1);
                if (board.ValidPosition(pos) && enemyExist(pos)) {
                    mat[pos.line, pos.row] = true;
                }
                pos.setValue(position.line - 1, position.row + 1);
                if (board.ValidPosition(pos) && enemyExist(pos)) {
                    mat[pos.line, pos.row] = true;
                }
            }
            else {
                pos.setValue(position.line + 1, position.row);
                if (board.ValidPosition(pos) && free(pos)) {
                    mat[pos.line, pos.row] = true;
                }
                pos.setValue(position.line + 2, position.row);
                Position p2 = new Position(position.line + 1, position.row);
                if (board.ValidPosition(pos) && free(pos) && moves == 0) {
                    mat[pos.line, pos.row] = true;
                }
                pos.setValue(position.line + 1, position.row - 1);
                if (board.ValidPosition(pos) && enemyExist(pos)) {
                    mat[pos.line, pos.row] = true;
                }
                pos.setValue(position.line + 1, position.row + 1);
                if (board.ValidPosition(pos) && enemyExist(pos)) {
                    mat[pos.line, pos.row] = true;
                }
            }

            return mat;
        }
    }
}
