using Newtonsoft.Json;

namespace EndProject.MVC.Extencions;

public class ValidationErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; }
    public string TraceId { get; set; }
}

public static class SimplifyErrorResponse
{
    public static string SimplifyError(string jsonResponse)
    {
        var validationErrorResponse = JsonConvert.DeserializeObject<ValidationErrorResponse>(jsonResponse);

        if (validationErrorResponse != null && validationErrorResponse.Errors != null)
        {
            var allErrorMessages = validationErrorResponse.Errors
                .SelectMany(error => error.Value)
                .ToList();

            return string.Join("; ", allErrorMessages);
        }

        return "An unexpected error occurred. Please try again later.";
    }
}
