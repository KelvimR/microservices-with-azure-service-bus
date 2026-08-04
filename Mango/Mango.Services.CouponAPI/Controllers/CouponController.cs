using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouponController : ControllerBase
{
    private readonly AppDbContext _dbcontext;

    public CouponController(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    [HttpGet]
    public object Get()
    {
        try
        {
            IEnumerable<Coupon> objList = _dbcontext.Coupons.ToList();
            return objList;
        }
        catch (Exception ex)
        {

            throw;
        }

        return null;
    }

    [HttpGet]
    [Route("{id:int}")]
    public object Get(int id)
    {
        try
        {
            Coupon objList = _dbcontext.Coupons.First(c => c.CouponId == id);
            return objList;
        }
        catch (Exception ex)
        {

            throw;
        }

        return null;
    }
}
