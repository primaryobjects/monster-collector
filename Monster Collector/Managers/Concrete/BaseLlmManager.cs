using LlmTornado.Images;

public abstract class BaseLlmManager(string? apiKey) : LLM
{
    protected string? _apiKey = apiKey;

    public bool IsValid() => _apiKey != null;

    public virtual Task<string?> GetTextAsync(string prompt, string input)
    {
        throw new NotImplementedException();
    }

    public virtual Task<ImageResult?> GetImage(string prompt)
    {
        throw new NotImplementedException();
    }
}