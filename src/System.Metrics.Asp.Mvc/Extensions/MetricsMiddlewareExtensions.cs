using Microsoft.AspNetCore.Http;

namespace System.Metrics.Asp.Mvc.Extensions
{
    public static class MetricsMiddlewareExtensions
    {
        
        /// <Summary>
        /// conditionally chains multiple middlewares
        /// </summar>
        public static MetricsMiddleware UseIf(this MetricsMiddleware subject, Func<HttpContext, bool> condition, Func<MetricsMiddleware, MetricsMiddleware> setup)
        {
            return async (ctx, ep, next) => {
                    MetricsMiddleware finalizer = async (context, _, n) => { await n(); };

                    var d = setup(finalizer);

                    if(condition(ctx))
                    {
                        await d(ctx, ep, () => subject(ctx, ep, next));
                        return;
                    }

                    await subject(ctx, ep, next);
                };
        }

        /// <Summary>
        /// Chains metric middlewares
        /// </summar>
        public static MetricsMiddleware Use(this MetricsMiddleware subject, MetricsMiddleware obj)
        {
            return async (ctx, ep, nxt) =>
            {
                await obj(ctx, ep, () => subject(ctx, ep, nxt));
            };
        }
    }
}