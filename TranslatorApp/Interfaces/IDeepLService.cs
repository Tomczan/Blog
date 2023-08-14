namespace TranslatorApp.Interfaces
{
    public interface IDeepLService
    {
        Task<TextReply> TranslatePost(string title, string content, string delimiter = "*");
    }
}