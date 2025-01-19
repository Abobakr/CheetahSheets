using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Web;
using t = System.Type;

namespace ExcelonLibrary
{
    public class Excelon
    {
        static Application excelApp = new Application() { Visible = false, DisplayAlerts = false, EnableSound = false };

        static char[] toBeTrimmed = File.ReadAllText(HttpRuntime.AppDomainAppPath + @"\App_Data\toBeTrimmed.txt").ToCharArray();
        static public string saveChangesToExcel(string json, string mainFile, string sharefile)
        {
            Workbook workbook = null;
            try
            {
                SheetsChanges wbChanges = JsonConvert.DeserializeObject<SheetsChanges>(json);
                workbook = excelApp.Workbooks.Open(mainFile);
                foreach (var change in wbChanges.changes)
                {
                    var sheet = workbook.Sheets.Item[change.name];
                    string value = change.val.Trim(toBeTrimmed);
                    sheet.Cells[change.row + 1, change.col + 1] = value;
                }
                workbook.SaveAs(sharefile, XlFileFormat.xlWorkbookDefault, t.Missing, t.Missing, t.Missing, t.Missing, XlSaveAsAccessMode.xlNoChange, t.Missing, t.Missing, t.Missing);
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
            finally
            {
                workbook?.Close();
                excelApp.Quit();
            }
            return "OK";
        }

        static public string saveChangesToPDF(string json, string mainFile, string sheetName)
        {
            Workbook workbook = null;
            try
            {
                SheetsChanges wbChanges = JsonConvert.DeserializeObject<SheetsChanges>(json);
                workbook = excelApp.Workbooks.Open(mainFile);
                foreach (var change in wbChanges.changes)
                {
                    var sheet = workbook.Sheets.Item[change.name];
                    string value = change.val.Trim(toBeTrimmed);
                    sheet.Cells[change.row + 1, change.col + 1] = value;
                }

                Worksheet worksheet = workbook.Sheets.Item[sheetName];
                string filePath = @"\PDF\" + sheetName;
                worksheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, HttpRuntime.AppDomainAppPath + filePath, XlFixedFormatQuality.xlQualityStandard, true, false, t.Missing, t.Missing, false);
                return filePath + ".pdf";
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
            finally
            {
                workbook?.Close();
                excelApp.Quit();
            }
        }

        static public void updateEwaTitle(string Title, string ewaPath)
        {
            string[] lines = File.ReadAllLines(ewaPath);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("<title>"))
                {
                    lines[i] = "<title>" + Title + "</title>";
                    File.WriteAllLines(ewaPath, lines);
                    return;
                }
            }
        }

    }


    class SheetsChanges
    {
        public ChangedCell[] changes;
    }
    class ChangedCell
    {
        public string name;
        public byte row;
        public byte col;
        public string val;
    }
}
