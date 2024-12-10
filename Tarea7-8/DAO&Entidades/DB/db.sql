DROP DATABASE IF EXISTS tarea5;
CREATE DATABASE tarea5;
USE tarea5;

DROP TABLE IF EXISTS libros;
DROP TABLE IF EXISTS usuarios;

CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    age INT NOT NULL CHECK (age > 0 AND age <= 120), -- Validación de rango de edad
    mail VARCHAR(30) NOT NULL UNIQUE, -- Correo único
    password CHAR(128) NOT NULL,
    salt CHAR(128) NOT NULL,
    unsub_date TIMESTAMP,
    role VARCHAR(10) NOT NULL CHECK (ROLE IN ('admin', 'user')) -- Validación de rol
);

CREATE TABLE libros (
	id INT AUTO_INCREMENT PRIMARY KEY,
	name VARCHAR(50) UNIQUE NOT NULL,
	rent_date TIMESTAMP,
	expiration_date TIMESTAMP,
	user_id INT,
	
	FOREIGN KEY (user_id) REFERENCES usuarios(id)
);

INSERT INTO libros (name) VALUES("libro1"),("libro3"), ("libro4"), ("libro5");

DROP USER IF EXISTS tarea_5_user;
CREATE USER tarea_5_user IDENTIFIED BY 'tarea_5_user';

GRANT SELECT, INSERT, DELETE, UPDATE ON usuarios TO tarea_5_user;
GRANT SELECT, INSERT, DELETE, UPDATE ON libros TO tarea_5_user;

SELECT * FROM libros;
SELECT * FROM usuarios;
