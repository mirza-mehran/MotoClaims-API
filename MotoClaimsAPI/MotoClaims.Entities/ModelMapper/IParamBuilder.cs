using System.Data;
using System.Data.Common;

namespace MotoClaims.Entities.ModelMapper
{
    public interface IParamBuilder
    {
        /// <summary>
        /// Pars the specified pname.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="pval">The pval.</param>
        /// <returns>DbParameter.</returns>
        DbParameter Par(string pname, object pval);

        /// <summary>
        /// Outs the par.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="val">The value.</param>
        /// <returns>DbParameter.</returns>
        DbParameter OutPar(string pname, object val);

        /// <summary>
        /// Pars the n variable character.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="pval">The pval.</param>
        /// <param name="size">The size.</param>
        /// <returns>DbParameter.</returns>
        DbParameter ParNVarChar(string pname, string pval, int size);

        /// <summary>
        /// Pars the n variable character maximum.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="pval">The pval.</param>
        /// <returns>DbParameter.</returns>
        DbParameter ParNVarCharMax(string pname, string pval);

        /// <summary>
        /// Pars the table.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="pval">The pval.</param>
        /// <returns>DbParameter.</returns>
        DbParameter ParTable(string pname, DataTable pval);
    }
}