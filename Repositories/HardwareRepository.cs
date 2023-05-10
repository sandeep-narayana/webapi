using webapi1.Models;
using webapi1.utilities;
using Dapper;
namespace webapi1.Repositories;

public interface IHardwareRepository 
{
    Task<Hardware> Create(Hardware Item);
    Task<bool> Update (Hardware Item);
    Task<bool> Delete (int id);

    Task<List<Hardware>> getAllForEmployees(long employee_number);

    Task<Hardware> getHardwareForEmployee(long id );


}

public class HardwareRepository : BaseRepository, IHardwareRepository
{
    public HardwareRepository(IConfiguration configuration) : base(configuration)
    {

    }


    // create
    public async Task<Hardware> Create(Hardware Item)
    {
        var query = $@"INSERT INTO {TableNames.hardware} (name,mac_address,type,user_employee_number) VALUES(@Name,@MacAddress,@type,@userEmployeeNumber)";

        using (var con = NewConnection){
             return await con.QuerySingleOrDefaultAsync<Hardware>(query,Item);
        }
    }

    public async Task<bool> Delete(int id)
    {
        var query = @$" DELETE FROM {TableNames.hardware} WHERE Id = @Id ";

        using (var con = NewConnection)
        {
            long rowCount = await con.ExecuteAsync(query,new {
                Id = id
            });
            return rowCount == 1;
        }


    }

    public async Task<List<Hardware>> getAllForEmployees(long employee_number)
    {
        var query = @$" SELECT * FROM {TableNames.hardware} WHERE user_employee_number = @UserEmployeeNumber ";

        List<Hardware> res; 
        using (var con = NewConnection)
        {
            res = (await con.QueryAsync<Hardware>(query,new{
                UserEmployeeNumber = employee_number
            })).AsList(); 
        }
        return res;
    }

    public async Task<Hardware> getHardwareForEmployee(long id)
    {
        var query = @$" SELECT * FROM {TableNames.hardware} WHERE Id = @Id";

        Hardware res; 
        using (var con = NewConnection)
        {
            res = (Hardware) await con.QuerySingleOrDefaultAsync<Hardware>(query,new{
                Id = id
            });
        }
        return res;
    }

    public async Task<bool> Update(Hardware Item)
    {
        var query = @$"UPDATE {TableNames.hardware} SET name = @Name, mac_address = @MacAddress, type = @Type, user_employee_number = @UserEmployeeNumber WHERE id = @Id";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query,Item);
            return rowCount == 1;
            
        }
    }
}
