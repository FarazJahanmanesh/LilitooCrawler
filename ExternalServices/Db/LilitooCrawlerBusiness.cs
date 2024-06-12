using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ExternalServices.Db;

public class LilitooCrawlerBusiness
{
    public void InsertProduct(Product product)
    {
        SqlCommand command = null;
        SqlDataReader reader = null;
        try
        {
            command = LilitooCrawlerConnection.Command("AZ.");//procedure name
            command.Parameters["@Name"].Value = string.IsNullOrEmpty(product.Name) ? DBNull.Value : product.Name;
            command.Parameters["@Price"].Value = string.IsNullOrEmpty(product.Price) ? DBNull.Value : product.Price;
            command.Parameters["@OldPrice"].Value = string.IsNullOrEmpty(product.OldPrice) ? DBNull.Value : product.OldPrice;
            command.Parameters["@NewPrice"].Value = string.IsNullOrEmpty(product.NewPrice) ? DBNull.Value : product.NewPrice;
            command.Parameters["@IsExist"].Value = product.IsExist;
            command.Parameters["@ImageUrls"].Value = string.IsNullOrEmpty(JsonConvert.SerializeObject(product.ImageUrls)) ? DBNull.Value : JsonConvert.SerializeObject(product.ImageUrls);
            command.Parameters["@Description"].Value = string.IsNullOrEmpty(JsonConvert.SerializeObject(product.Description)) ? DBNull.Value : JsonConvert.SerializeObject(product.Description);
            reader = command.ExecuteReader();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
            }
            if (command != null)
            {
                command.Connection.Close();
                command.Connection.Dispose();
                command.Dispose();
            }
        }
    }
}
