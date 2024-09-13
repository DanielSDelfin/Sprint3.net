using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sprint3.Models;
using Sprint3.Persistence;

namespace Sprint3.Controllers
{
    public class DesempenhoFinanceiroController : Controller
    {
        private readonly OracleDbContext _context;

        public DesempenhoFinanceiroController(OracleDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém a lista de desempenho financeiro.
        /// </summary>
        /// <returns>Lista de desempenho financeiro.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.DesempenhoFinanceiro.ToListAsync());
        }

        /// <summary>
        /// Obtém detalhes de um desempenho financeiro específico.
        /// </summary>
        /// <param name="id">ID do desempenho financeiro.</param>
        /// <returns>Detalhes do desempenho financeiro.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desempenhoFinanceiro = await _context.DesempenhoFinanceiro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desempenhoFinanceiro == null)
            {
                return NotFound();
            }

            return View(desempenhoFinanceiro);
        }

        /// <summary>
        /// Exibe a página para criar um novo desempenho financeiro.
        /// </summary>
        /// <returns>View para criar um novo desempenho financeiro.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Cria um novo desempenho financeiro.
        /// </summary>
        /// <param name="desempenhoFinanceiro">Dados do desempenho financeiro a serem criados.</param>
        /// <returns>Redireciona para a lista de desempenho financeiro se a criação for bem-sucedida.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Receita,Lucro,Crescimento")] DesempenhoFinanceiro desempenhoFinanceiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(desempenhoFinanceiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(desempenhoFinanceiro);
        }

        /// <summary>
        /// Exibe a página para editar um desempenho financeiro existente.
        /// </summary>
        /// <param name="id">ID do desempenho financeiro a ser editado.</param>
        /// <returns>View para editar o desempenho financeiro.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desempenhoFinanceiro = await _context.DesempenhoFinanceiro.FindAsync(id);
            if (desempenhoFinanceiro == null)
            {
                return NotFound();
            }
            return View(desempenhoFinanceiro);
        }

        /// <summary>
        /// Edita um desempenho financeiro existente.
        /// </summary>
        /// <param name="id">ID do desempenho financeiro a ser editado.</param>
        /// <param name="desempenhoFinanceiro">Dados do desempenho financeiro atualizados.</param>
        /// <returns>Redireciona para a lista de desempenho financeiro se a edição for bem-sucedida.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Receita,Lucro,Crescimento")] DesempenhoFinanceiro desempenhoFinanceiro)
        {
            if (id != desempenhoFinanceiro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(desempenhoFinanceiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesempenhoFinanceiroExists(desempenhoFinanceiro.Id))
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
            return View(desempenhoFinanceiro);
        }

        /// <summary>
        /// Exibe a página para confirmar a exclusão de um desempenho financeiro.
        /// </summary>
        /// <param name="id">ID do desempenho financeiro a ser excluído.</param>
        /// <returns>View para confirmação de exclusão.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var desempenhoFinanceiro = await _context.DesempenhoFinanceiro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (desempenhoFinanceiro == null)
            {
                return NotFound();
            }

            return View(desempenhoFinanceiro);
        }

        /// <summary>
        /// Remove um desempenho financeiro existente.
        /// </summary>
        /// <param name="id">ID do desempenho financeiro a ser removido.</param>
        /// <returns>Redireciona para a lista de desempenho financeiro após a exclusão.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var desempenhoFinanceiro = await _context.DesempenhoFinanceiro.FindAsync(id);
            if (desempenhoFinanceiro != null)
            {
                _context.DesempenhoFinanceiro.Remove(desempenhoFinanceiro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesempenhoFinanceiroExists(long id)
        {
            return _context.DesempenhoFinanceiro.Any(e => e.Id == id);
        }
    }
}
