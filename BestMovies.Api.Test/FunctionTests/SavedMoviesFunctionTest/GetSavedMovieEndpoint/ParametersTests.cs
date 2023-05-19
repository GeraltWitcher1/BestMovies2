﻿using BestMovies.Api.Functions;
using BestMovies.Api.Repositories;
using BestMovies.Api.Test.Helpers;

namespace BestMovies.Api.Test.FunctionTests.SavedMoviesFunctionTest.GetSavedMovieEndpoint;

public class ParametersTests
{
    private readonly ISavedMoviesRepository _savedMoviesRepository;
    private readonly SavedMoviesFunctions _sut;
    private readonly MockLogger<SavedMoviesFunctions> _logger;

    public ParametersTests()
    {
        _savedMoviesRepository = Substitute.For<ISavedMoviesRepository>();
        _logger = Substitute.For<MockLogger<SavedMoviesFunctions>>();
        _sut = new SavedMoviesFunctions(_savedMoviesRepository);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task GetSavedMovie_UserIdHasInvalidValues_ReturnsBadRequest(string userId)
    {
        //Arrange
        var request = new DefaultHttpRequest(new DefaultHttpContext())
        {
            Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(""))),
        };

        //Act
        var response = await _sut.GetSavedMovie(request, userId, 1, _logger);
        var result = (ContentResult)response;

        //Assert
        Assert.Equal(400, result.StatusCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetSavedMovie_MovieIdHasInvalidValues_ReturnsBadRequest(int movieId)
    {
        //Arrange
        var request = new DefaultHttpRequest(new DefaultHttpContext())
        {
            Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(""))),
        };

        //Act
        var response = await _sut.GetSavedMovie(request, "userId", movieId, _logger);
        var result = (ContentResult)response;

        //Assert
        Assert.Equal(400, result.StatusCode);
    }
}
