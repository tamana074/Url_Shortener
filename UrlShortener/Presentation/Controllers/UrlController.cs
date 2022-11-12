using System.Net;
using DataAccess.References;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces; 

namespace Presentation.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlServices _urlServices;

        public UrlController(IUrlServices urlServices)
        {
            _urlServices = urlServices;
        }


        [HttpGet("{urlCode}")]
        public IActionResult Index(string urlCode)
        {
            try
            {
                if (string.IsNullOrEmpty(urlCode))
                {
                    return BadRequest(Resource.BadRequestError);
                }

                var response = _urlServices.GetUrlByUrlCode(urlCode);
                switch (response.Result.StatusCode)
                {

                    case HttpStatusCode.OK:
                        return Ok(response.Result.Message);

                    case HttpStatusCode.BadRequest:
                        return BadRequest(response.Result.Message);
                   
                    case HttpStatusCode.NotFound:
                        return BadRequest(response.Result.Message);

                    default:
                        return Problem(response.Result.Message);
                }
            }
            catch (Exception e)
            {
                return Problem(Resource.PublicError);
            }
        }


        [HttpPost("generate-short-url")]
        public IActionResult GenerateShortUrl([FromBody] string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return BadRequest(Resource.BadRequestError);
                }
                var response = _urlServices.GenerateUrl(url);
                
                    switch (response.Result.StatusCode)
                    {

                        case HttpStatusCode.OK:
                            return Ok(response.Result.Message);

                        case HttpStatusCode.BadRequest:
                            return BadRequest(response.Result.Message);

                        default:
                            return Problem(response.Result.Message);
                    } 
            }
            catch (Exception e)
            {
                return Problem(Resource.PublicError);

            }
        }
    }
}
