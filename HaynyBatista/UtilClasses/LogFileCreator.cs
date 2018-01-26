using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
namespace HaynyBatista.UtilClasses
{
    public class LogFileCreator
    {
        private string sLogFormat;
        private string sErrorTime;

        public LogFileCreator()
        {
            sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            sErrorTime = DateTime.Now.ToString("yyyyMMdd");

        }

        public void ErrorLog(string sPathName, string sErrMsg)
        {
            
            String LogPath = Path.Combine(sPathName, sErrorTime + ".txt");

            StreamWriter sw = new StreamWriter(LogPath, true);
            sw.WriteLine(sLogFormat + sErrMsg);
            sw.Flush();
            sw.Close();
        }
    }
}