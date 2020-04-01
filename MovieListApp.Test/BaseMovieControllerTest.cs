using Moq;
using MovieListApp.Services;
using MovieListApp.Controllers;
using MovieListApp.Models;
using System.Collections.Generic;

namespace MovieListApp.Test
{
    public abstract class BaseMovieControllerTest
    {
        protected readonly List<Movie> Movies;
        protected readonly Mock<IMovieService> MockService;
        protected readonly MoviesController ControllerTest;

        protected BaseMovieControllerTest(List<Movie> movies)
        {
            Movies = movies;
            MockService = new Mock<IMovieService>();
            MockService.Setup(s => s.Get()).Returns(Movies);
            ControllerTest = new MoviesController(MockService.Object);
        }
    }
}
