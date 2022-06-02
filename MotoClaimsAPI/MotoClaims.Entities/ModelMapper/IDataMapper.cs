using System.Data.Common;

namespace MotoClaims.Entities.ModelMapper
{
    public interface IDataMapper
    {
        //an object that implements this interface can set its own properties from the passed in data reader
        /// <summary>
        /// Maps the properties.
        /// </summary>
        /// <param name="dr">The dr.</param>
        void MapProperties(DbDataReader dr);
    }
}