namespace QFSqlServerConsoleApp
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    public interface IGetJustifs
    {

        List<GetJustifsResults> Execute();
        IEnumerable<GetJustifsResults> Execute(IDbConnection conn);
        GetJustifsResults GetOne();
        GetJustifsResults GetOne(IDbConnection conn);
        System.Int32 ExecuteScalar();
        System.Int32 ExecuteScalar(IDbConnection conn);
        GetJustifsResults Create(IDataRecord record);
        int ExecuteNonQuery();
        int ExecuteNonQuery(IDbConnection conn);
    }
    public class GetJustifs : IGetJustifs
    {
        public virtual int ExecuteNonQuery()
        {
            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                return ExecuteNonQuery(conn);
            }
        }
        public virtual int ExecuteNonQuery(IDbConnection conn)
        {
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = getCommandText();
            return cmd.ExecuteNonQuery();
        }
        private string getCommandText()
        {
            Stream strm = typeof(GetJustifsResults).Assembly.GetManifestResourceStream("QFSqlServerConsoleApp.GetJustifs.sql");
            string queryText = new StreamReader(strm).ReadToEnd();
#if DEBUG
            //Comments inverted at runtime in debug, pre-build in release
            queryText = queryText.Replace("-- designTime", "/*designTime");
            queryText = queryText.Replace("-- endDesignTime", "endDesignTime*/");
            queryText = queryText.Replace("--designTime", "/*designTime");
            queryText = queryText.Replace("--endDesignTime", "endDesignTime*/");
#endif
            return queryText;
        }
        private void AddAParameter(IDbCommand Cmd, string DbType, string DbName, object Value, int Length)
        {
            var dbType = (SqlDbType)System.Enum.Parse(typeof(SqlDbType), DbType);
            var myParam = new SqlParameter(DbName, dbType, Length);
            myParam.Value = Value != null ? Value : DBNull.Value;
            Cmd.Parameters.Add(myParam);
        }
        public virtual List<GetJustifsResults> Execute()
        {
            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                return Execute(conn).ToList();
            }
        }
        public virtual IEnumerable<GetJustifsResults> Execute(IDbConnection conn)
        {
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = getCommandText();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return Create(reader);
                }
            }
        }
        public virtual GetJustifsResults GetOne()
        {
            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                return GetOne(conn);
            }
        }
        public virtual GetJustifsResults GetOne(IDbConnection conn)
        {
            var all = Execute(conn);
            using (IEnumerator<GetJustifsResults> iter = all.GetEnumerator())
            {
                iter.MoveNext();
                return iter.Current;
            }
        }
        public virtual System.Int32 ExecuteScalar()
        {
            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                return ExecuteScalar(conn);
            }
        }
        public virtual System.Int32 ExecuteScalar(IDbConnection conn)
        {
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = getCommandText();
            return (System.Int32)cmd.ExecuteScalar();
        }
        public virtual GetJustifsResults Create(IDataRecord record)
        {
            var returnVal = new GetJustifsResults();
            if (record[0] != null && record[0] != DBNull.Value)
                returnVal.JustifID = (int)record[0];
            if (record[1] != null && record[1] != DBNull.Value)
                returnVal.ThumbB64 = (string)record[1];
            if (record[2] != null && record[2] != DBNull.Value)
                returnVal.DateTransaction = (System.DateTime)record[2];
            if (record[3] != null && record[3] != DBNull.Value)
                returnVal.DateExpedition = (System.DateTime)record[3];
            if (record[4] != null && record[4] != DBNull.Value)
                returnVal.Cif = (string)record[4];
            if (record[5] != null && record[5] != DBNull.Value)
                returnVal.MontantHT = (decimal)record[5];
            if (record[6] != null && record[6] != DBNull.Value)
                returnVal.TauxTva = (decimal)record[6];
            if (record[7] != null && record[7] != DBNull.Value)
                returnVal.MontantTva = (decimal)record[7];
            if (record[8] != null && record[8] != DBNull.Value)
                returnVal.ReceptionNumber = (string)record[8];
            if (record[9] != null && record[9] != DBNull.Value)
                returnVal.ContentTypeImage = (string)record[9];
            if (record[10] != null && record[10] != DBNull.Value)
                returnVal.ContentTypeThumb = (string)record[10];
            if (record[11] != null && record[11] != DBNull.Value)
                returnVal.PreviewB64 = (string)record[11];
            if (record[12] != null && record[12] != DBNull.Value)
                returnVal.ImageB64 = (string)record[12];
            if (record[13] != null && record[13] != DBNull.Value)
                returnVal.SignedJustif = (string)record[13];
            if (record[14] != null && record[14] != DBNull.Value)
                returnVal.UserId = (int)record[14];
            if (record[15] != null && record[15] != DBNull.Value)
                returnVal.NotilusCodeSociete = (string)record[15];
            if (record[16] != null && record[16] != DBNull.Value)
                returnVal.NotilusCodePersonne = (string)record[16];
            if (record[17] != null && record[17] != DBNull.Value)
                returnVal.Used = (bool)record[17];
            if (record[18] != null && record[18] != DBNull.Value)
                returnVal.Archived = (bool)record[18];
            if (record[19] != null && record[19] != DBNull.Value)
                returnVal.Hidden = (bool)record[19];
            returnVal.OnLoad();
            return returnVal;
        }
    }
    public partial class GetJustifsResults
    {
        public int JustifID; //(int not null)
        public string ThumbB64; //(text null)
        public System.DateTime DateTransaction; //(datetime not null)
        public System.DateTime DateExpedition; //(datetime null)
        public string Cif; //(nvarchar null)
        public decimal MontantHT; //(money null)
        public decimal TauxTva; //(decimal null)
        public decimal MontantTva; //(money null)
        public string ReceptionNumber; //(varchar null)
        public string ContentTypeImage; //(varchar null)
        public string ContentTypeThumb; //(varchar null)
        public string PreviewB64; //(text null)
        public string ImageB64; //(text null)
        public string SignedJustif; //(text null)
        public int UserId; //(int null)
        public string NotilusCodeSociete; //(nvarchar null)
        public string NotilusCodePersonne; //(nvarchar null)
        public bool Used; //(bit null)
        public bool Archived; //(bit null)
        public bool Hidden; //(bit null)
    }
}
