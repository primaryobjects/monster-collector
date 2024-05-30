using LlmTornado;
using LlmTornado.Chat.Models;
using LlmTornado.Code;
using LlmTornado.Models;

public class CohereManager : LLM
{
    public bool IsValid()
    {
        return Environment.GetEnvironmentVariable("CohereApiKey") != null;
    }

    public async Task<string?> GetTextAsync(string prompt, string input) 
    {
        string apiKey = Environment.GetEnvironmentVariable("CohereApiKey") ?? "";
        TornadoApi api = new TornadoApi(new List<ProviderAuthentication> { new ProviderAuthentication(LLmProviders.Cohere, apiKey) });
        ChatModel model = ChatModel.Cohere.CommandRPlus;

        string? response = await api.Chat.CreateConversation(model)
            .AppendSystemMessage(prompt)
            .AppendUserInput(input)
            .GetResponse();

        return response;
    }
}