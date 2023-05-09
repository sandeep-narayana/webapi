using webapi1.DTOs;

namespace webapi11.Models  // package
{
    // ENUM for Gender
    public enum Gender{
        male = 1,
        female = 2,
    }

    // user entity class
    public record User
    {

        public long EmployeeNumber{get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public DateTimeOffset DateOfBirth {get; set;}
        public long Mobile {get; set;}
        public string Email {get; set;}
        public Gender Gender {get; set;}

        public userDto asDto => new userDto{
                    Email = this.Email,
                    EmployeeNumber = this.EmployeeNumber,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Mobile = this.Mobile,
                    Gender = this.Gender.ToString().ToLower(),
                };

        

        // public userDto asDto
        // {
        //     get {
        //         return new userDto{
        //             Email = this.Email,
        //             EmployeeNumber = this.EmployeeNumber,
        //             FirstName = this.FirstName,
        //             LastName = this.LastName,
        //             Mobile = this.Mobile,
        //             Gender = this.Gender.ToString().ToLower(),
        //         };
        //     }

        // }



    }
}
