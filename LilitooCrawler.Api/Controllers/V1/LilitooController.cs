using Domain.Common;
using Domain.Entities;
using Domain.Interfaces.Services;
using ExternalServices.Db;
using ExternalServices.Services;
using LilitooCrawler.Api.File;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net;

namespace LilitooCrawler.Api.Controllers.V1;
public class LilitooController : BaseController
{
    private LilitooCrawlerBusiness business = new LilitooCrawlerBusiness();
    private readonly ILilitooReadServices _readServices;
    public LilitooController(ILilitooReadServices readServices)
    {
        _readServices = readServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetCosmetic()
    {
        var response = new ActionResponse<List<Product>>();
        try
        {
            string url = "https://lilitoo.com/product-category/makeup/";
            var result = await _readServices.GetProducts(url);
            if (result == null || result[0].Name == "" || result[0].Name == null)
                throw new Exception("result is null");
            foreach(var item in result)
            {
                business.InsertProduct(item);
            }
            response.Data = new List<Product>();
            response.Data = result;
            response.Message = "success";
            response.Status = 200;
            response.State = ResponseStateEnum.SUCCESS;
            return Ok(response);

        }
        catch (Exception ex)
        {
            response.Message = "error";
            response.Status = 503;
            response.State = ResponseStateEnum.FAILED;
            response.Errors.Add(ex?.InnerException?.Message);
            return BadRequest(response);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetMouthAndTooth()
    {
        var response = new ActionResponse<List<Product>>();
        try
        {
            string url = "https://lilitoo.com/product-category/%d8%af%d9%87%d8%a7%d9%86-%d9%88-%d8%af%d9%86%d8%af%d8%a7%d9%86/";
            var result = await _readServices.GetProducts(url);
            if (result == null || result[0].Name == "" || result[0].Name == null)
                throw new Exception("result is null");
            foreach (var item in result)
            {
                business.InsertProduct(item);
            }
            response.Data = new List<Product>();
            response.Data = result;
            response.Message = "success";
            response.Status = 200;
            response.State = ResponseStateEnum.SUCCESS;
            return Ok(response);

        }
        catch (Exception ex)
        {
            response.Message = "error";
            response.Status = 503;
            response.State = ResponseStateEnum.FAILED;
            response.Errors.Add(ex?.InnerException?.Message);
            return BadRequest(response);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetSkinCare()
    {
        var response = new ActionResponse<List<Product>>();
        try
        {
            string url = "https://lilitoo.com/product-category/skin-care/";
            var result = await _readServices.GetProducts(url);
            if (result == null || result[0].Name == "" || result[0].Name == null)
                throw new Exception("result is null");
            foreach (var item in result)
            {
                business.InsertProduct(item);
            }
            response.Data = new List<Product>();
            response.Data = result;
            response.Message = "success";
            response.Status = 200;
            response.State = ResponseStateEnum.SUCCESS;
            return Ok(response);

        }
        catch (Exception ex)
        {
            response.Message = "error";
            response.Status = 503;
            response.State = ResponseStateEnum.FAILED;
            response.Errors.Add(ex?.InnerException?.Message);
            return BadRequest(response);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetHairCare()
    {
        var response = new ActionResponse<List<Product>>();
        try
        {
            string url = "https://lilitoo.com/product-category/hair-care/";
            var result = await _readServices.GetProducts(url);
            if (result == null || result[0].Name == "" || result[0].Name == null)
                throw new Exception("result is null");
            foreach (var item in result)
            {
                business.InsertProduct(item);
            }
            response.Data = new List<Product>();
            response.Data = result;
            response.Message = "success";
            response.Status = 200;
            response.State = ResponseStateEnum.SUCCESS;
            return Ok(response);

        }
        catch (Exception ex)
        {
            response.Message = "error";
            response.Status = 503;
            response.State = ResponseStateEnum.FAILED;
            response.Errors.Add(ex?.InnerException?.Message);
            return BadRequest(response);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUnCategorized()
    {
        var response = new ActionResponse<List<Product>>();
        try
        {
            string url = "https://lilitoo.com/product-category/%d8%a8%d8%af%d9%88%d9%86-%d8%af%d8%b3%d8%aa%d9%87%d8%a8%d9%86%d8%af%db%8c/";
            var result = await _readServices.GetProducts(url);
            if (result == null || result[0].Name == "" || result[0].Name == null)
                throw new Exception("result is null");
            foreach (var item in result)
            {
                business.InsertProduct(item);
            }
            response.Data = new List<Product>();
            response.Data = result;
            response.Message = "success";
            response.Status = 200;
            response.State = ResponseStateEnum.SUCCESS;
            return Ok(response);

        }
        catch (Exception ex)
        {
            response.Message = "error";
            response.Status = 503;
            response.State = ResponseStateEnum.FAILED;
            response.Errors.Add(ex?.InnerException?.Message);
            return BadRequest(response);
        }
    }
}
