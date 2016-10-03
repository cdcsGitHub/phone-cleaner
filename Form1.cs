using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace TrainingExercise1
{
    /// <summary>
    /// The class Form1 displays the form for the user to 
    /// interact with, and also performs the process to 
    /// manipulate the data.
    /// </summary>
    public partial class Form1 : Form
    {
        FileManip files = new FileManip();
        SaveFileDialog saveFD = new SaveFileDialog();
        OpenFileDialog openFD = new OpenFileDialog();

        /// <summary>
        /// Initializes the windows form.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ReadAndParseData takes a csv file and converts its contents
        /// into a datatable to be used for manipulation.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        private static DataTable ReadAndParseData(String path, Char seperator)
        {

            // 
            // Instead of using a List of string arrays, a more applicable 
            // .NET object is the DataTable. It is an in-memory representation
            // of something similar to an excel table.
            //
            // We load it in much the same way you load up your list of string arrays.
            //

            //
            // DataTable lives in the System.Data namespace of the .NET Framework
            //
            DataTable dt;
            dt = new DataTable();
            
            // Setup the columns
            for (int index = 0; index < 20; index++)
            {
                // Create a column object, name it uniquely, and make it a String
                DataColumn col = new DataColumn("Col" + index.ToString("D"), System.Type.GetType("System.String"));
                // Add it to the collection of columns in the DataTable
                dt.Columns.Add(col);
            }

            // Fill the DataTable with rows of data
            //
            using (StreamReader sr = new StreamReader(path))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    // The NewRow() method on the DataTable object will create a new DataRow object
                    // that has the same number of columns as the DataTable does. This eliminates
                    // the need to re-define the columns for each row.
                    //
                    DataRow dataRow = dt.NewRow();
                    String[] rowValues = line.Split(seperator);
                    int index = 0;
                    foreach (String colValue in rowValues)
                    {
                        // The DataRow object has a "SetField" method that allows you to use a 
                        // strongly-typed statement (ie: specify the type of object you want the
                        // field to be treated as) to assign values to it.
                        //
                        // In this case, we are telling SetField() that the value will be of type String
                        // and that the name of the field is (for example) Col2, and the value
                        // is in the String variable "colValue".
                        //
                        dataRow.SetField<String>("Col" + index.ToString("D"), colValue);
                        index++;
                    }
                    // Don't forget to add the row to the DataTable after filling it in with values
                    dt.Rows.Add(dataRow);
                }
            }
            return dt;
        }

        /// <summary>
        /// Displays a string array into a data grid view format.
        /// </summary>
        /// <param name="parsedData"></param>
        private void DrawDataGridView(List<string[]> parsedData)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 18;
            for (int i = 0; i < 18; i++)
            {
                var sb = new StringBuilder(parsedData[0][i]);
                sb.Replace('_', ' ');
                sb.Replace("\"", "");
                dataGridView1.Columns[i].Name = sb.ToString();
            }
            foreach (string[] row in parsedData)
            {
                dataGridView1.Rows.Add(row);
            }
            dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
        }

        /// <summary>
        /// The code for the button that the user clicks on to begin the process
        /// followed by a openfileDialog and savefileDialog to choose file to 
        /// manipulate and where to save the changed file.
        /// First displays a MessageBox with instruction, then calls upon method
        /// ReadandParse data to access and change field values. 
        /// The field values are changed by methods in the DataProcess class
        /// and written to save file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1OriginalValues_Click(object sender, EventArgs e)
        {
            const string message = ("\t\tWelcome to Clay's CSV parser!\n" + "Please select the file you wish to upload and enter the save location when prompted.");
            MessageBox.Show(message, "Welcome", MessageBoxButtons.OK);

            string directoryPath = files.uploadPath();
            
            // Calling ReadAndParseData gives us back a DataTable filled with columns and rows from the csv file
            DataTable dt = ReadAndParseData(directoryPath, ';');
            bool first = true;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (first)
                {
                    first = false;
                    continue;
                }

                DataProcess d = new DataProcess();
                DataProcess d1 = new DataProcess();
                DataProcess d2 = new DataProcess();

                string Col10 = d.PhoneNumberClean(dataRow.Field<String>("Col10"));
                string Col11 = d1.PhoneNumberClean(dataRow.Field<String>("Col11"));
                string Col12 = d.extCreator(Col11, dataRow.Field<String>("Col12"));

                dataRow.SetField<String>("Col10", Col10);
                dataRow.SetField<String>("Col11", Col11);
                dataRow.SetField<String>("Col12", Col12);
            }

            string savePath = files.outputPath();
            using (StreamWriter sw = new StreamWriter(savePath))
            {
                int colCount = dt.Columns.Count;
                foreach (DataRow dataRows in dt.Rows)
                {
                    for (int i = 0; i < colCount; i++)
                    {
                        sw.Write(dataRows[i]);
                        if (i < colCount - 1)
                        {
                            sw.Write(";");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
            }
            DataTable dts = ReadAndParseData(savePath, ';');

            dataGridView1.DataSource = dts;
        }
    }
}
