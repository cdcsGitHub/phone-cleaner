using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrainingExercise1
{
    /// <summary>
    /// This class contains the methods that deal with 
    /// filenames and filepaths that are used by this
    /// program.
    /// </summary>
    class FileManip
    {
        SaveFileDialog saveFD = new SaveFileDialog();
        OpenFileDialog openFD = new OpenFileDialog();
        
        /// <summary>
        /// Returns the filepath and filename of the
        /// file to upload and manipulate in string
        /// format
        /// </summary>
        /// <returns></returns>
        public string uploadPath()
        {
            openFD.InitialDirectory = "C:";
            openFD.Filter = "CSV files| *.csv";
            bool tF = false;
            do
            {
                openFD.ShowDialog();
                if (openFD.CheckFileExists)
              
                    tF = true;
            }
            while (tF == false);
            string directoryPath = openFD.FileName;
            return directoryPath;
        }

        /// <summary>
        /// Returns the file name and path in 
        /// string format, of where to save the 
        /// manipulated file to.
        /// </summary>
        /// <returns></returns>
        public string outputPath()
        {
            saveFD.Filter = "CSV files| *.csv";
            saveFD.DefaultExt = "csv";
            saveFD.ShowDialog();
            string savePath = saveFD.FileName;
            return savePath;
        }

    }
}
