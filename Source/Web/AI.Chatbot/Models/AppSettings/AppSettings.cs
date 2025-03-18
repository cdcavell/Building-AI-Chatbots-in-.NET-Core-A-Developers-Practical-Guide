using System.Text.Json.Serialization;

namespace AI.Chatbot.Models.AppSettings
{
    public class AppSettings(IConfiguration configuration) : ClassLibrary.Mvc.Services.AppSettings.Models.AppSettings(configuration)
    {
    }
}
