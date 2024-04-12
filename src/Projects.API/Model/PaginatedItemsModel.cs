﻿namespace diploma.Projects.API.Model;

public class PaginatedItemsModel<TEntity>(
    int pageIndex, int pageSize, long count,
    IEnumerable<TEntity> data) where TEntity : class
{
    public int PageIndex { get; } = pageIndex;

    public int PageSize { get; } = pageSize;

    public long Count { get; } = count;

    public IEnumerable<TEntity> Data { get; } = data;
}