using System;
using System.Data;

namespace MotoClaims.DataAccess.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        Guid Id { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }

        void Begin();

        void Commit();

        void Rollback();
    }
}