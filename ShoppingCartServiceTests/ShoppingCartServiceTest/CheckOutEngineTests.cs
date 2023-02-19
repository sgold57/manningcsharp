using System;
using ShoppingCartService.BusinessLogic;
using ShoppingCartService.Models;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Controllers.Models;
using ShoppingCartService.Mapping;
using AutoMapper;
using System.Diagnostics.Metrics;
using System.IO;
using Xunit.Abstractions;

namespace ShoppingCartServiceTest
{
    public class CheckOutEngineTests
    {


        public CheckOutEngineTests()
        {

        }

        [InlineData(CustomerType.Standard, 0)]
        [InlineData(CustomerType.Premium, 10)]
        [Theory]
        public void CalculateTotals_DiscountExists_IfPremiumCustomer_DiscountDoesNotExist_IfStandardCustomer(
            CustomerType customerType,
            double expected)
        {
            var testMapConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper testMapper = testMapConfig.CreateMapper();

            Address originAddress = CreateAddress();
            ShippingCalculator testShippingCalculator = new ShippingCalculator();
            CheckOutEngine sut = new CheckOutEngine(testShippingCalculator, testMapper);

            List<Item> testList = CreateItemList();

            Address testShippingAddress = CreateAddress();

            Cart testCart = CreateCart(
                address: testShippingAddress,
                customerType: customerType,
                items: testList
                );

            CheckoutDto actual = sut.CalculateTotals(testCart);

            Assert.Equal(expected, actual.CustomerDiscount);

        }

        [InlineData(1, 102)]
        [InlineData(2, 204)]
        [Theory]
        public void CalculateTotals_StandardCustomer(
            uint itemQuantity,
            double expected)
        {
            var testMapConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper testMapper = testMapConfig.CreateMapper();

            ShippingCalculator testShippingCalculator = new ShippingCalculator();
            CheckOutEngine sut = new CheckOutEngine(testShippingCalculator, testMapper);

            List<Item> testList = CreateItemList(itemQuantity);

            Address testShippingAddress = CreateAddress();

            Cart testCartStandardCustomer = CreateCart(
                address: testShippingAddress,
                customerType: CustomerType.Standard,
                items: testList
                );

            CheckoutDto actual = sut.CalculateTotals(testCartStandardCustomer);

            Assert.Equal(expected, actual.Total);

        }

        [InlineData(1, 91.80)]
        [InlineData(2, 183.60)]
        [Theory]
        public void CalculateTotals_PremiumCustomer(
            uint itemQuantity,
            double expected)
        {
            var testMapConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper testMapper = testMapConfig.CreateMapper();

            ShippingCalculator testShippingCalculator = new ShippingCalculator();
            CheckOutEngine sut = new CheckOutEngine(testShippingCalculator, testMapper);

            List<Item> testList = CreateItemList(itemQuantity);

            Address testShippingAddress = CreateAddress();

            Cart testCartStandardCustomer = CreateCart(
                address: testShippingAddress,
                customerType: CustomerType.Premium,
                items: testList
                );

            CheckoutDto actual = sut.CalculateTotals(testCartStandardCustomer);

            Assert.Equal(expected, actual.Total);

        }

        private List<Item> CreateItemList(
            uint quantity = 1,
            double price = 100)
        {
            List<Item> newList = new List<Item>();
            newList.Add(new Item { Quantity = quantity, Price = price });

            return newList;
        }

        private Address CreateAddress(
            string country = "USA",
            string city = "Milwaukee",
            string street = "123 Maple Lane")
        {
            return new Address()
            {
                Country = country,
                City = city,
                Street = street
            };
        }

        private Cart CreateCart(
            Address address,
            List<Item> items,
            CustomerType customerType,
            ShippingMethod shippingMethod = ShippingMethod.Standard)
        {
            return new Cart()
            {
                CustomerType = customerType,
                ShippingMethod = shippingMethod,
                ShippingAddress = address,
                Items = items
            };
        }

    }
}

