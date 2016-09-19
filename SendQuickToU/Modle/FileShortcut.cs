using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendQuickToU.Modle
{
    class FileShortcut
    {
        private string rightName;
        private string rightNameAlias;
        private string fileType;
        private string paraType;

        public FileShortcut(string RightName,string RightNameAlias,string FileType,string ParaType)
        {
            this.rightName = RightName;
            this.rightNameAlias = RightNameAlias;
            this.fileType = FileType;
            this.ParaType = ParaType;
        }

        public string RightName
        {
            get
            {
                return rightName;
            }

            set
            {
                rightName = value;
            }
        }

        public string RightNameAlias
        {
            get
            {
                return rightNameAlias;
            }

            set
            {
                rightNameAlias = value;
            }
        }

        public string FileType
        {
            get
            {
                return fileType;
            }

            set
            {
                fileType = value;
            }
        }

        public string ParaType
        {
            get
            {
                return paraType;
            }

            set
            {
                paraType = value;
            }
        }
    }
}
