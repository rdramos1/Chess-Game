using tabuleiro;

namespace tabuleiro {
    class Board {

        public int line { get; set; }
        public int row { get; set; }
        private Part[,] parts;

        public Board(int line, int row) {
            this.line = line;
            this.row = row;
            parts = new Part[line, row];
        }

        public Part part(int line, int row) {
            return parts[line, row];
        }

        public Part part(Position pos) {
            return parts[pos.line, pos.row];
        }
        
        public bool PartExists(Position pos) {
            ValidatePosition(pos);
            return part(pos) != null;
        }

        public void PutPart(Part p, Position pos) {
            if (PartExists(pos)) {
                throw new BoardException("There is already a part in that position!");
            } else {
                parts[pos.line, pos.row] = p;
                p.position = pos;
            }
        }

        public Part RemovePart(Position pos) {
            if (part(pos) == null) {
                return null;
            }
            Part aux = part(pos);
            aux.position = null;
            parts[pos.line, pos.row] = null;
            return aux;
        }

        public bool ValidPosition(Position pos) {
            if (pos.line < 0 || pos.line >= line || pos.row < 0 || pos.row >= row) {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos) {
            if (!ValidPosition(pos)) {
                throw new BoardException("Invalid position!");
            }
        }

    }
}
