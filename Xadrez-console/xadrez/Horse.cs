using tabuleiro;

namespace xadrez {
    class Horse : Part {
        public Horse(Board board, Color color) : base(board, color) {
        }
        public override string ToString() {
            return "H";
        }

        private bool CanMove(Position pos) {
            Part p = board.part(pos);
            return p == null || p.color != this.color;
        }

        public override bool[,] possibleMovements() {
            bool[,] mat = new bool[board.line, board.row];
            Position pos = new Position(0, 0);

            pos.setValue(position.line - 1, position.row - 2);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            pos.setValue(position.line - 2, position.row - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            pos.setValue(position.line - 2, position.row + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            pos.setValue(position.line - 1, position.row + 2);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            pos.setValue(position.line + 1, position.row + 2);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            pos.setValue(position.line + 2, position.row + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            pos.setValue(position.line + 2, position.row + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            pos.setValue(position.line + 2, position.row - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            pos.setValue(position.line + 1, position.row - 2);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            //NO
            pos.setValue(position.line - 1, position.row - 1);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
                if (board.part(pos) != null && board.part(pos).color != this.color) {
                    break;
                }
                pos.setValue(pos.line - 1, pos.row - 1);

            }
            //NE
            pos.setValue(position.line - 1, position.row + 1);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
                if (board.part(pos) != null && board.part(pos).color != this.color) {
                    break;
                }
                pos.setValue(pos.line - 1, pos.row + 1);

            }

            //SE
            pos.setValue(position.line + 1, position.row + 1);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
                if (board.part(pos) != null && board.part(pos).color != this.color) {
                    break;
                }
                pos.setValue(pos.line + 1, pos.row + 1);

            }

            //SO
            pos.setValue(position.line + 1, position.row - 1);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
                if (board.part(pos) != null && board.part(pos).color != this.color) {
                    break;
                }
                pos.setValue(pos.line + 1, pos.row - 1);

            }

            return mat;
        }
    }
}
