using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractorsAPI.Data;
using ContractorsAPI.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContractorsAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с подрядчиками.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContractorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/contractors
        /// <summary>
        /// Получает список всех подрядчиков.
        /// </summary>
        /// <returns>Список подрядчиков.</returns>
        [HttpGet]
        [ApiExplorerSettings(GroupName = "Contractors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Contractor>>> GetContractors()
        {
            var contractors = await _context.Contractors.ToListAsync();
            if (contractors == null || contractors.Count == 0)
            {
                return NotFound("Список подрядчиков пуст.");
            }
            return Ok(contractors);
        }

        // GET: api/contractors/{id}
        /// <summary>
        /// Получает информацию о подрядчике по его ID.
        /// </summary>
        /// <param name="id">ID подрядчика.</param>
        /// <returns>Информация о подрядчике или сообщение об ошибке.</returns>
        [HttpGet("{id}")]
        [ApiExplorerSettings(GroupName = "Contractors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contractor>> GetContractor(
            [FromRoute][Required] int id) // Явное указание источника параметра
        {
            var contractor = await _context.Contractors.FindAsync(id);

            if (contractor == null)
            {
                return NotFound("Подрядчик с указанным ID не найден.");
            }

            return Ok(contractor);
        }

        // POST: api/contractors
        /// <summary>
        /// Добавляет нового подрядчика.
        /// </summary>
        /// <param name="contractor">Данные нового подрядчика.</param>
        /// <returns>Созданный подрядчик.</returns>
        [HttpPost]
        [ApiExplorerSettings(GroupName = "Contractors")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerRequestExample(typeof(Contractor), typeof(ContractorExample))]
        public async Task<ActionResult<Contractor>> PostContractor([FromBody] Contractor contractor)
        {
            if (contractor == null)
            {
                return BadRequest("Данные подрядчика не могут быть пустыми.");
            }

            _context.Contractors.Add(contractor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContractor), new { id = contractor.Id }, contractor);
        }

        // Класс для примера данных
        public class ContractorExample : IExamplesProvider<Contractor>
        {
            public Contractor GetExamples()
            {
                return new Contractor
                {
                    TypeId = 1,
                    Name = "ООО Ромашка",
                    City = "Москва",
                    Street = "Ленина",
                    Building = "10",
                    TaxId = "1234567890",
                    LastName = "Иванов",
                    FirstName = "Иван",
                    MiddleName = "Иванович",
                    Phone = "+79991234567",
                    Email = "ivanov@example.com",
                    Rating = 4.5m
                };
            }
        }

        // PUT: api/contractors/{id}
        /// <summary>
        /// Обновляет данные существующего подрядчика.
        /// </summary>
        /// <param name="id">ID подрядчика.</param>
        /// <param name="contractor">Обновлённые данные подрядчика.</param>
        /// <returns>Статус выполнения операции.</returns>
        [HttpPut("{id}")]
        [ApiExplorerSettings(GroupName = "Contractors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutContractor(int id, [FromBody] Contractor contractor)
        {
            if (id != contractor.Id)
            {
                return BadRequest("ID в URL не совпадает с ID в теле запроса.");
            }

            _context.Entry(contractor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractorExists(id))
                {
                    return NotFound("Подрядчик с указанным ID не найден.");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Данные подрядчика успешно обновлены.");
        }

        // DELETE: api/contractors/{id}
        /// <summary>
        /// Удаляет подрядчика по ID.
        /// </summary>
        /// <param name="id">ID подрядчика.</param>
        /// <returns>Статус выполнения операции.</returns>
        [HttpDelete("{id}")]
        [ApiExplorerSettings(GroupName = "Contractors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteContractor(int id)
        {
            var contractor = await _context.Contractors.FindAsync(id);
            if (contractor == null)
            {
                return NotFound("Подрядчик с указанным ID не найден.");
            }

            _context.Contractors.Remove(contractor);
            await _context.SaveChangesAsync();

            return Ok($"Подрядчик с ID {id} успешно удалён.");
        }

        private bool ContractorExists(int id)
        {
            return _context.Contractors.Any(e => e.Id == id);
        }
    }
}