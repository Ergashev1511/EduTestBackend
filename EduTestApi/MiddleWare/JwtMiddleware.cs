using EduTest.Application.JwtTokenSerives;

namespace EduTestApi.MiddleWare
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IJwtTokenService jwtTokenService)
        {
            // Authorization headerdan tokenni olish
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    // Tokenni validatsiya qilish
                    var user = jwtTokenService.ValidateToken(token);
                    if (user != null)
                    {
                        // Token valid bo'lsa, foydalanuvchini HTTP contextga qo'shish
                        context.Items["User"] = user;
                    }
                }
                catch (Exception)
                {
                    // Agar tokenni validatsiya qilishda xato bo'lsa, xato logi qilinishi mumkin
                }
            }

            // So'rovni keyingi middleware yoki controllerga yuborish
            await _next(context);
        }
    }
}
