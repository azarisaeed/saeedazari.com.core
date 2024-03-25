
namespace SaeedAzari.core.entities.models;

public interface IListResult<TResult> : IResult<IEnumerable<TResult>>
{
    public int PageSize { get; }
    public int TotalPages { get; }
    public int PageNumber { get; }
    public long TotalCount { get; }
    public bool HasPrevious { get; }
    public bool HasNext { get; }
}

public class ListResult<TResult> : Result<IEnumerable<TResult>>, IListResult<TResult>
{
    public int PageSize { get; }
    public int TotalPages { get; }
    public int PageNumber { get; }
    public long TotalCount { get; }
    public bool HasPrevious { get; }
    public bool HasNext { get; }

    public ListResult(IEnumerable<TResult> items) : base(items)
    {
        var count = items.Count();
        PageNumber = 1;
        PageSize = count;
        TotalCount = count;
        HasPrevious = false;
        HasNext = false;
        TotalPages = 1;
    }
    public ListResult(IEnumerable<TResult> items, long TotalCount, int PageNumber, int RecordCount) : base(items)
    {
        this.PageNumber = PageNumber;
        PageSize = RecordCount;
        this.TotalCount = TotalCount;
        HasPrevious = false;
        HasNext = false;
        TotalPages = 1;
    }

    public ListResult(Exception exception) : base(exception)
    {
    }
}