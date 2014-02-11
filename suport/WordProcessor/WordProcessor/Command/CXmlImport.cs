using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessor.Command
{
    public class CXmlImport : Command
    {
        public override void Execute()
        {
            base.Execute();
            MDocument = MDocument.NewDocument();
            if (MDocument == null) return;
            MDocument.XmlImport();
        }
    }
}
