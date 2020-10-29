using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Vendas.Domain
{
    public class Pedido
    {
        public static int MAX_UNIDADES_ITEM = 15;
        public static int MIM_UNIDADES_ITEM = 1;
        protected Pedido()
        {
            _pedidoItens = new List<PedidoItem>();
        }
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }
        private readonly List<PedidoItem> _pedidoItens;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItens;

        private void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(x => x.CalcularValor());
        }

        private bool PedidoItemExistente(PedidoItem item)
        {
            return _pedidoItens.Any(x => x.ProdutoId == item.ProdutoId);
        }

        private void ValidarQuantidadeItemPermitida(PedidoItem item)
        {
            var quantidadeItems = item.Quantidade;
            if (PedidoItemExistente(item))
            {
                var itemExistente = _pedidoItens.FirstOrDefault(x => x.ProdutoId == item.ProdutoId);
                quantidadeItems += itemExistente.Quantidade;
            }

            if (quantidadeItems > MAX_UNIDADES_ITEM) throw new DomainException($"Maximo de {MAX_UNIDADES_ITEM} unidades por produto");
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            ValidarQuantidadeItemPermitida(pedidoItem);
            if (PedidoItemExistente(pedidoItem))
            {
                var itemExistente = _pedidoItens.FirstOrDefault(x => x.ProdutoId == pedidoItem.ProdutoId);
                itemExistente.AdicionarUnidade(pedidoItem.Quantidade);
                pedidoItem = itemExistente;
                _pedidoItens.Remove(itemExistente);
            }

            _pedidoItens.Add(pedidoItem);
            CalcularValorPedido();
        }

        public void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClienteId = clienteId
                };

                pedido.TornarRascunho();
                return pedido;
            }
        }
    }

    public enum PedidoStatus
    {
        Rascunho = 0,
        Iniciado =1,
        Pago = 4,
        Entregue = 5,
        Cancelado = 6
    }

    public class PedidoItem
    {
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
        {
            if (quantidade < Pedido.MIM_UNIDADES_ITEM) throw new DomainException($"Minino de {Pedido.MIM_UNIDADES_ITEM} unidades por produto");

            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        internal void AdicionarUnidade(int quantidade)
        {
            Quantidade += quantidade;
        }

        internal decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }
    }    
}