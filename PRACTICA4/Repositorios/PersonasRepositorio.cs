using Microsoft.Extensions.Configuration;
using PRACTICA4.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PRACTICA4.Repositorios
{
    public class PersonasRepositorio : IPersonaRepositorio
    {
        private readonly IConfiguration _configuration;

        public PersonasRepositorio(IConfiguration configuration )
        {
            _configuration = configuration;
        }
        public async Task<List<PersonaModel>> ObtenerTodasLasPersonas()
        {
            var lista = new List<PersonaModel>();
            string connectionString = _configuration.GetConnectionString("Defaul");
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_obtener_personas" , sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sql.Open();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var nuevoItem = new PersonaModel()
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString()
                    };

                    lista.Add(nuevoItem);
                }
            }

                return lista;
        }

        public async  Task GuardarPersona(PersonaCreacionModel model)
        {
            string connectionString = _configuration.GetConnectionString("Defaul");
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_guardar_persona", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@nombre", model.Nombre));
            sql.Open();
            await cmd.ExecuteNonQueryAsync();
        }


        public async Task<PersonaEdicionModel> ObtenerPersonasPorId(int id)
        {
            var persona = new PersonaEdicionModel();
            string connectionString = _configuration.GetConnectionString("Defaul");
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_obtener_persona_por_id", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            sql.Open();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var nuevoItem = new PersonaEdicionModel()
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString()
                    };

                    persona = nuevoItem;
                }
            }

            return persona;
        }

        public async Task ActualizarPersona(PersonaEdicionModel model)
        {
            string connectionString = _configuration.GetConnectionString("Defaul");
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_actualizar_persona", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", model.Id));
            cmd.Parameters.Add(new SqlParameter("@nombre", model.Nombre));
            sql.Open();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task EliminarPersona(int id)
        {
            string connectionString = _configuration.GetConnectionString("Defaul");
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_eliminar_persona", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            sql.Open();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
