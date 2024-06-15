using LlmTornado.Chat.Models;
using LlmTornado.Images;

public interface LLM
{
    bool IsValid();
    Task<string?> GetTextAsync(string prompt, string input, ChatModel? model = null);
    Task<ImageResult?> GetImage(string prompt);
}