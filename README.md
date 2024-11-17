## Tarea 1  
Codigo con la funcion recursiva `colocarPieza(int col)` y la logica de validacion `esValido(int col)` dentro de la clase Reina1  
Se uso un array para representar el tablero, donde el indice es la columna y el valor la fila que ocupa la pieza  
Luego se usa una funcion `imprimirTablero()` para hacer la impresion cuando `colocarPieza(int col)` encontro una solucion. Cabe destacar que el codigo termina una vez se encontro una solucion y no sigue buscando otras.  
Por defecto se comienza por la primer columna, este el output de la funcion  
![Tarea1 output](/assets/Tarea%201%20consola.png)  
  
## Tarea 2  
Todas las clases se encuentran en la biblioteca `piezasAjedrez`  
Se tiene una clase abstracta `Pieza` con un metodo `EsValido(int col, int[] tablero)` el cual determina, dado el tablero pasado, si la posicion (columna, tablero[ columna ]) en la que se quiere colocar la pieza es valida, ademas de un atributo `char InicialPieza`. Luego, cada pieza hace una implementacion distinta de la clase abstracta segun su naturaleza individual.  
La clase `Tablero` se le da un tamanio N y una pieza con la cual llenar el tablero. La clase implementa el metodo recursivo `ColocarPieza(int col)` y cuando encuentra una solucion se detiene e imprime el tablero.  
Por defecto se comienza por la primer columna, este el output de la funcion  
![Tarea2 output](/assets/Tarea%202%20consola.png)  