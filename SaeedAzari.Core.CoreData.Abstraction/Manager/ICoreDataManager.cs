﻿using System.Linq.Expressions;
using SaeedAzari.Core.CoreData.Abstraction.Entities;

namespace SaeedAzari.Core.CoreData.Abstraction.Manager
{
	public interface ICoreDataManager<TData> where TData : BaseCoreData
	{
		Task<bool> Any(Expression<Func<TData, bool>> filter, CancellationToken cancellationToken = default);
		IQueryable<TData> AsQueryable();
		Task<IEnumerable<TData>> Find(Expression<Func<TData, bool>> filter, CancellationToken cancellationToken = default);
		Task<IEnumerable<TData>> GetAll(CancellationToken cancellationToken = default);
	}
}