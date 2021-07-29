using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClassRA
{
    public class Logger
    {
        public static void WriteLog(Exception ex)
        {
            string msg = $@"DateTime.Now";
            System.IO.File.AppendAllText("C:\\Log\\log.log", ex.ToString());
            
        }
    }
}
