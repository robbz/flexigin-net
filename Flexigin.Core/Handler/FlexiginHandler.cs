using System.Configuration;
using System.Web;

namespace Flexigin.Core.Handler
{
    public class FlexiginHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var path = context.Request.QueryString["p"];

            if (string.IsNullOrEmpty(path))
            {
                context.Response.Write("Please provide a valid path.");
                return;
            }

            var loader = new ComponentLoader();
            var basePath = ConfigurationManager.AppSettings["flexigin:basePath"] ?? "~/";
                basePath = context.Server.MapPath(basePath);

            var component = loader.GetComponent(basePath, path);

            context.Response.StatusCode = (int) component.StatusCode;
            context.Response.ContentType = component.ContentType;

#if DEBUG
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
#else
            var cacheMinutes = Convert.ToInt16(ConfigurationManager.AppSettings["flexigin:cacheMinutes"]);
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetExpires(DateTime.Now.AddMinutes(cacheMinutes));
            context.Response.Cache.SetMaxAge(new TimeSpan(0, cacheMinutes, 0));
#endif

            context.Response.Write(component.Content);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
