using LlmTornado;
using LlmTornado.Chat.Models;
using LlmTornado.Code;
using LlmTornado.Images;
using LlmTornado.Models;

public class CohereManager : BaseLlmManager
{
    public CohereManager()
        : base(Environment.GetEnvironmentVariable("CohereApiKey"))
    {
    }

    public override async Task<string?> GetTextAsync(string prompt, string input)
    {
        TornadoApi api = new([new ProviderAuthentication(LLmProviders.Cohere, _apiKey ?? "")]);
        ChatModel model = ChatModel.Cohere.CommandRPlus;

        string? response = await api.Chat.CreateConversation(model)
            .AppendSystemMessage(prompt)
            .AppendUserInput(input)
            .GetResponse();

        return response;
    }

    public async override Task<ImageResult?> GetImage(string prompt)
    {
        TornadoApi api = new([new ProviderAuthentication(LLmProviders.Cohere, _apiKey ?? "")]);
        ImageResult? response = await api.ImageGenerations.CreateImageAsync(
            new ImageGenerationRequest(
                prompt,
                quality: ImageQuality.Standard,
                responseFormat: ImageResponseFormat.Url,
                model: Model.Dalle3
            )
        );

        return response;
    }
}