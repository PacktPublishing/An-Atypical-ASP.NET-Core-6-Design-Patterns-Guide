using FluentValidation;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IValidator<MyOptions>, MyOptionsValidator>()
    .AddSingleton<IValidateOptions<MyOptions>, MyOptionsValidateOptions>()
;
builder.Services
    .AddOptions<MyOptions>()
    // Uncomment the following line to make the application start
    //.Bind(builder.Configuration.GetSection("MyOptions"))
    .ValidateOnStart()
;
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

public class MyOptions
{
    public string? Name { get; set; }
}

public class MyOptionsValidator : AbstractValidator<MyOptions>
{
    public MyOptionsValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class MyOptionsValidateOptions : IValidateOptions<MyOptions>
{
    private readonly IValidator<MyOptions> _validator;
    public MyOptionsValidateOptions(IValidator<MyOptions> validator)
    {
        _validator = validator;
    }

    public ValidateOptionsResult Validate(string name, MyOptions options)
    {
        var validationResult = _validator.Validate(options);
        if (validationResult.IsValid)
        {
            return ValidateOptionsResult.Success;
        }
        var errorMessage = validationResult.Errors.Select(x => x.ErrorMessage);
        return ValidateOptionsResult.Fail(errorMessage);
    }
}