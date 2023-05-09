
using webapi11.Models;
using Dapper;
using webapi1.utilities;

namespace webapi1.Repositories;


// interface for method
public interface IUserRepository 
{ 
    Task<User> Create(User user);
    Task<bool> Update(User user);
    Task<bool> Delete (long userId);
    Task<User> get(long userId);
    Task<List<User>> getList();
}
    public class UserRepository : BaseRepository,IUserRepository
    {
        // constructors
         public UserRepository(IConfiguration config) : base(config)
         {

         }

    public async Task<User> Create(User user)
    {

        var query = @$"INSERT INTO ""{TableNames.user}""(first_name,last_name,date_of_birth,mobile,email,gender) 
        VALUES(@FirstName,@LastName,@DateOfBirth, @Mobile,@Email,@Gender) RETURNING *;";

        using(var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<User>(query,user); // it will autometically match the fields 
            return res;
        }

    }

    // delete user by id
    public async Task<bool> Delete(long userId)
    {
        var query = @$" DELETE FROM ""{TableNames.user}"" WHERE employee_number = @EmployeeNumber ";

        using(var Con = NewConnection)
        {
            var rowCount = await Con.ExecuteAsync(query,new {
                EmployeeNumber= userId
            });
            return rowCount == 1;
        }
    }

    // get user by id
    public async Task<User> get(long userId)
    {
        //var query = $@" SELECT * FROM ""{TableNames.user}"" WHERE employee_number = {userId} ";
        var query = $@" SELECT * FROM ""{TableNames.user}"" WHERE employee_number = @empNum ";

       
        User res;
        using(var con = NewConnection){
            //res = (User)await con.QuerySingleOrDefaultAsync<User>(query); //if two records will be there it will through an error

            // risk ==> security issue ==> SQL injection 
            // OR 1=1 DROP TABLE user;
            // so we will use this // remove the dynamic element from queryother they can modify it by sending a long code 

            res = (User)await con.QuerySingleOrDefaultAsync<User>(query,new {
                empNum = userId // this will allow only one
            });

        }
        return res;
    }


    // get all the contacts
    public async Task<List<User>> getList()
    {

       var query = $@"SELECT * FROM ""user""";

       List<User> res;

        using(var con = NewConnection){
            res =  (await con.QueryAsync<User>(query)).AsList(); // return type is List
        }

        return res;
    }

    public async Task<bool> Update(User user)
    {
        var query = @$"UPDATE ""{TableNames.user}"" SET first_name = @FirstName, last_name = @LastName, email = @Email, mobile = @Mobile WHERE employee_number = @EmployeeNumber";

        using(var con = NewConnection){
            var rowCount =  await con.ExecuteAsync(query,user); // excute with no return => sql will thrown the count of updated rows;
            return rowCount==1;  // this will return bool value true if == 1 esle false

        }


    }
}



