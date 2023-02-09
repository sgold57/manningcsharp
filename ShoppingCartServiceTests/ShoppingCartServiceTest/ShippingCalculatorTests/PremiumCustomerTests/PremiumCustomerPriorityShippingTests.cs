
using ShoppingCartService.BusinessLogic;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;

namespace ShoppingCartServiceTest;

public class PremiumCustomerPriorityShippingTests
{
    public PremiumCustomerPriorityShippingTests()
    {

    }

    [Fact]
    public void CalculateShippingCost_OneItemSameCity()
    {
        ShippingCalculator testShippingCalculator = new ShippingCalculator();
        List<Item> testListOneItem = new List<Item>();
        testListOneItem.Add(
            new Item()
            {
                ProductId = "1",
                ProductName = "Toilet Plunger",
                Price = 30,
                Quantity = 1
            });

        Address testShippingAddress = new Address()
        {
            Country = "USA",
            City = "Dallas",
            Street = "1408 Cusack Drive"

        };

        Cart testCartPremiumCustomerSameCityOneItem = new Cart()
        {
            Id = "000",
            CustomerId = "1",
            CustomerType = CustomerType.Premium,
            ShippingMethod = ShippingMethod.Priority,
            ShippingAddress = testShippingAddress,
            Items = testListOneItem
        };

        var actual = testShippingCalculator.CalculateShippingCost(testCartPremiumCustomerSameCityOneItem);

        Assert.Equal(1, actual);

    }


    [Fact]
    public void CalculateShippingCost_MoreThanOneItemSameCity()
    {
        ShippingCalculator testShippingCalculator = new ShippingCalculator();
        List<Item> testListMoreThanOneItem = new List<Item>();
        testListMoreThanOneItem.Add(
            new Item()
            {
                ProductId = "1",
                ProductName = "Toilet Plunger",
                Price = 75,
                Quantity = 5
            });

        Address testShippingAddress = new Address()
        {
            Country = "USA",
            City = "Dallas",
            Street = "1408 Cusack Drive"

        };

        Cart testCartPremiumCustomerSameCityMoreThanOneItem = new Cart()
        {
            Id = "000",
            CustomerId = "1",
            CustomerType = CustomerType.Premium,
            ShippingMethod = ShippingMethod.Priority,
            ShippingAddress = testShippingAddress,
            Items = testListMoreThanOneItem
        };

        var actual = testShippingCalculator.CalculateShippingCost(testCartPremiumCustomerSameCityMoreThanOneItem);

        Assert.Equal(5, actual);

    }

    [Fact]
    public void CalculateShippingCost_OneItemSameCountry()
    {
        ShippingCalculator testShippingCalculator = new ShippingCalculator();
        List<Item> testListOneItem = new List<Item>();
        testListOneItem.Add(
            new Item()
            {
                ProductId = "1",
                ProductName = "Toilet Plunger",
                Price = 30,
                Quantity = 1
            });

        Address testShippingAddress = new Address()
        {
            Country = "USA",
            City = "Tallahassee",
            Street = "1408 Cusack Drive"

        };

        Cart testCartPremiumCustomerSameCountryOneItem = new Cart()
        {
            Id = "000",
            CustomerId = "1",
            CustomerType = CustomerType.Premium,
            ShippingMethod = ShippingMethod.Priority,
            ShippingAddress = testShippingAddress,
            Items = testListOneItem
        };

        var actual = testShippingCalculator.CalculateShippingCost(testCartPremiumCustomerSameCountryOneItem);

        Assert.Equal(2, actual);

    }


    [Fact]
    public void CalculateShippingCost_MoreThanOneItemSameCountry()
    {
        ShippingCalculator testShippingCalculator = new ShippingCalculator();
        List<Item> testListMoreThanOneItem = new List<Item>();
        testListMoreThanOneItem.Add(
            new Item()
            {
                ProductId = "1",
                ProductName = "Toilet Plunger",
                Price = 75,
                Quantity = 4
            });
        testListMoreThanOneItem.Add(
            new Item()
            {
                ProductId = "37654",
                ProductName = "BopIt",
                Price = 436,
                Quantity = 1
            });

        Address testShippingAddress = new Address()
        {
            Country = "USA",
            City = "Portland",
            Street = "1408 Cusack Drive"

        };

        Cart testCartPremiumCustomerSameCountryMoreThanOneItem = new Cart()
        {
            Id = "000",
            CustomerId = "1",
            CustomerType = CustomerType.Premium,
            ShippingMethod = ShippingMethod.Priority,
            ShippingAddress = testShippingAddress,
            Items = testListMoreThanOneItem
        };

        var actual = testShippingCalculator.CalculateShippingCost(testCartPremiumCustomerSameCountryMoreThanOneItem);

        Assert.Equal(10, actual);

    }

    [Fact]
    public void CalculateShippingCost_OneItemInternational()
    {
        ShippingCalculator testShippingCalculator = new ShippingCalculator();
        List<Item> testListOneItem = new List<Item>();
        testListOneItem.Add(
            new Item()
            {
                ProductId = "1",
                ProductName = "Toilet Plunger",
                Price = 30,
                Quantity = 1
            });

        Address testShippingAddress = new Address()
        {
            Country = "Canada",
            City = "Ontario",
            Street = "123 Elm Street"

        };

        Cart testCartPremiumCustomerInternationalOneItem = new Cart()
        {
            Id = "000",
            CustomerId = "1",
            CustomerType = CustomerType.Premium,
            ShippingMethod = ShippingMethod.Priority,
            ShippingAddress = testShippingAddress,
            Items = testListOneItem
        };

        var actual = testShippingCalculator.CalculateShippingCost(testCartPremiumCustomerInternationalOneItem);

        Assert.Equal(15, actual);

    }


    [Fact]
    public void CalculateShippingCost_MoreThanOneItemInternational()
    {
        ShippingCalculator testShippingCalculator = new ShippingCalculator();
        List<Item> testListMoreThanOneItem = new List<Item>();
        testListMoreThanOneItem.Add(
            new Item()
            {
                ProductId = "1",
                ProductName = "Monopoly Board Game",
                Price = 75,
                Quantity = 3
            });

        Address testShippingAddress = new Address()
        {
            Country = "Canada",
            City = "Edmonton",
            Street = "345 Oilers Avenue"

        };

        Cart testCartPremiumCustomerInternationalMoreThanOneItem = new Cart()
        {
            Id = "000",
            CustomerId = "1",
            CustomerType = CustomerType.Premium,
            ShippingMethod = ShippingMethod.Priority,
            ShippingAddress = testShippingAddress,
            Items = testListMoreThanOneItem
        };

        var actual = testShippingCalculator.CalculateShippingCost(testCartPremiumCustomerInternationalMoreThanOneItem);

        Assert.Equal(45, actual);

    }
}
