using PRACTICA4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRACTICA4.Repositorios
{
    public interface IPersonaRepositorio
    {
        Task ActualizarPersona(PersonaEdicionModel model);
        Task EliminarPersona(int id);
        Task GuardarPersona(PersonaCreacionModel model);
        Task<PersonaEdicionModel> ObtenerPersonasPorId(int id);
        Task<List<PersonaModel>> ObtenerTodasLasPersonas();
    }
}
