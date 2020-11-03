using System;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class VoucherTests
    {
        [Fact(DisplayName = "Validar voucher tipo valor valido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoValorValido_DeveEstarValido()
        {
            // Arrange 
            var voucher = new Voucher("PROMO-15-REAIS",null,15,1,DateTime.Now.AddDays(15),true,false,TipoDescontoVoucher.Valor);
            
            // Act
            var result = voucher.ValidarSeAplicavel();
            // Assert 
            Assert.True(result.IsValid);

            // 13:08
        }
    }
}