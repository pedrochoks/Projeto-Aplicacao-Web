
using Katchau_Back.Data;
using Katchau_Back.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Katchau_Back.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpPost]
        public async Task <IActionResult> Login(string EmailLogin, string SenhaLogin)
        {
            if (string.IsNullOrWhiteSpace(EmailLogin) || string.IsNullOrEmpty(SenhaLogin))
            {
                TempData["Erro"] = "Preencha todos os campos";
                return View("Index");
            }

            byte[] hash = HashService.GerarHashBytes(SenhaLogin);

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == EmailLogin);

            if (usuario == null || usuario.Senha != hash)
            {
                TempData["Erro"] = "Email ou senha invalidos";
                return View("Index");
            }
            
            return RedirectToAction ("Index", "Produtos");




        }
    }
}