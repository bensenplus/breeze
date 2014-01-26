﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Waf.Foundation;
using Waf.InformationManager.Infrastructure.Interfaces.Applications;

namespace Test.InformationManager.Infrastructure.Modules.Applications.Services
{
    [Export(typeof(IShellService)), Export]
    public class MockShellService : Model, IShellService
    {
        private readonly List<ToolBarCommand> toolBarCommands;


        public MockShellService()
        {
            this.toolBarCommands = new List<ToolBarCommand>();
        }


        public IEnumerable<ToolBarCommand> ToolBarCommands { get { return toolBarCommands; } }
        
        public object ShellView { get; set; }
        
        public object ContentView { get; set; }


        public void AddToolBarCommands(IEnumerable<ToolBarCommand> commands)
        {
            toolBarCommands.AddRange(commands);
        }

        public void ClearToolBarCommands()
        {
            toolBarCommands.Clear();
        }
    }
}
