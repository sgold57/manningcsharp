using ShoppingCartService.BusinessLogic.Validation;
using ShoppingCartService.Models;
namespace ShoppingCartServiceTest;

public class AddressValidatorTests
{
    [Fact]
    public void TrueWhenAddressIsValid()
    {
        AddressValidator testValidator = new AddressValidator();
        var validTestAddress = new Address
        {
            Country = "USA",
            City = "Milwaukee",
            Street = "1408 Cusack Drive"
        };

        bool result = testValidator.IsValid(validTestAddress);

        Assert.True(result);

    }

    [Fact]
    public void FalseWhenAddressIsEmpty()
    {
        AddressValidator testValidator = new AddressValidator();
        var emptyTestAddress = new Address();

        bool result = testValidator.IsValid(emptyTestAddress);

        Assert.False(result);

    }

    [Fact]
    public void FalseWhenCountryIsEmpty()
    {
        AddressValidator testValidator = new AddressValidator();
        var noCountryTestAddress = new Address
        {
            Country = "",
            City = "New York",
            Street = "1 Broadway"
        };

        bool result = testValidator.IsValid(noCountryTestAddress);

        Assert.False(result);
    }

    [Fact]
    public void FalseWhenCityIsEmpty()
    {
        AddressValidator testValidator = new AddressValidator();
        var noCityTestAddress = new Address
        {
            Country = "United States",
            City = "",
            Street = "1234 Elm Street"
        };

        bool result = testValidator.IsValid(noCityTestAddress);

        Assert.False(result);
    }

    [Fact]
    public void FalseWhenStreetIsEmpty()
    {
        AddressValidator testValidator = new AddressValidator();
        var noStreetTestAddress = new Address
        {
            Country = "United States",
            City = "San Diego",
            Street = ""
        };

        bool result = testValidator.IsValid(noStreetTestAddress);

        Assert.False(result);
    }


}
