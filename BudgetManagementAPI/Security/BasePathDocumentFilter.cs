using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class BasePathDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = "http://Budget-Recip-ap0L6CPLaIjq-1514432606.us-east-1.elb.amazonaws.com" } };
    }
}