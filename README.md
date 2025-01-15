# Trabajo final backend  
  
## Endpoints:  
  
### /Cartas:  
- `GET` **/cartas**  
Una lista de todas las cartas
- `GET` **/cartas/{id-name}**  
Informacion de la carta con ese id o nombre
- `GET` **/cartas/series**  
Una lista de las series de cartas
- `GET` **/cartas/series/{id-name}**  
Informacion de la serie con ese id o nombre
- `GET` **/cartas/serie/{id-name}/all**  
Una lista de las cartas que pertenecen a esa serie  
  
### /Auth  
- `POST` **/sign-up**  
Se pasa todos los datos del usuario, de los cuales el rol debe ser uno del enum {admin, juez, organizador, jugador}
- `POST` **/login**  
Se pasa email y contraseña, se devuelve un access token que dura 1 dia con otra información util. Ademas se devuelve una cookie con el token y de duración un dia. 
