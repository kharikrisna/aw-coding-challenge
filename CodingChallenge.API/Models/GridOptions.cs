using CodingChallenge.DataAccess;

namespace CodingChallenge.API.Models
{

    public class GridOptions
    {
        public const string SortColumnKey = "sort";
        public const string SortDirectionKey = "dir";
        public const string PageKey = "page";
        public const string SortAscendingValue = "ASC";
        public const string SortDescendingValue = "DESC";

        public GridOptions()
        {
            ItemsPerPage = 10;
        }

        public string SortColumn { get; set; }
        public SortDirection SortDirection { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }

        public int PageCount
        {
            get { return (TotalItems/ItemsPerPage) + (TotalItems%ItemsPerPage > 0 ? 1 : 0); }
        }
    }

   
}