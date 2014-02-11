using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WordProcessor.Control;
using WordProcessor.Designer;
using WordProcessor.Painter;

namespace WordProcessor.Dom
{
    /// <remarks>
    /// 表格
    /// </remarks>
    public class DTable : DMember
    {
        public readonly List<WtRow> Rows = new List<WtRow>();
        public readonly List<WtCell> SelectedCells = new List<WtCell>();
        public MDocument OwnerDocument;
        public WtRow CurrentRow;
        private WtRow _dragRow;
        private static readonly Pen MPen = new Pen(Color.Black, 1);
        private bool _isInit;
        private int _paintCount = -1;
        public readonly DTableSelectIcon TableSelectIcon = new DTableSelectIcon();

        public DTable()
        {
            MType = MemberType.Table;
            MPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            MPen.DashPattern = new float[] { 3, 2 };
        }

        public DTable(int rowCount, int colCount)
        {
            MType = MemberType.Table;
            if (rowCount == 0) rowCount = 1;
            if (colCount == 0) colCount = 1;
            //Width = EditorSetting.DefaultTableRowWidth;
            MPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            MPen.DashPattern = new float[] { 3, 2 };
            CreateTable(rowCount, colCount);
        }

        public void CreateTable(int rowCount, int colCount)
        {
            CreateRows(rowCount, colCount);
            SetRowsRelation();
        }

        private void CreateRows(int rowCount, int colCount)
        {
            for (var i = 0; i < rowCount; i++)
            {
                var row = new WtRow() { OwnerTable = this };
                for (var j = 0; j < colCount; j++)
                {
                    var cell = row.CreateCells();
                    cell.Width = row.Width/colCount;
                    cell.Height = row.Height;
                    row.Add(cell);
                }
                Rows.Add(row);
                row.SetCellsRelation();
                //this.Height += row.Height;
            }
        }

        public void SetRowsRelation()
        {
            for (var i = 1; i < Rows.Count; i++)
            {
                Rows[i - 1].NextDocRow = Rows[i];
                Rows[i].PreDocRow = Rows[i - 1];
                Rows[i].SetCellsRelation();
            }
        }

        public WtRow LocateCurrentRow(int x, int y)
        {
            foreach (var wtRow in Rows)
            {
                if (x > wtRow.X && x <= wtRow.X + wtRow.Width
                    && y > wtRow.Y && y <= wtRow.Y + wtRow.Height)
                {
                    CurrentRow = wtRow;
                    wtRow.LocateCurrentCell(x, y);
                    break;
                }
            }
            return CurrentRow;
        }

        public void ContentChangeResize()
        {
            //var tempHeight = Rows.Sum(wtRow => wtRow.Height);
            //Height = tempHeight;
            OwnerDocument.Pagination();
        }

        public void CellDragResize()
        {
            //Height = 0;
            //Width = 0;
            foreach (var wtRow in Rows)
            {
                wtRow.CellDragResize();
                //Height += wtRow.Height;
                //var tempWidth = wtRow.Cells.Sum(wtCell => wtCell.Width);
                //if (Width < tempWidth) Width += tempWidth;
            }
        }

        public void RowDragResize()
        {
            //var tempHeight = Rows.Sum(wtRow => wtRow.Height);
            //Height = tempHeight;
            //OwnerDocRow.OwnerDocument.Pagination();
        }

        public XmlElement Convert2Xml(XmlDocument xmlDocument)
        {
            var tableElement = xmlDocument.CreateElement("table");
            tableElement.SetAttribute("width", Convert.ToString(Width));
            tableElement.SetAttribute("height", Convert.ToString(Height));

            foreach (var row in Rows)
            {
                var rowElement = xmlDocument.CreateElement("tRow");
                tableElement.AppendChild(rowElement);
                rowElement.SetAttribute("width", Convert.ToString(row.Width));
                rowElement.SetAttribute("height", Convert.ToString(row.Height));

                foreach (var cell in row.Cells)
                {
                    var cellElement = cell.Convert2Xml(xmlDocument);
                    rowElement.AppendChild(cellElement);
                }
            }
            return tableElement;
        }

        public DTable Xml2Object(XmlElement xmlElement)
        {
            if (!"table".Equals(xmlElement.Name)) return this;

            Width = Convert.ToInt32(xmlElement.GetAttribute("width"));
            Height = Convert.ToInt32(xmlElement.GetAttribute("height"));

            var rowElements = xmlElement.ChildNodes;
            foreach (XmlNode rowNode in rowElements)
            {
                if (!"tRow".Equals(rowNode.Name)) continue;
                var rowElement = (XmlElement) rowNode;
                var row = new WtRow
                    {
                        Width = Convert.ToInt32(rowElement.GetAttribute("width")),
                        Height = Convert.ToInt32(rowElement.GetAttribute("height")),
                        OwnerTable = this
                    };

                var cellElements = rowElement.ChildNodes;
                foreach (XmlNode cellNode in cellElements)
                {
                    if(!"tCell".Equals(cellNode.Name)) continue;
                    var cellElement = (XmlElement) cellNode;
                    var cell = new WtCell
                        {
                            OwnerTable = this,
                            OwnerRow = row,
                            OwnerDocument = {Context = OwnerDocument.Context}
                        };
                    cell.Xml2Object(cellElement);
                    row.Add(cell);
                }
                row.SetCellsRelation();
                Rows.Add(row);
            }
            SetRowsRelation();
            return this;
        }

        public void InsertRowCell(bool isUp)
        {
            if (CurrentRow == null) return;
            WtRow row = null;
            row = isUp ? InsertRowBefore(CurrentRow) : InsertRowAfter(CurrentRow);
            row.AddRange(CurrentRow.Cells);
            row.Replace(CurrentRow.Cells);
            ContentChangeResize();
            OwnerDocument.Context.Invalidate();
        }

        public void InsertColumnCell(bool isLeft)
        {
            if (CurrentRow == null || CurrentRow.CurrentCell == null) return;
            var currentCell = CurrentRow.CurrentCell;
            var range = currentCell.X + currentCell.Width;
            foreach (var wtRow in Rows)
            {
                foreach (var wtCell in wtRow.Cells.Where(wtCell => wtCell.X + wtCell.Width == range))
                {
                    WtCell cell = null;
                    if (isLeft)
                    {
                        cell = wtRow.InsertCellBefore(wtCell);
                    }
                    else
                    {
                        cell = wtRow.InsertCellAfter(wtCell);
                    }
                    cell.Height = wtCell.Height;
                    var tempWidth = wtRow.Width / wtRow.Cells.Count;
                    var lastWidth = wtRow.Width - tempWidth * (wtRow.Cells.Count - 1);
                    foreach (var tCell in wtRow.Cells)
                    {
                        tCell.Width = tempWidth;
                        if (tCell.NextCell == null) tCell.Width = lastWidth;
                    }
                    wtRow.SetCellProperty();
                    break;
                }
            }
            CellDragResize();
            OwnerDocument.Context.Invalidate();
        }

        public void MergeCell()
        {
            if (SelectedCells.Count == 0) return;

            var startCell = SelectedCells[0];

            if (!startCell.OwnerRows.Contains(startCell.OwnerRow))
            {
                startCell.OwnerRows.Add(startCell.OwnerRow);
            }

            foreach (var cell in SelectedCells)
            {
                if (startCell.Equals(cell)) continue;

                var cellOwnerRow = cell.OwnerRow;

                if (cell.X > startCell.X && cell.Y == startCell.Y)
                {
                    startCell.Width += cell.Width;
                    cellOwnerRow.Remove(cell);
                }
                else if (cell.Y > startCell.Y && cell.X == startCell.X)
                {
                    startCell.Height += cell.Height;
                    startCell.IsRowSpan = true;
                    if (!startCell.OwnerRows.Contains(cellOwnerRow))
                    {
                        startCell.OwnerRows.Add(cellOwnerRow);
                    }
                    cellOwnerRow.Replace(cell, startCell);
                }
                else
                {
                    cellOwnerRow.Remove(cell);
                }
            }
            MergeRow();
            OwnerDocument.Context.Invalidate();
        }

        private void MergeRow()
        {
            var tempRows = new List<WtRow>();
            for (var i = 0; i < Rows.Count; i++ )
            {
                if (i + 1 > Rows.Count - 1) break;
                if (Rows[i].Cells.Count == 1 && Rows[i + 1].Cells.Count == 1)
                {
                    Rows[i].Height += Rows[i + 1].Height;
                    tempRows.Add(Rows[i + 1]);
                }
            }

            foreach (var tempRow in tempRows)
            {
                RemoveRow(tempRow);
            }
        }

        public void SplitCell(int rowCount, int colCount)
        {
            if (rowCount == 1 && colCount == 1) return;
            if (CurrentRow == null || CurrentRow.CurrentCell == null) return;

            var cell = CurrentRow.CurrentCell;

            if (cell.IsRowSpan)
            {

            }
            else
            {
                var cells = cell.OwnerRow.Cells;

                if (rowCount > 1)
                {
                    //拆分行
                    var tRows = SplitRow(cell.OwnerRow, rowCount);

                    //除被拆分的单元格外，同行其他单元格因要放入拆分后的各行中，故跨行标志isRowSpan设置为真
                    foreach (var wtCell in cells)
                    {
                        if (wtCell.Equals(cell)) continue;
                        wtCell.IsRowSpan = true;
                        wtCell.Height = cell.Height*rowCount;
                    }

                    List<WtCell> replaceCells = null;

                    foreach (var wtRow in tRows)
                    {
                        if (wtRow.Equals(cell.OwnerRow))
                        {
                            replaceCells = SplitCell(rowCount, colCount, wtRow, cell);
                            continue;
                        }

                        //在拆分行中加入拆分单元格所在行的所有单元格
                        wtRow.AddRange(cells);
                        wtRow.Replace(replaceCells);
                    }
                }
                else
                {
                    SplitCell(rowCount, colCount, cell.OwnerRow, cell);
                }
            }
            ContentChangeResize();
            OwnerDocument.Context.Invalidate();
        }

        private List<WtCell> SplitCell(int rowCount, int colCount, WtRow wtRow, WtCell cell)
        {
            var tempWidth = cell.Width / colCount;
            var lastWidth = cell.Width - tempWidth*(colCount - 1);

            var cells = new List<WtCell>();
            WtCell tempCell = null;
            for (var i = 0; i < colCount; i++)
            {
                if (i == 0)
                {
                    cell.Width = tempWidth;
                    tempCell = cell;
                }
                else
                {
                    tempCell = wtRow.InsertCellAfter(tempCell);
                    tempCell.Width = i < colCount - 1 ? tempWidth : lastWidth;
                    tempCell.Height = cell.Height;
                }
                cells.Add(tempCell);
            }
            wtRow.SetCellProperty();
            return cells;
        }

        private IEnumerable<WtRow> SplitRow(WtRow tRow, int rowCount)
        {
            var rows = new List<WtRow> {tRow};
            for (var i = 1; i < rowCount; i++)
            {
                var row = InsertRowAfter(tRow);
                row.Width = tRow.Width;
                row.Height = tRow.Height;
                rows.Add(row);
            }
            return rows;
        }

        public WtRow InsertRowAfter(WtRow tRow)
        {
            var index = Rows.IndexOf(tRow);
            if (index < 0) return null;

            var row = new WtRow() {OwnerTable = this, Width = tRow.Width};

            Rows.Insert(index + 1, row);

            Rows[index].NextDocRow = row;
            row.PreDocRow = Rows[index];

            if (index + 2 < Rows.Count)
            {
                Rows[index + 2].PreDocRow = row;
                row.NextDocRow = Rows[index + 2];
            }

            return row;
        }

        public WtRow InsertRowBefore(WtRow tRow)
        {
            var index = Rows.IndexOf(tRow);
            if (index < 0) return null;

            var row = new WtRow() { OwnerTable = this, Width = tRow.Width };

            Rows.Insert(index, row);

            if (index - 1 >= 0)
            {
                Rows[index - 1].NextDocRow = row;
                row.PreDocRow = Rows[index - 1]; 
            }

            if (index + 1 < Rows.Count)
            {
                Rows[index + 1].PreDocRow = row;
                row.NextDocRow = Rows[index + 1];
            }

            return row;
        }

        public void RemoveRow(WtRow tRow)
        {
            if (!Rows.Contains(tRow)) return;

            if (tRow.PreDocRow != null)
            {
                tRow.PreDocRow.NextDocRow = tRow.NextDocRow;
            }

            if (tRow.NextDocRow != null)
            {
                tRow.NextDocRow.PreDocRow = tRow.PreDocRow;
            }

            Rows.Remove(tRow);
        }

        /// <summary>
        /// 起始点在表格外的选择
        /// </summary>
        public override void Select(bool isSelectAll=false)
        {
            if (isSelectAll)
            {
                foreach (var wtRow in Rows)
                {
                    wtRow.IsSelected = true;
                }
            }
            else
            {
                var context = OwnerDocument.Context;
                var endY = context.MouseCurrentPosition.Y;
                foreach (var wtRow in Rows.Where(wtRow => endY > wtRow.Y))
                {
                    wtRow.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// 起始点在表格内的选择
        /// </summary>
        public void InnerSelect(WtCell startCell, WtCell endCell)
        {
            if (startCell == null || endCell == null) return;

            SelectedCells.Clear();

            var startX = startCell.X > endCell.X ? endCell.X : startCell.X;
            var startY = startCell.Y > endCell.Y ? endCell.Y : startCell.Y;
            var xRange = Math.Abs(startCell.X - endCell.X);
            var yRange = Math.Abs(startCell.Y - endCell.Y);

            foreach (var wtRow in Rows)
            {
                if (wtRow.Y < startY || wtRow.Y > startY + yRange) continue;
                var cells = wtRow.Cells;
                foreach (var wtCell in cells.Where(wtCell => wtCell.X >= startX && wtCell.X <= startX + xRange))
                {
                    wtCell.IsSelected = true;
                    SelectedCells.Add(wtCell);
                }
            }
        }

        public override void ClearSelect()
        {
            foreach (var wtRow in Rows)
            {
                wtRow.IsSelected = false;
                foreach (var wtCell in wtRow.Cells)
                {
                    wtCell.IsSelected = false;
                    wtCell.OwnerDocument.ClearMyCellSelect();
                }
            }
        }

        public override bool TouchMe(int x, int y)
        {
            IsTouch = x >= this.X && x <= this.X + this.Width &&
                      y >= this.Y && y <= this.Y + this.Height;
            return IsTouch;
        }

        public bool IsInRange(int x, int y)
        {
            return x >= this.X && x <= this.X + this.Width && y >= this.Y && y <= this.Y + this.Height;
        }

        public WtCell GetRegionCell(int x, int y)
        {
            return (from wtRow in Rows where wtRow.IsInRange(x, y) select wtRow.GetRegionCell(x, y)).FirstOrDefault();
        }

        public void SetDocument(MDocument document)
        {
            OwnerDocument = document;
            foreach (var wtRow in Rows)
            {
                wtRow.OwnerDocument = document;
                wtRow.SetCellProperty();
            }
        }
    }

    /// <remarks>
    /// 表格行
    /// </remarks>
    public class WtRow : DDocRow
    {
        public readonly List<WtCell> Cells = new List<WtCell>();
        public DTable OwnerTable;
        public WtCell CurrentCell;

        private bool _isDragRowBorder;
        private bool _isDragColBorder;
        private Rectangle _rowBorderRectangle;
        private Rectangle _colBorderRectangle;
        private WtCell _dragCell;
        private const int SelectIconOffsetX = 12;
        private const int SelectIconOffsetY = 14;
        
        public delegate void DynamicPaint(Graphics graphics, int x1, int y1, int x2, int y2);
        public readonly Dictionary<Rectangle, DynamicPaint> DynamicPaints = new Dictionary<Rectangle, DynamicPaint>();

        private static readonly Pen MPen = new Pen(Color.Black, 1);

        public WtRow()
        {
            IsReadOnly = true;
            Width = EditorSetting.DefaultTableRowWidth;
            Height = EditorSetting.DefaultTableRowHeight;
            MPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            MPen.DashPattern = new float[] { 3, 2 };
        }

        public WtCell CreateCells()
        {
            var cell = new WtCell() {OwnerRow = this, OwnerTable = OwnerTable};
            Cells.Add(cell);
            return cell;
        } 

        public void AddRange(List<WtCell> cells)
        {
            Cells.AddRange(cells);
        }

        public void Add(WtCell cell)
        {
            if (Cells.Contains(cell)) return;
            cell.OwnerRow = this;
            cell.OwnerTable = this.OwnerTable;
            Cells.Add(cell);
            var count = Cells.Count;
            if (count > 1)
            {
                Cells[count - 2].NextCell = Cells[count - 1];
                Cells[count - 1].PreCell = Cells[count - 2];
            }
        }

        public void Remove(WtCell cell)
        {
            if (!Cells.Contains(cell)) return;

            if (cell.PreCell != null)
            {
                cell.PreCell.NextCell = cell.NextCell;
            }

            if (cell.NextCell != null)
            {
                cell.NextCell.PreCell = cell.PreCell;
            }

            cell.PreCell = null;
            cell.NextCell = null;

            Cells.Remove(cell);
        }

        public void Replace(WtCell removeCell, WtCell replaceCell)
        {
            var index = Cells.IndexOf(removeCell);
            if (index < 0) return;

            Remove(removeCell);
            Cells.Insert(index, replaceCell);

            if (index >= 1)
            {
                Cells[index - 1].NextCell = replaceCell;
                replaceCell.PreCell = Cells[index - 1];
            }

            if (index < Cells.Count - 1)
            {
                Cells[index + 1].PreCell = replaceCell;
                replaceCell.NextCell = Cells[index + 1];
            }
        }

        public void Replace(List<WtCell> tCells)
        {
            if (tCells == null) return;
            for (var i = 0; i < tCells.Count;i++ )
            {
                var tempCell = new WtCell()
                {
                    OwnerRow = this,
                    OwnerTable = this.OwnerTable,
                    Width = tCells[i].Width,
                    Height = tCells[i].Height
                };
                var index = Cells.IndexOf(tCells[i]);
                Cells.RemoveAt(index);
                Cells.Insert(index, tempCell);
            }
            SetCellsRelation();
            SetCellProperty();
        }

        public WtCell InsertCellAfter(WtCell beforeCell)
        {
            var index = Cells.IndexOf(beforeCell);
            if (index < 0) return null;

            var cell = new WtCell() {OwnerTable = this.OwnerTable,OwnerRow = this};

            Cells.Insert(index + 1, cell);

            Cells[index].NextCell = cell;
            cell.PreCell = Cells[index];

            if (index + 2 < Cells.Count)
            {
                Cells[index + 2].PreCell = cell;
                cell.NextCell = Cells[index + 2];
            }

            return cell;
        }

        public WtCell InsertCellBefore(WtCell afterCell)
        {
            var index = Cells.IndexOf(afterCell);
            if (index < 0) return null;

            var cell = new WtCell() { OwnerTable = this.OwnerTable, OwnerRow = this };

            Cells.Insert(index, cell);

            if (index - 1 >= 0)
            {
                Cells[index - 1].NextCell = cell;
                cell.PreCell = Cells[index - 1];
            }

            if (index + 1 < Cells.Count)
            {
                Cells[index + 1].PreCell = cell;
                cell.NextCell = Cells[index + 1];
            }

            return cell;
        }

        public bool IsEmpty()
        {
            return Cells.Count == 0;
        }

        public void SetCellsRelation()
        {
            for (var i = 1; i < Cells.Count; i++)
            {
                Cells[i - 1].NextCell = Cells[i];
                Cells[i].PreCell = Cells[i - 1];
            }
        }

        public override void Paint(Graphics graphics)
        {
            foreach (var t in Cells)
            {
                t.Paint(graphics);
            }

            //拖动时的效果
            if (_isDragRowBorder)
            {
                OwnerTable.Rows[OwnerTable.Rows.Count - 1].DynamicPaints.Add(new Rectangle(_rowBorderRectangle.X, _rowBorderRectangle.Y,
                              _rowBorderRectangle.X + _rowBorderRectangle.Width, _rowBorderRectangle.Y), PaintDragRowBorder);
            }
            else if (_isDragColBorder)
            {
                OwnerTable.Rows[OwnerTable.Rows.Count - 1].DynamicPaints.Add(
                    new Rectangle(_colBorderRectangle.X, _colBorderRectangle.Y,
                                  _colBorderRectangle.X,
                                  _colBorderRectangle.Y + _colBorderRectangle.Height), PaintDragCellBorder);
            }
            else if (OwnerTable.TableSelectIcon.Visible)
            {
                if (!OwnerTable.Rows[OwnerTable.Rows.Count - 1].DynamicPaints.ContainsKey(
                        new Rectangle(0, 0, 0, 0)))
                {
                    OwnerTable.Rows[OwnerTable.Rows.Count - 1].DynamicPaints.Add(
                        new Rectangle(0, 0, 0, 0),
                        PaintDrawClickIcon);
                }
            }

            if (DynamicPaints.Count > 0)
            {
                foreach (var dynamicPaint in DynamicPaints)
                {
                    dynamicPaint.Value(graphics, dynamicPaint.Key.X, dynamicPaint.Key.Y, dynamicPaint.Key.Width,
                                       dynamicPaint.Key.Height);
                }
                DynamicPaints.Clear();
            }
        }

        public void PaintDragRowBorder(Graphics graphics, int x1, int y1, int x2, int y2)
        {
            graphics.DrawLine(MPen, x1, y1, x2, y2);
        }

        public void PaintDragCellBorder(Graphics graphics, int x1, int y1, int x2, int y2)
        {
            graphics.DrawLine(MPen, x1, y1, x2, y2);
        }

        public void PaintDrawClickIcon(Graphics graphics, int x1, int y1, int x2, int y2)
        {
            graphics.DrawImage(OwnerTable.TableSelectIcon.SelectIcon, OwnerTable.TableSelectIcon.X, OwnerTable.TableSelectIcon.Y);
        }

        public override void Click(int x, int y)
        {
            if (!OwnerDocument.ClickMembers.Contains(OwnerTable))
            {
                OwnerDocument.ClickMembers.Add(OwnerTable);
            }
            OwnerTable.TableSelectIcon.X = OwnerTable.X - SelectIconOffsetX;
            OwnerTable.TableSelectIcon.Y = OwnerTable.Y - SelectIconOffsetY;
            OwnerTable.TableSelectIcon.Width = SelectIconOffsetX;
            OwnerTable.TableSelectIcon.Height = SelectIconOffsetY;
            OwnerTable.TableSelectIcon.Visible = true;
            OwnerDocument.Context.Invalidate();
            LocateCurrentCell(x, y);
        }

        public override void MoveIn(int x, int y)
        {
            OwnerDocument.TouchMember = OwnerTable;

            var flag = false;
            var context = OwnerDocument.Context;
            var cursorType = MConstant.CursorType.Ibeam;

            if (y >= this.Y + this.Height - 2 && y <= this.Y + this.Height)
            {
                cursorType = MConstant.CursorType.Hsplit;
                flag = true;
            }

            if (!flag && this.Cells.Any(wtCell => x >= wtCell.X + wtCell.Width - 2 && x <= wtCell.X + wtCell.Width))
            {
                cursorType = MConstant.CursorType.Vsplit;
            }

            context.ChangeCursor(cursorType);
        }

        private bool IsTouchRowBorder(int y)
        {
            return y >= this.Y + this.Height - 2 && y <= this.Y + this.Height;
        }

        private WtCell TouchCellBorder(int x)
        {
            return Cells.FirstOrDefault(wtCell => x >= wtCell.X + wtCell.Width - 2 && x <= wtCell.X + wtCell.Width);
        }

        public override void DragBegin(int x, int y)
        {
            var context = OwnerDocument.Context;
            var currentPage = ((EditorDocument) OwnerDocument).CurrentPage;
            if (!IsTouchRowBorder(y))
            {
                _dragCell = TouchCellBorder(x);
                if (_dragCell != null)
                {
                    //控制向左拖动的位置
                    if (context.MouseCurrentPosition.X <= _dragCell.X) return;
                    //控制向右拖动的位置
                    if ((_dragCell.NextCell != null &&
                         context.MouseCurrentPosition.X >= _dragCell.NextCell.X + _dragCell.NextCell.Width) ||
                        context.MouseCurrentPosition.X >
                        currentPage.X + currentPage.PaddingLeft + currentPage.BodyWidth) return;
                    //以下设置拖动虚线的坐标和大小
                    var page = ((EditorDocument)OwnerDocument).GetPage(_dragCell.X, _dragCell.Y);
                    _isDragColBorder = true;
                    _colBorderRectangle = new Rectangle(context.MouseCurrentPosition.X, page.Y + page.OffsetY, 1,
                                                        page.Height);
                }
                else
                {
                    var wtCell = LocateCurrentCell(x, y);
                    if (wtCell != null)
                    {
                        wtCell.OwnerDocument.Drag(x, y);
                    }
                }
            }
            else
            {
                //控制拖动的位置向上不超过该行的纵坐标
                if (context.MouseCurrentPosition.Y <= this.Y) return;
                //以下设置拖动虚线的坐标和大小
                var page = ((EditorDocument)OwnerDocument).GetPage(this.X, this.Y);
                _isDragRowBorder = true;
                _rowBorderRectangle = new Rectangle(page.X + page.OffsetX, context.MouseCurrentPosition.Y, page.Width, 1);
            }
            context.Invalidate();
        }

        public override void DragEnd()
        {
            //设置新行或新列的坐标和大小
            if (_isDragRowBorder)
            {
                this.Height = _rowBorderRectangle.Y - this.Y;
                this.DragResize();
            }
            else if (_isDragColBorder)
            {
                var range = _dragCell.X + _dragCell.Width;
                foreach (var wtRow in OwnerTable.Rows)
                {
                    foreach (var wtCell in wtRow.Cells.Where(wtCell => wtCell.X + wtCell.Width == range))
                    {
                        var tempWidth = 0;
                        var suitableWidth = 0;
                        //水平方向超出页面
                        if (IsOutContextHorizonal(wtCell, _colBorderRectangle.X, out suitableWidth))
                        {
                            tempWidth = suitableWidth;
                        }
                        else
                        {
                            tempWidth = _colBorderRectangle.X - wtCell.X;
                        }
                        if (wtCell.Width < WtCell.MinWidth) tempWidth = WtCell.MinWidth;
                        wtCell.Width = tempWidth;
                        wtCell.DragResize();
                    }
                }
            }
            _isDragRowBorder = _isDragColBorder = false;
            OwnerDocument.Context.Invalidate();
        }

        private bool IsOutContextHorizonal(WtCell wtCell, int dragWidth, out int suitableWidth)
        {
            suitableWidth = 0;
            if (wtCell.NextCell != null) return false;
            
            var page = ((EditorDocument)OwnerDocument).CurrentPage;
            var pageRange = page.X + page.PaddingLeft + page.BodyWidth;

            if (dragWidth > pageRange)
            {
                suitableWidth = pageRange - wtCell.X;
                return true;
            }

            var flag = false;
            var tempWidth = 0;
            var tempCell = wtCell.NextCell;

            while (tempCell != null)
            {
                dragWidth += tempCell.Width;
                if (dragWidth > pageRange)
                {
                    flag = true;
                }
                tempWidth += tempCell.Width;
                tempCell = tempCell.NextCell;
            }

            if (flag)
            {
                suitableWidth = pageRange - tempWidth - wtCell.X;
            }

            return flag;
        }

        public WtCell LocateCurrentCell(int x, int y)
        {
            var flag = false;
            foreach (var wtCell in Cells)
            {
                if (x <= wtCell.X && wtCell.PreCell == null)
                {
                    flag = true;
                }
                else if (x > wtCell.X + wtCell.Width && wtCell.NextCell == null)
                {
                    flag = true;
                }
                else if (x > wtCell.X && x <= wtCell.X + wtCell.Width)
                {
                    flag = true;
                }

                if (!flag) continue;
                CurrentCell = wtCell;
                wtCell.Locate(x, y);
                break;
            }
            return CurrentCell;
        }

        public void SetCellProperty()
        {
            foreach (var cell in Cells)
            {
                if(cell.OwnerDocument.Context != null) continue;
                cell.OwnerDocument.Context = OwnerDocument.Context;
                cell.OwnerDocument.CreateRow();
            }
        }

        public void ContentChangeResize()
        {
            Width = 0;
            Height = 0;
            foreach (var wtCell in Cells)
            {
                var cellHeight = wtCell.GetContentHeight();

                if (wtCell.IsRowSpan)
                {
                    var tempHeight = wtCell.Y + cellHeight - this.Y;
                    if (Height < tempHeight)
                    {
                        Height = tempHeight;
                    }
                    Width += wtCell.Width;
                    continue;
                }

                if (Height < cellHeight)
                {
                    if (cellHeight < wtCell.DragHeight)
                    {
                        Height = wtCell.DragHeight;
                    }
                    else
                    {
                        Height = cellHeight;
                    }
                }
                Width += wtCell.Width;
            }

            foreach (var wtCell in Cells)
            {
                if (wtCell.IsRowSpan)
                {
                    var tRow = wtCell.GetLastOwnerRow();
                    wtCell.Height = tRow.Y + tRow.Height - wtCell.Y;
                    continue;
                }
                wtCell.Height = Height;
            }

            OwnerTable.ContentChangeResize();
        }

        public void CellDragResize()
        {
            Width = 0;
            foreach (var wtCell in Cells)
            {
                Width += wtCell.Width;
            }
        }

        public void DragResize()
        {
            var minHeight = 0;
            //取出允许的最小高度
            foreach (var wtCell in Cells)
            {
                wtCell.DragHeight = this.Height;
                var contentHeight = wtCell.GetContentHeight();
                var tempHeight = contentHeight > wtCell.DragHeight ? contentHeight : wtCell.DragHeight;
                if (minHeight < tempHeight) minHeight = tempHeight;
            }

            this.Height = minHeight;

            foreach (var wtCell in Cells)
            {
                if (wtCell.IsRowSpan)
                {
                    var tRow = wtCell.GetLastOwnerRow();
                    var tempHeight = tRow.Y + tRow.Height - wtCell.Y;
                    wtCell.Height = tempHeight;
                    continue;
                }
                wtCell.Height = minHeight;
            }

            OwnerTable.RowDragResize();
            OwnerDocument.Pagination();
        }

        public bool IsInRange(int x, int y)
        {
            return x >= this.X && x <= this.X + this.Width && y >= this.Y && y <= this.Y + this.Height; 
        }

        public WtCell GetRegionCell(int x, int y)
        {
            return Cells.FirstOrDefault(wtCell => wtCell.IsInRange(x, y));
        }
    }

    /// <remarks>
    /// 表格单元格
    /// </remarks>
    public class WtCell
    {
        public DTable OwnerTable;
        public WtRow OwnerRow;
        public CellDocument OwnerDocument = new CellDocument();
        private readonly CellPainter _cellPainter = new CellPainter();
        public WtCell PreCell;
        public WtCell NextCell;

        public int X
        {
            get
            {
                if (PreCell == null)
                {
                    return OwnerRow.X;
                }
                return PreCell.X + PreCell.Width;
            }
        }

        public int Y
        {
            get
            {
                return OwnerRow.Y;
            }
        }

        public int Width;
        public int Height;
        public int PaddingLeft = 3;
        public int PaddingRight = 8;
        public int PaddingTop = 2;
        public int PaddingBottom;
        public int DragHeight;
        public const int MinWidth = 15;

        //具有rowSpan特性的单元格采用
        public readonly List<WtRow> OwnerRows = new List<WtRow>(); 

        public bool IsSelected;
        public bool IsRowSpan;

        public WtCell()
        {
            OwnerDocument.OwnerCell = this;
            _cellPainter.SetDocument(OwnerDocument);
        }

        public void Paint(Graphics graphics)
        {
            _cellPainter.Paint(graphics);
        }

        public void Locate(int x, int y)
        {
            OwnerRow.OwnerDocument.ChangeContextCurrentDoc(OwnerDocument);
            OwnerDocument.LocateCurrentRow(x, y);
        }

        public void ContentChangeResize()
        {
            var tempHeight = GetContentHeight();
            if (!IsRowSpan)
            {
                this.Height = tempHeight;
                OwnerRow.ContentChangeResize();
            }
            else
            {
                this.Height = tempHeight;
                this.GetLastOwnerRow().ContentChangeResize();
            }
        }

        public void DragResize()
        {
            ComposeContent();
            if (!IsRowSpan)
            {
                if (this.NextCell == null)
                {
                    OwnerTable.CellDragResize();
                    ContentChangeResize();
                }
                else
                {
                    this.NextCell.Width = OwnerRow.X + OwnerRow.Width - this.NextCell.GetNextAllCellWidth(0) - this.X -
                                          this.Width;
                    this.NextCell.ComposeContent();
                    this.NextCell.ContentChangeResize();
                }
            }
            else
            {
                //因合并而跨行的单元格，因为nextCell指向唯一的问题，所以只能根据行内部单元格数组确定
                for (var i = 0; i < this.OwnerRows.Count; i++)
                {
                    var index = OwnerRows[i].Cells.IndexOf(this);
                    if (index < 0) return;
                    if (index == OwnerRows[i].Cells.Count - 1)
                    {
                        OwnerTable.CellDragResize();
                        ContentChangeResize();
                    }
                    else
                    {
                        var nextCell = OwnerRows[i].Cells[index + 1];
                        nextCell.Width = OwnerRows[i].X + OwnerRows[i].Width - nextCell.GetNextAllCellWidth(0) - this.X -
                                              this.Width;
                        nextCell.ComposeContent();
                        nextCell.ContentChangeResize();
                    }
                }
            }
        }

        public int GetNextAllCellWidth(int width)
        {
            if (NextCell == null) return width;
            width += NextCell.Width;
            return NextCell.GetNextAllCellWidth(width);
        }

        public int GetContentHeight()
        {
            return this.PaddingTop + this.PaddingBottom + OwnerDocument.SumRowHeight();
        }

        public WtRow GetLastOwnerRow()
        {
            WtRow tRow = null;
            foreach (var wtRow in OwnerTable.Rows.Where(wtRow => wtRow.Cells.Any(this.Equals)))
            {
                tRow = wtRow;
            }
            return tRow;
        }

        public void ComposeContent()
        {
            OwnerDocument.ResetRowWidth();
            OwnerDocument.ComposeContent();
        }

        public XmlElement Convert2Xml(XmlDocument xmlDocument)
        {
            var elements = OwnerDocument.Convert2Xml(xmlDocument);

            var cellElement = xmlDocument.CreateElement("tCell");
            cellElement.SetAttribute("width", Convert.ToString(Width));
            cellElement.SetAttribute("height", Convert.ToString(Height));
            //cellElement.SetAttribute("backColor", System.Drawing.ColorTranslator.ToHtml(BackColor));

            foreach (XmlElement rowElement in elements)
            {
                cellElement.AppendChild(rowElement);
            }

            return cellElement;
        }

        public WtCell Xml2Object(XmlElement xmlElement)
        {
            if (!"tCell".Equals(xmlElement.Name)) return this;
            Width = Convert.ToInt32(xmlElement.GetAttribute("width"));
            Height = Convert.ToInt32(xmlElement.GetAttribute("height"));
            //BackColor = System.Drawing.ColorTranslator.FromHtml(xmlElement.GetAttribute("backColor"));

            OwnerDocument.Xml2Object(xmlElement);

            return this;
        }

        public bool IsInRange(int x, int y)
        {
            return x >= this.X && x <= this.X + this.Width && y >= this.Y && y <= this.Y + this.Height;
        }

    }

    public class DTableSelectIcon
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public bool Visible;
        public DTable OwnerTable;
        public readonly Image SelectIcon = MImage.TableSelect;

        public void Click()
        {
        }
    }

}
