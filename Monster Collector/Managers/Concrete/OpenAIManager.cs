
using LlmTornado;
using LlmTornado.Chat.Models;
using LlmTornado.Code;
using LlmTornado.Images;
using LlmTornado.Models;

public class OpenAIManager : BaseLlmManager
{
    public OpenAIManager()
        : base(Environment.GetEnvironmentVariable("OpenAIApiKey"))
    {
    }

    public async override Task<string?> GetTextAsync(string prompt, string input)
    {
        TornadoApi api = new([new ProviderAuthentication(LLmProviders.OpenAi, _apiKey ?? "")]);
        ChatModel model = ChatModel.OpenAi.Gpt35.Turbo;

        string? response = await api.Chat.CreateConversation(model)
            .AppendSystemMessage(prompt)
            .AppendUserInput(input)
            .GetResponse();

        return response;
    }

    public async override Task<ImageResult?> GetImage(string prompt)
    {
        TornadoApi api = new([new ProviderAuthentication(LLmProviders.OpenAi, _apiKey ?? "")]);
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