namespace DotNetCoreTraining20230617;

public static class DevCode
{
    public static IQueryable<T> Pagination<T>(this IQueryable<T> query, int pageNo, int pageSize)
    {
        int skipRow = (pageNo - 1) * pageSize;
        return query.Skip(skipRow).Take(pageSize);
    }
}