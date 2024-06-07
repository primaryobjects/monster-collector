public interface LLM
{
    bool IsValid();
    Task<string?> GetTextAsync(string prompt, string input);
}