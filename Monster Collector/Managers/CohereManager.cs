using LlmTornado;
using LlmTornado.Chat.Models;
using LlmTornado.Code;
using LlmTornado.Images;
using LlmTornado.Models;

public static class CohereManager
{
    public static bool IsValid()
    {
        return Environment.GetEnvironmentVariable("CohereApiKey") != null;
    }

    public static async Task<string?> GetText(string prompt, string input)
    {
        string apiKey = Environment.GetEnvironmentVariable("CohereApiKey")!;
        TornadoApi api = new([new ProviderAuthentication(LLmProviders.Cohere, apiKey)]);
        ChatModel model = ChatModel.Cohere.CommandRPlus;

        string? response = await api.Chat.CreateConversation(model)
            .AppendSystemMessage(prompt)
            .AppendUserInput(input)
            .GetResponse();

        return response;
    }

    public static async Task<ImageResult?> GetImage(string prompt)
    {
        string apiKey = Environment.GetEnvironmentVariable("OpenAIApiKey")!;
        TornadoApi api = new([new ProviderAuthentication(LLmProviders.OpenAi, apiKey)]);
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