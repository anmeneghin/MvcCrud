using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCrud.Data;
using MvcCrud.Models;

namespace MvcCrud.Controllers
{
    // Controller responsável pelas operações CRUD (Listar, Criar, Editar, Ver, Excluir)
    // relativas à entidade Product.
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // O contexto do EF Core é injetado via construtor (Dependency Injection).
        public ProductsController(ApplicationDbContext context) => _context = context;

        // GET: Products
        // Lista todos os produtos (Index).
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // GET: Products/Details/5
        // Mostra os detalhes de um produto específico.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // GET: Products/Create
        // Exibe o formulário para criar um novo produto.
        public IActionResult Create() => View();

        // POST: Products/Create
        // Recebe os dados do formulário e salva um novo produto no banco.
        [HttpPost]
        [ValidateAntiForgeryToken] // Protege contra CSRF
        public async Task<IActionResult> Create([Bind("Name,Price,Description")] Product product)
        {
            // Validação do modelo: verifica atributos como [Required], [StringLength], etc.
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redireciona para a lista após criar
            }
            // Se houver erro de validação, volta para a view exibindo mensagens de erro.
            return View(product);
        }

        // GET: Products/Edit/5
        // Exibe o formulário de edição preenchido com os dados do produto.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Edit/5
        // Recebe os dados editados e atualiza o produto no banco.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description")] Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product); // Marca a entidade como modificada
                    await _context.SaveChangesAsync(); // Persiste alterações
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Verifica se o registro ainda existe em caso de concorrência
                    if (!await _context.Products.AnyAsync(e => e.Id == product.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product); // Retorna à view se houver erros de validação
        }

        // GET: Products/Delete/5
        // Exibe confirmação de exclusão.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        // Executa a exclusão quando o usuário confirma.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}