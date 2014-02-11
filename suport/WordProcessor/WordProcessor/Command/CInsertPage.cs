using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessor.Command
{
    class CInsertPage : Command
    {
        public override void Execute()
        {
            MDocument.CreatePage();
        }
    }
}
