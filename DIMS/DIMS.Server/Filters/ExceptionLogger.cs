using DIMS.Logger;
using System.Web.Mvc;

namespace DIMS.Server.Filters
{
    public class ExceptionLogger : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled)
            {
                CustomLogger.Error(exceptionContext.Exception.Message, exceptionContext.Exception, exceptionContext.Exception.Message);

                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}