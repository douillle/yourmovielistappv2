using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MovieListApp.Models;
using System;

namespace MovieListApp.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMongoCollection<Movie> movies;

        public MovieService(IConfiguration config)
        {
            MongoClient client = new MongoClient();

            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                client = new MongoClient(config.GetConnectionString("MoviesDb"));
            }
            else
            {
                client = new MongoClient(config.GetConnectionString("MoviesDbProd"));
            }
            
            IMongoDatabase database = client.GetDatabase("MoviesDb");
            movies = database.GetCollection<Movie>("Movies");
        }

        public List<Movie> Get()
        {
            return movies.Find(movie => true).ToList();
        }

        public Movie Get(string id)
        {
            return movies.Find(movie => movie.Id == id).FirstOrDefault();
        }

        public Movie Create(Movie movie)
        {
            movies.InsertOne(movie);
            return movie;
        }

        public void Update(string id, Movie movieIn)
        {
            movies.ReplaceOne(movie => movie.Id == id, movieIn);
        }

        public void Remove(Movie movieIn)
        {
            movies.DeleteOne(movie => movie.Id == movieIn.Id);
        }

        public void Remove(string id)
        {
            movies.DeleteOne(movie => movie.Id == id);
        }
    }
}
