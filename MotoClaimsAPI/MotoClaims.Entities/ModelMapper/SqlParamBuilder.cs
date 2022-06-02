using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace MotoClaims.Entities.ModelMapper
{
    public class SqlParamBuilder : IParamBuilder
    {
        /// <summary>
        /// Pars the specified pname.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="pval">The pval.</param>
        /// <returns>DbParameter.</returns>
        public DbParameter Par(string pname, object pval)
        {
            return new SqlParameter(pname, pval);
        }

        /// <summary>
        /// Outs the par.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="val">The value.</param>
        /// <returns>DbParameter.</returns>
        public DbParameter OutPar(string pname, object val)
        {
            return new SqlParameter(pname, val) { Direction = ParameterDirection.Output };
        }

        /// <summary>
        /// Pars the n variable character.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="pval">The pval.</param>
        /// <param name="size">The size.</param>
        /// <returns>DbParameter.</returns>
        public DbParameter ParNVarChar(string pname, string pval, int size)
        {
            SqlParameter p = new SqlParameter(pname, SqlDbType.NVarChar, size);
            p.Value = pval;
            return p;
        }

        /// <summary>
        /// Pars the n variable character maximum.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="pval">The pval.</param>
        /// <returns>DbParameter.</returns>
        public DbParameter ParNVarCharMax(string pname, string pval)
        {
            SqlParameter p = new SqlParameter(pname, SqlDbType.NVarChar, -1);
            p.Value = pval;
            return p;
        }

        /// <summary>
        /// Pars the variable character.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="pval">The pval.</param>
        /// <param name="size">The size.</param>
        /// <returns>DbParameter.</returns>
        public DbParameter ParVarChar(string pname, string pval, int size)
        {
            SqlParameter p = new SqlParameter(pname, SqlDbType.VarChar, size);
            p.Value = pval;
            return p;
        }

        /// <summary>
        /// Pars the text.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="pval">The pval.</param>
        /// <returns>DbParameter.</returns>
        public DbParameter ParText(string pname, string pval)
        {
            SqlParameter p = new SqlParameter(pname, SqlDbType.Text, -1);
            p.Value = pval;
            return p;
        }

        /// <summary>
        /// Pars the table.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="pval">The pval.</param>
        /// <returns>DbParameter.</returns>
        public DbParameter ParTable(string pname, DataTable pval)
        {
            SqlParameter p = new SqlParameter(pname, SqlDbType.Structured, -1);
            p.Value = pval;
            return p;
        }
    }
}