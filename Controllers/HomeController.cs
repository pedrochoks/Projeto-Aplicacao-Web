using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Katchau_Back.Models;

namespace Katchau_Back.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Login()
    {
        return RedirectToAction("Index", "Login");
    }
    public IActionResult Cadastro()
    {
        return RedirectToAction("Index", "Cadastro");
    }
}
