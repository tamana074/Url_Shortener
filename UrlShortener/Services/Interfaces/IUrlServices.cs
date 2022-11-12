using DataAccess.Entities;
using Services.Models;

namespace Services.Interfaces;

public interface IUrlServices
{
    Task<ServiceResponseModel<Urls>> GenerateUrl(string longUrl);
    Task<ServiceResponseModel<Urls>> GetUrlByUrlCode(string code);
}