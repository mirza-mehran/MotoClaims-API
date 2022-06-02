using System;
using System.Data.Common;

namespace MotoClaims.Entities.ModelMapper
{
    /// <summary>
    /// Class DataReaderExtension.
    /// </summary>
    public static class DataReaderExtension
    {
        /// <summary>
        /// Columns the exists.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ColumnExists(this DbDataReader dataReader, string columnName)
        {
            for (int i = 0; i < dataReader.FieldCount; i++)
                if (dataReader.GetName(i).ToLower() == columnName.ToLower())
                    return true;

            return false;
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.String.</returns>
        public static string GetString(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            try
            {
                int index = dataReader.GetOrdinal(columnName);

                if (dataReader.IsDBNull(index))
                    return null;

                return dataReader.GetString(index);
            }
            catch
            { return null; }
        }

        /// <summary>
        /// Gets the string null to empty.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.String.</returns>
        public static string GetStringNullToEmpty(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return String.Empty;

            return dataReader.GetString(index);
        }

        /// <summary>
        /// Gets the decimal.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Decimal.</returns>
        public static decimal GetDecimal(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return 0;

            //if index is -1 then GetInt32 should throw an exception
            return dataReader.GetDecimal(index);
        }

        /// <summary>
        /// Gets the decimal.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="nullDefault">The null default.</param>
        /// <returns>System.Decimal.</returns>
        public static decimal GetDecimal(this DbDataReader dataReader, string columnName, decimal nullDefault)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return nullDefault;

            //if index is -1 then GetInt32 should throw an exception
            return dataReader.GetDecimal(index);
        }

        /// <summary>
        /// Gets the decimal nullable.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Nullable&lt;System.Decimal&gt;.</returns>
        public static decimal? GetDecimalNullable(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return null;

            return dataReader.GetDecimal(index);
        }

        /// <summary>
        /// Gets the byte.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Byte.</returns>
        public static byte GetByte(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return byte.MinValue;

            //if index is -1 then GetByte should throw an exception
            return dataReader.GetByte(index);
        }

        /// <summary>
        /// Gets the byte.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="nullDefault">The null default.</param>
        /// <returns>System.Byte.</returns>
        public static byte GetByte(this DbDataReader dataReader, string columnName, byte nullDefault)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return nullDefault;

            //if index is -1 then GetByte should throw an exception
            return dataReader.GetByte(index);
        }

        /// <summary>
        /// Gets the byte nullable.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Nullable&lt;System.Byte&gt;.</returns>
        public static byte? GetByteNullable(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return null;

            return dataReader.GetByte(index);
        }

        /// <summary>
        /// Gets the int16.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Int16.</returns>
        public static short GetInt16(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return short.MinValue;

            //if index is -1 then GetInt16 should throw an exception
            return dataReader.GetInt16(index);
        }

        /// <summary>
        /// Gets the int16.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="nullDefault">The null default.</param>
        /// <returns>System.Int16.</returns>
        public static short GetInt16(this DbDataReader dataReader, string columnName, short nullDefault)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return nullDefault;

            //if index is -1 then GetInt16 should throw an exception
            return dataReader.GetInt16(index);
        }

        /// <summary>
        /// Gets the int16 nullable.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Nullable&lt;System.Int16&gt;.</returns>
        public static short? GetInt16Nullable(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return null;

            return dataReader.GetInt16(index);
        }

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Int32.</returns>
        public static int GetInt32(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return int.MinValue;

            //if index is -1 then GetInt32 should throw an exception
            return dataReader.GetInt32(index);
        }

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="nullDefault">The null default.</param>
        /// <returns>System.Int32.</returns>
        public static int GetInt32(this DbDataReader dataReader, string columnName, int nullDefault)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return nullDefault;

            //if index is -1 then GetInt32 should throw an exception
            return dataReader.GetInt32(index);
        }

        /// <summary>
        /// Gets the int32 nullable.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
        public static int? GetInt32Nullable(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return null;

            return dataReader.GetInt32(index);
        }

        /// <summary>
        /// Gets the int64.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Int64.</returns>
        public static long GetInt64(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return int.MinValue;

            return dataReader.GetInt64(index);
        }

        /// <summary>
        /// Gets the long.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Int64.</returns>
        public static long GetLong(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return 0;

            return dataReader.GetInt64(index);
        }

        /// <summary>
        /// Gets the int64.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="nullDefault">The null default.</param>
        /// <returns>System.Int64.</returns>
        public static long GetInt64(this DbDataReader dataReader, string columnName, long nullDefault)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return nullDefault;

            return dataReader.GetInt64(index);
        }

        /// <summary>
        /// Gets the int64 nullable.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Nullable&lt;System.Int64&gt;.</returns>
        public static long? GetInt64Nullable(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return null;

            return dataReader.GetInt64(index);
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GetBoolean(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return false;

            return dataReader.GetBoolean(index);
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="nullDefault">if set to <c>true</c> [null default].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GetBoolean(this DbDataReader dataReader, string columnName, bool nullDefault)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return nullDefault;

            return dataReader.GetBoolean(index);
        }

        /// <summary>
        /// Gets the boolean nullable.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool? GetBooleanNullable(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return null;

            return dataReader.GetBoolean(index);
        }

        //===================

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>DateTime.</returns>
        public static DateTime GetDateTime(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return DateTime.MinValue;

            return dataReader.GetDateTime(index);
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="nullDefault">The null default.</param>
        /// <returns>DateTime.</returns>
        public static DateTime GetDateTime(this DbDataReader dataReader, string columnName, DateTime nullDefault)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return nullDefault;

            return dataReader.GetDateTime(index);
        }

        /// <summary>
        /// Gets the date time nullable.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        public static DateTime? GetDateTimeNullable(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return null;

            return dataReader.GetDateTime(index);
        }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>Guid.</returns>
        public static Guid GetGuid(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return Guid.Empty;

            //if index is -1 then GetInt32 should throw an exception
            return dataReader.GetGuid(index);
        }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="nullDefault">The null default.</param>
        /// <returns>Guid.</returns>
        public static Guid GetGuid(this DbDataReader dataReader, string columnName, Guid nullDefault)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return nullDefault;

            //if index is -1 then GetInt32 should throw an exception
            return dataReader.GetGuid(index);
        }

        /// <summary>
        /// Gets the unique identifier nullable.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Nullable&lt;Guid&gt;.</returns>
        public static Guid? GetGuidNullable(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return null;

            return dataReader.GetGuid(index);
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Double.</returns>
        public static double GetDouble(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return double.MinValue;

            //if index is -1 then GetInt32 should throw an exception
            return dataReader.GetDouble(index);
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="nullDefault">The null default.</param>
        /// <returns>System.Double.</returns>
        public static double GetDouble(this DbDataReader dataReader, string columnName, Double nullDefault)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return nullDefault;

            //if index is -1 then GetInt32 should throw an exception
            return dataReader.GetDouble(index);
        }

        /// <summary>
        /// Gets the double nullable.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.Nullable&lt;System.Double&gt;.</returns>
        public static double? GetDoubleNullable(this DbDataReader dataReader, string columnName)
        {
            //=============================================================================
            //DO NOT catch exceptions in this method.
            //We DO want an exception to generate an error log entry and an error message.
            //=============================================================================

            //Throws Exception if column name does not exist
            int index = dataReader.GetOrdinal(columnName);

            if (dataReader.IsDBNull(index))
                return null;

            return dataReader.GetDouble(index);
        }
    }
}