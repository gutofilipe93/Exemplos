using System;
using Xunit;
using NerdStore.Vendas.Application.Commands;

namespace NerdStore.Vendas.Application.Tests.Pedidos
{
    public class AdicionaItemPedidoCommandTest
    {
        [Fact(DisplayName = "Adicionar item command valido")] 
        [Trait("Categoria", "Vendas - Pedido commands")] 
        public void AdicionaItemPedidoCommand_CommandoEstaValido_DevePassarNaValidacao() 
        { 
          // Arrange 
          var pedidoCommand = new AdicionaItemPedidoCommand(Guid.NewGuid(),Guid.NewGuid(), "Produto teste", 2,100);
          
          // Act 
          var result = pedidoCommand.EhValido();
          
          // Assert 
          Assert.True(result);
        } 
    }
}