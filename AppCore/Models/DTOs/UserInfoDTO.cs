using AppCore.Models.OrderModels;

namespace AppCore.Models.DTOs;

public class UserInfoDTO
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string StreetAddress { get; set; }
    public string Phone { get; set; }
    public List<Order> Orders { get; set; }
}