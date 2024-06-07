using LlmTornado;
using LlmTornado.Chat.Models;
using LlmTornado.Code;
using LlmTornado.Images;
using LlmTornado.Models;

public abstract class BaseLlmManager(string? apiKey, LLmProviders provider) : LLM
{
    protected string? _apiKey = apiKey;
    protected LLmProviders _provider = provider;

    public bool IsValid() => _apiKey != null;

    public virtual async Task<string?> GetTextAsync(string prompt, string input)
    {
        TornadoApi api = new([new ProviderAuthentication(provider, _apiKey ?? "")]);
        ChatModel model = ChatModel.Cohere.CommandRPlus;

        string? response = await api.Chat.CreateConversation(model)
            .AppendSystemMessage(prompt)
            .AppendUserInput(input)
            .GetResponse();

        return response;
    }

    public virtual async Task<ImageResult?> GetImage(string prompt)
    {
        TornadoApi api = new([new ProviderAuthentication(provider, _apiKey ?? "")]);
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