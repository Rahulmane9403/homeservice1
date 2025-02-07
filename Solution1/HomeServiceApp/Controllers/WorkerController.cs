using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeServiceApp.Models;

namespace HomeServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly HomeServiceDBContext _context;

        public WorkerController(HomeServiceDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/Worker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Worker>>> GetWorkers()
        {
            if (_context.Workers == null)
            {
                return NotFound("Workers entity set is null.");
            }
            return await _context.Workers.ToListAsync();
        }

        // GET: api/Worker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> GetWorker(int id)
        {
            if (_context.Workers == null)
            {
                return NotFound("Workers entity set is null.");
            }

            var worker = await _context.Workers.FindAsync(id);

            if (worker == null)
            {
                return NotFound($"Worker with ID {id} not found.");
            }

            return worker;
        }

        // POST: api/Worker (Register a worker without photo & identity proof)
        [HttpPost]
        public async Task<ActionResult<Worker>> PostWorker([FromBody] Worker worker)
        {
            if (_context.Workers == null)
            {
                return Problem("Entity set 'HomeServiceDBContext.Workers' is null.");
            }

            try
            {
                _context.Workers.Add(worker);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetWorker), new { id = worker.Id }, worker);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // DELETE: api/Worker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            if (_context.Workers == null)
            {
                return NotFound("Workers entity set is null.");
            }

            var worker = await _context.Workers.FindAsync(id);
            if (worker == null)
            {
                return NotFound($"Worker with ID {id} not found.");
            }

            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
