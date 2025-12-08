using System;
using System.Collections.Generic;

namespace Katchau_Back.Models;

public partial class Usuario
{
    public int id_usuario { get; set; }

    public string? Nome { get; set; }

    public string? CPF { get; set; }

    public string? Rua { get; set; }

    public string? Bairro { get; set; }

    public string? Cidade { get; set; }

    public string? Estado { get; set; }

    public string? NumeroCasa { get; set; }

    public string? Telefone { get; set; }

    public string? Email { get; set; }

    public byte[]? Senha { get; set; }

    public virtual ICollection<Carrinho> Carrinhos { get; set; } = new List<Carrinho>();
}
