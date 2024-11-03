﻿using Grand.Api.Models;
using Grand.Api.Queries.Models.Common;
using Grand.Data;
using Grand.Domain;
using MediatR;

namespace Grand.Api.Queries.Handlers.Common;

public class GetGenericQueryHandler<T, C> : IRequestHandler<GetGenericQuery<T, C>, IQueryable<T>>
    where T : BaseApiEntityModel
    where C : BaseEntity
{
    private readonly IRepository<C> _repository;

    public GetGenericQueryHandler(IRepository<C> repository)
    {
        _repository = repository;
    }

    public async Task<IQueryable<T>> Handle(GetGenericQuery<T, C> request, CancellationToken cancellationToken)
    {
        var query = _repository.TableCollection<T>();
        if (string.IsNullOrEmpty(request.Id))
            return query;
        return await Task.FromResult(query.Where(x => x.Id == request.Id));
    }
}