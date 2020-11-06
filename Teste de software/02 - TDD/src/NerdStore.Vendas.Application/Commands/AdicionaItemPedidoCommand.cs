using System;
using FluentValidation;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Application.Commands
{
    public class AdicionaItemPedidoCommand
    {
        public Guid ClienteId { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }

        public AdicionaItemPedidoCommand(Guid clienteId, Guid produtoId, string nome, int quantidade, decimal valorUnitario)
        {
            ClienteId = clienteId;
            ProdutoId = produtoId;
            Nome= nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public bool EhValido()
        {
            return false;
        }
    }

    public class AdicionarItemPedidoValidation : AbstractValidator<AdicionaItemPedidoCommand>
    {
        public static string IdClienteErroMsg => "Id cliente inválido";
        public static string IdProdutoErroMsg => "Id produto inválido";
        public static string QtdMaxErroMsg => $"A quandtidade maxima de um item é {Pedido.MAX_UNIDADES_ITEM}";
        public static string QtdMinErroMsg => "A quantidade minima de um item é 1";
        public static string NomeErroMsg => "Nome do produto não foi informado";
        public static string ValorErroMsg => "O valor do item precisa ser maior que 0";

        // 08:46

    }
}