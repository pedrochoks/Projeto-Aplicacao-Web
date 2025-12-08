
using Katchau_Back.Data;
using Katchau_Back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Katchau_Back.Controllers
{
    public class ProdutoController : Controller
    {
       private readonly AppDbContext _context;
       public ProdutoController(AppDbContext context)
       {
            _context = context;
       }
       public IActionResult Index()
       {
            return View("Index");
       }
       public async Task<IActionResult> ListarPodutos(string categoria)
       {
            var produto = await _context.Produtos.
            Where(p => p.Categoria == categoria)
            .ToListAsync();

            switch (categoria)
    {
        case "Pneus":
            return View("Luigi", produto);

        case "Pecas para caminhao":
            return View("Mack", produto);

        case "Esportivas":
            return View("Mcquean", produto);

        case "Customizacao":
            return View("Ramon", produto);

        case "Pecas usadas":
            return View("Mate", produto);

        default:
            return View("Index", "Home");
    }
       }  

    }
}