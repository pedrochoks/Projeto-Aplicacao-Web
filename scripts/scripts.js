document.addEventListener('DOMContentLoaded', () => {

    // 1. DADOS DOS PRODUTOS (Preços devem ser números, sem "R$")
    const produtos = [
        { 
            nome: "LED Automotivo Neon Green", 
            categoria: "LEDs",
            descricao: "Alta Intensidade Descrição: Transforme o visual do seu carro com um verde intenso e moderno. Brilho de alta performance, baixo consumo e durabilidade extrema. Destaque-se na multidão.",
            preco: 95.50,
            parcelamento: "6x de R$ 15,61",
            imagem: "img-ramon/fusca led verde.png" 
        },
        { 
            nome: "Pump Street Kit Black Magic Hydraulics", 
            categoria: "Low Rider", // Tem que ser igual ao value do HTML
            descricao: "O 4 Pump 1/2″ Street Kit da Black Magic Hydraulics é um dos sistemas mais completos e robustos para projetos de low rider. Com quatro bombas independentes de alta performance, permite movimentos clássicos e avançados como three wheel, front hop e ajustes individuais",
            preco: 3700.56,
            parcelamento: "8x de R$ 462,57",
            imagem: "img-ramon/sistema lowrider.png" 
        },
        { 
            nome: "Adesivo Rick and Morty", 
            categoria: "Adesivos",
            descricao: "Para quem tem um QI alto ou só gosta de boas referências. Personalize seu vidro ou lataria com a dupla mais insana do multiverso. Material automotivo que não desbota.",
            preco: 340.65,
            parcelamento: "3x de R$ 46,80",
            imagem: "img-ramon/mercedes rick morty.png" 
        },
        { 
            nome: "LED Interno Deep Blue", 
            categoria: "LEDs",
            descricao: "Luz azul profunda para customizar o interior da sua nave.",
            preco: 157.00,
            parcelamento: "5x de R$ 71,40",
            imagem: "img-ramon/led azul interno.png" 
        },
        { 
            nome: "Tinta Vermelha Candy", 
            categoria: "Tintas",
            descricao: "Tinta especial de alta cobertura e brilho intenso.",
            preco: 220.00,
            parcelamento: "4x de R$ 55,00",
            imagem: "img-ramon/placeholder.png" 
        },
        {
            nome: "A"
        }
    ];

    // 2. PEGANDO OS ELEMENTOS DO HTML
    const container = document.getElementById('lista-produtos');
    const botaoFiltrar = document.getElementById('filtrar');
    const checkboxes = document.querySelectorAll('#opcoes-categorias input[type="checkbox"]');
    
    // Pegando os inputs de preço
    const inputMin = document.getElementById('min-preco');
    const inputMax = document.getElementById('max-preco');

    // Função auxiliar para formatar dinheiro (Ex: 100 -> R$ 100,00)
    function formatarMoeda(valor) {
        return valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
    }

    // 3. FUNÇÃO DE RENDERIZAR (Mostrar na tela)
    function renderizar(lista) {
        container.innerHTML = ''; // Limpa a tela antes de desenhar

        if (lista.length === 0) {
            container.innerHTML = '<h3 style="color: white; text-align: center; width: 100%; margin-top: 20px;">Nenhum produto encontrado com esses filtros.</h3>';
            return;
        }

        lista.forEach(produto => {
            const div = document.createElement('div');
            div.className = 'produto'; // Mantém sua classe original

            div.innerHTML = `
                <img src="${produto.imagem}" alt="${produto.nome}" onerror="this.src='https://placehold.co/380x260?text=Sem+Foto'">
                <div class="info">
                    <h3>${produto.nome}</h3>
                    <p>${produto.descricao}</p>
                    <p class="preco">${formatarMoeda(produto.preco)}</p>
                    <p class="parcelamento">${produto.parcelamento}</p>
                    <button class="botao-carrinho">
                        <svg xmlns="http://www.w3.org/2000/svg" width="22" height="22" viewBox="0 0 24 24" fill="none"
                            stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"
                            style="vertical-align: middle; margin-right: 8px;">
                            <circle cx="9" cy="21" r="1"></circle>
                            <circle cx="20" cy="21" r="1"></circle>
                            <path d="M1 1h4l2.68 13.39a2 2 0 0 0 2 1.61h9.72a2 2 0 0 0 2-1.61L23 6H6"></path>
                        </svg>
                        Adicionar ao Carrinho
                    </button>
                </div>
            `;
            container.appendChild(div);
        });
    }

    // 4. FUNÇÃO DE FILTRAR (A mágica acontece aqui)
    function filtrar() {
        // A. Pega as categorias marcadas
        const categoriasMarcadas = Array.from(checkboxes)
            .filter(cb => cb.checked)
            .map(cb => cb.value);

        // B. Pega os valores de preço (se estiver vazio, usa 0 ou Infinito)
        let minPreco = parseFloat(inputMin.value);
        let maxPreco = parseFloat(inputMax.value);

        if (isNaN(minPreco)) minPreco = 0;           // Se não digitou nada, assume 0
        if (isNaN(maxPreco)) maxPreco = Infinity;    // Se não digitou nada, assume Infinito

        // C. Filtra o array original
        const produtosFiltrados = produtos.filter(produto => {
            // 1. Verifica a Categoria (Se nenhuma marcada, aceita todas)
            const categoriaOk = (categoriasMarcadas.length === 0) || (categoriasMarcadas.includes(produto.categoria));
            
            // 2. Verifica o Preço
            const precoOk = (produto.preco >= minPreco) && (produto.preco <= maxPreco);

            // Só retorna o produto se passar nas DUAS verificações
            return categoriaOk && precoOk;
        });

        // Mostra o resultado
        renderizar(produtosFiltrados);
    }

    // 5. ATIVAR O BOTÃO
    if (botaoFiltrar) {
        botaoFiltrar.addEventListener('click', (e) => {
            e.preventDefault(); // Evita recarregar a página
            filtrar();
        });
    }

    // Começa mostrando tudo
    renderizar(produtos);
});