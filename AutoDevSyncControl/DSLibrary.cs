using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoDevSync
{
    class DSLibrary
    {

        public DSLibrary(String p)
        {
            FullFileName = p;
            IsLibrary = false;
            ProjectName = "";
            OutputFileName = "";
        }

        public DSLibrary(String fullName, bool isLibrary)
        {
            FullFileName = fullName;
            IsLibrary = isLibrary;
            ProjectName = "";
            OutputFileName = "";
        }

        public DSLibrary(String fullName, bool isLibrary, String projectName)
        {
            FullFileName = fullName;
            IsLibrary = isLibrary;
            ProjectName = projectName;
            OutputFileName = "";
        }

        public DSLibrary(String fullName, bool isLibrary, String projectName, String outputFileName)
        {
            FullFileName = fullName;
            IsLibrary = isLibrary;
            ProjectName = projectName;
            OutputFileName = outputFileName;
			/*
			
			sd sdjf sdlf sdlflsdlksdff
			 sd falsesd
			 fs
			 
			
			*/
        }

        public String FullFileName { get; set; }

        public bool IsLibrary { get; set; }

        public String ProjectName { get; set; }

        public String OutputFileName { get; set; }
    }
}
