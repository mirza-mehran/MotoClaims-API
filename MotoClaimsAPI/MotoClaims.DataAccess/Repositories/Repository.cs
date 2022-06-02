using Dapper;
using MotoClaims.DataAccess.Generics;
using System;
using System.Collections.Generic;
using System.Data;

namespace MotoClaims.DataAccess.Repositories
{
    public class Repository<T> : DBGenerics, IRepository<T> where T : class
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection { get { return _transaction.Connection; } }
        public Repository()
       // public Repository(IDbTransaction transaction)
        {
           // _transaction = transaction;
        }

        public T QueryFirst(string sql)
        {
            return _connection.QueryFirst<T>(sql, null);
        }

        public virtual T GetByIdSp(string order, string where)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetAllPaggedSorted(int StartRowIndex, int PageSize, ref int Total, string SortExpression, string SearchText)
        {
            throw new NotImplementedException();
        }

        //public int RecordCount(string where)
        //{
        //    return _connection.RecordCount<T>(where, null,_transaction,null);
        //}

        //public IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string order, string where)
        //{
        //    return _connection.GetListPaged<T>(pageNumber, rowsPerPage, where, order, null, _transaction);
        //}

        public int CreateRecord(T entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateRecord(T entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteRecord(T entity)
        {
            throw new NotImplementedException();
        }

        public int CheckDuplication(T entity)
        {
            throw new NotImplementedException();
        }
    }
}