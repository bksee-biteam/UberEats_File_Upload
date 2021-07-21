using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using UberEats_Upload.DataClasses;


namespace UberEats_Upload
{
    class UberEatsSalesFile
    {
        public string fullFilePath { get; }
        public string fileName { get; }


        private IEnumerable<string> FileData;
        public bool fileIgnored = false;

        private bool reportMissingSpecialOfferFields;



        private List<UberEatsData> uberDataTableList = new List<UberEatsData>() { new uberDefault(), new uberFranceBelgium() , new UberEuropeMiddleEastAfrica()};



        public UberEatsSalesFile(FileInfo fi) : base()
        {
            fullFilePath = fi.FullName;
            fileName = fi.Name;

            fileIgnored =  fi.Name.Contains("france_belgium") || fi.Name.Contains("default") || fi.Name.Contains("europe_middle_east_africa") ? false : true;

        }


        public void ProcessFile()
        {

            string TargetTableName;
            List<string> data_row;
            List<string> row_header;

 

            FileData = File.ReadLines(this.fullFilePath, Encoding.GetEncoding(28591));
            
          

            var line_header = FileData.First();

            row_header = getDataRow(line_header);

          


            TargetTableName = SpecifyTableNameBasedOnFileName(fileName);

            if (TargetTableName == "UBER_EATS_FRANCE_BELGIUM")
            {
                reportMissingSpecialOfferFields = ReportMissingSpecialOfferField(row_header);
            }



            foreach (string item in FileData.Skip(1)) //skip Header
            {
                data_row = getDataRow(item);



                processDataRow(data_row, reportMissingSpecialOfferFields);
                AddDataRowToTable(data_row, TargetTableName);

            }

          saveToDb();



        }


        private bool ReportMissingSpecialOfferField (List<string> rowHeaderFields)
        {
            return ! rowHeaderFields.Any(s => s.Contains("Special Offer on Delivery (excl VAT)")); 
        }


        private string SpecifyTableNameBasedOnFirstField (string firstFieldFromHeader)
        {

            switch( firstFieldFromHeader)
            {
                case "Store Name":
                    return "UBER_EATS_DEFAULT";
                case "Order ID":
                    return "UBER_EATS_FRANCE_BELGIUM";
                default:
                    return "TABLE_NOT_SPECIFIED";
            }

        }


        private string SpecifyTableNameBasedOnFileName(string fileName)
        {

            switch (fileName)
            {
                case string a when a.Contains("-default"):
                    return "UBER_EATS_DEFAULT"; 
                
                case string b when b.Contains("-france_belgium"):
                return "UBER_EATS_FRANCE_BELGIUM";

                case string c when c.Contains("-europe_middle_east_africa"):
                    return "UBER_EATS_EUROPE_MIDDLE_EAST_AFRICA";

                default:
                    return "TABLE_NOT_SPECIFIED";
            }

        }


        private void AddDataRowToTable(List<string> datarow_, string table_name_)
        {


            UberEatsData o_data = uberDataTableList.Find(x => x.table.TableName == "STAGING_" + table_name_);


            try
            {

                if (!(o_data is null))
                {
                    o_data.AddRow(datarow_);
                }
                else
                {
                    LogFile.SaveAppLog($"Table {table_name_} is not currentrly supported.");
                }


            }

            catch(Exception e)
            {
                LogFile.SaveErrorLog(String.Format($"Row cannot be added to table: \"{table_name_}\" .File name:  \"{new FileInfo(fullFilePath).Name}\". Row data: {String.Join("|", datarow_)}"));
            }


        }


        private void saveToDb()
        {
            

            foreach (UberEatsData uberData in uberDataTableList)
            {

                if (uberData.table.Rows.Count > 0)
                {
                    uberData.SaveToDb();

                }

            }

        }







        private List<string> getDataRow(string row) {

            List<string> tmpRow = new List<string>();

            int i = 1;
            foreach (var item in row.Split(','))
            {

                if (i < 170) 
                {
                    if(item.Trim() == string.Empty)
                    {
                     tmpRow.Add(null);
                    }
                  else
                    tmpRow.Add(item.Trim());
                }
                i++;
            }
            return tmpRow;
        }




        private List<string> processDataRow (List<string> data_row_from_file,bool reportMissingSpecialOffer = false)
        {
            addFileNameToRow(data_row_from_file);


            if (reportMissingSpecialOffer)
            { addEmptyFieldsSpecialOffer(data_row_from_file); }


            return data_row_from_file;
        }



        private void addEmptyFieldsSpecialOffer(List<string> data_row_from_file)
        {
            //Report without Speciail offer, 27,28,29 - 0 default values!
 
            data_row_from_file.Insert(27, "0");
            data_row_from_file.Insert(28, "0");
            data_row_from_file.Insert(29, "0");
        }




        private List<string> addFileNameToRow(List<string> data_row_from_file)
        {

            data_row_from_file.Add(new FileInfo(fullFilePath).Name);

            return data_row_from_file;
        }


    }
}
