using Microsoft.AspNetCore.Mvc;
using webapi1.DTOs;
using webapi1.Repositories;
using webapi11.Models;

namespace webapi1.Controllers;


[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController > _logger;
    private readonly IUserRepository _userRepository;

    private readonly IHardwareRepository _hardwareRepository;

    public UserController(ILogger<UserController> logger,IUserRepository userRepository,IHardwareRepository hardwareRepository
    )
    {
        _logger = logger;
        _userRepository = userRepository;
        _hardwareRepository = hardwareRepository;
    }


    //get user by id
    [HttpGet("{employee_number}")]
    public async Task<ActionResult<userDto>> getUserById([FromRoute] long employee_number)
    {
        var user = await _userRepository.get(employee_number);
        if(user== null){
            return Ok(NotFound("no user found with this id"));
        }
        
        // convert it to dto
        // var userDto = user.asDto;
 
        // return Ok(userDto);

        // if i want the details with hardware

        var dto = user.asDto;
        dto.Hardware = await _hardwareRepository.getAllForEmployees(user.EmployeeNumber);

        return Ok(dto);


    }

    [HttpGet]
    public async Task<ActionResult<List<userDto>>> getAllUser() // ActionResult for status code
    
    {
        var userList  = await _userRepository.getList();
        // convert it into userDTO
        var userDto = userList.Select(x=>x.asDto); // stream like map
        
        
        return Ok(userDto);
 
    }

    // create user
    [HttpPost]
    public async Task<ActionResult<userDto>> createUser([FromBody] userCreateDto data )
    {
        //if(ModelState.IsValid) // cheking the validity but not required in new versions

        // here we will check mini 18 years and gender must be male or female (1,2)

        if(!(new string[] {"male","female"}.Contains(data.Gender.Trim().ToLower())) )
        {
            return BadRequest("Gender value is not recognised");

        }


         var subtractDate = DateTimeOffset.Now - data.DateOfBirth; // return type timespan

        if(subtractDate.TotalDays/365<18){
            return BadRequest("Employee must be atleast 18 years old");
        }

        var toCreateUser = new User{
            FirstName = data.FirstName.Trim(),
            LastName = data.LastName.Trim(),
            Email = data.Email.Trim().ToLower(),
            DateOfBirth = data.DateOfBirth,
            Mobile = data.Mobile,
            Gender = Enum.Parse<Gender>(data.Gender)
        };
        var createdUser = await _userRepository.Create(toCreateUser);

        //return Ok(createdUser.asDto);

        return StatusCode(StatusCodes.Status201Created,createdUser.asDto);

        

    }


    // update user
    [HttpPut("{employee_number}")]
    public async Task<ActionResult> UpdateUserbyId([FromRoute] long employee_number, [FromBody] userUpdateDto data)
    {
        // get the existing user
        var existingUser = await _userRepository.get(employee_number);
        if(existingUser==null){
            return NotFound("No user found with this employee id");
        }

        var toUpdatedUser = existingUser with{
            Email = data.Email?.Trim()?.ToLower() ?? existingUser.Email, // imp
            FirstName= data.FirstName?.Trim()?.ToLower() ?? existingUser.FirstName,
            LastName = data.LastName?.Trim()?.ToLower() ?? existingUser.LastName,
            Mobile = data.Mobile ?? existingUser.Mobile, // ? cannot be applied with operand long so for that modify updatedto
        }; 

        var didUpdate = await _userRepository.Update(toUpdatedUser);

        if(didUpdate==false){
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");
        }

        return NoContent();



    } 

    // delete user
    [HttpDelete("{employee_number}")]
    public async Task<ActionResult> DeleteUserById([FromRoute] long employee_number)
    {
        // find the employee
        var user = await _userRepository.get(employee_number);
        if(user==null){
            return NotFound("No user found with this employee id");
        }

        var didDelete = await _userRepository.Delete(employee_number);
        if(didDelete == false){
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not delete user");
        }

        return NoContent();

    }

}
