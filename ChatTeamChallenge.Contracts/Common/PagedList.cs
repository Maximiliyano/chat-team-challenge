using Microsoft.EntityFrameworkCore;

namespace ChatTeamChallenge.Contracts.Common;

public class PagedList<T>
{
    public List<T> Items { get; set; }
    
    public int Page { get; }
    
    public int PageSize { get; }
    
    public int TotalRows { get; }

    private PagedList(List<T> items, int page, int pageSize, int totalRows)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalRows = totalRows;
    }

    public bool HasNextPage => Page * PageSize < TotalRows;

    public bool HasPreviousPage => Page-1 is not 0;

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        var totalRows = await query.CountAsync();
        var items = await query
            .OrderBy(c => c)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>(items, page, pageSize, totalRows);
    }
}