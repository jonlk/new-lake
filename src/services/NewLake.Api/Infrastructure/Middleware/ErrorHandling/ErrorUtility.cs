internal static class ErrorUtility
{
    internal static async Task SetErrorResponse(Exception ex, HttpContext context)
    {
        var exceptionType = ex.GetType();

        //Http Exception
        if (exceptionType == typeof(HttpRequestException))
        {
            context.Response.StatusCode = StatusCodes.Status418ImATeapot;
            await context.Response.WriteAsync(ExceptionConstants.HTTP_ERROR);
        }

        //File Not Found Exception
        if (exceptionType == typeof(FileNotFoundException))
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(ExceptionConstants.FILE_NOT_FOUND_ERROR);
        }

        //Other exceptions below...
    }
}