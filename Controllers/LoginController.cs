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
        public async Task<IActionResult> Login(string EmailLogin, string SenhaLogin)
        {
            if (string.IsNullOrWhiteSpace(EmailLogin) || string.IsNullOrWhiteSpace(SenhaLogin))
            {
                TempData["Erro"] = "Preencha todos os campos";
                return RedirectToAction("Index");
            }

            byte[] hash = HashService.GerarHashBytes(SenhaLogin);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == EmailLogin);

            if (usuario == null || !usuario.Senha.SequenceEqual(hash))
            {
                TempData["Erro"] = "Email ou senha inválidos";
                return RedirectToAction("Index");
            }

            HttpContext.Session.SetInt32("UsuarioId", usuario.id_usuario);
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);

            if (usuario.id_Regra == 1)
                HttpContext.Session.SetString("UsuarioAdmin", "true");
            else
                HttpContext.Session.SetString("UsuarioAdmin", "false");

            return RedirectToAction("Index", "Home");
        }
    }
}
