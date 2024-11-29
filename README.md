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
  
## Tarea 3  
Una aplicacion de consola simple CRUD que se conecta a una base de datos y puede crear, leer, actualizar y borrar (borrado lógico) usuarios.  
La conección a la base de datos se hace mediante un `DAO`, mientras que la lógica de la aplicacion de consola es manejada por `Program.cs` y `ProgramHandler.cs` el cual hace las llamadas a las funciones del DAO para obtener y mostrar la informacion de la base de datos.  
![Tarea3 menu](/assets/Tarea%203%20consola%20menu.png)  
  
## Tarea 4
Se ha implementado la misma base de datos que en la tarea 3, con el único cambio de que todo el codigo de manera esta en ingles.  
Ahora, en vez de llamar a la base de datos con una aplicacion de consolta, se implemento una API la cual mediante postman se puede crear, leer, actualizar y eliminar usuarios de la base de datos. Además, el DAO que maneja la coneccion con la tabla de usuarios de la base de datos, ahora es un singleton.  
También se implementa un borrado lógico (actualizando el campo unsub_date de null a la fecha que se eliminó), por esta razon, cuando se quiere eliminar o actualizar un campo (como también crear), se usa el metodo `POST` y solo para la busqueta de usuarios el metodo `GET`.  
Por último, se crearon modelos para las consultas POST, los cuales se encuentran en la biblioteca de clases DAO&Entidades  
  
## Tarea 5  
Es una version mejorada de la tarea 4, en la que se agrega una contraseña para hacer un login con mail, esta esta hasheada usando la funcion generadora de hash PBKDF2 (la cual está integrada nativamente en .NET) configurada con 600.000 iteraciones, algoritmo de hasheo SHA256 y outputLength de 64 bytes. Todo implementado por la clase `PasswordHasher`.  
Cuando se desee crear un usuario (register) se pasara nombre, edad (mayor a 14), mail (gmail), contraseña (un texto). Luego este usuario se guardará en la base de datos en donde la contraseña sera un hash y tmb se guarda el salt. Al momento de hacer un login se pasa el mail y la contraseña como texto, y el programa hace la validacion comparando con el hash y salt de la base de datos.  
Además se hicieron otras mejoras en el código con respecto a la anterior tarea, como por ejemplo que cada post devuelva un status code con un mensaje para dar informacion del error al usuario, junto con mejores en la documentacion de swagger.  

## Tarea 6
Como extension de la tarea 5, se agrego un servicio de autenticacion (`AuthService`) el cual genera un token JWT.  
Ahora, si el login es exitoso, se devuelve un token el cual será necesario para realizar las operaciones de obtener usuarios, actualizarlos y borrarlos (sin este token se devolverá un 401 Unauthorized).