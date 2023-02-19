
using ShoppingCartService.BusinessLogic;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Models;

namespace ShoppingCartServiceTest;

public class StandardCustomerTests
{
    public StandardCustomerTests()
    {

    }

    [InlineData(0, ShippingMethod.Standard, 0)]
    [InlineData(1, ShippingMethod.Standard, 1)]
    [InlineData(6, ShippingMethod.Standard, 6)]
    [InlineData(0, ShippingMethod.Priority, 0)]
    [InlineData(1, ShippingMethod.Priority, 2)]
    [InlineData(5, ShippingMethod.Priority, 10)]
    [InlineData(0, ShippingMethod.Express, 0)]
    [InlineData(1, ShippingMethod.Express, 2.5)]
    [InlineData(5, ShippingMethod.Express, 12.5)]
    [InlineData(0, ShippingMethod.Expedited, 0)]
    [InlineData(1, ShippingMethod.Expedited, 1.2)]
    [InlineData(5, ShippingMethod.Expedited, 6)]
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

        Cart testCartStandardCustomerSameCity = CreateCart(
            shippingMethod: shippingMethod,
            address: destinationAddressSameCity,
            items: testList
        );

        var actual = sut.CalculateShippingCost(testCartStandardCustomerSameCity);

        Assert.Equal(expected, actual);

    }

    [InlineData(0, ShippingMethod.Standard, 0)]
    [InlineData(1, ShippingMethod.Standard, 2)]
    [InlineData(5, ShippingMethod.Standard, 10)]
    [InlineData(0, ShippingMethod.Priority, 0)]
    [InlineData(1, ShippingMethod.Priority, 4)]
    [InlineData(5, ShippingMethod.Priority, 20)]
    [InlineData(0, ShippingMethod.Express, 0)]
    [InlineData(1, ShippingMethod.Express, 5)]
    [InlineData(4, ShippingMethod.Express, 20)]
    [InlineData(0, ShippingMethod.Expedited, 0)]
    [InlineData(1, ShippingMethod.Expedited, 2.4)]
    [InlineData(5, ShippingMethod.Expedited, 12)]
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

        Cart testCartStandardCustomerSameCountry = CreateCart(
            shippingMethod: shippingMethod,
            address: destinationAddressSameCountry,
            items: testList
        );

        var actual = sut.CalculateShippingCost(testCartStandardCustomerSameCountry);

        Assert.Equal(expected, actual);

    }

    [InlineData(0, ShippingMethod.Standard, 0)]
    [InlineData(1, ShippingMethod.Standard, 15)]
    [InlineData(3, ShippingMethod.Standard, 45)]
    [InlineData(0, ShippingMethod.Priority, 0)]
    [InlineData(1, ShippingMethod.Priority, 30)]
    [InlineData(3, ShippingMethod.Priority, 90)]
    [InlineData(0, ShippingMethod.Express, 0)]
    [InlineData(1, ShippingMethod.Express, 37.5)]
    [InlineData(3, ShippingMethod.Express, 112.5)]
    [InlineData(0, ShippingMethod.Expedited, 0)]
    [InlineData(1, ShippingMethod.Expedited, 18)]
    [InlineData(3, ShippingMethod.Expedited, 54)]
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

        Cart testCartStandardCustomerInternational = CreateCart(
            shippingMethod: shippingMethod,
            address: destinationAddressSameCountry,
            items: testList
        );

        var actual = sut.CalculateShippingCost(testCartStandardCustomerInternational);

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
        CustomerType customerType = CustomerType.Standard)
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
