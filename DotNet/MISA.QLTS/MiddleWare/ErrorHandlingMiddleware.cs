using Microsoft.AspNetCore.Http;
using MISA.ApplicationCore.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace MISA.QLTS.MiddleWare
{
    /// <summary>
    /// Middleware xử lý exception .Nó sẽ can thiệp vào mỗi khi chương trình gặp exception
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        //Chính là cái tham chiếu để thi hành cái MiddleWare phía sau ErrorHandlingMiddleware
        private readonly RequestDelegate next;

        //Phương thức khởi tạo
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Được gọi khi HttpContext đi qua MiddleWare trong pipeline.
        /// Nếu gặp lỗi thì sẽ xử lý exception.Ngược lại thì sẽ next đến middleware kế tiếp
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        /// <summary>
        /// Trả về kết quả thông báo mỗi khi chương trình gặp lỗi exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(
                new ServiceResult
                {
                    Data = new
                    {
                        devMsg = ex.Message,
                        cusMsg = "Có lỗi xảy ra vui lòng liên hệ Misa"
                    },
                    Messeger = "Có lỗi xảy ra vui lòng liên hệ Misa",
                    MisaCode = MISAEnum.Exception
                });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }   
    }
}
