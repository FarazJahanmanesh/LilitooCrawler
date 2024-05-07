using Domain.Interfaces.ExternalServices;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalServices.Services;
public class LilitooServices: ILilitooServices
{
    //ارایشی و بهداشتی
    public async Task GetCosmeticProduct()
    {

    }
    //مراقبت پوستی
    public async Task GetSkinCareProduct()
    {

    }
    //مراقبت مو
    public async Task GetHairCareProduct()
    {

    }
    //دهان و دندان
    public async Task GetMouthAndToothProduct()
    {

    }
    //فاقد دسته 
    public async Task<string> GetUncategorizedProduct()
    {
        RestClient client = new RestClient("https://lilitoo.com/product-category/بدون-دستهبندی/");
        RestRequest request = new RestRequest();
        request.Method = Method.Get;
        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            var content = response.Content;
            if(content != null) 
            { 
                return content;
            }
            else
            {
                return "problem to take content";
            }
        }
        else
        {
            return "problem to send request";
        }
    }
}
