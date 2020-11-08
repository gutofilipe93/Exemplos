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
            mocker.GetMock<IPedidoRepository>().Verify(x => x.Adicionar(It.IsAny<Pedido>()),Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(x => x.UnitOfWork.Commit(),Times.Once);
            //mocker.GetMock<IMediator>().Verify(x => x.Publish(It.IsAny<INotification>(),CancellationToken.None),Times.Once);
        }
    }
}