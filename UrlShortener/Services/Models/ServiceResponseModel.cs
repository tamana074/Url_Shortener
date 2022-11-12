using System.Net;

namespace Services.Models;

public class ServiceResponseModel
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }

}

public class ServiceResponseModel<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}