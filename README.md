# Desafio Técnico — Target Sistemas

API REST desenvolvida em C# com ASP.NET Web API 2, resolvendo os três desafios propostos no processo seletivo: cálculo de comissão de vendas, movimentação de estoque e cálculo de juros por atraso.

## Tecnologias

- C#
- .NET Framework
- ASP.NET Web API 2
- Data Annotations (validação de entrada)

## Estrutura do projeto

O projeto está separado em três camadas:

| Camada | Responsabilidade |
|---|---|
| **Models** | Classes de entrada e saída dos endpoints, com as regras de validação via Data Annotations |
| **Repository** | Toda a lógica de negócio (cálculos, agrupamentos e movimentações) |
| **Controllers** | Recebem a requisição, validam o `ModelState` e devolvem a resposta |

Os controllers não contêm regra de negócio: apenas delegam para o repositório e tratam o retorno.

---

## 1. VendasController — Cálculo de comissão

Recebe uma lista de vendas e devolve a comissão total acumulada por vendedor.

**Regras aplicadas a cada venda:**

| Valor da venda | Comissão |
|---|---|
| Abaixo de R$ 100,00 | Sem comissão |
| De R$ 100,00 a R$ 499,99 | 1% |
| A partir de R$ 500,00 | 5% |

As vendas são percorridas uma a uma, a comissão é calculada individualmente e somada ao vendedor correspondente. Cada vendedor aparece uma única vez no retorno, com o total acumulado. O arredondamento para duas casas é feito ao final, depois de todas as somas, para não acumular diferença de centavos.

**Endpoint:** `POST /api/Vendas`

**Requisição:**

```json
{
  "vendas": [
    { "vendedor": "João Silva", "valor": 1200.50 },
    { "vendedor": "João Silva", "valor": 250.30 },
    { "vendedor": "Maria Souza", "valor": 90.75 }
  ]
}
```

**Resposta:**

```json
[
  { "vendedor": "João Silva", "comissao": 62.53 },
  { "vendedor": "Maria Souza", "comissao": 0.00 }
]
```

---

## 2. EstoqueController — Movimentação de estoque

Recebe uma lista de movimentações de entrada e saída e devolve a quantidade final em estoque após cada uma delas.

Cada movimentação gera:

- um **número identificador único**, sequencial e não repetido;
- o **tipo** da movimentação (`Entrada` ou `Saida`), que identifica a operação realizada;
- a **quantidade final** do produto após o lançamento.

O saldo é mantido entre as requisições, então movimentações enviadas em chamadas diferentes continuam de onde o estoque parou. Um produto que ainda não existe no saldo é criado zerado antes de receber a primeira entrada.

**Endpoints:**

- `POST /api/Estoque` — lança as movimentações
- `GET /api/Estoque` — consulta o saldo atual

**Requisição:**

```json
{
  "estoque": [
    { "codigoProduto": 101, "descricaoProduto": "Caneta Azul", "estoque": 150, "tipo": "Entrada" },
    { "codigoProduto": 101, "descricaoProduto": "Caneta Azul", "estoque": 30,  "tipo": "Saida" }
  ]
}
```

**Resposta:**

```json
[
  {
    "id": 1,
    "codigoProduto": 101,
    "descricaoProduto": "Caneta Azul",
    "tipo": "Entrada",
    "quantidade": 150,
    "estoqueFinal": 150
  },
  {
    "id": 2,
    "codigoProduto": 101,
    "descricaoProduto": "Caneta Azul",
    "tipo": "Saida",
    "quantidade": 30,
    "estoqueFinal": 120
  }
]
```

---

## 3. JurosController — Cálculo de juros

Recebe um valor e uma data de vencimento e calcula os juros até a data de hoje, aplicando **2,5% ao dia** sobre o valor original (juros simples).

O cálculo considera a diferença em dias entre a data de vencimento e a data atual. O retorno traz o valor original, os dias de atraso, o valor dos juros e o valor total a pagar.

A data de vencimento é validada por um atributo customizado (`DataFuturaAttribute`), que impede o envio de datas fora do período esperado e devolve a mensagem de erro no `ModelState`.

**Endpoint:** `POST /api/Juros`

**Requisição:**

```json
{
  "valor": 1000.00,
  "dataVencimento": "2026-09-25"
}
```

**Resposta:**

```json
{
  "valorOriginal": 1000.00,
  "dataVencimento": "2026-09-25T00:00:00",
  "dataHoje": "2026-10-05T00:00:00",
  "diasAtraso": 10,
  "valorJuros": 250.00,
  "valorTotal": 1250.00
}
```

---

## Validações

Todos os endpoints validam a entrada antes de processar:

- corpo da requisição nulo ou lista não informada retorna `400 Bad Request`;
- `ModelState` inválido retorna `400` com as mensagens das Data Annotations;
- erros de regra de negócio são tratados e retornados com a mensagem correspondente.

## Como executar

1. Clone o repositório
2. Abra a solution no Visual Studio
3. Restaure os pacotes NuGet
4. Execute o projeto (F5)
5. Faça as requisições pelo Postman usando os endpoints acima, com **Body → raw → JSON**

## Autor

Lucas Moretti — [github.com/DEVLucasMoretti](https://github.com/DEVLucasMoretti)
