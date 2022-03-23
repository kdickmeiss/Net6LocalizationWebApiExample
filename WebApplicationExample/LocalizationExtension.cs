namespace WebApplicationExample;

public static class LocalizationExtension
{
    public static void UseLocalization(this IApplicationBuilder app)
    {
        var supportedCultures = new[] { "en-US", "de-DE" };
        app.UseRequestLocalization(options =>
        {
            options.SetDefaultCulture(supportedCultures[0]);
            options.AddSupportedCultures(supportedCultures);
            options.AddSupportedUICultures(supportedCultures);
            options.ApplyCurrentCultureToResponseHeaders = true;
        });
    }
}
