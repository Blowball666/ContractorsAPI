using ContractorsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ��������� Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Contractors API",
        Version = "v1",
        Description = "API ��� ���������� ������������.", // �������� API        
        License = new OpenApiLicense
        {
            Name = "MIT License", // ��������
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // ������� ���� � XML-����� ������������
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    // ����������� �������� �� �����
    c.TagActionsBy(api => api.GroupName);
    c.DocInclusionPredicate((name, api) => true);
});

// ���������� ����� � ���������.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// �������� Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contractors API v1"));
}

// ��������� �������� HTTP-��������.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();