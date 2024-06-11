using Domain.Entities;
using Domain.Interfaces.ExternalServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net;

namespace ExternalServices.Services;
public class LilitooServices: ILilitooServices
{
    public async Task<List<Product>> GetProducts(string url)
    {
        IWebDriver driver = new ChromeDriver
        {
            Url = url
        };
        List<string> productLinks = new List<string>();
        List<Product> Products = new List<Product>();
        try
        {
            List<string> pageLinks = new List<string>();
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
                Product product = new Product();
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
                    product.Images = images;
                }
                catch
                {
                    product.Images = null;
                }

                Products.Add(product);

            }
            return Products;
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
                Product product = new Product();
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
                    product.Images = images;
                }
                catch
                {
                    product.Images = null;
                }

                Products.Add(product);

            }
            return Products;
        }
    }
}
