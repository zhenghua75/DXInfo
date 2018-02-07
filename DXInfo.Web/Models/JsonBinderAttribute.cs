using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Script.Serialization;

namespace DXInfo.Web.Models
{
    public class JsonModelBinder : IModelBinder
    {
        public object BindModel(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            try
            {
                var json = controllerContext.HttpContext.Request
                           .Params[bindingContext.ModelName];

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                // Swap this out with whichever Json deserializer you prefer.
                return Newtonsoft.Json.JsonConvert
                       .DeserializeObject(json, bindingContext.ModelType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class JsonBinderAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }
    }
}