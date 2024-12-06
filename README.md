### API CRUD
## Tarea 5  
Es una version mejorada de la tarea 4, en la que se agrega una contraseña para hacer un login con mail, esta esta hasheada usando la funcion generadora de hash PBKDF2 (la cual está integrada nativamente en .NET) configurada con 600.000 iteraciones, algoritmo de hasheo SHA256 y outputLength de 64 bytes. Todo implementado por la clase `PasswordHasher`.  
Cuando se desee crear un usuario (register) se pasara nombre, edad (mayor a 14), mail (gmail), contraseña (un texto). Luego este usuario se guardará en la base de datos en donde la contraseña sera un hash y tmb se guarda el salt. Al momento de hacer un login se pasa el mail y la contraseña como texto, y el programa hace la validacion comparando con el hash y salt de la base de datos.  
Además se hicieron otras mejoras en el código con respecto a la anterior tarea, como por ejemplo que cada post devuelva un status code con un mensaje para dar informacion del error al usuario, junto con mejores en la documentacion de swagger.  

## Tarea 6
Como extension de la tarea 5, se agrego un servicio de autenticacion (`AuthService`) el cual genera un token JWT.  
Ahora, si el login es exitoso, se devuelve un token el cual será necesario para realizar las operaciones de obtener usuarios, actualizarlos y borrarlos (sin este token se devolverá un 401 Unauthorized) como se ve a continuacion:  
![Get all users error](/assets/get%20all%20error%20(401).png)  
En cambio, primero se debe hacer un login para obtener un token.  
![Login](/assets/login%20success.png)  
Una vez obtenido el token, se envia en la cabecera de la peticion como authentication, pudiendo realizar la peticion efectivamente:  
![Get all users success](/assets/get%20all%20success.png)