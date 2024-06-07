using LlmTornado.Code;

public class CohereManager : BaseLlmManager
{
    public CohereManager()
        : base(Environment.GetEnvironmentVariable("CohereApiKey"), LLmProviders.Cohere)
    {
    }
}