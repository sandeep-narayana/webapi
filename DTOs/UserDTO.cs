using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using webapi1.Models;

namespace webapi1.DTOs;

public record userDto{


        [JsonPropertyName("employee_number")] 
        public long EmployeeNumber{get; set;}
        [JsonPropertyName("first_name")] 
        public string FirstName {get; set;}
        [JsonPropertyName("last_name")] 
        public string LastName {get; set;}
        public long Mobile {get; set;}
        public string Email {get; set;}
        public string Gender {get; set;} // here it will me in form of male or female

        public List<Hardware> Hardware{get; set;}
}

public record userCreateDto{

        [JsonPropertyName("first_name")] 
        [Required] // constraint for data integrity
        [MaxLength(50)] // max lenght will be 50 otherwise rejected
        public string FirstName {get; set;}
        [JsonPropertyName("last_name")] 
        [Required]
        [MaxLength(50)]
        public string LastName {get; set;}
        [JsonPropertyName("date_of_birth")]
        [Required]
        public DateTimeOffset DateOfBirth {get; set;}
        [Required]
        public long Mobile {get; set;}
        [Required]
        [MaxLength(255)]
        public string Email {get; set;}
        [Required]
        [MaxLength(6)]
        public string Gender {get; set;} // here it will me in form of male or female
        
}

public record userUpdateDto{

        [JsonPropertyName("first_name")]
        public string FirstName {get; set;}
        [JsonPropertyName("last_name")]
        public string LastName {get; set;}
        public long? Mobile {get; set;} = null;
        public string Email {get; set;}
}