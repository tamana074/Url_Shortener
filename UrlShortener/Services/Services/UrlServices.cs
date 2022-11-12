using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Entities;
using DataAccess.References;
using Services.Interfaces;
using Services.Models;

namespace Services.Services
{
    public class UrlServices : IUrlServices
    {

        private readonly ShortenerService _shortenerService;
       // private readonly MongoDBService _mongoDb;

        public UrlServices(ShortenerService shortenerService /*, MongoDBService mongoDb */)
        {
            _shortenerService = shortenerService;
           //_mongoDb = mongoDb;
        }

        public async Task<ServiceResponseModel<Urls>> GenerateUrl(string longUrl)
        {
            var response = new ServiceResponseModel<Urls>();
            try
            {

                var validateUrl = _shortenerService.ValidationUrl(longUrl);
                if (!validateUrl)
                {
                    response.Message = Resource.InvalidUrl;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }
                var url = _shortenerService.GenerateShortUrl(longUrl);


                //await _mongoDb.CreateAsync(url);
                response.Message = Resource.SuccessMessage;
                response.StatusCode = HttpStatusCode.OK;
                response.Data = url;
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.Message = Resource.PublicError;
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
