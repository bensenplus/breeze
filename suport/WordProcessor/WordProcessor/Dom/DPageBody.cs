using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.Designer;
using WordProcessor.Interface;

namespace WordProcessor.Dom
{
    public class DPageBody : DMember
    {
        public delegate void MouseMoveIn(int x, int y);
        public MouseMoveIn MouseEnter;
        public delegate void MouseMoveOut();
        public MouseMoveOut MouseLeave;

        public DPageBody PrePageBody;
        public DPageBody NextPageBody;
        public ICompatible Data;

        public override int X { get; set; }
        public override int Y { get; set; }
        public override int Width { get; set; }
        public override int Height { get; set; }

        public DPageBody(int width, int height)
        {
            IsEdit = true;
            Width = width;
            Height = height;
        }

        public void MoveIn(int x, int y)
        {
            if (MouseEnter == null) return;
            MouseEnter(x, y);
        }

        public void MoveOut()
        {
            if (MouseLeave == null) return;
            MouseLeave();
        }

        public void Resize(int x, int y)
        {
            if (Data == null) return;
            Data.Resize(x, y);
        }

        public void Locate(int x, int y)
        {
            if (Data == null || !IsEdit) return;
            Data.Locate(x, y);
        }

        public void SetEdit(bool isEdit)
        {
            var tempBody = GetTopPageBody();
            while (tempBody != null)
            {
                tempBody.IsEdit = isEdit;
                tempBody = tempBody.NextPageBody;
            }
        }

        public DPageBody GetTopPageBody()
        {
            var tempBody = PrePageBody;
            while (tempBody != null)
            {
                if (tempBody.PrePageBody == null) return tempBody;
                tempBody = tempBody.PrePageBody;
            }
            return this;
        }

        public bool IsInRange(int x, int y)
        {
            return y >= this.Y && y <= this.Y + this.Height;
        }
    }
}
