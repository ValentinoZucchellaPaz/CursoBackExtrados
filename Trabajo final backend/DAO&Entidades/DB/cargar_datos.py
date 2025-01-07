import requests
import mysql.connector
from pydantic import BaseModel
from typing import List, Optional
from datetime import datetime

# API TYPES
class SetPokemon(BaseModel):
    id:str
    name:str
    series:str
    releaseDate:str

class ImagePokemon(BaseModel):
    small:str

class Attack(BaseModel):
    damage:str

class Card(BaseModel):
    id: str
    name: str
    hp:str
    attacks: Optional[List[Attack]] = None
    images: ImagePokemon
    set: SetPokemon

class ApiResponsePokemon(BaseModel):
    data: List[Card]

class ApiResponseSet(BaseModel):
    data: List[SetPokemon]


def get_damage_number(attack_list: list[Attack])->int:
    res = 0
    for attack in attack_list:
        damage:str = attack.damage
        if(damage != ''):
            damage_str_parse = damage.removesuffix('+').removesuffix('×').removesuffix('x').removesuffix('-')
            damage_int= int(damage_str_parse)
            if(damage_int>res):
                res=damage_int
    return res

def fetch_pokemon_cards(endpoint: str) -> List[Card]:
    response = requests.get(endpoint)
    response.raise_for_status()  # Lanza una excepción en caso de error HTTP
    api_response = ApiResponsePokemon.model_validate(response.json(),strict=True, from_attributes=True)  # Valida los datos
    return api_response.data

def fetch_pokemon_sets(endpoint:str)->List[SetPokemon]:
    response = requests.get(endpoint)
    response.raise_for_status()  # Lanza una excepción en caso de error HTTP
    api_response = ApiResponseSet.model_validate(response.json(),strict=True, from_attributes=True)  # Valida los datos
    return api_response.data

def save_in_database(pokemon_data:List[Card], set_data:List[SetPokemon]):
    # config
    connection = mysql.connector.connect(
        host="localhost",
        user="admin",
        password="24122002",
        database="torneo_cartas"
    )
    cursor = connection.cursor()
    
    # cargar sets
    query_sets = """
    INSERT INTO sets (id, nombre, serie, fecha_salida)
    VALUES (%s, %s, %s, %s)
    """
    for set in set_data:
        cursor.execute(query_sets, (
            set.id,
            set.name,
            set.series,
            datetime.strptime(set.releaseDate, "%Y/%m/%d").date()
        ))

    # cargar pokemons que atacan
    query_pokemons = """
    INSERT INTO cartas (id, nombre, ilustracion, ataque, defensa, set_id)
    VALUES (%s, %s, %s, %s, %s, %s)
    """
    for pokemon in pokemon_data:
        if pokemon.id == 'base3-3': continue
        damage = get_damage_number(pokemon.attacks)
        if damage == 0: continue
        cursor.execute(query_pokemons, (pokemon.id, pokemon.name, pokemon.images.small, damage,int(pokemon.hp), pokemon.set.id))
        
    # limpiar sets que no son usados
    query_clean_sets = """
    DELETE FROM Sets WHERE id NOT IN (
        SELECT DISTINCT set_id FROM Cartas
    );
    """
    cursor.execute(query_clean_sets)
    
    # mandar transaccion y cerrar conexion
    connection.commit()
    cursor.close()
    connection.close()

def main():
    pokemon_data = fetch_pokemon_cards('https://api.pokemontcg.io/v2/cards')
    sets_data = fetch_pokemon_sets('https://api.pokemontcg.io/v2/sets')
    save_in_database(pokemon_data, sets_data)
    print("Datos guardados correctamente en la base de datos.")

if __name__ == "__main__":
    main()
