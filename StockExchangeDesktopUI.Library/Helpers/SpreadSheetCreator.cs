using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchangeDesktopUI.Library.Helpers
{
    public static class SpreadSheetCreator
    {
        public static void Create<T>(string fileName, IEnumerable<T> dataIntoSpreadsheet)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                var json = JsonConvert.SerializeObject(dataIntoSpreadsheet);
                DataTable dataTable = (DataTable)JsonConvert.DeserializeObject (json, typeof(DataTable));

                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                SheetData sd = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sd);


                Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());

                Sheet sheet = new Sheet()
                {
                    Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Report Sheet"
                };

                sheets.Append(sheet);

                Row excelTitleRow = new Row();

                foreach (DataColumn column in dataTable.Columns)
                {
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue(column.ColumnName);
                    excelTitleRow.Append(cell);
                }

                sd.AppendChild<Row>(excelTitleRow);

                foreach (DataRow row in dataTable.Rows)
                {
                    Row newRow = new Row();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        Cell cell = new Cell();
                        var a = row.GetType();

                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(row[column.ColumnName].ToString());
                        newRow.AppendChild(cell);
                    }
                    sd.AppendChild(newRow);
                }
                workbookPart.Workbook.Save();

            }
        }
    }
}
