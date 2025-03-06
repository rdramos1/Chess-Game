using tabuleiro;

namespace xadrez {
    class Pawn : Part {
        private ChessMatch match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color) {
            this.match = match;
        }
        public override string ToString() {
            return "P";
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

                // #SpecialMove En Passant
                if (position.line == 3) {
                    Position left = new Position(position.line, position.row - 1);
                    if (board.ValidPosition(left) && enemyExist(left) && board.part(left) == match.vulnerableEnPassant) {
                        mat[left.line - 1, left.row] = true;
                    }
                    Position right = new Position(position.line, position.row + 1);
                    if (board.ValidPosition(right) && enemyExist(right) && board.part(right) == match.vulnerableEnPassant) {
                        mat[right.line - 1, right.row] = true;
                    }
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

                // #SpecialMove En Passant
                if (position.line == 4) {
                    Position left = new Position(position.line, position.row - 1);
                    if (board.ValidPosition(left) && enemyExist(left) && board.part(left) == match.vulnerableEnPassant) {
                        mat[left.line + 1, left.row] = true;
                    }
                    Position right = new Position(position.line, position.row + 1);
                    if (board.ValidPosition(right) && enemyExist(right) && board.part(right) == match.vulnerableEnPassant) {
                        mat[right.line + 1, right.row] = true;
                    }
                }
            }
            return mat;
        }
    }
}
