using System.ComponentModel;

namespace EFP.RequestList.Libraries.Enums
{
    public enum RequestedContentType
    {
        [Description("Не задано")]
        Unknown,
        [Description("Игра")]
        Game,
        [Description("Видео")]
        Video,
        [Description("Музыка")]
        Music
    }
}
