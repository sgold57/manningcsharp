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

        [Fact]
        public void CalculateTotals_StandardCustomer_NoCustomerDiscount()
        {
            var testMapConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper testMapper = testMapConfig.CreateMapper();

            ShippingCalculator testShippingCalculator = new ShippingCalculator();
            CheckOutEngine testCheckOutEngine = new CheckOutEngine(testShippingCalculator, testMapper);

            List<Item> testListOneItem = new List<Item>();
            testListOneItem.Add(
                new Item()
                {
                    ProductId = "1",
                    ProductName = "Toilet Plunger",
                    Price = 29.99,
                    Quantity = 1
                });

            Address testShippingAddress = new Address()
            {
                Country = "USA",
                City = "Milwaukee",
                Street = "1408 Cusack Drive"

            };

            Cart testCartStandardCustomerSameCountryOneItem = new Cart()
            {
                Id = "000",
                CustomerId = "1",
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                ShippingAddress = testShippingAddress,
                Items = testListOneItem
            };

            CheckoutDto actual = testCheckOutEngine.CalculateTotals(testCartStandardCustomerSameCountryOneItem);

            Assert.Equal(0, actual.CustomerDiscount);

        }

        [Fact]
        public void CalculateTotals_StandardCustomer_TotalEqualsCostPlusShipping()
        {
            var testMapConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper testMapper = testMapConfig.CreateMapper();

            ShippingCalculator testShippingCalculator = new ShippingCalculator();
            CheckOutEngine testCheckOutEngine = new CheckOutEngine(testShippingCalculator, testMapper);

            List<Item> testListOneItem = new List<Item>();
            testListOneItem.Add(
                new Item()
                {
                    ProductId = "1",
                    ProductName = "Toilet Plunger",
                    Price = 29.99,
                    Quantity = 1
                });

            Address testShippingAddress = new Address()
            {
                Country = "USA",
                City = "Milwaukee",
                Street = "1408 Cusack Drive"

            };

            Cart testCartStandardCustomerSameCountryOneItem = new Cart()
            {
                Id = "0340",
                CustomerId = "231",
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                ShippingAddress = testShippingAddress,
                Items = testListOneItem
            };

            CheckoutDto actual = testCheckOutEngine.CalculateTotals(testCartStandardCustomerSameCountryOneItem);

            Assert.Equal(31.99, actual.Total);

        }

        [Fact]
        public void CalculateTotals_StandardCustomerMoreThanOneItem_TotalEqualsCostPlusShipping()
        {
            var testMapConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper testMapper = testMapConfig.CreateMapper();

            ShippingCalculator testShippingCalculator = new ShippingCalculator();
            CheckOutEngine testCheckOutEngine = new CheckOutEngine(testShippingCalculator, testMapper);

            List<Item> testListTwoQuantityOneItem = new List<Item>();
            testListTwoQuantityOneItem.Add(
                new Item()
                {
                    ProductId = "728",
                    ProductName = "Chair",
                    Price = 31.50,
                    Quantity = 2
                });

            Address testShippingAddress = new Address()
            {
                Country = "USA",
                City = "Trenton",
                Street = "369 Maple Lane"

            };

            Cart testCartStandardCustomerSameCountryTwoQuantityOneItem = new Cart()
            {
                Id = "9663",
                CustomerId = "143",
                CustomerType = CustomerType.Standard,
                ShippingMethod = ShippingMethod.Standard,
                ShippingAddress = testShippingAddress,
                Items = testListTwoQuantityOneItem
            };

            CheckoutDto actual = testCheckOutEngine.CalculateTotals(testCartStandardCustomerSameCountryTwoQuantityOneItem);

            Assert.Equal(67, actual.Total);
        }

        [Fact]
        public void CalculateTotals_PremiumCustomer_HasCustomerDiscount()
        {
            var testMapConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper testMapper = testMapConfig.CreateMapper();

            ShippingCalculator testShippingCalculator = new ShippingCalculator();
            CheckOutEngine testCheckOutEngine = new CheckOutEngine(testShippingCalculator, testMapper);

            List<Item> testListOneItem = new List<Item>();
            testListOneItem.Add(
                new Item()
                {
                    ProductId = "2",
                    ProductName = "Coffee Maker",
                    Price = 42.75,
                    Quantity = 1
                });

            Address testShippingAddress = new Address()
            {
                Country = "USA",
                City = "Trenton",
                Street = "369 Maple Lane"

            };

            Cart testCartPremiumCustomerStandardShippingOneItem = new Cart()
            {
                Id = "001",
                CustomerId = "98",
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Standard,
                ShippingAddress = testShippingAddress,
                Items = testListOneItem
            };

            CheckoutDto actual = testCheckOutEngine.CalculateTotals(testCartPremiumCustomerStandardShippingOneItem);

            Assert.Equal(10, actual.CustomerDiscount);

        }

        [Fact]
        public void CalculateTotals_PremiumCustomer_OneItemTotalEqualsCostPlusShippingMinusDiscount()
        {
            var testMapConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper testMapper = testMapConfig.CreateMapper();

            ShippingCalculator testShippingCalculator = new ShippingCalculator();
            CheckOutEngine testCheckOutEngine = new CheckOutEngine(testShippingCalculator, testMapper);

            List<Item> testListOneItem = new List<Item>();
            testListOneItem.Add(
                new Item()
                {
                    ProductId = "5",
                    ProductName = "Hundred Dollar Bill",
                    Price = 100,
                    Quantity = 1
                });

            Address testShippingAddress = new Address()
            {
                Country = "USA",
                City = "Trenton",
                Street = "369 Maple Lane"

            };

            Cart testCartPremiumCustomerStandardShippingOneItem = new Cart()
            {
                Id = "645",
                CustomerId = "157",
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Standard,
                ShippingAddress = testShippingAddress,
                Items = testListOneItem
            };

            CheckoutDto actual = testCheckOutEngine.CalculateTotals(testCartPremiumCustomerStandardShippingOneItem);

            Assert.Equal(91.80, actual.Total);

        }

        [Fact]
        public void CalculateTotals_PremiumCustomer_MoreThanOneItemTotalEqualsCostPlusShippingMinusDiscount()
        {
            var testMapConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper testMapper = testMapConfig.CreateMapper();

            ShippingCalculator testShippingCalculator = new ShippingCalculator();
            CheckOutEngine testCheckOutEngine = new CheckOutEngine(testShippingCalculator, testMapper);

            List<Item> testListTwoItems = new List<Item>();
            testListTwoItems.Add(
                new Item()
                {
                    ProductId = "867",
                    ProductName = "FatHead",
                    Price = 32,
                    Quantity = 1
                });
            testListTwoItems.Add(
                new Item()
                {
                    ProductId = "3245",
                    ProductName = "BobbleHead",
                    Price = 20,
                    Quantity = 1
                });

            Address testShippingAddress = new Address()
            {
                Country = "USA",
                City = "Trenton",
                Street = "369 Maple Lane"

            };

            Cart testCartPremiumCustomerStandardShippingTwoItems = new Cart()
            {
                Id = "645",
                CustomerId = "157",
                CustomerType = CustomerType.Premium,
                ShippingMethod = ShippingMethod.Standard,
                ShippingAddress = testShippingAddress,
                Items = testListTwoItems
            };

            CheckoutDto actual = testCheckOutEngine.CalculateTotals(testCartPremiumCustomerStandardShippingTwoItems);

            Assert.Equal(50.40, actual.Total);

        }

    }
}

