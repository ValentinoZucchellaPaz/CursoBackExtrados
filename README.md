# Trabajo final backend  
  
## Endpoints:  
  
### /Info: obten informacion de cartas torneos y usuarios  
- `GET` **/cartas**  
Una lista de todas las cartas
- `GET` **/cartas/{id-name}**  
Informacion de la carta con ese id o nombre
- `GET` **/series**  
Una lista de las series de cartas
- `GET` **/series/{id-name}**  
Informacion de la serie con ese id o nombre
- `GET` **/serie/{id-name}/cartas**  
Una lista de las cartas que pertenecen a esa serie  
- `GET` **/torneos**  
Una lista de todos los torneos (pasados y activos)  
- `GET` **/torneos/{id}**  
TODA la informacion del torneo (series, cartas, juegos, jugadores inscriptos, jueces)  
- `GET` **/usuarios/jugadores**  
Una lista de las cartas que pertenecen a esa serie  
- `GET (PROTECTED: juez, organizador, admin)` **/usuarios/jueces**  
Una lista de las cartas que pertenecen a esa serie  
- `GET (PROTECTED: organizador, admin)` **/usuarios/organizadores**  
Una lista de las cartas que pertenecen a esa serie  
- `GET (PROTECTED): admin` **/usuarios/admins**  
Una lista de las cartas que pertenecen a esa serie  
  
### /Auth: administra usuarios en db  
- `POST` **/sign-up**  
Se pasa todos los datos del usuario, de los cuales el rol debe ser uno del enum {admin, juez, organizador, jugador}
- `POST` **/login**  
Se pasa email y contraseña, se devuelve un access token que dura 1 dia con otra información util. Ademas se devuelve una cookie con el token y de duración un dia. 
- `POST (PROTECTED: admin)` **/borrar-usuario**  
Borrado logico del usuario en db 
- `POST (PROTECTED: organizador, admin)` **/crear-juez**  
Se crea un juez con id_usuario del JWT enviado en la cabecera de autenticación. 
- `POST (PROTECTED: admin)` **/crear-organizador**  
Se crea un organizador con id_usuario del JWT enviado en la cabecera de autenticación. 
- `POST (PROTECTED: admin)` **/crear-admin**  
Se crea un admin con id_usuario del JWT enviado en la cabecera de autenticación. 
  
### /Torneo: administra el torneo  
- `POST (PROTECTED: jugador)` **{id}/inscribir-jugador**  
Un jugador se inscribe junto a su mazo para estar en la lista de espera del torneo con esa id
- `POST (PROTECTED: organizador)` **{id}/iniciar**  
El organizador del torneo acepta la lista de inscriptos y da comienzo del torneo con esa id  
- `POST (PROTECTED: organizador)` **{id}/editar**  
El organizador del torneo puede editar el torneo con esa id (cambiar instancia)  
- `POST (PROTECTED: admin)` **{id}/cancelar**  
El admin puede cancelar el torneo con esa id  