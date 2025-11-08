namespace Restaurants.Application.Common;

public class PagesResults<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }

    public PagesResults(IEnumerable<T> items,int totalItems,int pageNumber,int pageSize)
    {
        Items = items;
        TotalCount = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        ItemsFrom = (pageNumber - 1) * pageSize + 1 ;
        ItemsTo = Math.Min(pageNumber * pageSize, totalItems);

    }
}
