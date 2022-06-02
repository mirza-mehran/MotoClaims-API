using MotoClaims.DataAccess.Generics;
using MotoClaims.DataAccess.UOW;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MotoClaims.DataAccess.Repositories
{
    public sealed class Repositories : IDisposable
    {
        public Repositories()
        {
            _connection = new SqlConnection(DBHelper.ConnectionString);
            _connection.Open();
            _unitOfWork = new UOW.UnitOfWork(_connection);
        }

        private IDbConnection _connection = null;
        private UOW.UnitOfWork _unitOfWork = null;

        public UOW.UnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            _connection.Dispose();
        }
    }
}