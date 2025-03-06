using tabuleiro;

namespace xadrez {
    class King: Part{
        private ChessMatch match;

        public King(Board board, Color color, ChessMatch match) : base(board, color) {
            this.match = match;
        }
        public override string ToString() {
            return "K";
        }

        private bool CanMove(Position pos) {
            Part p = board.part(pos);
            return p == null || p.color != this.color;
        }
        private bool testRookCastling(Position pos) {
            Part p = board.part(pos);
            return p != null && p is Tower && p.color == color && p.moves == 0;
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

            // #Special move Castling
            if(moves == 0 && !match.check) {
                // #Special move Castling Kingside Rook
                Position posT1 = new Position(position.line, position.row + 3);
                if (testRookCastling(posT1)) {
                    Position p1 = new Position(position.line, position.row + 1);
                    Position p2 = new Position(position.line, position.row + 2);
                    if (board.part(p1) == null && board.part(p2) == null) {
                        mat[position.line, position.row + 2] = true;
                    }
                }
                // #Special move Castling Queenside Rook
                Position posT2 = new Position(position.line, position.row - 4);
                if (testRookCastling(posT2)) {
                    Position p1 = new Position(position.line, position.row - 1);
                    Position p2 = new Position(position.line, position.row - 2);
                    Position p3 = new Position(position.line, position.row - 3);
                    if (board.part(p1) == null && board.part(p2) == null && board.part(p3) == null) {
                        mat[position.line, position.row - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
