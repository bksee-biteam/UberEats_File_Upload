using System.Data.SqlClient;
using System.Threading;
using System.Data;
using System;


namespace UberEats_Upload
{
    class DataBaseOperator
    {


        private short numberOfRetryAttemmpts = 3;
        private int numberOfDeletedRows;
        private int executionTimeOut = 360;

        protected void Insert(DataTable _table, string Tablename)
        {


            using (SqlConnection connect_db = new SqlConnection(MyGlobals.DatabaseConnectionStr))
            {

                SqlBulkCopy bulkCopy =
                new SqlBulkCopy
                (
                connect_db,
                SqlBulkCopyOptions.TableLock |
                SqlBulkCopyOptions.FireTriggers |
                SqlBulkCopyOptions.UseInternalTransaction,
                null
                );


                bulkCopy.DestinationTableName = Tablename;
                bulkCopy.BulkCopyTimeout = executionTimeOut;

                do
                {
                    try
                    {

                        connect_db.Open();

                        bulkCopy.WriteToServer(_table);

                        connect_db.Close();

                        numberOfRetryAttemmpts = 0;

                    }
                    
                    catch (Exception e)
                    {

                       Thread.Sleep(100 * 100);

                        numberOfRetryAttemmpts -= 1;

                        connect_db.Close();

                        if (numberOfRetryAttemmpts == 0)
                        {
                            LogFile.SaveErrorLog($"Problem with connection to database. Error Message: " + e.Message);
                            throw;
                        }



                    }
                } while (numberOfRetryAttemmpts > 0);

            }

            LogFile.SaveLogInsert(_table.TableName, _table.Rows.Count);

            _table.Clear();



        }




        public void DeleteFromDB(string tablename,   string Store_Number, string First_Business_Date, string Last_Business_Date)
        {
            using (SqlConnection connect_db = new SqlConnection(MyGlobals.DatabaseConnectionStr))
            {

                try
                {


                    connect_db.Open();


                    string sqlCommandText = "DELETE FROM " + tablename + " WHERE " + "Store_Number = '" + Store_Number + "'" + " AND First_Business_Date = '" + First_Business_Date + "'";

                    sqlCommandText += " AND  Last_Business_Date ='" + Last_Business_Date + "'";


                    using (SqlCommand cmd = new SqlCommand(sqlCommandText, connect_db))
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = executionTimeOut;

                        numberOfDeletedRows = cmd.ExecuteNonQuery();

                    }

                    LogFile.SaveLogDelete(numberOfDeletedRows, tablename);

                }

                catch(Exception e)
                {
                    LogFile.SaveErrorLog($"Problem with connection to database. Error Message: " + e.Message);
                    throw;
                }
               

            }

        }

        public void EmptyStagingTables()
        {
            ExecuteStoredProcedure("dbo.sp_UBER_EATS_REMOVE_DATA_FROM_STAGING");
        }


        public void MoveDataFromStaging()
        {
               ExecuteStoredProcedure("dbo.sp_UBER_EATS_MOVE_DATA_FROM_STAGING");
        }

 

        public void ExecuteStoredProcedure(string procedureName)
        {

            try
            {
                using (SqlConnection connect_db = new SqlConnection(MyGlobals.DatabaseConnectionStr))
                {
                    
                    using (var cmd = new SqlCommand(procedureName, connect_db)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        connect_db.Open();
                        cmd.CommandTimeout = executionTimeOut;
                        cmd.ExecuteNonQuery();
                    }


                }
            }

            catch (Exception e)
            {
                LogFile.SaveErrorLog($"Problem with connection to database. Error Message: " + e.Message);
                throw;
            }


        }


       



    }


}
