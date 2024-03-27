namespace Domain;

public class Address
{
    public int Number { get; init; }
    public string Street { get; init; }
    public int PostCode { get; init; }
    public Cities City { get; init; }
    public Address(int number, string street, int postCode, Cities city)
    {
        Number = number;
        Street = street;
        PostCode = postCode;
        City = city;
    }
    public bool Equal(Address other)
    {
        return other.Number == Number
        && other.Street.Equals(Street, StringComparison.OrdinalIgnoreCase)
        && other.PostCode == PostCode
        && other.City == City;
    }
}