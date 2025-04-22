using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Repositories;

namespace RRHH.WebApi.Controllers
{
    /// <summary>
    /// Controlador para la creacion, edicion y eliminacion de empresas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {

        private readonly EmpresasRepository _repository;

        /// <summary>
        /// Constructor del controlador de Empresas.
        /// </summary>
        /// <param name="repository">Instancia de la interfaz de acceso a la base de datos.</param>
        public EmpresaController(EmpresasRepository repository)
        {
            _repository = repository;
        }


    }


}