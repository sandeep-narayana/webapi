
using webapi11.Models;

namespace webapi1.Repositories;


// interface for method
public interface IUserRepository
{ 
    Task<User> Create(User user);
    Task<User> Update(User user);
    Task Delete (long userId);
    Task<User> get(long userId);
    Task<List<User>> getList();


}
    public class UserRepository : IUserRepository
    {
        // constructors
         public UserRepository(){

         }

    public Task<User> Create(User user)
    {
        throw new NotImplementedException();
    }

    public Task Delete(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<User> get(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> getList()
    {
        throw new NotImplementedException();
    }

    public Task<User> Update(User user)
    {
        throw new NotImplementedException();
    }
}



