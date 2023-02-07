using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProLinkLib.NFS
{
    public class File : IObject
    {
        public string FileName;
        public long Size;
        public string CreationDate;
        public string ModifiedDate;
        public string LastAccessed;
    }
}
