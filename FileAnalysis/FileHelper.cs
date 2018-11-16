using System;
using System.Collections.Generic;
using System.IO;

namespace FileAnalysis
{
    public class FileHelper
    {
        private static List<FileInfo> lst = new List<FileInfo>();
        public static List<FileInfo> getFile(string path, string extName)
        {
             getdir(path, extName);
             return lst;
         }
        private static void getdir(string path, string extName)
         {
             try
             {
                 string[] dir = Directory.GetDirectories(path); //文件夹列表   
                 DirectoryInfo fdir = new DirectoryInfo(path);
                 FileInfo[] file = fdir.GetFiles();
                 //FileInfo[] file = Directory.GetFiles(path); //文件列表   
                 if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空                   
                 {
                     foreach (FileInfo f in file) //显示当前目录所有文件   
                     {
                         if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                         {
                             lst.Add(f);
                         }
                     }
                     foreach (string d in dir)
                     {
                         getdir(d, extName);//递归   
                     }
                 }
             }
             catch (Exception ex)
             {
                 //LogHelper.WriteLog(ex);
                 throw ex;
             }
         }
}
}
