using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessor.Command
{
    class CNewDocument : Command
    {
        public override void Execute()
        {
            base.Execute();
            MDocument = MDocument.NewDocument();
        }
    }
}
