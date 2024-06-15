using LlmTornado.Chat.Models;
using LlmTornado.Code;

public class OpenAIManager : BaseLlmManager
{
    public OpenAIManager()
        : base(Environment.GetEnvironmentVariable("OpenAIApiKey"), LLmProviders.OpenAi)
    {
    }

    public override Task<string?> GetTextAsync(string prompt, string input, ChatModel? model)
    {
        return base.GetTextAsync(prompt, input, ChatModel.OpenAi.Gpt35.Turbo);
    }
}