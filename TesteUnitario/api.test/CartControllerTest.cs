using System;
using System.Collections.Generic;
using System.Linq;
using api.Controllers;
using api.Services;
using Moq;
using NUnit.Framework;

namespace api.test
{
    public class CartControllerTest
    {

        private CartController controller;
        private Mock<IPaymentService> paymentServiceMock;
        private Mock<ICartService> cartServiceMock;        
        private Mock<IShipmentService> shipmentServiceMock;
        private Mock<ICard> cardMock;
        private Mock<IAddressInfo> addressInfoMock;
        private List<CartItem> items;

        [Test]
        public void Setup()
        {
            paymentServiceMock = new Mock<IPaymentService>();                
            cardMock = new Mock<ICard>();
            shipmentServiceMock = new Mock<IShipmentService>();

            addressInfoMock = new Mock<IAddressInfo>();
            cardMock = new Mock<ICard>();

            var cartItemMock = new Mock<CartItem>();
            cartItemMock.Setup(item => item.Price).Returns(10);

            items = new List<CartItem>
            {
                cartItemMock.Object
            };
            
            cartServiceMock.Setup(c => c.Items()).Returns(items.AsEnumerable());
            controller = new CartController(cartServiceMock.Object, paymentServiceMock.Object,shipmentServiceMock.Object);
        }

        [Test]
        public void ShouldReturnCharges()
        {
            paymentServiceMock.Setup(x => x.Charge(It.IsAny<double>(),cardMock.Object)).Returns(true);

            var result = controller.CheckOut(cardMock.Object,addressInfoMock.Object);

            shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()),Times.Never());

            Assert.Equals("charged",result);
        }

        [Test]
        public void ShouldReturnNotCharges()
        {
                        paymentServiceMock.Setup(x => x.Charge(It.IsAny<double>(),cardMock.Object)).Returns(false);

            var result = controller.CheckOut(cardMock.Object,addressInfoMock.Object);

            shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()),Times.Never());

            Assert.Equals("not charged",result);
        }
    }
}