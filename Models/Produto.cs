using System;
using System.Collections.Generic;

namespace Katchau_Back.Models;

public partial class Produto
{
    public int id_produto { get; set; }

    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public double? Preco { get; set; }

    public string? Categoria { get; set; }

    // public int? Qt_Clique { get; set; }
    public string? foto{get; set;}

    public virtual ICollection<Carrinho> Carrinhos { get; set; } = new List<Carrinho>();
}
