using Domain.Interfaces.Services;
using ExternalServices.Services;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LilitooCrawler.Api.Controllers.V1;
public class LilitooController : BaseController
{
    private readonly ILilitooReadServices _readServices;
    public LilitooController(ILilitooReadServices readServices)
    {
        _readServices = readServices;
    }
    [HttpGet]
    public async Task<IActionResult> GetMo()
    {
        IWebDriver driver = new ChromeDriver
        {
            Url = "https://lilitoo.com/product-category/%d8%a8%d8%af%d9%88%d9%86-%d8%af%d8%b3%d8%aa%d9%87%d8%a8%d9%86%d8%af%db%8c/"
        };

        // Navigate to the webpage
        driver.Navigate();

        // Find the element by class name
        IWebElement element = driver.FindElement(By.CssSelector(".product-element-top .product-image-link"));

        // Get the value of the href attribute
        string hrefValue = element.GetAttribute("href");
        element.Click();
        // Output the href value
        Console.WriteLine("Href Attribute Value: " + hrefValue);

        // Close the browser
        //await _readServices.GetMouthAndToothProduct();
        //await _readServices.GetUncategorizedProduct();

        driver.Url = hrefValue;

        IWebElement productTitle = driver.FindElement(By.ClassName("product_title"));
        string title = productTitle.Text;

        IWebElement productDescription = driver.FindElement(By.ClassName("woocommerce-product-details__short-description"));
        string description = productDescription.Text;

        IWebElement priceElement = driver.FindElement(By.CssSelector(".price"));

        string priceText = priceElement.Text;

        // Extracting the original price
        string originalPrice = priceText.Split("تومان")[1].Trim();

        // Extracting the current price
        string currentPrice = priceText.Split("تومان")[2].Trim();

        //image


        return Ok();
    }
    [HttpGet("hair-care")]
    public async Task<IActionResult> GetDahan()
    {
        IWebDriver driver = new ChromeDriver
        {
            Url = "https://lilitoo.com/product-category/skin-care/"
        };


        List<object> list = new List<object>();
        List<string> pageLinks = new List<string>();
        List<string> productLinks = new List<string>();

        IWebElement ulElement = driver.FindElement(By.ClassName("page-numbers"));
        IList<IWebElement> anchorTags = ulElement.FindElements(By.CssSelector("a.page-numbers"));

        foreach (IWebElement anchorTag in anchorTags)
        {
            string href = anchorTag.GetAttribute("href");
            pageLinks.Add(href);
        }
        pageLinks.Remove(pageLinks.Last());

        foreach (var link in pageLinks)
        {
            IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".product-element-bottom"));
            foreach(var element in elements)
            {
                IWebElement h3Element = element.FindElement(By.ClassName("wd-entities-title"));
                IWebElement anchorTag = h3Element.FindElement(By.TagName("a"));

                string hrefValue = anchorTag.GetAttribute("href");
                productLinks.Add(hrefValue);
            }
            driver.Url = link;
        }

        foreach (var link in productLinks)
        {

            string Name = "";
            List<string> Images = new List<string>();
            List<string> Description = new List<string>();
            string OldPrice="";
            string NewPrice = "";
            bool IsExsist = false;

            IWebDriver driver2 = new ChromeDriver
            {
                Url = link
            };

        }

        return Ok();
    }
}
