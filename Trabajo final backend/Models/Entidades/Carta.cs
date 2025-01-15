﻿namespace Models.Entidades
{
    public class Carta(int id, string nombre, string ilustracion, int ataque, int defensa)
    {
        public required int Id { get; set; } = id;
        public required string Nombre { get; set; } = nombre;
        public required string Ilustracion { get; set; } = ilustracion;
        public required int Ataque {  get; set; } = ataque;
        public required int Defensa { get; set; } = defensa;
    }
}
