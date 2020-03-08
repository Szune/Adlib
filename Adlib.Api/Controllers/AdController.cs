using System.Collections.Generic;
using Adlib.Api.Helpers;
using Adlib.Api.Models;
using Adlib.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adlib.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdController : Controller
    {
        private readonly IAdService _adService;
        
        // TODO: add authorization
        public AdController(IAdService adService)
        {
            _adService = adService;
        }

        // TODO: add model validation
        [HttpPost("add")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddAd([FromBody] AddAdModel model)
        {
            var (id, success) = _adService.Add(model);
            if (success)
                return Ok(id);
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAd(int id)
        {
            if ( _adService.Delete(id))
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        [HttpGet("get")]
        [ProducesResponseType(typeof(IEnumerable<GetAdModel>), StatusCodes.Status200OK)]
        public IActionResult GetAdsDefaultOrder()
        {
            var order = OrderAdBy.TimeDesc;
            var ads = _adService.Get(order);
            return Ok(ads);
        }
        
        [HttpGet("get/{sort}")]
        [ProducesResponseType(typeof(IEnumerable<GetAdModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAds(string sort)
        {
            if (EnumX.TryParse<OrderAdBy>(sort ?? "", out var order))
            {
                var ads = _adService.Get(order);
                return Ok(ads);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}