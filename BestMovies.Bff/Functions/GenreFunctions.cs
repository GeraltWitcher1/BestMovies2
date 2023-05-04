﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BestMovies.Bff.Extensions;
using BestMovies.Bff.Interface;
using BestMovies.Shared.Dtos.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TMDbLib.Client;

namespace BestMovies.Bff.Functions;


public class GenreFunctions
{
    private const string Tag = "Genre";
    
    private readonly IGenreService _genreService;

    public GenreFunctions(IGenreService genreService)
    {
        _genreService = genreService;
    }
    
    [FunctionName(nameof(GetGenreNames))]
    [OpenApiOperation(operationId: nameof(GetGenreNames), tags: new[] { Tag })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(IEnumerable<string>), Description = "Returns the names of all the available genres.")]
    public async Task<IActionResult> GetGenreNames(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "genres")]
        HttpRequest req,
        ILogger log)
    {
        try
        {
            var genres = await _genreService.GetGenreNames();
            return new OkObjectResult(genres);
        }
        catch (Exception ex)
        {
            return new ContentResult
            {
                StatusCode = 500,
                Content = ex.Message
            };
        }
    }
    
}