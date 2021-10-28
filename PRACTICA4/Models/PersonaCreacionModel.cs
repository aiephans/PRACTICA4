using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PRACTICA4.Models
{
    public class PersonaCreacionModel
    {
        [Required(ErrorMessage ="El campo Nombre es requerido")]
        public string Nombre { get; set; }
    }
}
