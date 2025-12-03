using System;
using System.Collections.Generic;

namespace Katchau_Back.Models;

public partial class Carrinho
{
    public int id_carrinho { get; set; }

    public int? id_produto { get; set; }

    public int? id_usuario { get; set; }

    public virtual Produto? id_produtoNavigation { get; set; }

    public virtual Usuario? id_usuarioNavigation { get; set; }
}
