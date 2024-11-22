using Microsoft.AspNetCore.Mvc;

namespace Product.Management.Api.Middleware;

public class ErrorDetails : ProblemDetails
{
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}