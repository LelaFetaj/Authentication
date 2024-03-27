using Authentication.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllerWithFilters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomAuthentication(builder.Configuration);
//builder.Services.AddSwagger();
builder.Services.AddAuthenticationContext();
builder.Services.AddAuthenticationRepositories();
builder.Services.AddAuthenticationServices();
//builder.Services.AddJourneyContext();
//builder.Services.AddJourneyBrokers();
//builder.Services.AddJourneyServices();
//builder.Services.AddCustomLogging();
//builder.Services.AddHealthChecks()
//    .AddCustomHealthChecks(builder.Configuration);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(o =>
//{
//    o.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = Convert.ToBoolean(builder.Configuration["AuthConfiguration:ValidateIssuer"]),
//        ValidateAudience = Convert.ToBoolean(builder.Configuration["AuthConfiguration:ValidateAudience"]),
//        ValidateIssuerSigningKey = Convert.ToBoolean(builder.Configuration["AuthConfiguration:ValidateIssuerSigningKey"]),
//        RequireExpirationTime = Convert.ToBoolean(builder.Configuration["AuthConfiguration:RequireExpirationTime"]),
//        ValidateLifetime = Convert.ToBoolean(builder.Configuration["AuthConfiguration:ValidateLifetime"]),
//        RequireSignedTokens = Convert.ToBoolean(builder.Configuration["AuthConfiguration:RequireSignedTokens"]),
//        IssuerSigningKey = new SymmetricSecurityKey(
//                            Encoding.UTF8.GetBytes(builder.Configuration["AuthConfiguration:SigningKey"])),
//    };
//});

builder.Services.AddSwagger();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name V1");
    });
}

app.UseHsts();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
