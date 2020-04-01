using System.Collections.Generic;
using MovieListApp.Models;

namespace MovieListApp.Services
{
    public interface IMovieService
    {
        List<Movie> Get();
        Movie Get(string id);
        Movie Create(Movie movie);
        void Update(string id, Movie movieIn);
        void Remove(Movie movieIn);
        void Remove(string id);
    }
}
