namespace xadrez {
    class xadrezPosition {

        public char row { get; set; }
        public int line { get; set; }   

        public xadrezPosition(char row, int line) {
            this.row = row;
            this.line = line;
        }

        override public string ToString() {
            return "" + row + line;
        }

    }
}
