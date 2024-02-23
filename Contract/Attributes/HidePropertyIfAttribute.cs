using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Any;

namespace Contract.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HidePropertyIfAttribute : Attribute
    {
        public string PropertyName { get; }
        public string PropertyValue { get; }

        public HidePropertyIfAttribute(string propertyName, string propertyValue)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
    }


    public class ConditionalPropertySchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var modelType = context.Type;
            var propertiesWithAttributes = modelType.GetProperties()
                .Select(property => (Property: property, Attribute: property.GetCustomAttributes(typeof(HidePropertyIfAttribute), false).FirstOrDefault() as HidePropertyIfAttribute))
                .Where(x => x.Attribute != null);

            foreach (var (property, attribute) in propertiesWithAttributes)
            {
                var conditionPropertyName = attribute!.PropertyName;
                var conditionPropertyValue = attribute!.PropertyValue;
                var conditionProperty = schema.Properties?.FirstOrDefault(p => p.Key.Equals(conditionPropertyName, StringComparison.OrdinalIgnoreCase)).Value;
                if (conditionProperty != null && conditionProperty.Enum?.Any() == true)
                {
                    var isConditionMet = conditionProperty.Enum.Contains(new OpenApiString(conditionPropertyValue));
                    if (isConditionMet)
                    {
                        schema.Properties?.Remove(property.Name);
                    }
                }
            }
        }
    }
}
