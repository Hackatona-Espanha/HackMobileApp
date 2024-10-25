using Hack4Edu.Views;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace Hack4Edu
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("latobold.ttf", "latobold");
                    fonts.AddFont("latoregular.ttf", "latoregular");
                    fonts.AddFont("PassionOne-Regular.ttf", "passion");
                });

#if DEBUG
            builder.Logging.AddDebug();
            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddTransient<AssigmentView>();
#endif

            return builder.Build();
        }
    }
}
