namespace Airport.Infrastructure.Helpers.PaginationParameters
{
    public class FlightParameters
    {
        private const int MaxPageSize = 20;
        private readonly int _pageNumber = 1;
        private readonly int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            init => _pageSize = value <= 0 ? 10 : value > MaxPageSize ? MaxPageSize : value;
        }

        public int PageNumber
        {
            get => _pageNumber;
            init => _pageNumber = value <= 0 ? 1 : value;
        }
    }
}