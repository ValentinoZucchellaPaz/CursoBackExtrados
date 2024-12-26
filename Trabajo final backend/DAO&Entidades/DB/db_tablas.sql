CREATE TABLE PokemonBase (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(255) NOT NULL,
    ilustracion VARCHAR(255) NOT NULL,
    ataque INT NOT NULL,
    defensa INT NOT NULL,
    generacion INT NOT NULL,
    
    FOREIGN KEY (generacion) REFERENCES generacion(id)
);

CREATE TABLE Generacion (
	id INT AUTO_INCREMENT PRIMARY KEY,
	nombre VARCHAR(20) NOT NULL,
	fecha_de_salida VARCHAR(20) NOT NULL
);

SELECT * FROM pokemonbase;
SELECT * FROM generacion;