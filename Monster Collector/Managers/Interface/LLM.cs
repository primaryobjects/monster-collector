using LlmTornado.Images;

public interface LLM
{
    bool IsValid();
    Task<string?> GetTextAsync(string prompt, string input);
    Task<ImageResult?> GetImage(string prompt);
}