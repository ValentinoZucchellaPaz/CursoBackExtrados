using piezasAjedrez;

Alfil a = new Alfil();
Reina r = new Reina();
Rey R = new Rey();
Peon p = new Peon();
Torre t = new Torre();
Caballo c = new Caballo();

Tablero tablero8Reinas = new Tablero(8, r);
tablero8Reinas.ColocarPieza();

Tablero tablero8Torres = new Tablero(8, t);
tablero8Torres.ColocarPieza();

Tablero tablero8Alfiles = new Tablero(8, a);
tablero8Alfiles.ColocarPieza();

Tablero tablero8Peones = new Tablero(8, p);
tablero8Peones.ColocarPieza();

Tablero tablero8Reyes = new Tablero(8, R);
tablero8Reyes.ColocarPieza();

Tablero tablero8Caballos = new Tablero(8, c);
tablero8Caballos.ColocarPieza();