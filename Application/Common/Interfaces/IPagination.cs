namespace Application.Common.Interfaces
{
    public interface IPagination<T>
    {
        int CurrentPageIndex { get; set; }
        int PageSize { get; set; }
        int TotalItemCount { get; set; }

        IList<T> List { get; set; }
    }
}