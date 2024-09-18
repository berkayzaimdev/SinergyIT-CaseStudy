namespace Catalog.API.Models.Pagination;

public class PaginationResponse
{
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 0;
    public int TotalPages { get; set; } = 0;
    public int TotalCount { get; private set; } = 0;

    public PaginationResponse(int pageNumber, int pageSize, int totalPages)
    {
        PageNumber = pageNumber;
		PageSize = pageSize;
        TotalPages = totalPages;
        TotalCount = pageSize*totalPages;
    }

    public PaginationResponse()
    {
        
    }
}
