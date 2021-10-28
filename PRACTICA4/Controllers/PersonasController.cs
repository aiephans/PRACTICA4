using Microsoft.AspNetCore.Mvc;
using PRACTICA4.Models;
using PRACTICA4.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRACTICA4.Controllers
{
    public class PersonasController : Controller
    {
        private readonly IPersonaRepositorio _repositorio;

        public PersonasController(IPersonaRepositorio repositorio )
        {
            _repositorio = repositorio;
        }
        public async Task<IActionResult> Index()
        {
            var listadoPersonas = await _repositorio.ObtenerTodasLasPersonas();
            return View(listadoPersonas);
        }

        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevoPost(PersonaCreacionModel model)
        {
            if (ModelState.IsValid)
            {
                await _repositorio.GuardarPersona(model);
                var listadoPersonas = await _repositorio.ObtenerTodasLasPersonas();
                return View("Index", listadoPersonas);
            }

            return View("Nuevo", model);
        }

        public async Task<IActionResult> Editar([FromRoute] int id)
        {
            var model = await _repositorio.ObtenerPersonasPorId(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditarPost(PersonaEdicionModel model)
        {
            if (ModelState.IsValid)
            {
                await _repositorio.ActualizarPersona(model);
                var listadoPersonas = await _repositorio.ObtenerTodasLasPersonas();
                return View("Index", listadoPersonas);
            }

            return View("Editar", model);
        }

        public async Task<IActionResult> EliminarPersona([FromRoute] int id)
        {
            await _repositorio.EliminarPersona(id);
            var listadoPersonas = await _repositorio.ObtenerTodasLasPersonas();
            return View("Index", listadoPersonas);
        }

    }
}
