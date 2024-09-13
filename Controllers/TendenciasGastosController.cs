using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sprint3.Models;
using Sprint3.Persistence;

namespace Sprint3.Controllers
{
    public class TendenciasGastosController : Controller
    {
        private readonly OracleDbContext _context;

        public TendenciasGastosController(OracleDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém a lista de tendências de gastos.
        /// </summary>
        /// <returns>Lista de tendências de gastos.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TendenciasGastos.ToListAsync());
        }

        /// <summary>
        /// Obtém detalhes de uma tendência de gastos específica.
        /// </summary>
        /// <param name="id">ID da tendência de gastos.</param>
        /// <returns>Detalhes da tendência de gastos.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tendenciasGastos = await _context.TendenciasGastos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tendenciasGastos == null)
            {
                return NotFound();
            }

            return View(tendenciasGastos);
        }

        /// <summary>
        /// Exibe a página para criar uma nova tendência de gastos.
        /// </summary>
        /// <returns>View para criar uma nova tendência de gastos.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Cria uma nova tendência de gastos.
        /// </summary>
        /// <param name="tendenciasGastos">Dados da tendência de gastos a serem criados.</param>
        /// <returns>Redireciona para a lista de tendências de gastos se a criação for bem-sucedida.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ano,GastoMarketing,GastoAutomacao")] TendenciasGastos tendenciasGastos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tendenciasGastos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tendenciasGastos);
        }

        /// <summary>
        /// Exibe a página para editar uma tendência de gastos existente.
        /// </summary>
        /// <param name="id">ID da tendência de gastos a ser editada.</param>
        /// <returns>View para editar a tendência de gastos.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tendenciasGastos = await _context.TendenciasGastos.FindAsync(id);
            if (tendenciasGastos == null)
            {
                return NotFound();
            }
            return View(tendenciasGastos);
        }

        /// <summary>
        /// Edita uma tendência de gastos existente.
        /// </summary>
        /// <param name="id">ID da tendência de gastos a ser editada.</param>
        /// <param name="tendenciasGastos">Dados atualizados da tendência de gastos.</param>
        /// <returns>Redireciona para a lista de tendências de gastos se a edição for bem-sucedida.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Ano,GastoMarketing,GastoAutomacao")] TendenciasGastos tendenciasGastos)
        {
            if (id != tendenciasGastos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tendenciasGastos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TendenciasGastosExists(tendenciasGastos.Id))
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
            return View(tendenciasGastos);
        }

        /// <summary>
        /// Exibe a página para confirmar a exclusão de uma tendência de gastos.
        /// </summary>
        /// <param name="id">ID da tendência de gastos a ser excluída.</param>
        /// <returns>View para confirmação de exclusão.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tendenciasGastos = await _context.TendenciasGastos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tendenciasGastos == null)
            {
                return NotFound();
            }

            return View(tendenciasGastos);
        }

        /// <summary>
        /// Remove uma tendência de gastos existente.
        /// </summary>
        /// <param name="id">ID da tendência de gastos a ser removida.</param>
        /// <returns>Redireciona para a lista de tendências de gastos após a exclusão.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tendenciasGastos = await _context.TendenciasGastos.FindAsync(id);
            if (tendenciasGastos != null)
            {
                _context.TendenciasGastos.Remove(tendenciasGastos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TendenciasGastosExists(long id)
        {
            return _context.TendenciasGastos.Any(e => e.Id == id);
        }
    }
}
