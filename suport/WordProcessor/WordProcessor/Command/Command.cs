using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Control;
using WordProcessor.Designer;

namespace WordProcessor.Command
{
    /// <remarks>
    /// 命令接口
    /// </remarks>
    public abstract class Command
    {
        protected MDocument MDocument;

        public virtual void Execute()
        {
            MDocument = WEditorView.ViewMapDocument.First().Value;
            //MDocument = MDocument.Context.CurrentDocument;
        }
    }

    public enum WCommandType
    {
        FontSet,
        FontColorSet,
        NewDocument,
        NewTextInput,
        NewTable,
        NewCheckBox,
        NewComboBox,
        NewHorizonLine,
        NewImage,
        XmlImport,
        XmlExport,
        ParagraphLeft,
        ParagraphMiddle,
        ParagraphRight,
        Null
    }

}
