using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N5Solution.Core.Entities;
using N5Solution.Core.Interfaces.Services;
using N5Solution.Services;
using N5Solution.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace N5Solution.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisoController : ControllerBase
    {
        private readonly IPermisoService _permisoService;
        private readonly IMapper _mapper;
        private ElasticService _elasticService;
        private KafkaService _kafkaService;

        public PermisoController(IPermisoService permisoService, IMapper mapper, ElasticService elasticService, KafkaService kafkaService)
        {
            _permisoService = permisoService;
            _mapper = mapper;
            _elasticService = elasticService;
            _kafkaService = kafkaService;

            _kafkaService.TopicDefault = "n5topic";
        }

        // GET: api/<PermisoController>
        [Route("GetPermissions")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermisoModel>>> GetPermissions()
        {
            var permisos = await _permisoService.GetAll();
            
            await _elasticService.CreateIndexIfNotExists("n5index");
            await _elasticService.AddOrUpdateBulk(permisos);
            
            var mensaje = new MensajeOperation {Id = Guid.NewGuid().ToString(), NameOperation = Operations.Get };

            await _kafkaService.SendMessageAsync(JsonSerializer.Serialize(mensaje));

            var mappedPermisos = _mapper.Map<IEnumerable<Permiso>, IEnumerable<PermisoModel>>(permisos);

            return Ok(mappedPermisos);
        }

        // GET api/<PermisoController>/5
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<PermisoModel>> Get(int id)
        {
            var permiso = await _permisoService.GetPermisoById(id);
            var mappedPermiso = _mapper.Map<Permiso, PermisoModel>(permiso);

            return Ok(mappedPermiso);
        }

        // POST api/<PermisoController>
        [Route("RequestPermission")]
        [HttpPost]
        public async Task<ActionResult<PermisoModel>> Post([FromBody]PermisoModel permiso)
        {
            try
            {
                var permisoCreated = await _permisoService.CreatePermiso(_mapper.Map<PermisoModel, Permiso>(permiso));
                var permisoCreatedModel = _mapper.Map<Permiso, PermisoModel>(permisoCreated);

                var mensaje = new MensajeOperation { Id = Guid.NewGuid().ToString(), NameOperation = Operations.Request };

                await _kafkaService.SendMessageAsync(JsonSerializer.Serialize(mensaje));

                await _elasticService.CreateIndexIfNotExists("n5index");
                await _elasticService.AddOrUpdate(permisoCreatedModel);
                
                return Ok(permisoCreatedModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        // PUT api/<PermisoController>/5
        //[Route("ModifyPermission/{id}")]
        [HttpPut("ModifyPermission/{id}")]
        public async Task<ActionResult<PermisoModel>> Put(int id, [FromBody] PermisoModel permiso)
        {
            try
            {
                var permisoUpdated = await _permisoService.UpdatePermiso(id, _mapper.Map<PermisoModel, Permiso>(permiso));
                var permisoUpdatedModel = _mapper.Map<Permiso, PermisoModel>(permisoUpdated);

                await _elasticService.CreateIndexIfNotExists("n5index");
                await _elasticService.AddOrUpdate(permisoUpdatedModel);

                var mensaje = new MensajeOperation { Id = Guid.NewGuid().ToString(), NameOperation = Operations.Modify };

                await _kafkaService.SendMessageAsync(JsonSerializer.Serialize(mensaje));

                return Ok(permisoUpdatedModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PermisoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
