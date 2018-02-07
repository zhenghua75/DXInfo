namespace System.Linq.Dynamic
{
    using System;
    using System.Linq;

    internal static class IQueryableUtil<T>
    {
        public static int Count(IQueryable Source)
        {
            return Source.OfType<T>().AsQueryable<T>().Count<T>();
        }

        public static IQueryable Page(IQueryable Source, int PageSize, int PageIndex)
        {
            return Source.OfType<T>().AsQueryable<T>().Skip<T>((PageSize * (PageIndex - 1))).Take<T>(PageSize);
        }
    }
}

