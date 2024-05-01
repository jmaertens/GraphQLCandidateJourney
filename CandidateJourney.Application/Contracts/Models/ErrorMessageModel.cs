using System.Net;

namespace CandidateJourney.Application.Contracts.Models;

public class ErrorMessageModel
{
    public ErrorMessageModel(HttpStatusCode statusCode, IEnumerable<string> messages)
    {
        StatusCode = (int)statusCode;
        Messages = messages;
    }

    public ErrorMessageModel(HttpStatusCode statusCode, string message) : this(statusCode, new[] { message }) { }

    public int StatusCode { get; set; }

    public IEnumerable<string> Messages { get; set; }

    public string? Exception { get; set; }
}