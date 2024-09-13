namespace Catalog.API.Models.Pagination;

public record PaginationResponse
(
	int Page = 0,
	int TotalPages = 0,
	int TotalCount = 0
);
