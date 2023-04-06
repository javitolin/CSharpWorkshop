using HttpExercise;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const int CORRECT_ANSWER = 3;
const int REQUIRED_RETRIES = 3;
TimeSpan WAIT_BETWEEN_RETRIES = TimeSpan.FromSeconds(2);
ConcurrentDictionary<Guid, UserRetries> users = new ConcurrentDictionary<Guid, UserRetries>();

Random random = new Random();


app.MapGet("/", () =>
{
    return "Hello and welcome to this simple CTF! You'll need to answer the questions and point to the correct endpoint to get the next question. In order to start, just GET the fourth word in this sentence. Good luck!";
});

app.MapGet("/start", () =>
{
    return "Very good! Riddle me this... What is the name of the function that is used to create a Path from two or more Paths? PUT that and send me some an example for directory C:\\ and \"Program Files\" in the body as \"example\"";
});

app.MapPut("/Combine", async (HttpContext context) =>
{
    try
    {
        Example? example = await System.Text.Json.JsonSerializer.DeserializeAsync<Example>(context.Request.Body);
        if (example == null || example.example == null)
        {
            context.Response.StatusCode = StatusCodes.Status402PaymentRequired;
            await context.Response.WriteAsync("Where is my example??");
            return;
        }

        if (example.example != Path.Combine("C:", "Program Files"))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Wrong example!");
            return;
        }

        await context.Response.WriteAsync("Nice! Try to answer this one... How can I get the name of the folder for a current filename? You should be POST that by now... In the body, send me an \"input\" and \"output\" for that usage");
    }
    catch
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("You've probaly gave me something fishy");
    }
});

app.MapPost("/GetDirectoryName", async (HttpContext context) =>
{
    try
    {
        InputOutput? inputOutput = await System.Text.Json.JsonSerializer.DeserializeAsync<InputOutput>(context.Request.Body);
        if (inputOutput == null || string.IsNullOrEmpty(inputOutput.input) || string.IsNullOrEmpty(inputOutput.output))
        {
            context.Response.StatusCode = StatusCodes.Status402PaymentRequired;
            await context.Response.WriteAsync("Where is my input-output pair??");
            return;
        }

        if (Path.GetDirectoryName(inputOutput.input) != inputOutput.output)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Wrong input-output pair!");
            return;
        }
        await context.Response.WriteAsync("Very good! Best practice says that... How should you NOT read a whole file? DELETE that from your lexicon!");
    }
    catch
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("You've probaly gave me something fishy");
    }
});

app.MapDelete("/ReadAllText", () =>
{
    return "Wow! Now answer this next question... How to create a GET request using a HTTP Client? Show me you know how!";
});

app.MapGet("/GetAsync", () =>
{
    return "Excellent! Now riddle me this... In the final exercise of the previous lesson we found X files. You should POST that *Answer*...";
});

app.MapPost("/Answer", async (HttpContext context) =>
{
    context.Response.StatusCode = StatusCodes.Status404NotFound;
    await context.Response.WriteAsync("You forgot something");
});

app.MapPost("/Answer/{number}", async (HttpContext context) =>
{
    try
    {
        if (!context.Request.RouteValues.TryGetValue("number", out object? number))
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync("You forgot something");
            return;
        }

        if (number is null || !int.TryParse(number.ToString(), out int numberInteger))
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await context.Response.WriteAsync("That's no number");
            return;
        }

        if (numberInteger != CORRECT_ANSWER)
        {
            context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
            await context.Response.WriteAsync("Wrong answer");
            return;
        }

        var currentGuid = Guid.NewGuid();
        users.TryAdd(currentGuid, new UserRetries { LastRetry = DateTime.Now, RetryNumber = 0 });

        var result = new
        {
            Message = $"Very good! Now just GET /Success/{currentGuid}",
            GUID = currentGuid
        };

        var json = JsonConvert.SerializeObject(result);
        await context.Response.WriteAsync(json);
    }
    catch
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("You've probaly gave me something fishy");
    }
});

app.MapGet("/Success/{uid}", async (HttpContext context) =>
{
    try
    {
        if (!context.Request.RouteValues.TryGetValue("uid", out object? uid) || uid == null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync("You forgot something");
            return;
        }

        if (!Guid.TryParse(uid.ToString(), out var receivedGuid) || !users.ContainsKey(receivedGuid))
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await context.Response.WriteAsync("Are you sure that's the correct GUID?");
            return;
        }

        var currentUser = users[receivedGuid];
        if (DateTime.Now - currentUser.LastRetry < WAIT_BETWEEN_RETRIES)
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await context.Response.WriteAsync("Too fast!");
            return;
        }
        currentUser.LastRetry = DateTime.Now;

        if (currentUser.RetryNumber >= REQUIRED_RETRIES)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsync("Very good! Here is your flag \u1F6A9 !");
            return;
        }

        currentUser.RetryNumber++;
        context.Response.StatusCode = StatusCodes.Status418ImATeapot;
        await context.Response.WriteAsync("You'll need to try a few more times");
    }
    catch
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("You've probaly gave me something fishy");
    }
});


app.MapGet("/randomAnswer", async (HttpContext context) =>
{
    if (random.Next(1, 100) > 30)
    {
        context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        await context.Response.WriteAsync("OOOooPs, try again!");
        return;
    }

    context.Response.StatusCode = StatusCodes.Status200OK;
    await context.Response.WriteAsync("All Good!");

});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}