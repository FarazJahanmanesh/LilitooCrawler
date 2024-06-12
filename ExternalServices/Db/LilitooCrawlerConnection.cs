using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ExternalServices.Db;


public class LilitooCrawlerConnection
{
    public static IConfigurationRoot Configuration;
    public static string GetConnectionString()
    {
        var builder = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json");
        Configuration = builder.Build();
        var connectionString = Configuration.GetSection("ConnectionStrings:ZinafConnection").Value.ToString();
        return connectionString;
    }
    public static SqlConnection Connection
    {
        get
        {
            SqlConnection.ClearAllPools();
            var connectionString = GetConnectionString();
            SqlConnection _Connection = new SqlConnection(connectionString);
            _Connection.Open();
            return _Connection;
        }
    }
    public static SqlCommand Command(string StoredProcedureName)
    {
        int sleepTime = 2000;
        SqlConnection.ClearAllPools();
        var connectionString = GetConnectionString();
        SqlCommand _Command = new SqlCommand(StoredProcedureName, new SqlConnection(connectionString))
        {
            CommandType = CommandType.StoredProcedure
        };
        _Command.CommandTimeout = 30;
        try
        {
            _Command.Connection.Open();
            _Command.CommandTimeout = 30;
            SqlCommandBuilder.DeriveParameters(_Command);
            return _Command;
        }
        catch (Exception ex)
        {
            try
            {
                if (_Command.Connection.State == ConnectionState.Closed)
                {
                    System.Threading.Thread.Sleep(sleepTime);
                    _Command.Connection.Open();
                    SqlCommandBuilder.DeriveParameters(_Command);
                }
                return _Command;
            }
            catch (System.Exception)
            {
                try
                {
                    if (_Command.Connection.State == ConnectionState.Closed)
                    {
                        System.Threading.Thread.Sleep(sleepTime);
                        _Command.Connection.Open();
                        SqlCommandBuilder.DeriveParameters(_Command);
                    }
                    return _Command;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
    }
    public static SqlCommand FunctionCommand(string FunctionName)
    {
        int sleepTime = 2000;
        SqlConnection.ClearAllPools();
        var connectionString = GetConnectionString();
        SqlCommand _Command = new SqlCommand(FunctionName, new SqlConnection(connectionString))
        {
            CommandType = CommandType.Text
        };
        _Command.CommandTimeout = 30;
        try
        {
            _Command.Connection.Open();
            _Command.CommandTimeout = 30;
            SqlCommandBuilder.DeriveParameters(_Command);
            return _Command;
        }
        catch (Exception ex)
        {
            try
            {
                if (_Command.Connection.State == ConnectionState.Closed)
                {
                    System.Threading.Thread.Sleep(sleepTime);
                    _Command.Connection.Open();
                    SqlCommandBuilder.DeriveParameters(_Command);
                }
                return _Command;
            }
            catch (System.Exception)
            {
                try
                {
                    if (_Command.Connection.State == ConnectionState.Closed)
                    {
                        System.Threading.Thread.Sleep(sleepTime);
                        _Command.Connection.Open();
                        SqlCommandBuilder.DeriveParameters(_Command);
                    }
                    return _Command;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
    }
}

