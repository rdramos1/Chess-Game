using tabuleiro;

namespace xadrez {
    class King: Part{
        public King(Board board, Color color) : base(board, color) {
        }
        public override string ToString() {
            return "K";
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
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            //ne
            pos.setValue(position.line - 1, position.row + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            //direita
            pos.setValue(position.line, position.row + 1 );
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            //se
            pos.setValue(position.line + 1, position.row + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            //baixo
            pos.setValue(position.line + 1, position.row);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            //so
            pos.setValue(position.line + 1, position.row - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            //esquerda
            pos.setValue(position.line, position.row - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            //no
            pos.setValue(position.line - 1, position.row - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) {
                mat[pos.line, pos.row] = true;
            }

            return mat;
        }
    }
}
