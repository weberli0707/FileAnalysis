using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAnalysis
{
   public class DirectoryHelper
    {
        private static List<DirectoryInfo> lst = new List<DirectoryInfo>();
        public static List<DirectoryInfo> getFile(string path)
        {
            DirectoryInfo fdir = new DirectoryInfo(path);
            GetDirectory(fdir);
            return lst;
        }
        private static void GetDirectory(DirectoryInfo dir)
        {
            try
            {
                foreach (DirectoryInfo dChild in dir.GetDirectories("*"))
                {
                    lst.Add(dChild);
                    if (dChild.GetDirectories("*").Length > 0)
                    {
                        GetDirectory(dChild);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
