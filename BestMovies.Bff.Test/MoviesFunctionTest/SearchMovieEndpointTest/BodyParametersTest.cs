﻿using BestMovies.Bff.Functions;
using BestMovies.Bff.Interface;
using BestMovies.Bff.Test.Helpers;

namespace BestMovies.Bff.Test.MoviesFunctionTest.SearchMovieEndpointTest;

public class BodyParametersTest
{
    private readonly IMovieService _movieService;
    private readonly MockLogger<MovieFunctions> _logger;
    private readonly MovieFunctions _sut;
    public BodyParametersTest()
    {
        _movieService = Substitute.For<IMovieService>();
        _logger = Substitute.For<MockLogger<MovieFunctions>>();
        _sut = new MovieFunctions(_movieService);
    }

    [Fact]
    public async Task SearchMovieEndpoint_BodyParameters_IsNotProvided()
    {
        //Arrange
        var _request = new DefaultHttpRequest(new DefaultHttpContext())
        {
            Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject("")))
        };


        // ACT
        var response = await _sut.SearchMovie(_request, _logger);
        var result = (BadRequestObjectResult)response;

        //Assert
        Assert.Equal(400, result.StatusCode);
        _logger.Received().Log(LogLevel.Information, Arg.Is<string>(s => s.Contains("Search parameters were not provided")));
    }
}