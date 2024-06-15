using LlmTornado;
using LlmTornado.Chat.Models;
using LlmTornado.Code;
using LlmTornado.Images;
using LlmTornado.Models;

public abstract class BaseLlmManager : LLM
{
    protected string? _apiKey;
    protected LLmProviders _provider;
    protected TornadoApi _api;

    public BaseLlmManager(string? apiKey, LLmProviders provider)
    {
        _apiKey = apiKey;
        _provider = provider;
        _api  = new([new ProviderAuthentication(provider, _apiKey ?? "")]);
    }

    protected ChatModel GetTextModel()
    {
        ChatModel model = _provider switch
        {
            LLmProviders.Cohere => ChatModel.Cohere.CommandRPlus,
            LLmProviders.Anthropic => ChatModel.Anthropic.Claude3.Opus,
            LLmProviders.OpenAi or LLmProviders.AzureOpenAi => ChatModel.OpenAi.Gpt35.Turbo,
            _ => ChatModel.Cohere.CommandRPlus,
        };

        return model;
    }

    public bool IsValid() => _apiKey != null;

    public virtual async Task<string?> GetTextAsync(string prompt, string input, ChatModel? model)
    {
        string? response = await _api.Chat.CreateConversation(model ?? GetTextModel())
            .AppendSystemMessage(prompt)
            .AppendUserInput(input)
            .GetResponse();

        return response;
    }

    public virtual async Task<ImageResult?> GetImage(string prompt)
    {
        ImageResult? response = await _api.ImageGenerations.CreateImageAsync(
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