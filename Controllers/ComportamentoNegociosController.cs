using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sprint3.Models;
using Sprint3.Persistence;

namespace Sprint3.Controllers
{
    public class ComportamentoNegociosController : Controller
    {
        private readonly OracleDbContext _context;

        public ComportamentoNegociosController(OracleDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém a lista de comportamento de negócios.
        /// </summary>
        /// <returns>Lista de comportamento de negócios.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComportamentoNegocios.ToListAsync());
        }

        /// <summary>
        /// Obtém detalhes de um comportamento de negócios específico.
        /// </summary>
        /// <param name="id">ID do comportamento de negócios.</param>
        /// <returns>Detalhes do comportamento de negócios.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comportamentoNegocios = await _context.ComportamentoNegocios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comportamentoNegocios == null)
            {
                return NotFound();
            }

            return View(comportamentoNegocios);
        }

        /// <summary>
        /// Cria um novo comportamento de negócios.
        /// </summary>
        /// <returns>View para criar um novo comportamento de negócios.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Cria um novo comportamento de negócios.
        /// </summary>
        /// <param name="comportamentoNegocios">Dados do comportamento de negócios a serem criados.</param>
        /// <returns>Redireciona para a lista de comportamento de negócios se a criação for bem-sucedida.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InteracoesPlataforma,FrequenciaUso,Feedback,UsoRecursosEspecificos")] ComportamentoNegocios comportamentoNegocios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comportamentoNegocios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comportamentoNegocios);
        }

        /// <summary>
        /// Edita um comportamento de negócios existente.
        /// </summary>
        /// <param name="id">ID do comportamento de negócios a ser editado.</param>
        /// <returns>View para editar o comportamento de negócios.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comportamentoNegocios = await _context.ComportamentoNegocios.FindAsync(id);
            if (comportamentoNegocios == null)
            {
                return NotFound();
            }
            return View(comportamentoNegocios);
        }

        /// <summary>
        /// Edita um comportamento de negócios existente.
        /// </summary>
        /// <param name="id">ID do comportamento de negócios a ser editado.</param>
        /// <param name="comportamentoNegocios">Dados do comportamento de negócios atualizados.</param>
        /// <returns>Redireciona para a lista de comportamento de negócios se a edição for bem-sucedida.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,InteracoesPlataforma,FrequenciaUso,Feedback,UsoRecursosEspecificos")] ComportamentoNegocios comportamentoNegocios)
        {
            if (id != comportamentoNegocios.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comportamentoNegocios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComportamentoNegociosExists(comportamentoNegocios.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comportamentoNegocios);
        }

        /// <summary>
        /// Remove um comportamento de negócios existente.
        /// </summary>
        /// <param name="id">ID do comportamento de negócios a ser removido.</param>
        /// <returns>View para confirmação de exclusão.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comportamentoNegocios = await _context.ComportamentoNegocios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comportamentoNegocios == null)
            {
                return NotFound();
            }

            return View(comportamentoNegocios);
        }

        /// <summary>
        /// Remove um comportamento de negócios existente.
        /// </summary>
        /// <param name="id">ID do comportamento de negócios a ser removido.</param>
        /// <returns>Redireciona para a lista de comportamento de negócios após a exclusão.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var comportamentoNegocios = await _context.ComportamentoNegocios.FindAsync(id);
            if (comportamentoNegocios != null)
            {
                _context.ComportamentoNegocios.Remove(comportamentoNegocios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComportamentoNegociosExists(long id)
        {
            return _context.ComportamentoNegocios.Any(e => e.Id == id);
        }
    }
}
