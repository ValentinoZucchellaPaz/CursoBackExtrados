## Tarea 7: Cors, options, DI  
  
En esta tarea se implemento el uso de Cors, Options para obtener la configuracion de los token JWT desde appsettings.json, y además Inyección de dependencias para crear un singleton del Dao (que consume el connection string directo del archivo de config) como también de los services que se pasan como parametros al controlador (el cual hace instancias de estos para poder realizar las acciones de la api).  
El funcionamiento de la api es sencillo, se puede obtener, crear, actualizar y borrar usuarios siempre y cuando se esté logeado, de lo contrario se muestrara un 401, como se puede ver en la imagen a continuacion (notar el uso de la cabecera origins para el cors):
  ![Peticion GET fallida por token vencido](/assets/Get%20all%20fallido%20(401).png)  
  Primero debo hacer login, lo que me retorna el token con el que tengo que mandar todas las peticiones para realizar las distintas peticiones (esto ademas me devuelve algo de informacion del usuario)  
  ![Login](/assets/Post%20login%20(200).png)  
  Finalmente realizo la consulta sin problemas  
  ![Peticion GET exitosa](/assets/Get%20all%20(200).png)