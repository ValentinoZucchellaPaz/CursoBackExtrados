import requests
import mysql.connector

def is_base_pokemon(evolution_chain_url):
    """Verifica si el Pokémon es de la forma base (sin evoluciones)."""
    evolution_response = requests.get(evolution_chain_url)
    if evolution_response.status_code == 200:
        evolution_chain_data = evolution_response.json()
        # Si el Pokémon no tiene ninguna evolución (evolves_to vacío), es un Pokémon base
        if not evolution_chain_data["chain"]["evolves_to"]:
            return True
    return False

def fetch_base_pokemon_data():
    base_url = "https://pokeapi.co/api/v2/generation/"
    pokemon_data = []

    for generation_id in range(1, 9):  # Desde la generación 1 hasta 8
        print(f"Procesando generación {generation_id}...")
        generation_url = f"{base_url}{generation_id}/"
        generation_response = requests.get(generation_url)

        if generation_response.status_code == 200:
            generation_data = generation_response.json()

            for species in generation_data["pokemon_species"]:
                species_response = requests.get(species["url"])
                if species_response.status_code == 200:
                    species_data = species_response.json()
                    evolution_chain_url = species_data["evolution_chain"]["url"]
                    
                    # Verificar si el Pokémon es base (sin evolución)
                    if is_base_pokemon(evolution_chain_url):
                        pokemon_response = requests.get(f"https://pokeapi.co/api/v2/pokemon/{species_data['name']}/")
                        
                        if pokemon_response.status_code == 200:
                            pokemon_details = pokemon_response.json()
                            pokemon_data.append({
                                "nombre": species_data["name"],
                                "ilustracion": pokemon_details["sprites"]["front_default"],
                                "ataque": pokemon_details["stats"][1]["base_stat"],
                                "defensa": pokemon_details["stats"][2]["base_stat"],
                                "generacion": generation_id
                            })
    return pokemon_data

def save_pokemons_to_database(pokemon_data):
    connection = mysql.connector.connect(
        host="localhost",
        user="admin",
        password="24122002",
        database="torneo_cartas"
    )
    cursor = connection.cursor()

    query = """
    INSERT INTO PokemonBase (nombre, ilustracion, ataque, defensa, generacion)
    VALUES (%s, %s, %s, %s, %s)
    """
    for pokemon in pokemon_data:
        cursor.execute(query, (pokemon["nombre"], pokemon["ilustracion"], pokemon["ataque"], pokemon["defensa"], pokemon["generacion"]))
    connection.commit()
    cursor.close()
    connection.close()



def save_series_to_database():
    connection = mysql.connector.connect(
        host="localhost",
        user="admin",
        password="24122002",
        database="torneo_cartas"
    )
    cursor = connection.cursor()

    query = """
    INSERT INTO Generacion (nombre, fecha_de_salida)
    VALUES (%s, %s)
    """
    cursor.execute(query, ("Generacion 1", "27/2/1996"))
    cursor.execute(query, ("Generacion 2", "21/11/1999"))
    cursor.execute(query, ("Generacion 3", "21/11/2002"))
    cursor.execute(query, ("Generacion 4", "28/9/2006"))
    cursor.execute(query, ("Generacion 5", "29/1/2010"))
    cursor.execute(query, ("Generacion 6", "8/1/2013"))
    cursor.execute(query, ("Generacion 7", "27/2/2016"))
    cursor.execute(query, ("Generacion 8", "27/2/2019"))
    
    connection.commit()
    cursor.close()
    connection.close()


def main():
    pokemon_data = fetch_base_pokemon_data()
    save_pokemons_to_database(pokemon_data)
    save_series_to_database()
    # print(pokemon_data)
    print("Datos guardados correctamente en la base de datos.")

if __name__ == "__main__":
    main()
