using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace UberEats_Upload
{


    abstract class UberEatsData : DataBaseOperator
    {

        public List<int> dateColumnsSet = new List<int>();
        public List<int> dateTimeColumnsSet = new List<int>();
        public List<int> timeColumnsSet = new List<int>();
        public DataTable table = new DataTable();


        public void AddRow(List<string> column_values)
        {

            convertDateColumns(column_values);
            convertDateTimeColumns(column_values);
            convertTimeColumns(column_values);

            table.Rows.Add(column_values.ToArray());

        }


        public void SaveToDb()
        {
            Insert(table, table.TableName);
        }

        public void DeleteExistingDataFromDb (string Store_Number, string First_Business_Date, string Last_Business_Date)
        {
            DeleteFromDB(table.TableName,Store_Number, First_Business_Date, Last_Business_Date);
        }

        //public bool checkIfNewerDataNotExistsInDb(string store_number, string firstBusinessDate, string fileDateCreated, string fileTimeCreated)
        //{
        //    return ! CheckIfNewerDataExists( store_number,  firstBusinessDate,  fileDateCreated,  fileTimeCreated);
        //}



        public void convertDateColumns(List<string> column_values)
        {
            if (dateColumnsSet != null)
                for (int i = 0; i <= dateColumnsSet.Count - 1; i++)
                {

                    string column_val = (column_values[dateColumnsSet[i]]);

                    if (column_val != null)
                        column_values[dateColumnsSet[i]] = DateConverter.getDateFromIntegerAndSaveToString(Convert.ToInt64(column_val));
                }
        }




        public void convertDateTimeColumns(List<string> column_values)
        {
            if (dateTimeColumnsSet != null)
                for (int i = 0; i <= dateTimeColumnsSet.Count - 1; i++)
                {

                    string column_val = (column_values[dateTimeColumnsSet[i]]);

                    if (column_val != null)
                        column_values[dateTimeColumnsSet[i]] = DateConverter.getDateTimeFromIntegerAndSaveToString(Convert.ToInt64(column_val));
                }
        }


        public void convertTimeColumns (List<string> column_values)
        {

            if (timeColumnsSet != null)
                for (int i = 0; i <= timeColumnsSet.Count - 1; i++)
                {
                    DateConverter.formatStringToValidTime(column_values[timeColumnsSet[i]]);
                }
            
        }

    }
}
