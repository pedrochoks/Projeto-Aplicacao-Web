
using System.Drawing;
using Katchau_Back.Data;
using Katchau_Back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Katchau_Back.Services;

namespace Katchau_Back.Controllers
{
    public class CadastroController : Controller
    {
        private readonly AppDbContext _context;
        public CadastroController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Cadastro(String NomeUsuario, String CPFUsuario, String RuaUsuario, String BairroUsuario, String CidadeUSuario, String EstadoUsuario, String NumeroCasaUsuario, String TelefoneUsuario, String EmailUsuario, String SenhaUsuario, String SenhaConfirmada)
        {
            if (string.IsNullOrWhiteSpace(NomeUsuario) || 
            string.IsNullOrEmpty(CPFUsuario) || 
            string.IsNullOrWhiteSpace(RuaUsuario) ||
            string.IsNullOrWhiteSpace(BairroUsuario) ||
            string.IsNullOrWhiteSpace(CidadeUSuario) ||
            string.IsNullOrWhiteSpace(EstadoUsuario)||
            string.IsNullOrWhiteSpace(NumeroCasaUsuario) ||
            string.IsNullOrWhiteSpace(TelefoneUsuario) ||
            string.IsNullOrWhiteSpace(EmailUsuario) ||
            string.IsNullOrWhiteSpace(SenhaUsuario) ||
            string.IsNullOrWhiteSpace(SenhaConfirmada))
            {
                TempData["Erro"] = "Informacoes incompativeis ou incompletas";
                return View("Index");
            }

            if (SenhaUsuario != SenhaConfirmada)
            {
                TempData["Erro"] = "As senhas nao conferem";
                return View("Index");
            }
            if (await _context.Usuarios.AnyAsync(usuario => usuario.CPF == CPFUsuario))
            {
                TempData["Erro"] = "CPF ja cadastrado";
                return RedirectToAction("Index", "Login");
            }

            if (await _context.Usuarios.AnyAsync(usuario => usuario.Email == EmailUsuario))
            {
                TempData["Erro"] = "Email ja cadastrado";
                return RedirectToAction("Index", "Produto");
            }
            byte[] hash = HashService.GerarHashBytes(SenhaUsuario);
            Usuario usuario = new Usuario
            {
                Nome = NomeUsuario,
                CPF = CPFUsuario,
                Rua = RuaUsuario,
                Bairro = BairroUsuario,
                Cidade = CidadeUSuario,
                Estado = EstadoUsuario,
                NumeroCasa = NumeroCasaUsuario,
                Telefone = TelefoneUsuario,
                Email = EmailUsuario,
                Senha = hash,
                id_Regra = 2
            };

            await _context.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Login");
        }
    }
}