namespace Catalog.API.Models.Pagination;

public record PaginationRequest
(
	int Page = 0,
	int PageSize = 0
);
