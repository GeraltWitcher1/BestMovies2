﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMDbLib.Objects.Changes;
using TMDbLib.Objects.Discover;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Lists;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.Trending;
using TMDbLib.Objects.TvShows;
using Credits = TMDbLib.Objects.Movies.Credits;

namespace BestMovies.Bff.Services;

// ReSharper disable once InconsistentNaming
public interface ITMDbWrapperService
{
    Task<TMDbConfig> GetConfigAsync();

    Task<byte[]> GetImageBytesAsync(string size, string filePath, bool useSsl = false,
        CancellationToken token = new CancellationToken());

    Task<SearchContainer<SearchMovie>> GetMoviePopularListByGenreAsync(Genre searchedGenre, string? region, string? language);
    DiscoverMovie DiscoverMoviesAsync();

    Task<List<Genre>> GetMovieGenresAsync(CancellationToken cancellationToken = new CancellationToken());

    Task<Movie> GetMovieAsync(int movieId, MovieMethods extraMethods = MovieMethods.Undefined,
        CancellationToken cancellationToken = new CancellationToken());
    
    Task<Credits> GetMovieCreditsAsync(int movieId, CancellationToken cancellationToken = new CancellationToken());

    Task<ImagesWithId> GetMovieImagesAsync(int movieId, CancellationToken cancellationToken = new CancellationToken());
    
    Task<SearchContainer<SearchMovie>> GetMoviePopularListAsync(string? language = null, int page = 0, string? region = null,
        CancellationToken cancellationToken = new CancellationToken());
    
    Task<Person> GetPersonAsync(int personId, PersonMethods extraMethods = PersonMethods.Undefined,
        CancellationToken cancellationToken = new CancellationToken());

    Task<ProfileImages> GetPersonImagesAsync(int personId, CancellationToken cancellationToken = new CancellationToken());

    Task<MovieCredits> GetPersonMovieCreditsAsync(int personId, CancellationToken cancellationToken = new CancellationToken());
    
    Task<SearchContainer<SearchMovie>> SearchMovieAsync(string query, int page = 0, bool includeAdult = false, int year = 0, string? region = null,
        int primaryReleaseYear = 0, CancellationToken cancellationToken = new CancellationToken());

}