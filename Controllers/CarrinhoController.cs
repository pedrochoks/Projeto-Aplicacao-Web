using Katchau_Back.Data;
using Katchau_Back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Katchau_Back.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly AppDbContext _context;

        public CarrinhoController(AppDbContext context)
        {
            _context = context;
        }

        // Exibir carrinho
        public IActionResult Index()
        {
            int? usuarioID = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioID == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var carrinho = _context.Carrinhos
                .Include(c => c.id_produtoNavigation)
                .Where(c => c.id_usuario == usuarioID)
                .ToList();

            return View(carrinho);
        }

        // Adicionar produto ao carrinho
        [HttpPost]
        public async Task<IActionResult> Adicionar(int id)
        {
            int? usuarioID = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioID == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.id_produto == id);

            if (produto == null)
            {
                TempData["Erro"] = "Produto não encontrado";
                return RedirectToAction("Index", "Produto");
            }

            var jaExiste = await _context.Carrinhos.FirstOrDefaultAsync(c =>
                c.id_usuario == usuarioID &&
                c.id_produto == id
            );

            if (jaExiste != null)
            {
                TempData["Erro"] = "Produto já está no carrinho";
                return RedirectToAction("Index");
            }

            var novoItem = new Carrinho
            {
                id_usuario = usuarioID.Value,
                id_produto = id
            };

            await _context.Carrinhos.AddAsync(novoItem);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Produto adicionado ao carrinho";

            return RedirectToAction("Index");
        }

        // Remover produto do carrinho
        [HttpPost]
        public async Task<IActionResult> Remover(int id)
        {
            int? usuarioID = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioID == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var item = await _context.Carrinhos.FirstOrDefaultAsync(c =>
                c.id_carrinho == id &&
                c.id_usuario == usuarioID
            );

            if (item == null)
            {
                TempData["Erro"] = "Item não encontrado";
                return RedirectToAction("Index");
            }

            _context.Carrinhos.Remove(item);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Produto removido do carrinho";

            return RedirectToAction("Index");
        }
    }
}
