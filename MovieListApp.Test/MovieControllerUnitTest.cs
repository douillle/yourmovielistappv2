using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;
using MovieListApp.Models;
using MovieListApp.Controllers;
using System.Linq;

namespace MovieListApp.Test
{
    public class MovieControllerUnitTest : BaseMovieControllerTest
    {
        private static readonly List<Movie> moviesTest = new List<Movie>
        {
            new Movie { Id = "1", Title = "Film1", Genre = "Genre1", Year=2020, ImageUrl="/images/donniedarkov2.jpg" },
            new Movie { Id = "2", Title = "Film2", Genre = "Genre2", Year=2020, ImageUrl="/images/fightclubv2.jpg" }
        };

        public MovieControllerUnitTest() : base(moviesTest) { }

        #region Index Test
        [Fact]
        public void IndexShouldReturnViewAndAListOfMovieType()
        {
            var res = ControllerTest.Index();

            var viewResult = Assert.IsType<ViewResult>(res);

            Assert.IsAssignableFrom<List<Movie>>(viewResult.ViewData.Model);
        }

        #endregion

        #region Details Test
        [Fact]
        public void DetailsShouldReturnView()
        {
            var existingId = "1";

            MockService.Setup(s => s.Get(existingId)).Returns(moviesTest.Find(m => m.Id == existingId));

            var res = ControllerTest.Details(existingId);

            Assert.IsType<ViewResult>(res);
        }

        [Fact]
        public void DetailsShouldReturn404NotFound()
        {
            var notexistingId = "5";

            MockService.Setup(s => s.Get(notexistingId)).Returns(moviesTest.Find(m => m.Id == notexistingId));

            var res = ControllerTest.Details(notexistingId);

            Assert.IsType<NotFoundResult>(res);
        }

        #endregion

        #region Creation Test
        [Fact]
        public void CreateGetShouldHaveNoView()
        {
            var result = ControllerTest.Create();

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public void CreatePostShouldReturnMovieIfModelIsInvalid()
        {
            var model = new Movie();
            ControllerTest.ModelState.AddModelError("error", "testerror");

            var result = ControllerTest.Create(model);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreatePostShouldReturnRedirectToActionIndexIfModelIsValid()
        {
            var model = new Movie();

            var result = ControllerTest.Create(model);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(MoviesController.Index), redirectResult.ActionName);
        }

        #endregion

        #region Edit Test
        [Fact]
        public void EditGetWithInvalidIdShouldReturnNotFound()
        {
            var notexistingId = "5";

            MockService.Setup(s => s.Get(notexistingId)).Returns(moviesTest.Find(m => m.Id == notexistingId));

            var result = ControllerTest.Edit(notexistingId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditGetShouldReturnView()
        {
            var existingId = "1";

            MockService.Setup(s => s.Get(existingId)).Returns(moviesTest.Find(m => m.Id == existingId));

            var result = ControllerTest.Edit(existingId);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditPostShouldReturnMovieIfModelIsInvalid()
        {
            var model = new Movie();
            ControllerTest.ModelState.AddModelError("error", "testerror");

            var result = ControllerTest.Edit(model.Id, model);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditPostShouldReturnRedirectToActionIndexIfModelIsValid()
        {
            var model = moviesTest.FirstOrDefault();

            var result = ControllerTest.Edit(model.Id, model);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(MoviesController.Index), redirectResult.ActionName);
        }

        #endregion

        #region Delete Test
        [Fact]
        public void DeleteANonExistingMovieShouldReturnNotFound()
        {
            var notexistingId = "5";

            MockService.Setup(s => s.Get(notexistingId)).Returns(moviesTest.Find(m => m.Id == notexistingId));

            var result = ControllerTest.Delete(notexistingId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeletePostShouldReturnRedirectToActionIndex()
        {
            var model = moviesTest.FirstOrDefault();
            MockService.Setup(s => s.Get(model.Id)).Returns(moviesTest.FirstOrDefault());

            var result = ControllerTest.DeleteConfirmed(model.Id);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(MoviesController.Index), redirectResult.ActionName);
        }
        #endregion
    }
}
