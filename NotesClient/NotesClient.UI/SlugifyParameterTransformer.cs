using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace NotesClient.UI
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            if (value is null)
            {
                return null;
            }

            // Slugify value
            return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}
