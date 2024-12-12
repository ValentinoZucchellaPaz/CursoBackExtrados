## Tarea 7: Cors, options, DI  
  
En esta tarea se implemento el uso de Cors, Options para obtener la configuracion de los token JWT desde appsettings.json, y además Inyección de dependencias para crear un singleton del Dao (que consume el connection string directo del archivo de config) como también de los services que se pasan como parametros al controlador (el cual hace instancias de estos para poder realizar las acciones de la api).  
El funcionamiento de la api es sencillo, se puede obtener, crear, actualizar y borrar usuarios siempre y cuando se esté logeado, de lo contrario se muestrara un 401, como se puede ver en la imagen a continuacion (notar el uso de la cabecera origins para el cors):
  ![Peticion GET fallida por token vencido](/assets/Get%20all%20fallido%20(401).png)  
  Primero debo hacer login, lo que me retorna el token con el que tengo que mandar todas las peticiones para realizar las distintas peticiones (esto ademas me devuelve algo de informacion del usuario)  
  ![Login](/assets/Post%20login%20(200).png)  
  Finalmente realizo la consulta sin problemas  
  ![Peticion GET exitosa](/assets/Get%20all%20(200).png)

## Tarea 8: Roles, DateTime, Book's endpoint  
  
Se realizaron cambios para que se recupere la informacion del usuario loggeado desde HttpContextAccesor, por lo que los cambios sobre usuarios (update) se harán sobre el usuario loggeado, lo mismo para alquilar un libro (se pone a nombre del usuario loggeado). Esto trae una desventaja con las peticiones en postman debido a que se tiene que cambiar el token de validacion que se le pasa a cada peticion (debe ser recibido cuando se loggee, si es de otro login se estaría usando otra cuenta por asi decirlo).  
Además, se cambio la estructura de la base de datos para agregar roles de usuario, en el que solo los 'user' podrán alquilar libros y solo los 'admin' podrán eliminar usuarios. También se deja de trabajar con string para las fechas, se usa `DateTime` en la API y `TIMESTAMP` en la base de datos (todas las fechas estan en UTC).  
**Nuevos endpoints: users/book - users/rent-book.** En el primero se pueden visualizar todos los libros (hardcodeados desde db) y en el segundo se pueden alquilar a nombre del usuario loggeado. Se alquilan por 5 dias y solo se pueden alquilar si no estan ya alquilados.  
  

## Extra  
Se implemento una nueva estrategia para lanzar excepciones predeterminadas y personalizadas desde cualquier punto de la api sin tener ningun try-catch en el controller u otra parte de la api.  
Se uso un custom middleware que maneja todas las excepciones de la api y devuelve una respuesta siempre. Dando así mayor entendimiento de los errores lanzados al usuario.  
**Asincronía:** se agrego asincronia en todos los metodos, asi toda llamada a la base de datos será asincrónica.