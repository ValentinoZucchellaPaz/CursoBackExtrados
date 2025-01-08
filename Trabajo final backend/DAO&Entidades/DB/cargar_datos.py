import requests
import random
import mysql.connector
from pydantic import BaseModel
from typing import List
from datetime import datetime

# TIPOS DE POKEMON

class Stat(BaseModel):
    name:str

class PokemonStat(BaseModel):
    base_stat: int
    stat: Stat

class PokemonSprite(BaseModel):
    front_default:str

class Pokemon(BaseModel):
    id:int
    name:str
    stats: list[PokemonStat]
    sprites: PokemonSprite

class PokemonSpecies(BaseModel):
    name:str
    url:str

class ApiRes(BaseModel):
    results: List[PokemonSpecies]

class DbPokemon(BaseModel):
    id:int
    nombre:str
    ilustracion:str
    ataque:int
    defensa:int

# TIPOS DE SERIES

class PokeSet(BaseModel):
    id:str
    name: str
    series: str
    releaseDate: str

class SetApiRes(BaseModel):
    data: List[PokeSet]

class DbSerie(BaseModel):
    nombre:str
    fecha_salida: str


def fetch_1st_gen()->List[DbPokemon]:
    res = requests.get('https://pokeapi.co/api/v2/pokemon-species/?limit=151')
    res.raise_for_status()
    api_res = ApiRes.model_validate(res.json())
    db_pokemons:List[DbPokemon] = []
    for pokemon in api_res.results:
        poke_res = requests.get(f'https://pokeapi.co/api/v2/pokemon/{pokemon.name}')
        poke_res.raise_for_status()
        pokemon = Pokemon.model_validate(poke_res.json())
        db_pokemons.append(DbPokemon(
            id=pokemon.id,
            nombre=pokemon.name,
            ilustracion=pokemon.sprites.front_default,
            ataque=pokemon.stats[1].base_stat,
            defensa=pokemon.stats[2].base_stat
        ))
        
    return db_pokemons

def fetch_series():
    res=requests.get('https://api.pokemontcg.io/v2/sets')
    res.raise_for_status()
    api_res = SetApiRes.model_validate(res.json())
    db_series_names: List[str] = []
    db_series: List[DbPokemon] = []
    for set in api_res.data:
        serie = DbSerie(nombre=set.series, fecha_salida=set.releaseDate)
        if serie.nombre not in db_series_names:
            db_series_names.append(serie.nombre)
            db_series.append(serie)
    return db_series

def generate_cartas_de_serie():
    # db config
    connection = mysql.connector.connect(
        host="localhost",
        user="admin",
        password="24122002",
        database="torneo_cartas"
    )
    cursor = connection.cursor()
    query = """
    INSERT INTO cartas_por_serie (id_carta, id_serie)
    VALUES (%s, %s)
    """

    # Lista de IDs de cartas (1 a 151)
    cartas = list(range(1, 152))
    # Lista de IDs de series (1 a 10)
    series = list(range(1, 11))
    # Número de cartas por serie
    cartas_por_serie = 30

    # Generar asignaciones
    for id_serie in series:
        # Seleccionar 30 cartas únicas aleatorias para esta serie
        cartas_seleccionadas = random.sample(cartas, cartas_por_serie)
        # Agregar las tuplas (id_serie, id_carta) a la lista de asignaciones
        for id_carta in cartas_seleccionadas:
            cursor.execute(query, (id_carta, id_serie))

    # mandar transaccion y cerrar conexion
    connection.commit()
    cursor.close()
    connection.close()

def save_in_database(pokemon_data:List[DbPokemon], series_data:List[DbSerie]):
    # config
    connection = mysql.connector.connect(
        host="localhost",
        user="admin",
        password="24122002",
        database="torneo_cartas"
    )
    cursor = connection.cursor()
    
    # cargar pokemons
    query_pokemons = """
    INSERT INTO cartas (nombre, ilustracion, ataque, defensa)
    VALUES (%s, %s, %s, %s)
    """
    for pokemon in pokemon_data:
        cursor.execute(query_pokemons, (pokemon.nombre, pokemon.ilustracion, pokemon.ataque, pokemon.defensa))

    # cargar series
    query_series="""
    INSERT INTO series (nombre, fecha_salida)
    VALUES (%s, %s)
    """
    for serie in series_data:
        cursor.execute(query_series, (serie.nombre, datetime.strptime(serie.fecha_salida, "%Y/%m/%d").date()))
    
    
    # mandar transaccion y cerrar conexion
    connection.commit()
    cursor.close()
    connection.close()


def main():
    series = fetch_series()
    pokemon = fetch_1st_gen()
    save_in_database(pokemon_data=pokemon, series_data=series[:10])
    generate_cartas_de_serie()

if __name__ == "__main__":
    main()
