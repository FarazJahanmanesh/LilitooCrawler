using Domain.Interfaces.ExternalServices;
using HtmlAgilityPack;
using RestSharp;
using System.Text.RegularExpressions;
using System.Web;

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
    //لینک محصولات فاقد دسته
    //private
    private async Task<string> GetUncategorizedProductsLink()
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
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(response.Content);

                HtmlNode linkNode = doc.DocumentNode.SelectSingleNode("//div[@class='product-element-bottom']//a");
                string link = linkNode.GetAttributeValue("href", "");
                if (!string.IsNullOrEmpty(link))
                    return link;
                else
                    return "prroblem to get link";
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

    public async Task<string> GetUncategorizedProduct()
    {
        var links = await GetUncategorizedProductsLink();
        foreach(var link in links)
        {
            RestClient client = new RestClient(links);
            RestRequest request = new RestRequest();
            request.Method = Method.Get;
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = response.Content;
                if (content != null)
                {
                    string pattern = @"<div class=""woocommerce-product-details__short-description"">\s*<ul>(.*?)<\/ul>\s*<\/div>";
                    Match match = Regex.Match(response.Content, pattern, RegexOptions.Singleline);

                    if (match.Success)
                    {
                        string shortDescription = match.Groups[1].Value;

                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(shortDescription);
                        List<string> liTextList = new List<string>();
                        foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//li"))
                        {
                            liTextList.Add(node.InnerText);
                        }
                    }

                    return "prroblem to get link";
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
        return "oooh";
    }
}
