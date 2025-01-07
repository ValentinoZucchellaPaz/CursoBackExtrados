-- CARTAS Y SERIES
CREATE TABLE Sets(
	id VARCHAR(15) PRIMARY KEY,
	nombre VARCHAR(50) NOT NULL,
	serie VARCHAR(30) NOT NULL,
	fecha_salida DATE NOT NULL
);
CREATE TABLE Cartas (
   id VARCHAR(10) PRIMARY KEY,
   nombre VARCHAR(100) NOT NULL,
   ilustracion VARCHAR(100) NOT NULL,
   ataque INT NOT NULL,
   defensa INT NOT NULL,
   set_id VARCHAR(30) NOT NULL,
   
	FOREIGN KEY (set_id) REFERENCES Sets(id)
);

SELECT * FROM cartas;
SELECT * FROM sets;

DROP TABLE cartas;
DROP TABLE sets;

-- USUARIOS Y ROLES: jugador, juez, organizador, administrador
CREATE TABLE Usuarios (
	id INT AUTO_INCREMENT PRIMARY KEY,
	nombre VARCHAR(100) NOT NULL,
	pais VARCHAR(50) NOT NULL,
	email VARCHAR(100) NOT NULL
);

CREATE TABLE Jugador (
	id_usuario INT PRIMARY KEY,
	alias VARCHAR(50) UNIQUE,
	avatar VARCHAR(100) NOT NULL, -- foto de usuario
	juegos_ganados INT NOT NULL,
	juegos_perdidos INT NOT NULL,
	torneos_ganados INT NOT NULL,
	descalificaciones JSON, -- {torneos: [{id_torneo, descripcion}]}
	
	FOREIGN KEY (id_usuario) REFERENCES Usuarios(id)
);

CREATE TABLE Juez(
	id_usuario PRIMARY KEY,
	avatar VARCHAR(100) NOT NULL,
	alias VARCHAR(50) UNIQUE,
	torneos_oficializados JSON, -- {torneos: [id_torneo]}
	
	FOREIGN KEY (id_usuario) REFERENCES Usuarios(id)
);

CREATE TABLE Organizador(
	id_usuario PRIMARY KEY,
	torneos_organizados JSON, -- {torneos: [id_torneo]}
	
	FOREIGN KEY (id_usuario) REFERENCES Usuarios(id)
);

CREATE TABLE Administrador(
	id_usuario PRIMARY KEY,
	
	FOREIGN KEY (id_usuario) REFERENCES Usuarios(id)
);



-- TORNEOS Y JUEGOS
