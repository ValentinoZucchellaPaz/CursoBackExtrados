CREATE TABLE usuarios (
	id INT PRIMARY KEY AUTO_INCREMENT,
	nombre VARCHAR(20) NOT NULL,
	edad INT NOT NULL,
	mail VARCHAR(30) NOT NULL,
	fecha_baja VARCHAR(20) -- 17 OCT. 2024 17:00
)
DROP TABLE usuarios;

SELECT * FROM usuarios;

INSERT INTO Usuarios (nombre, edad, mail) VALUES ("Valentino", 21, "v@g");
INSERT INTO Usuarios (nombre, edad, mail) VALUES ("Simon", 31, "s@g");
INSERT INTO Usuarios (nombre, edad, mail) VALUES ("Rain", 20, "r@g");
INSERT INTO Usuarios (nombre, edad, mail) VALUES ("Cristian", 28, "c@g");
INSERT INTO Usuarios (nombre, edad, mail) VALUES ("Rodrigo", 22, "ro@g");
INSERT INTO Usuarios (nombre, edad, mail) VALUES ("Agustin", 24, "a@g");


CREATE USER "tarea_3_user" IDENTIFIED BY "tarea_3_user";
GRANT SELECT, INSERT, DELETE, UPDATE ON usuarios TO 'tarea_3_user';


DROP USER tarea_3_user;

-- cambiar fecha baja a alguna fecha