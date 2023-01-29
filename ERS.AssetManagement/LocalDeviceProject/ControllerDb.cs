using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDeviceProject
{
    public class ControllerDb
    {

        public string AbsolutePath { get; set; }

        public ControllerDb() 
        {
            string path = "data.xml";
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            AbsolutePath = Path.Combine(dir, path);
        }


    }
}
