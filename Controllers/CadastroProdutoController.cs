
using System.Drawing;
using Katchau_Back.Data;
using Katchau_Back.Models;
using Microsoft.AspNetCore.Mvc;



namespace Katchau_Back.Controllers
{
    public class CadastroProdutoController : Controller
    {
        private readonly AppDbContext _context;

        public CadastroProdutoController (AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarProduto(string nomeProduto, string? descricaoProduto, double precoProduto, string categoria, int qt_CliqueProduto, string fotoProduto)
        {
            if (string.IsNullOrWhiteSpace(nomeProduto) || string.IsNullOrWhiteSpace(categoria) || precoProduto <= 0)
            {
                TempData["Erro"] = "Preencha todos os campos corretamente";
                return View("Index");
            }
            if (fotoProduto == null)
            {
                fotoProduto = "/imagens/fotoProdutoBase.jpeg";
            }
            Produto produto = new Produto
            {
                Nome = nomeProduto,
                Descricao = descricaoProduto,
                Preco = precoProduto,
                Categoria = categoria,
                foto = fotoProduto
                

            };

            await _context.AddAsync(produto);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index", "Home");
        }
    }
}