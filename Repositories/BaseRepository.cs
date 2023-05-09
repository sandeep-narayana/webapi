using Npgsql;
using webapi1.Settings;

namespace webapi1.Repositories;


public class BaseRepository
{


private readonly IConfiguration _configuration;

    // constructor
    public BaseRepository(IConfiguration configuration)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        _configuration = configuration;
    }

    public NpgsqlConnection NewConnection => new NpgsqlConnection(_configuration
    .GetSection(nameof(PostgresSettings)).Get<PostgresSettings>().ConnectionString);
}