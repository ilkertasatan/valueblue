﻿using ValueBlue.MovieSearch.Application.Common.Interfaces;
using ValueBlue.MovieSearch.Domain.Movies;

namespace ValueBlue.MovieSearch.Application.UseCases.SearchMovie
{
    public class SearchMovieSuccessResult : IQueryResult
    {
        public SearchMovieSuccessResult(Movie movie)
        {
            Movie = movie;
        }

        public Movie Movie { get; }
    }

    public class MovieNotFoundResult : IQueryResult
    {
        public MovieNotFoundResult(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}