using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CodingChallenge.DataAccess.Interfaces;
using CodingChallenge.DataAccess.Models;
using CodingChallenge.Utilities;

namespace CodingChallenge.DataAccess
{
    public class LibraryService : ILibraryService
    {
        public LibraryService() { }

        private IEnumerable<Movie> GetMovies()
        {
            var k = ConfigurationManager.AppSettings["LibraryPath"];
            return _movies ?? (_movies = ConfigurationManager.AppSettings["LibraryPath"].FromFileInExecutingDirectory().DeserializeFromXml<Library>().Movies);
        }
        private IEnumerable<Movie> _movies { get; set; }

        public int SearchMoviesCount(string title)
        {
            return SearchMovies(title).Count();
        }

        public IEnumerable<Movie> SearchMovies(string title, int? skip = null, int? take = null, string sortColumn = null, SortDirection sortDirection = SortDirection.Ascending)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "";
            }
           
            var movies = GetMovies().Where(s => s.Title.Contains(title));
           
            if (skip.HasValue && take.HasValue)
            {
                movies = movies.Skip(skip.Value).Take(take.Value);
            }
            if (sortColumn == null)
            {
                return movies.ToList().Distinct();
            }
            else
                return movies.ToList().OrderBy(x => x.GetType().GetProperty(sortColumn).GetValue(x, null))
                    .OrderBy(x => x.Title.StartsWith("The ", System.StringComparison.OrdinalIgnoreCase)
                    || x.Title.StartsWith("a ", System.StringComparison.OrdinalIgnoreCase)
                    || x.Title.StartsWith("an ", System.StringComparison.OrdinalIgnoreCase)).Distinct();

        }
    }
}
