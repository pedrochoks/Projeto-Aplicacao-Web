

using Katchau_Back.Data;
using Microsoft.AspNetCore.Mvc;

namespace Katchau_Back.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly AppDbContext _context;
        public CarrinhoController (AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Adicionar(int id)
        {
            int ? usuarioID = HttpContext.Session.GetInt32("id_usuario");

            if (usuarioID == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var produto = _context.Produtos.FirstOrDefault(p => p.id_produto == id);

            if (produto == null)
            {
                TempData["Erro"] = "Produto nao encontrado";
                return RedirectToAction("Index", "Produto");
            }

            return View("Index");





        } 
    }
}