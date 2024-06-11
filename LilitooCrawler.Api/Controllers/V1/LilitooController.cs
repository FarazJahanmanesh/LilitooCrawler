using Domain.Interfaces.Services;
using ExternalServices.Services;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net;

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
    [HttpGet]
    public async Task<IActionResult> GetDahanAndDandan()
    {
        IWebDriver driver = new ChromeDriver
        {
            Url = "https://lilitoo.com/product-category/makeup/"
        };


        List<object> list = new List<object>();
        List<string> pageLinks = new List<string>();
        List<string> productLinks = new List<string>();
        List<TestResult> tests = new List<TestResult>();

        try
        {
            IWebElement ulElement1 = driver.FindElement(By.ClassName("page-numbers"));
            IList<IWebElement> anchorTags1 = ulElement1.FindElements(By.CssSelector("a.page-numbers"));
            foreach (IWebElement anchorTag in anchorTags1)
            {
                string href = anchorTag.GetAttribute("href");
                pageLinks.Add(href);
            }
            pageLinks.Remove(pageLinks.Last());
            pageLinks.Add(driver.Url);
            foreach (var link in pageLinks)
            {
                IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".product-element-bottom"));
                foreach (var element in elements)
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
                driver.Url = link;
                TestResult product = new TestResult();
                IWebElement element = driver.FindElement(By.ClassName("breadcrumb-last"));
                product.Name = element.Text;

                var shortDescriptionDiv = driver.FindElement(By.ClassName("woocommerce-product-details__short-description"));

                var ulElement = shortDescriptionDiv.FindElement(By.TagName("ul"));

                var liElements = ulElement.FindElements(By.TagName("li"));

                List<string> Description = new List<string>();
                foreach (var liElement in liElements)
                {
                    if (!string.IsNullOrEmpty(liElement.Text))
                        Description.Add(liElement.Text);
                }
                product.Description = Description;
                try
                {
                    IWebElement parentDiv = driver.FindElement(By.ClassName("elementor-product-simple"));
                    IWebElement childP = parentDiv.FindElement(By.CssSelector("p.stock.in-stock"));
                    product.IsExsist = true;
                }
                catch
                {
                    product.IsExsist = false;
                }

                try
                {
                    IWebElement priceElement = driver.FindElement(By.CssSelector(".price"));

                    string priceText = priceElement.Text;

                    product.OldPrice = priceText.Split("تومان")[1].Trim();

                    product.NewPrice = priceText.Split("تومان")[2].Trim();

                    product.Price = priceText.Split("تومان")[2].Trim();
                }
                catch
                {
                    IWebElement priceElement = driver.FindElement(By.CssSelector(".price"));

                    string priceText = priceElement.Text;

                    product.Price = priceText.Split("تومان")[1].Trim();
                }

                try
                {

                    List<string> imagesUrl = new List<string>();
                    List<string> images = new List<string>();

                    IWebElement firstDiv = driver.FindElement(By.ClassName("wd-carousel-wrap"));
                    IList<IWebElement> anchorTags = firstDiv.FindElements(By.TagName("a"));

                    foreach (var anchor in anchorTags)
                    {
                        string href = anchor.GetAttribute("href");
                        imagesUrl.Add(href);
                    }

                    foreach (string image in imagesUrl)
                    {
                        WebClient webClient = new WebClient();
                        byte[] imageBytes = webClient.DownloadData(image);
                        string base64String = Convert.ToBase64String(imageBytes);
                        images.Add(base64String);
                    }
                    product.ImageUrls = imagesUrl;
                    //product.Images = images;
                }
                catch
                {
                    product.Images = null;
                }

                tests.Add(product);

            }
        }
        catch
        {
            IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".product-element-bottom"));
            foreach (var element in elements)
            {
                IWebElement h3Element = element.FindElement(By.ClassName("wd-entities-title"));
                IWebElement anchorTag = h3Element.FindElement(By.TagName("a"));

                string hrefValue = anchorTag.GetAttribute("href");
                productLinks.Add(hrefValue);
            }


            foreach (var link in productLinks)
            {
                driver.Url = link;
                TestResult product = new TestResult();
                IWebElement element = driver.FindElement(By.ClassName("breadcrumb-last"));
                product.Name = element.Text;

                var shortDescriptionDiv = driver.FindElement(By.ClassName("woocommerce-product-details__short-description"));

                var ulElement = shortDescriptionDiv.FindElement(By.TagName("ul"));

                var liElements = ulElement.FindElements(By.TagName("li"));

                List<string> Description = new List<string>();
                foreach (var liElement in liElements)
                {
                    if (!string.IsNullOrEmpty(liElement.Text))
                        Description.Add(liElement.Text);
                }
                product.Description = Description;
                try
                {
                    IWebElement parentDiv = driver.FindElement(By.ClassName("elementor-product-simple"));
                    IWebElement childP = parentDiv.FindElement(By.CssSelector("p.stock.in-stock"));
                    product.IsExsist = true;
                }
                catch
                {
                    product.IsExsist = false;
                }

                try
                {
                    IWebElement priceElement = driver.FindElement(By.CssSelector(".price"));

                    string priceText = priceElement.Text;

                    product.OldPrice = priceText.Split("تومان")[1].Trim();

                    product.NewPrice = priceText.Split("تومان")[2].Trim();

                    product.Price = priceText.Split("تومان")[2].Trim();
                }
                catch
                {
                    IWebElement priceElement = driver.FindElement(By.CssSelector(".price"));

                    string priceText = priceElement.Text;

                    product.Price = priceText.Split("تومان")[1].Trim();
                }

                try
                {

                    List<string> imagesUrl = new List<string>();
                    List<string> images = new List<string>();

                    IWebElement firstDiv = driver.FindElement(By.ClassName("wd-carousel-wrap"));
                    IList<IWebElement> anchorTags = firstDiv.FindElements(By.TagName("a"));

                    foreach (var anchor in anchorTags)
                    {
                        string href = anchor.GetAttribute("href");
                        imagesUrl.Add(href);
                    }

                    foreach (string image in imagesUrl)
                    {
                        WebClient webClient = new WebClient();
                        byte[] imageBytes = webClient.DownloadData(image);
                        string base64String = Convert.ToBase64String(imageBytes);
                        images.Add(base64String);
                    }
                    product.ImageUrls = imagesUrl;
                    //product.Images = images;
                }
                catch
                {
                    product.Images = null;
                }

                tests.Add(product);

            }
        }


        return Ok(tests);
    }
}
public class TestResult
{
    public string? Name { get; set; } //did
    public string? Price { get; set; }//did
    public string? OldPrice { get; set; }//did
    public string? NewPrice { get; set; }//did
    public bool? IsExsist { get; set; }//did
    public List<string>? Images { get; set; }//did
    public List<string>? ImageUrls { get; set; }//did
    public List<string>? Description { get; set; }//did
}