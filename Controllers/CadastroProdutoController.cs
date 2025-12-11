
using System.Drawing;
using Katchau_Back.Data;
using Katchau_Back.Models;
using Microsoft.AspNetCore.Mvc;



namespace Katchau_Back.Controllers
{
    public class CadastroProdutoController : Controller
    {
        private readonly AppDbContext _context;

        public CadastroProdutoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarProduto(string nomeProduto, string? descricaoProduto, double precoProduto, string categoria, IFormFile fotoProduto)
        {
            if (string.IsNullOrWhiteSpace(nomeProduto) || string.IsNullOrWhiteSpace(categoria) || precoProduto <= 0)
            {
                TempData["Erro"] = "Preencha todos os campos corretamente";
                return View("Index");
            }
            
            Produto produto = new Produto
            {
                Nome = nomeProduto,
                Descricao = descricaoProduto,
                Preco = precoProduto,
                Categoria = categoria,
            
            };

            using (var ms = new MemoryStream())
            {
                fotoProduto.CopyTo(ms);
                produto.foto = ms.ToArray();
            }

            await _context.AddAsync(produto);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        
    }
}