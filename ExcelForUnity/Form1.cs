using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using ExcelDataReader;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace ExcelForUnity
{
    public partial class Form1 : Form
    {
        FileStream fileStream;
        bool isFileLoaded;

        DataSet result;
        
        Dictionary<int, Type[,]> cellTypes;

        string path;

        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xls;*.xlsx", ValidateNames = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        fileStream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read); //Dosya Kullanımdaysa hata veriyor..
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    isFileLoaded = true;
                    LabelFileName.Text = ofd.SafeFileName;
                    path = ofd.FileName.Replace(ofd.SafeFileName, "");
                    IExcelDataReader reader = ExcelReaderFactory.CreateReader(fileStream);
                    result = reader.AsDataSet();

                    cellTypes = new Dictionary<int, Type[,]>();

                    for (int workSheetIndex = 0; workSheetIndex < reader.ResultsCount; workSheetIndex++)
                    {
                        Type[,] types = new Type[reader.RowCount, reader.FieldCount];
                        int rowIndex = 0;
                        while (reader.Read())
                        {
                            for (int columnIndex = 0; columnIndex <= reader.FieldCount - 1; columnIndex++)
                            {
                                types[rowIndex, columnIndex] = reader.GetFieldType(columnIndex);
                            }
                            rowIndex++;
                        }
                        cellTypes.Add(workSheetIndex, types);
                        reader.NextResult();
                    }

                    reader.Close();
                    reader.Dispose();
                    reader = null;
                }
            }
        }

        private void ButtonConvertToJson_Click(object sender, EventArgs e)
        {
            if (isFileLoaded == false)
                return;

            string TEXTJSON = "{\r\n";

            string comma;
            List<string> headers;
            string items = "";

            for (int tableIndex = 0; tableIndex < result.Tables.Count; tableIndex++)
            {
                TEXTJSON += "\t\"" + result.Tables[tableIndex].TableName + "\": [\r\n";

                var table = result.Tables[tableIndex];

                headers = new List<string>();

                for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                {
                    headers.Add("\"" + table.Rows[0][columnIndex] + "\": ");
                }

                items = "";

                for (int rowIndex = 1; rowIndex < table.Rows.Count; rowIndex++)
                {
                    items += "\t\t{\r\n";
                    for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                    {
                        comma = "";
                        if (columnIndex < table.Columns.Count - 1)
                        {
                            comma += ",";
                        }
                        string text = "\t\t\t" + headers[columnIndex] + GetFixedType(tableIndex, table, rowIndex, columnIndex) + comma + "\r\n";
                        items += text;
                    }
                    comma = "";
                    if (rowIndex < table.Rows.Count - 1)
                    {
                        comma += ",";
                    }
                    items += "\t\t}" + comma + "\r\n";
                }

                comma = "";
                if (tableIndex < result.Tables.Count - 1)
                {
                    comma += ",";
                }
                TEXTJSON += items + "\t]" + comma + "\r\n";
            }

            TEXTJSON += "}";

            string fileName = path + "/JSonData.txt";

            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                fs.Close();
                File.AppendAllText(fileName, Environment.NewLine + TEXTJSON);
                labelJSonConverted.Text = "Converted!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetFixedType(int tableIndex, DataTable table, int rowIndex, int columnIndex)
        {
            string text = "";
            Type type = cellTypes[tableIndex][rowIndex, columnIndex];

            string target = "" + table.Rows[rowIndex][columnIndex];

            if (type.Equals(typeof(System.String)))
            {
                if (target.Substring(target.Length - 1).Equals("f"))
                {
                    float v = 0;
                    if (float.TryParse(target.Substring(0, target.Length - 1), out v))
                    {
                        text = target.Substring(0, target.Length - 1);
                    }
                    else
                    {
                        text = "\"" + table.Rows[rowIndex][columnIndex] + "\"";
                    }
                }
                else
                {
                    text = "\"" + table.Rows[rowIndex][columnIndex] + "\"";
                }
            }
            if (type.Equals(typeof(System.Double)))
            {
                text = "" + table.Rows[rowIndex][columnIndex];
            }
            if (type.Equals(typeof(System.DateTime)))
            {
                text = "" + table.Rows[rowIndex][columnIndex];
            }

            return text;
        }

        private void ButtonConvertToCSharp_Click(object sender, EventArgs e)
        {
            if (isFileLoaded == false)
                return;

            string serializeText = "";
            if (CheckBoxIsSerializable.Checked)
            {
                serializeText = "[Serializable]\r\n";
            }

            PluralizationService pluralizationService = PluralizationService.CreateService(new CultureInfo("en-us"));

            string TEXTCSHARP = "";

            string items = "";

            for (int tableIndex = 0; tableIndex < result.Tables.Count; tableIndex++)
            {
                items = serializeText;
                items += "public class " + pluralizationService.Singularize("" + result.Tables[tableIndex].TableName) + "\r\n{";

                var table = result.Tables[tableIndex];

                for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                {
                    items += "\r\n\tpublic " + GetVariableType(tableIndex, table, columnIndex) + " " + table.Rows[0][columnIndex] + ";";
                }

                items += "\r\n}";
                TEXTCSHARP += items + "\r\n\r\n";
            }

            items = serializeText + "public class RootObject\r\n{\r\n";
            for (int tableIndex = 0; tableIndex < result.Tables.Count; tableIndex++)
            {
                items += "\tpublic List<" + pluralizationService.Singularize("" + result.Tables[tableIndex].TableName) + "> " + result.Tables[tableIndex].TableName + ";\r\n";
            }

            items += "}";
            TEXTCSHARP += items;

            string fileName = path + "/CSharpData.txt";

            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                fs.Close();
                File.AppendAllText(fileName, Environment.NewLine + TEXTCSHARP);
                labelCSharpConverted.Text = "Converted!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private string GetVariableType(int tableIndex, DataTable table, int columnIndex)
        {
            Type type = cellTypes[tableIndex][1, columnIndex];

            if (table.Rows[1][columnIndex] == null)
                return "object";

            string target = "" + table.Rows[1][columnIndex];

            if (type.Equals(typeof(System.String)))
            {
                if (target.Substring(target.Length - 1).Equals("f"))
                {
                    float v = 0;
                    if (float.TryParse(target.Substring(0, target.Length - 1), out v))
                    {
                        return "float";
                    }
                    else
                    {
                        return "string";
                    }
                }
                else
                {
                    return "string";
                }
            }
            if (type.Equals(typeof(System.Double)))
            {
                return "int";
            }
            if (type.Equals(typeof(System.DateTime)))
            {
                return "DateTime";
            }

            return "Object";
        }
    }
}