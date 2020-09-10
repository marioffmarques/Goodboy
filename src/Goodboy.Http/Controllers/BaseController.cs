using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Goodboy.Practices.Manager;
using Microsoft.AspNetCore.Mvc;

namespace Goodboy.Http
{
    public class BaseController<TEntity, TEntityDto> : Controller where TEntity : class 
                                                                  where TEntityDto : class
    {
        readonly IManager<TEntity> _manager;

        public BaseController(IManager<TEntity> manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Gets the Entity List
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="offset">Offset</param>
        /// <returns>The Entities List</returns>
        /// <response code="200">The Entities List</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        public virtual async Task<IActionResult> GetEntityList([FromQuery]int? index, [FromQuery] int? offset)
        {
            var result = await _manager.GetList(index, offset);
            return Ok(new OkResponse(Mapper.Map<IEnumerable<TEntityDto>>(result.Item1), result.Item2));
        }

        /// <summary>
        /// Gets an Entity
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>The Entity</returns>
        [HttpGet("{id:guid}")]
        [ActionName("GetEntityById")]
        public virtual async Task<IActionResult> GetEntityById(Guid id)
        {
            var entity = await _manager.Get(id);

            if (entity == null)
            {
                return NotFound(new NotFoundResponse(ExceptionKeyHelper.GetString(ExceptionKey.ERROR_GET)));
            }
            return Ok(new OkResponse(Mapper.Map<TEntityDto>(entity), 1));
        }

        /// <summary>
        /// Creates the entity.
        /// </summary>
        /// <returns>The entity.</returns>
        /// <param name="entityDto">Entity dto.</param>
        [HttpPost]
        public virtual async Task<IActionResult> CreateEntity([FromBody] TEntityDto entityDto)
        {
            var createdEntity = await _manager.Create(Mapper.Map<TEntity>(entityDto));

            if (createdEntity == null)
            {
                return BadRequest(new BadRequestResponse(ModelState, ExceptionKeyHelper.GetString(ExceptionKey.ERROR_CREATE)));
            }
            return CreatedAtAction("GetEntityById", new { id = createdEntity.GetType().GetProperty("Id")?.GetValue(createdEntity) }, new CreatedResponse(Mapper.Map<TEntityDto>(createdEntity), 1));
        }

        /// <summary>
        /// Updates the type of the company.
        /// </summary>
        /// <returns>The entity type.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="entityDto">Entity dto.</param>
        [HttpPut("{id:Guid}")]
        public virtual async Task<IActionResult> UpdateEntity(Guid id, [FromBody] TEntityDto entityDto)
        {
            var entityResult = await _manager.Update(Mapper.Map<TEntity>(entityDto));

            if (entityResult == null)
            {
                return NotFound(new NotFoundResponse(ExceptionKeyHelper.GetString(ExceptionKey.ERROR_UPDATE)));
            }
            return Ok(new OkResponse(Mapper.Map<TEntityDto>(entityResult), 1));
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <returns>The entite.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id:Guid}")]
        public virtual async Task<IActionResult> DeleteEntity(Guid id)
        {
            var entityResult = await _manager.Delete(id);

            if (entityResult == null)
            {
                return NotFound(new NotFoundResponse(ExceptionKeyHelper.GetString(ExceptionKey.ERROR_DELETE)));
            }
            return Ok(new OkResponse(Mapper.Map<TEntityDto>(entityResult), 1));
        }
    }
}
