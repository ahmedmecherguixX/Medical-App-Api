using Medical_App_Api.Data;
using Medical_App_Api.Model;
using Medical_App_Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDataContext>(options =>
    options.UseSqlite("Data Source=MedicalAppApiDb.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddScoped<AppointmentServices>();
builder.Services.AddScoped<PatientServices>();
builder.Services.AddScoped<DoctorServices>();
builder.Services.AddScoped<LoginServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

// Appointment Endpoints
var appointmentGroup = app.MapGroup("/api/appointments")
    .WithTags("Appointments");

appointmentGroup.MapPost("/", async (AppointmentServices service, int patientId, int doctorId, int duration, DateTime dateTime) =>
{
    await service.AddAppointment(patientId, doctorId, duration, dateTime);
    return Results.Created();
})
.WithName("CreateAppointment")
.WithOpenApi();

appointmentGroup.MapPut("/{appointmentId}", async (AppointmentServices service, int appointmentId, int patientId, int doctorId, int duration, DateTime dateTime, AppointmentStatus status) =>
{
    await service.UpdateAppointment(appointmentId, patientId, doctorId, duration, dateTime, status);
    return Results.NoContent();
})
.WithName("UpdateAppointment")
.WithOpenApi();

appointmentGroup.MapPut("/{appointmentId}/description", async (AppointmentServices service, int appointmentId, string description) =>
{
    await service.DescribeAppointment(appointmentId, description);
    return Results.NoContent();
})
.WithName("DescribeAppointment")
.WithOpenApi();

appointmentGroup.MapDelete("/{appointmentId}", async (AppointmentServices service, int appointmentId) =>
{
    await service.RemoveAppointment(appointmentId);
    return Results.NoContent();
})
.WithName("DeleteAppointment")
.WithOpenApi();

// Patient Endpoints
var patientGroup = app.MapGroup("/api/patients")
    .WithTags("Patients");

patientGroup.MapPost("/", async (PatientServices service, int id, string name) =>
{
    await service.AddPatient(id, name);
    return Results.Created();
})
.WithName("CreatePatient")
.WithOpenApi();

patientGroup.MapDelete("/{id}", async (PatientServices service, int id) =>
{
    await service.RemovePatient(id);
    return Results.NoContent();
})
.WithName("DeletePatient")
.WithOpenApi();

// Doctor Endpoints
var doctorGroup = app.MapGroup("/api/doctors")
    .WithTags("Doctors");

doctorGroup.MapPost("/", async (DoctorServices service, int id, string name) =>
{
    await service.AddDoctor(id, name);
    return Results.Created();
})
.WithName("CreateDoctor")
.WithOpenApi();

doctorGroup.MapDelete("/{id}", async (DoctorServices service, int id) =>
{
    await service.RemoveDoctor(id);
    return Results.NoContent();
})
.WithName("DeleteDoctor")
.WithOpenApi();

// Login Endpoints
var loginGroup = app.MapGroup("/api/auth")
    .WithTags("Authentication");

loginGroup.MapPost("/register", async (LoginServices service, string email, string password) =>
{
    await service.AddLoginAccount(email, password);
    return Results.Created();
})
.WithName("Register")
.WithOpenApi();

loginGroup.MapPost("/change-password", async (LoginServices service, string email, string oldPassword, string newPassword) =>
{
    await service.ChangePassword(email, oldPassword, newPassword);
    return Results.Ok();
})
.WithName("ChangePassword")
.WithOpenApi();

loginGroup.MapDelete("/", async (LoginServices service, string email, string password) =>
{
    await service.RemoveLoginAccount(email, password);
    return Results.NoContent();
})
.WithName("DeleteAccount")
.WithOpenApi();

app.Run();
