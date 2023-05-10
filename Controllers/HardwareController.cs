using Microsoft.AspNetCore.Mvc;
using webapi1.DTOs;
using webapi1.Models;
using webapi1.Repositories;
using webapi11.Models;

namespace webapi1.Controllers;


[ApiController]
[Route("api/hardware")]
public class HardwareController : ControllerBase
{

    private readonly ILogger<HardwareController > _logger;
    private readonly IHardwareRepository _hardwareRepository;

    private readonly IUserRepository _userRepository;


    public HardwareController(ILogger<HardwareController> logger,IHardwareRepository hardwareRepository,IUserRepository userRepository
    )
    {
        _logger = logger;
        _hardwareRepository = hardwareRepository;
        _userRepository = userRepository;


    }


    // //get hardware by id
    // [HttpGet("{employee_number}")]
    // public async Task<ActionResult<userDto>> getUserById([FromRoute] long employee_number)
    // {
    //     var hardware = await _hardwareRepository.getHardwareForEmployee(employee_number);
    //     if(hardware== null){
    //         return Ok(NotFound("no hardware found with this id"));
    //     }
        
    //     // convert it to dto
    //     var userDto = hardware.asDto;
 
    //     return Ok(userDto);

    // }

    // [HttpGet]
    // public async Task<ActionResult<List<userDto>>> getAllUser() // ActionResult for status code
    // {
    //     var userList  = await _hardwareRepository.getList();
    //     // convert it into userDTO
    //     var userDto = userList.Select(x=>x.asDto); // stream like map
        
        
    //     return Ok(userDto);
 
    // }

    // create hardware
    [HttpPost]
    public async Task<ActionResult<Hardware>> createHardware([FromBody] HardwareCreateDto data )
    {
        var user = await _userRepository.get(data.userEmployeeNumber);
        if(user==null){
            return NotFound("NO USER FOUND WITH THIS USER ID");
        }


        var toCreatehardware = new Hardware{
            Name = data.Name.Trim(),
            MacAddress = data.MacAddress?.Trim(),
            type = (HardwareType)data.type,
            UserEmployeeNumber = data.userEmployeeNumber, // validation in imp here already done above
        };
        var createdHardware = await _hardwareRepository.Create(toCreatehardware);

        //return Ok(createdUser.asDto);

        return StatusCode(StatusCodes.Status201Created,createdHardware);
    }


    // update hardware
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateHardwarebyId([FromRoute] int id, [FromBody] HardwareCreateDto data)
    {
        // get the existing hardware
        var existingHardware = await _hardwareRepository.getHardwareForEmployee(id);
        if(existingHardware==null){
            return NotFound("No hardware found with this hardware id");
        }

        var user = await _userRepository.get(data.userEmployeeNumber);
        if(user==null){
            return NotFound("NO USER FOUND WITH THIS USER ID");
        }

        var toUpdatedhardware = existingHardware with{
            Name = data.Name.Trim(),
            MacAddress = data.MacAddress?.Trim(),
            type = (HardwareType)data.type,
            UserEmployeeNumber = data.userEmployeeNumber, // validation in imp here already done above
        }; 

        var didUpdate = await _hardwareRepository.Update(toUpdatedhardware);

        if(didUpdate==false){
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update hardware");
        }

        return NoContent();



    } 

    // delete hardware
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHardwareById([FromRoute] int id)
    {
        // find the employee
        var hardware = await _hardwareRepository.getHardwareForEmployee(id);
        if(hardware==null){
            return NotFound("No hardware found with this employee id");
        }

        var didDelete = await _hardwareRepository.Delete(id);
        if(didDelete == false){
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not delete hardware");
        }

        return NoContent();

    }

}
