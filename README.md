## Tarea 3  
Una aplicacion de consola simple CRUD que se conecta a una base de datos y puede crear, leer, actualizar y borrar (borrado lógico) usuarios.  
La conección a la base de datos se hace mediante un `DAO`, mientras que la lógica de la aplicacion de consola es manejada por `Program.cs` y `ProgramHandler.cs` el cual hace las llamadas a las funciones del DAO para obtener y mostrar la informacion de la base de datos.  
![Tarea3 menu](/assets/Tarea%203%20consola%20menu.png)  
  
## Tarea 4
Se ha implementado la misma base de datos que en la tarea 3, con el único cambio de que todo el codigo de manera esta en ingles.  
Ahora, en vez de llamar a la base de datos con una aplicacion de consolta, se implemento una API la cual mediante postman se puede crear, leer, actualizar y eliminar usuarios de la base de datos. Además, el DAO que maneja la coneccion con la tabla de usuarios de la base de datos, ahora es un singleton.  
También se implementa un borrado lógico (actualizando el campo unsub_date de null a la fecha que se eliminó), por esta razon, cuando se quiere eliminar o actualizar un campo (como también crear), se usa el metodo `POST` y solo para la busqueta de usuarios el metodo `GET`.  
Por último, se crearon modelos para las consultas POST, los cuales se encuentran en la biblioteca de clases DAO&Entidades  
