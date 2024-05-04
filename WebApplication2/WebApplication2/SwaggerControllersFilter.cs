using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApplication2
{
    public class SwaggerControllersFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var apiDescription in context.ApiDescriptions)
            {

                var controller = apiDescription.ActionDescriptor as ControllerActionDescriptor;

                if (!controller.ControllerTypeInfo.Assembly.FullName.Equals(typeof(Startup).Assembly.FullName))
                {
                    var nonAssemblyRoutes = swaggerDoc.Paths
                                            .Where(x => x.Key.ToLower().Contains(controller.AttributeRouteInfo.Template.ToLower()))
                                            .ToList();

                    nonAssemblyRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
                }
            }
        }
    }
}
