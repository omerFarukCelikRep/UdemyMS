namespace UdemyMS.Microservices.Order.Application.Dtos;
public class AddressDto
{
    public string Province { get; }
    public string District { get; }
    public string Street { get; }
    public string ZipCode { get; }
    public string Line { get; }
}