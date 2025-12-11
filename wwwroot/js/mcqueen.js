const precoMax = document.getElementById("precoMax");
const precoMin = document.getElementById("precoMin");

const checkbox = document.getElementById(".checkbox")

const container = document.getElementById(".lista-produtos");

const produtos = []



function filtrar(){
    
    const max = parseFloat(precoMax.value) || 0;
    const min = parseFloat(precoMin.value) || Infinity;
    
    const filtroCategorias = Array.from(checkbox)
    .filter(c => c.checked)
    .map(c => c.value)
    
    const filtrados = produtos.filter(p =>{
        precoOk = p.preco >= min && p.preco <= max;
        
        const categoriaOk = filtroCategorias.length ===0 ||
        filtroCategorias.includes(p.categoria);
        
        return precoOk && categoriaOk;
    });
    renderizar(filtrados);
}

precoMax.addEventListener("input",filtrar);
precoMin.addEventListener("input",filtrar);
checkbox.forEach(c => c.addEventListener("change",filtrar));

function formatarMoeda(valor) {
    return valor.toLocateString("pt-BR",{
        style: "currency",
        currency: "BRL"
    });
}