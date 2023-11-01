using UdemyMS.Microservices.Order.Domain.ValueObjects;

namespace UdemyMS.Microservices.Order.Application.Dtos;
public record AddressDto(string Province, string District, string Street, string ZipCode, string Line)
{
    public static implicit operator AddressDto(Address address) => new(
        address.Province,
        address.District,
        address.Street,
        address.ZipCode,
        address.Line);

    public static implicit operator Address(AddressDto addressDto) => new(addressDto.Province,
                                                                          addressDto.District,
                                                                          addressDto.Street,
                                                                          addressDto.ZipCode,
                                                                          addressDto.Line);
}