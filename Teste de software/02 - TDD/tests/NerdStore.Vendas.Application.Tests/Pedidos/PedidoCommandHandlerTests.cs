using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Moq.AutoMock;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Domain;
using Xunit;

namespace NerdStore.Vendas.Application.Tests.Pedidos
{
    public class PedidoCommandHandlerTests
    {
        [Fact(DisplayName = "Adiionar item novo pedido com sucesso")]
        [Trait("Categoria", "Vendas - Pedido Command Handler")]
        public async Task AdicionarItem_NovoPedido_DeveExecutarComSucesso()
        {
            // Arrange 
            var pedidoCommand = new AdicionaItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto teste", 2, 100);
            var mocker = new AutoMocker();
            var peddoHandle = mocker.CreateInstance<PedidoCommandHandler>();
            mocker.GetMock<IPedidoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await peddoHandle.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            mocker.GetMock<IPedidoRepository>().Verify(x => x.Adicionar(It.IsAny<Pedido>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(x => x.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar novo item pedido rascunho com sucesso")]
        [Trait("Categoria", "Vendas - Pedido Command Handler")]
        public async Task AdicionarItem_NovoItemAoPedidoRascunho_DeveExecutarComSucesso()
        {
            // Arrange 
            var clienteId = Guid.NewGuid();
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(clienteId);
            var pedidoItemExistente = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 2, 100);
            pedido.AdicionarItem(pedidoItemExistente);

            var pedidoCommand = new AdicionaItemPedidoCommand(clienteId, Guid.NewGuid(), "Produto teste", 2, 100);
            var mocker = new AutoMocker();
            var peddoHandle = mocker.CreateInstance<PedidoCommandHandler>();
            mocker.GetMock<IPedidoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            mocker.GetMock<IPedidoRepository>().Setup(r => r.ObterPedidoRascunhoPorCliente(clienteId)).Returns(Task.FromResult(pedido));

            // Act 
            var result = await peddoHandle.Handle(pedidoCommand, CancellationToken.None);

            // Assert 
            Assert.True(result);
            mocker.GetMock<IPedidoRepository>().Verify(x => x.AdicionarItem(It.IsAny<PedidoItem>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(x => x.Atualizar(It.IsAny<Pedido>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(x => x.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar item existente ao pedido rascunho com sucesso")]
        [Trait("Categoria", "Vendas - Pedido Command Handler")]
        public async Task AdicionarItem_ItemExistenteAoPedidoRascunho_DeveExecutarComSucesso()
        {
            // Arrange 
            var clienteId = Guid.NewGuid();
            var produtoId = Guid.NewGuid();
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(clienteId);
            var pedidoItemExistente = new PedidoItem(produtoId, "Produto Xpto", 2, 100);
            pedido.AdicionarItem(pedidoItemExistente);

            var pedidoCommand = new AdicionaItemPedidoCommand(clienteId, produtoId, "Produto Xpto", 2, 100);
            var mocker = new AutoMocker();
            var peddoHandle = mocker.CreateInstance<PedidoCommandHandler>();
            mocker.GetMock<IPedidoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            mocker.GetMock<IPedidoRepository>().Setup(r => r.ObterPedidoRascunhoPorCliente(clienteId)).Returns(Task.FromResult(pedido));

            // Act 
            var result = await peddoHandle.Handle(pedidoCommand, CancellationToken.None);

            // Assert 
            Assert.True(result);
            mocker.GetMock<IPedidoRepository>().Verify(x => x.AtualizarItem(It.IsAny<PedidoItem>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(x => x.Atualizar(It.IsAny<Pedido>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(x => x.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar item commad inválido")]
        [Trait("Categoria", "Vendas - Pedido Command Handler")]
        public async Task AdicionarItem_CommandInvalido_DeveRetornarFalsoELancarEventosDeNotificacao()
        {
            // Arrange 
            var pedidoCommand = new AdicionaItemPedidoCommand(Guid.Empty, Guid.Empty, "", 0, 0);
            var mocker = new AutoMocker();
            var peddoHandle = mocker.CreateInstance<PedidoCommandHandler>();

            // Act 
            var result = await peddoHandle.Handle(pedidoCommand, CancellationToken.None);

            // Assert 
            Assert.False(result);
            mocker.GetMock<IMediator>().Verify(x => x.Publish(It.IsAny<INotification>(),CancellationToken.None), Times.Exactly(5));
        }
    }
}