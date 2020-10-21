using System.Collections.Generic;
using CodingChallenge.DataAccess.Models;

namespace CodingChallenge.API.Models
{
    public class MovieListViewModel
    {
        public List<Movie> Movies { get; set; }
        public GridOptions GridOptions { get; set; }
       
    }
}