using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataFormat
{
    public class DateFormatSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(DateTime) || context.Type == typeof(DateTime?))
            {
                schema.Format = "date";
                schema.Example = new OpenApiString("MM/dd/yyyy");
            }
        }
    }
}
