using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouponController : ControllerBase
{
    private readonly AppDbContext _dbcontext;
    private readonly ResponseDto _response;
   
    public CouponController(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
        _response = new ResponseDto();
    }

    [HttpGet]
    public ResponseDto Get()
    {
        try
        {
            IEnumerable<Coupon> objList = _dbcontext.Coupons.ToList();            
            _response.Result = objList;
            
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpGet]
    [Route("{id:int}")]
    public ResponseDto Get(int id)
    {
        try
        {
            Coupon objList = _dbcontext.Coupons.First(c => c.CouponId == id);
            CouponDto couponDto = new CouponDto
            {
                CouponId = objList.CouponId,
                CouponCode = objList.CouponCode,
                DiscountAmount = objList.DiscountAmount,
                MinAmount = objList.MinAmount
            };

            _response.Result = couponDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpGet]
    [Route("GetByCode/{code}")]
    public ResponseDto GetByCode(string code)
    {
        try
        {
            Coupon objList = _dbcontext.Coupons.First(c => c.CouponCode.ToLower() == code.ToLower());
            CouponDto couponDto = new CouponDto
            {
                CouponId = objList.CouponId,
                CouponCode = objList.CouponCode,
                DiscountAmount = objList.DiscountAmount,
                MinAmount = objList.MinAmount
            };

            _response.Result = couponDto;

        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpPost]
    public ResponseDto Post([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon obj = _dbcontext.Coupons.Add(new Coupon
            {
                CouponCode = couponDto.CouponCode,
                DiscountAmount = couponDto.DiscountAmount,
                MinAmount = couponDto.MinAmount
            }).Entity;

            _dbcontext.Coupons.Add(obj);
            _dbcontext.SaveChanges();

            _response.Result = obj;

        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpPut]
    public ResponseDto Put([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon obj = _dbcontext.Coupons.Add(new Coupon
            {
                CouponCode = couponDto.CouponCode,
                DiscountAmount = couponDto.DiscountAmount,
                MinAmount = couponDto.MinAmount
            }).Entity;

            _dbcontext.Coupons.Update(obj);
            _dbcontext.SaveChanges();

            _response.Result = obj;

        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }

    [HttpDelete]
    public ResponseDto Delete(int id)
    {
        try
        {
            Coupon obj = _dbcontext.Coupons.First(c => c.CouponId == id);
            _dbcontext.Remove(obj);
            _dbcontext.SaveChanges();
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }

        return _response;
    }
}
