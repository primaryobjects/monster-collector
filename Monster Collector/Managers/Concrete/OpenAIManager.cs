using LlmTornado.Code;

public class OpenAIManager : BaseLlmManager
{
    public OpenAIManager()
        : base(Environment.GetEnvironmentVariable("OpenAIApiKey"), LLmProviders.OpenAi)
    {
    }
}