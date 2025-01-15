using Dapper;
using Models.DTO;
using Models.Entidades;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.DAOAuth
{
    public class DAOUsuario(string connectionString): IDAOUsuario
    {
        private string _connectionString = connectionString;
        public async Task<int?> CrearUsuarioAsync(string nombre, string pais, string email, string contraseña, string salt, string role, int? id_creador, string? alias, string? avatar)
        {
            var query = @"insert into usuarios (nombre, pais, email, contraseña, salt, role, id_creador, alias, avatar)
                        values (@nombre, @pais, @email, @contraseña, @salt, @role, @id_creador, @alias, @avatar);
                        select last_insert_id();";
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var res = await conn.QuerySingleAsync<int>(query, new { nombre, pais, email, contraseña, salt, role, id_creador, alias, avatar });
                if (res == 0)
                {
                    return null;
                }
                return res;
            }
        }

        public async Task<Usuario?> GetUsuarioPorMailAsync(string email)
        {
            var query = "select * from usuarios where usuarios.email = @email";
            using (var conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<Usuario>(query, new { email });
            }
        }
    }
}
