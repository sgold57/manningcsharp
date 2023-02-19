
using ShoppingCartService.BusinessLogic;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;

namespace ShoppingCartServiceTest;

public class PremiumCustomerTests
{
    public PremiumCustomerTests()
    {

    }

    [InlineData(0, ShippingMethod.Priority, 0)]
    [InlineData(1, ShippingMethod.Priority, 1)]
    [InlineData(5, ShippingMethod.Priority, 5)]
    [InlineData(0, ShippingMethod.Expedited, 0)]
    [InlineData(1, ShippingMethod.Expedited, 1)]
    [InlineData(5, ShippingMethod.Expedited, 5)]
    [Theory]
    public void CalculateShippingCost_SameCity(
        uint itemQuantity,
        ShippingMethod shippingMethod,
        double expected)
    {
        Address originAddress = CreateAddress();
        ShippingCalculator sut = new ShippingCalculator(originAddress);

        Address destinationAddressSameCity = CreateAddress(street: "1408 Cusack Street");
        List<Item> testList = CreateItemList(itemQuantity);

        Cart testCartPremiumCustomerSameCity = CreateCart(
            shippingMethod: shippingMethod,
            address: destinationAddressSameCity,
            items: testList
        );

        var actual = sut.CalculateShippingCost(testCartPremiumCustomerSameCity);

        Assert.Equal(expected, actual);

    }

    [InlineData(0, ShippingMethod.Priority, 0)]
    [InlineData(1, ShippingMethod.Priority, 2)]
    [InlineData(5, ShippingMethod.Priority, 10)]
    [InlineData(0, ShippingMethod.Expedited, 0)]
    [InlineData(1, ShippingMethod.Expedited, 2)]
    [InlineData(5, ShippingMethod.Expedited, 10)]
    [Theory]
    public void CalculateShippingCost_SameCountry(
        uint itemQuantity,
        ShippingMethod shippingMethod,
        double expected)
    {
        Address originAddress = CreateAddress();
        ShippingCalculator sut = new ShippingCalculator(originAddress);

        Address destinationAddressSameCountry = CreateAddress(city: "Portland");
        List<Item> testList = CreateItemList(itemQuantity);

        Cart testCartPremiumCustomerSameCountry = CreateCart(
            shippingMethod: shippingMethod,
            address: destinationAddressSameCountry,
            items: testList
        );

        var actual = sut.CalculateShippingCost(testCartPremiumCustomerSameCountry);

        Assert.Equal(expected, actual);

    }

    [InlineData(0, ShippingMethod.Priority, 0)]
    [InlineData(1, ShippingMethod.Priority, 15)]
    [InlineData(3, ShippingMethod.Priority, 45)]
    [InlineData(0, ShippingMethod.Expedited, 0)]
    [InlineData(1, ShippingMethod.Expedited, 15)]
    [InlineData(3, ShippingMethod.Expedited, 45)]
    [Theory]
    public void CalculateShippingCost_International(
        uint itemQuantity,
        ShippingMethod shippingMethod,
        double expected)
    {
        Address originAddress = CreateAddress();
        ShippingCalculator sut = new ShippingCalculator(originAddress);

        Address destinationAddressSameCountry = CreateAddress(country: "Canada");
        List<Item> testList = CreateItemList(itemQuantity);

        Cart testCartPremiumCustomerInternational = CreateCart(
            shippingMethod: shippingMethod,
            address: destinationAddressSameCountry,
            items: testList
        );

        var actual = sut.CalculateShippingCost(testCartPremiumCustomerInternational);

        Assert.Equal(expected, actual);
    }

    private List<Item> CreateItemList(uint quantity = 1)
    {
        List<Item> newList = new List<Item>();
        newList.Add(new Item { Quantity = quantity });

        return newList;
    }

    private Address CreateAddress(
        string country = "USA",
        string city = "Dallas",
        string street = "123 Main Street")
    {
        return new Address()
        {
            Country = country,
            City = city,
            Street = street
        };
    }

    private Cart CreateCart(
        ShippingMethod shippingMethod,
        Address address,
        List<Item> items,
        CustomerType customerType = CustomerType.Premium)
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
