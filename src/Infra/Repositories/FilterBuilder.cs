﻿using Domain.Interfaces;
using Domain.Interfaces.Filters;
using System.Linq.Expressions;

namespace Infra.Repositories
{
    public class FilterBuilder<TEntity> : IFilterBuilder<TEntity> where TEntity : IEntity
    {
        private readonly List<IFilter<TEntity>> _filters;

        public FilterBuilder()
        {
            _filters = new List<IFilter<TEntity>>();
        }

        public void Clear()
        {
            _filters.Clear();
        }

        public IFilterBuilder<TEntity> Equal(Expression<Func<TEntity, object>> field, object value)
        {
            _filters.Add(new Filter<TEntity>(field, FilterOperator.Equal, value));

            return this;
        }

        public IFilterBuilder<TEntity> Unequal(Expression<Func<TEntity, object>> field, object value)
        {
            _filters.Add(new Filter<TEntity>(field, FilterOperator.Unequal, value));

            return this;
        }

        public IFilterBuilder<TEntity> GreaterThan(Expression<Func<TEntity, object>> field, object value)
        {
            _filters.Add(new Filter<TEntity>(field, FilterOperator.GreaterThan, value));

            return this;
        }

        public IFilterBuilder<TEntity> LessThan(Expression<Func<TEntity, object>> field, object value)
        {
            _filters.Add(new Filter<TEntity>(field, FilterOperator.LessThan, value));

            return this;
        }

        public List<IFilter<TEntity>> Build()
        {
            return _filters;
        }
    }
}
