using System.Collections.Generic;

namespace MotoClaims.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        T QueryFirst(string sql);

        T GetByIdSp(string order, string where);

        IEnumerable<T> GetAllPaggedSorted(int StartRowIndex, int PageSize, ref int Total, string SortExpression, string SearchText);


       // int RecordCount( string where);

        int CreateRecord(T entity);

        int UpdateRecord(T entity);

        int DeleteRecord(T entity);

        int CheckDuplication(T entity);


    //    IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string order, string where);
        //TEntity FindById(object id);
        //void InsertGraph(TEntity entity);
        //void Update(TEntity entity);
        //void Delete(object id);
        //void Delete(TEntity entity);
        //void Insert(TEntity entity);
        //RepositoryQuery<TEntity> Query();
    }
}