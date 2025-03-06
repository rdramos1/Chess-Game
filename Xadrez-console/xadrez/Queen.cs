using tabuleiro;

namespace xadrez {
    class Queen : Part {
        public Queen(Board board, Color color) : base(board, color) {
        }
        public override string ToString() {
            return "Q";
        }

        private bool CanMove(Position pos) {
            Part p = board.part(pos);
            return p == null || p.color != this.color;
        }

        public override bool[,] possibleMovements() {
            bool[,] mat = new bool[board.line, board.row];
            Position pos = new Position(0, 0);

            //acima
            pos.setValue(position.line - 1, position.row);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
                if (board.part(pos) != null && board.part(pos).color != this.color) {
                    break;
                }
                pos.line = pos.line - 1;

            }
            //direita
            pos.setValue(position.line, position.row + 1);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
                if (board.part(pos) != null && board.part(pos).color != this.color) {
                    break;
                }
                pos.row = pos.row + 1;

            }

            //esquerda
            pos.setValue(position.line, position.row - 1);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
                if (board.part(pos) != null && board.part(pos).color != this.color) {
                    break;
                }
                pos.row = pos.row - 1;

            }

            //baixo
            pos.setValue(position.line + 1, position.row);
            while (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
                if (board.part(pos) != null && board.part(pos).color != this.color) {
                    break;
                }
                pos.line = pos.line + 1;

            }

            return mat;
        }
    }
}
