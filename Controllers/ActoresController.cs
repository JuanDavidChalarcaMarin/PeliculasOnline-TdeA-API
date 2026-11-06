using AutoMapper;
using AutoMapper.QueryableExtensions;
using api.DTOs;
using api.Entidades;
using api.Servicios;
using api.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/actores")]
    [ApiController]
    public class ActoresController : ControllerBase
    {
        private const string cacheTag = "actores";
        private const string contenedor = "actores";

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly IOutputCacheStore outputCacheStore;

        public ActoresController(
            ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos,
            IOutputCacheStore outputCacheStore)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
            this.outputCacheStore = outputCacheStore;
        }

        // GET /api/actores?pagina=1&recordsPorPagina=10
        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<ActorDTO>> Get([FromQuery] PaginacionDTO paginacion)
        {
            var queryable = context.Actores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);

            return await queryable
                .OrderBy(actor => actor.Nombre)
                .Paginar(paginacion)
                .ProjectTo<ActorDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET /api/actores/5
        [HttpGet("{id:int}", Name = "ObtenerActorPorId")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actor = await context.Actores
                .ProjectTo<ActorDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(actor => actor.Id == id);

            if (actor is null)
            {
                return NotFound();
            }

            return actor;
        }

        // GET /api/actores/buscarPorNombre/tom
        // Se declara antes de la ruta por id para mantener clara la ruta de búsqueda.
        [HttpGet("buscarPorNombre/{nombre}")]
        public async Task<List<ActorPeliculaDTO>> BuscarPorNombre(string nombre)
        {
            return await context.Actores
                .Where(actor => actor.Nombre.Contains(nombre))
                .OrderBy(actor => actor.Nombre)
                .Take(5)
                .Select(actor => new ActorPeliculaDTO
                {
                    Id = actor.Id,
                    Nombre = actor.Nombre,
                    Foto = actor.Foto,
                    Personaje = string.Empty
                })
                .ToListAsync();
        }

        // POST /api/actores
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);

            if (actorCreacionDTO.Foto is not null)
            {
                actor.Foto = await almacenadorArchivos.Almacenar(contenedor, actorCreacionDTO.Foto);
            }

            context.Actores.Add(actor);
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);

            var actorDTO = mapper.Map<ActorDTO>(actor);
            return CreatedAtRoute("ObtenerActorPorId", new { id = actor.Id }, actorDTO);
        }

        // PUT /api/actores/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(actor => actor.Id == id);

            if (actor is null)
            {
                return NotFound();
            }

            // El Profile ignora Foto: si no llega una foto nueva, se conserva la existente.
            mapper.Map(actorCreacionDTO, actor);

            if (actorCreacionDTO.Foto is not null)
            {
                actor.Foto = await almacenadorArchivos.Editar(
                    actor.Foto, contenedor, actorCreacionDTO.Foto);
            }

            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);

            return NoContent();
        }

        // DELETE /api/actores/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(actor => actor.Id == id);

            if (actor is null)
            {
                return NotFound();
            }

            await almacenadorArchivos.Borrar(actor.Foto, contenedor);
            context.Actores.Remove(actor);
            await context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);

            return NoContent();
        }
    }
}
