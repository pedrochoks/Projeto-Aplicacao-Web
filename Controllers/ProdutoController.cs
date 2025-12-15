using Katchau_Back.Data;
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
        public async Task<IActionResult> ListarProdutos(string categoria, double? PrecoMin, double? PrecoMax)
        {

            var produto = await _context.Produtos.
            Where(p => p.Categoria == categoria)
            .ToListAsync();
            if (PrecoMin != null && PrecoMax != null )
            {
                produto = await _context.Produtos.
                Where(p => p.Categoria == categoria && p.Preco >= PrecoMin && p.Preco <= PrecoMax)
                .ToListAsync();

                switch (categoria)
                {
                     case "Pneus":
                    return View("Luigi", produto);

                case "Pecas para caminhao":
                    return View("Mack", produto);


                case "Customizacao":
                    return View("Ramon", produto);

                case "Pecas usadas":
                    return View("Mate", produto);
                    
                default:
                    return View("Index", produto);   
                }
            }
            switch (categoria)
            {
                case "Pneus":
                    return View("Luigi", produto);

                case "Pecas para caminhao":
                    return View("Mack", produto);


                case "Customizacao":
                    return View("Ramon", produto);

                case "Pecas usadas":
                    return View("Mate", produto);
                default:
                    return View("Index", produto);
            }
            
        }

        
    }
}