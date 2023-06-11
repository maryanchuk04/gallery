﻿using Galery.Contracts;
using Galery.Db;
using Galery.Exceptions;
using Galery.Models;
using Galery.Services;

#region Configure services

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbSettings = new DbSettings();
builder.Configuration.GetSection("Database").Bind(dbSettings);
builder.Services.AddSingleton(dbSettings);

builder.Services.AddScoped<IImagesService, ImagesService>();
builder.Services.AddScoped<ImagesContext>();
builder.Services.AddScoped<IUploader, ImageUploader>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#endregion

#region Endpoints

app.MapGet("/images", async (IImagesService service) =>
    {
        try
        {
            var images = await service.GetAsync();
            return Results.Ok(images);
        }
        catch (Exception)
        {
            return Results.BadRequest("Ops... Vovan something went wrong :)");
        }
    })
    .WithName("Images")
    .WithOpenApi();

app.MapGet("/images/{id}", async (IImagesService service, string id) =>
    {
        try
        {
            var image = await service.GetAsync(id);
            return Results.Ok(image);
        }
        catch (NotFoundException)
        {
            return Results.NotFound();
        }
        catch (Exception)
        {
            return Results.BadRequest("Ops... Vovan something went wrong :)");
        }
    })
    .WithName("Image by id")
    .WithOpenApi();

app.MapDelete("/images/{id}", async (IImagesService service, string id) =>
    {
        try
        {
            await service.DeleteAsync(id);
            return Results.Ok();
        }
        catch (NotFoundException)
        {
            return Results.NotFound();
        }
        catch (Exception)
        {
            return Results.BadRequest("Ops... Vovan something went wrong :)");
        }
    })
    .WithName("Delete image by id")
    .WithOpenApi();

app.MapPost("/images", async (IUploader uploader, IFormFile file) =>
    {
        try
        {
            var image = await uploader.UploadAsync(file);
            return Results.Ok(image);
        }
        catch (Exception)
        {
            return Results.BadRequest("Ops... Vovan something went wrong :)");
        }
    }).Accepts<IFormFile>("multipart/form-data").Produces(200).WithName("Create image").WithOpenApi();

#endregion

app.Run();