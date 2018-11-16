using System;

namespace FileAnalysis
{
    public class FileCol
    {
        public FileCol(string strFileName, string strFilePath, DateTime dateCreateDate, DateTime dateAlterDate, long doubleFileSize)
        {
            ColFileName = strFileName;
            ColFilePath = strFilePath;
            ColCreateDate = dateCreateDate;
            ColAlterDate = dateAlterDate;
            ColFileSize = doubleFileSize;
        }
        public string ColFileName;
        public string ColFilePath;
        public DateTime ColCreateDate;
        public DateTime ColAlterDate;
        public long ColFileSize;
    }
}
