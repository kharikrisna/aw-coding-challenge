using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingChallenge.API.Models;
using CodingChallenge.DataAccess.Interfaces;
using CodingChallenge.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodingChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public ILibraryService LibraryService { get; private set; }
       
        public MoviesController(ILibraryService libraryService)
        {
            LibraryService = libraryService;
        }
        // GET:/api/Movies?sort=Title&dir=ASC
        [HttpGet]
        public IEnumerable<Movie> Get([FromQuery] GridOptions options,string? SearchByTtile)
        {
            options.TotalItems = LibraryService.SearchMoviesCount("");
            if (options.SortColumn == null)
                options.SortColumn = "ID";
            var model = new MovieListViewModel
            {
                GridOptions = options,
                Movies = LibraryService.SearchMovies(SearchByTtile, null, null, options.SortColumn).ToList()
            };

            return model.Movies;
        }

        // GET api/<MoviesController>/5
        [HttpGet("Id")]
        public string Get()
        {
            return " SearchByTtile";
        }

        // POST api/<MoviesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
