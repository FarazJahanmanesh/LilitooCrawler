using Domain.Entities;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace LilitooCrawler.Api.File;

public static class Excel
{
    public static void InsertProductsToExcel(List<Product> products)
    {
        var package = new ExcelPackage();


        var worksheet = package.Workbook.Worksheets.Add("Products");

        int row = 1;
        foreach (var product in products)
        {
            worksheet.Cells[row, 1].Value = product.Name;
            worksheet.Cells[row, 2].Value = product.Price;
            worksheet.Cells[row, 3].Value = product.OldPrice;
            worksheet.Cells[row, 4].Value = product.NewPrice;
            worksheet.Cells[row, 5].Value = product.IsExist;
            worksheet.Cells[row, 6].Value = JsonConvert.SerializeObject(product.Description);
            worksheet.Cells[row, 7].Value = JsonConvert.SerializeObject(product.ImageUrls);
            row++;
        }
        FileInfo excelFile = new FileInfo("C:\\Users\\f.jahanmanesh\\source\\Repo\\LilitooCrawler\\LilitooCrawler.Api\\File\\Sample.xlsx");
        package.SaveAs(excelFile);

    }
}
