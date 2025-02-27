using System.Collections.Generic;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace SupermarketReceipt.Test
{
    public class SupermarketXUnitTest
    {
        [Fact]
        public Task TenPercentDiscountVerify()
        {
            // ARRANGE
            SupermarketCatalog catalog = new FakeCatalog();
            var toothbrush = new Product("toothbrush", ProductUnit.Each);
            catalog.AddProduct(toothbrush, 0.99);
            var apples = new Product("apples", ProductUnit.Kilo);
            catalog.AddProduct(apples, 1.99);

            var cart = new ShoppingCart();
            cart.AddItemQuantity(apples, 2.5);
            cart.AddItemQuantity(toothbrush, 1);

            var teller = new Teller(catalog);
            teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, toothbrush, 10.0);

            // ACT
            var receipt = teller.ChecksOutArticlesFrom(cart);

            // ASSERT
            var receiptPrinter = new ReceiptPrinter();

            var receiptAsString = receiptPrinter.PrintReceipt(receipt);

            return Verifier.Verify(receiptAsString);
        }

        [Fact]
        public Task OtherDiscountVerify()
        {
            // ARRANGE
            SupermarketCatalog catalog = new FakeCatalog();
            var chipsAhoy = new Product("chips ahoy", ProductUnit.Each);
            catalog.AddProduct(chipsAhoy, 2.99);
            var sunglasses = new Product("sunglasses", ProductUnit.Each);
            catalog.AddProduct(sunglasses, 8.99);
            var catFood = new Product("cat food", ProductUnit.Each);
            catalog.AddProduct(catFood, 4.95);
            var pears = new Product("pears", ProductUnit.Kilo);
            catalog.AddProduct(pears, 1.99);

            var cart = new ShoppingCart();
            cart.AddItemQuantity(pears, 3.5);
            cart.AddItemQuantity(chipsAhoy, 5);
            cart.AddItemQuantity(sunglasses, 3);
            cart.AddItemQuantity(catFood, 3);
            cart.AddItemQuantity(chipsAhoy, 1);

            var teller = new Teller(catalog);
            teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, chipsAhoy, 12.0);
            teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, sunglasses, 999);
            teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, catFood, 6.0);

            // ACT
            var receipt = teller.ChecksOutArticlesFrom(cart);

            // ASSERT
            var receiptPrinter = new ReceiptPrinter();

            var receiptAsString = receiptPrinter.PrintReceipt(receipt);

            return Verifier.Verify(receiptAsString);
        }

        [Fact]
        public Task OtherDiscountVerifyNewDiscount()
        {
            // ARRANGE
            SupermarketCatalog catalog = new FakeCatalog();
            var chipsAhoy = new Product("chips ahoy", ProductUnit.Each);
            catalog.AddProduct(chipsAhoy, 2.99);

            var cart = new ShoppingCart();
            cart.AddItemQuantity(chipsAhoy, 2);

            var teller = new Teller(catalog);
            teller.AddSpecialOffer(SpecialOfferType.BuyOneGetOne, chipsAhoy, 42);


            // ACT
            var receipt = teller.ChecksOutArticlesFrom(cart);

            // ASSERT
            var receiptPrinter = new ReceiptPrinter();

            var receiptAsString = receiptPrinter.PrintReceipt(receipt);

            return Verifier.Verify(receiptAsString);
        }
    }
}