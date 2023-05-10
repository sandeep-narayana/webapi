using System.Text.Json.Serialization;

namespace webapi1.Models;


public enum HardwareType
{
    Laptop = 1,
    CPU = 2,
    Monitor = 3,
    Accessory = 4,
    SmartPhone = 5

}
public record Hardware
{
    public int Id{get; set;}
    public string Name{get; set;}
    [JsonPropertyName("mac_address")]
    public string MacAddress{get; set;} = null; // just to remind that it is nullable
    public HardwareType type{get; set;}
    [JsonPropertyName("user_employee_number")]
    public long UserEmployeeNumber{get; set;}
    [JsonPropertyName("create_at")]
    public DateTimeOffset CreateAt{get; set;}

}

public record HardwareCreateDto
{
    [JsonPropertyName("name")]
    public string Name{get; set;}
    [JsonPropertyName("mac_address")]
    public string MacAddress{get; set;} = null; // just to remind that it is nullable
    
    public int type{get; set;} // it is payload only
    [JsonPropertyName("user_employee_number")]
    public long userEmployeeNumber{get; set;}
}