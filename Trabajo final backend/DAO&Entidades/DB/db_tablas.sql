DROP DATABASE IF EXISTS torneo_cartas;
CREATE DATABASE torneo_cartas;
USE torneo_cartas;

DROP TABLE if EXISTS cartas_por_serie;
DROP TABLE if EXISTS cartas;
DROP TABLE if EXISTS series;

-- CARTAS Y SERIES
CREATE TABLE Cartas (
   id INT PRIMARY KEY AUTO_INCREMENT,
   nombre VARCHAR(100) NOT NULL,
   ilustracion VARCHAR(100) NOT NULL,
   ataque INT NOT NULL,
   defensa INT NOT NULL
);

CREATE TABLE Series (
	id INT PRIMARY KEY AUTO_INCREMENT,
	nombre VARCHAR(100) NOT NULL,
	fecha_salida DATE NOT NULL
);

CREATE TABLE cartas_por_serie (
	id_carta INT NOT NULL,
	id_serie INT NOT NULL,
	
	PRIMARY KEY(id_carta, id_serie),
	FOREIGN KEY (id_carta) REFERENCES cartas(id),
	FOREIGN KEY (id_serie) REFERENCES series(id)
);


-- MAZOS
CREATE TABLE Mazos(
	id INT AUTO_INCREMENT PRIMARY KEY,
	id_usuario INT NOT NULL,
	
	FOREIGN KEY (id_usuario) REFERENCES usuarios(id)
)

CREATE TABLE CartasPorMazo (
	id INT PRIMARY KEY AUTO_INCREMENT,
	id_carta INT NOT NULL,
	id_mazo INT NOT NULL,
	
	FOREIGN KEY (id_carta) REFERENCES cartas(id),
	FOREIGN KEY (id_mazo) REFERENCES  mazos(id),
	UNIQUE(id_mazo, id_carta) -- no se puede tener cartas duplicadas en los mazos
)

SELECT * FROM cartas;
SELECT * FROM series;
SELECT * FROM cartas_por_serie;


-- USUARIOS Y ROLES: jugador, juez, organizador, administrador
CREATE TABLE Usuarios (
	id INT AUTO_INCREMENT PRIMARY KEY,
	nombre VARCHAR(100) NOT NULL,
	pais VARCHAR(50) NOT NULL,
	email VARCHAR(100) NOT NULL,
	contraseña CHAR(128) NOT NULL,
	salt CHAR(128) NOT NULL,
	role ENUM('admin', 'jugador', 'juez', 'organizador') NOT NULL,
	id_creador INT NULL,
	alias VARCHAR(50) UNIQUE NULL,
	avatar VARCHAR(100) NULL,
	
	FOREIGN KEY (id_creador) REFERENCES usuarios(id)
);
INSERT INTO Usuarios (nombre, apellido, alias, email, pais, rol, foto_avatar, id_creador)
VALUES ('Admin', 'Inicial', 'admin1', 'admin1@example.com', 'Argentina', 'Administrador', NULL, NULL);


-- TORNEOS Y JUEGOS
CREATE TABLE Torneos (
	id INT PRIMARY KEY AUTO_INCREMENT,
	fecha_inicio DATETIME NOT NULL,
	fecha_fin DATETIME NOT NULL,
	pais VARCHAR(50) NOT NULL,
	fase ENUM('inscripcion', 'en curso', 'finalizado', 'cancelado') NOT NULL,
	id_ganador INT, -- solo hay ganador cuando fase='finalizado'
	id_organizador INT NOT NULL,
	
	FOREIGN KEY (id_organizador) REFERENCES usuarios(id)
	FOREIGN KEY (id_ganador) REFERENCES usuarios(id)
);

CREATE TABLE series_torneos ( -- relaciona las series disponibles para los torneos
	id INT PRIMARY KEY AUTO_INCREMENT,
	id_set VARCHAR(15) NOT NULL,
	id_torneo INT NOT NULL,
	
	FOREIGN KEY (id_torneo) REFERENCES torneos(id)
)

CREATE TABLE Juegos(
	id INT PRIMARY KEY AUTO_INCREMENT,
	fecha_inicio DATETIME NOT NULL,
	fecha_fin DATETIME NOT NULL,
	id_juez int NOT NULL,
	id_torneo INT NOT NULL,
	ronda ENUM('16', '8', '4', '2', '1') NOT NULL, -- 16avos, octavos, cuartos, semis, final
	id_jugador_a INT NOT NULL,
	id_jugador_b INT NOT NULL,
	id_ganador INT NOT NULL,
	id_descalificado INT NULL,
	razon_descalificado VARCHAR(100) NULL,
	
	FOREIGN KEY (id_torneo) REFERENCES torneos(id),
	FOREIGN KEY (id_ganador, id_jugador_a, id_jugador_b, id_descalificado) REFERENCES jugadores(id_usuario)
);

CREATE TABLE inscripcion_jugadores(
	id PRIMARY KEY AUTO_INCREMENT,
	id_jugador INT NOT NULL,
	id_torneo INT NOT NULL,
	id_mazo INT NOT NULL,
	
	FOREIGN KEY (id_jugador) REFERENCES jugadores(id_usuario),
	FOREIGN KEY (id_torneo) REFERENCES torneos(id),
	FOREIGN KEY (id_mazo) REFERENCES mazos(id)
);

CREATE TABLE inscripcion_jugadores_aceptados(
	id PRIMARY KEY AUTO_INCREMENT,
	id_jugador INT NOT NULL,
	id_torneo INT NOT NULL,
	id_mazo INT NOT NULL,
	
	FOREIGN KEY (id_jugador) REFERENCES jugadores(id_usuario),
	FOREIGN KEY (id_torneo) REFERENCES torneos(id),
	FOREIGN KEY (id_mazo) REFERENCES mazos(id)
)

CREATE TABLE jueces_oficializadores(
	id INT PRIMARY KEY AUTO_INCREMENT,
	id_juez INT NOT NULL,
	id_torneo INT NOT NULL,
	
	FOREIGN KEY (id_juez) REFERENCES jueces(id_usuario),
	FOREIGN KEY (id_torneo) REFERENCES torneos(id),
)