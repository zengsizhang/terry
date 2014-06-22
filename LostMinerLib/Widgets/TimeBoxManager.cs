namespace TimeSearcher.Widgets
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Windows.Forms;
    using TimeSearcher;
    using TimeSearcher.Filters;
    using TimeSearcher.Panels;

    public class TimeBoxManager
    {
        private readonly DataSet _dataSet;
        private readonly ArrayList _selectedTimeBoxes;
        private readonly ArrayList _timeBoxes;
        private TimeBox tbOfPrevMouseCoord;

        public TimeBoxManager(DataSet dataSet)
        {
            this._dataSet = dataSet;
            this._timeBoxes = new ArrayList();
            this._selectedTimeBoxes = new ArrayList();
        }

        public void addTimeBox(TimeBox timeBox)
        {
            if (timeBox is DisablingEntity)
            {
                this._dataSet.DisablingManager.AddEntity((DisablingEntity) timeBox);
            }
            this._timeBoxes.Add(timeBox);
        }

        public void clearTimeBoxes()
        {
            while (this._timeBoxes.Count > 0)
            {
                this.removeTimeBox((TimeBox) this._timeBoxes[0]);
            }
        }

        public void commitMoveOnSelectedTBs(MouseEventArgs mea)
        {
            foreach (TimeBox box in this._selectedTimeBoxes)
            {
                box.commitMove();
                box.onMouseUp(mea);
                this.removeTimeBox(box);
                this.addTimeBox(box);
            }
        }

        public void deselectAll()
        {
            this._selectedTimeBoxes.Clear();
        }

        public void get_curr_loc(QueryPanel hostPanel, Point prevMousePosition, MouseEventArgs mea)
        {
            TimeBox box = this.getTBmouseIsOverOnAmong(hostPanel, this._selectedTimeBoxes, prevMousePosition);
            if (box != null)
            {
                TimeBox.CURSOR_LOC prevCursorLoc = box.getCursorLoc(prevMousePosition);
            }
        }

        public SearchBox getFirstSelectedSearchBox()
        {
            foreach (TimeBox box in this._selectedTimeBoxes)
            {
                if (box is SearchBox)
                {
                    return (SearchBox) box;
                }
            }
            return null;
        }
        public FilterBox getFirstSelectedFilterBox()
        {
            foreach (TimeBox box in this._selectedTimeBoxes)
            {
                if (box is FilterBox)
                {
                    return (FilterBox)box;
                }
            }
            return null;
        }

        public TimeBox getTBmouseIsOver(QueryPanel hostPanel, Point currMousePosition)
        {
            return this.getTBmouseIsOverOnAmong(hostPanel, this._timeBoxes, currMousePosition);
        }

        private TimeBox getTBmouseIsOverOnAmong(QueryPanel hostPanel, ArrayList tbCollection, Point currMousePosition)
        {
            foreach (TimeBox box in tbCollection)
            {
                if (box.onQP(hostPanel) && (box.getCursorLoc(currMousePosition) != TimeBox.CURSOR_LOC.NONE))
                {
                    return box;
                }
            }
            return null;
        }

        public bool IsMouseOverAnyTimeBox(QueryPanel hostPanel, Point currMousePosition)
        {
            return (null != this.getTBmouseIsOverOnAmong(hostPanel, this._timeBoxes, currMousePosition));
        }

        public void mouseDownAndMove(QueryPanel hostPanel, Point prevMousePosition, MouseEventArgs mea)
        {
            TimeBox box = this.getTBmouseIsOverOnAmong(hostPanel, this._selectedTimeBoxes, prevMousePosition);
            if (box != null)
            {
                TimeBox.CURSOR_LOC prevCursorLoc = box.getCursorLoc(prevMousePosition);
                Point mouseDiff = Utils.mouseCoorDiff(mea, prevMousePosition);
                switch (prevCursorLoc)
                {
                    case TimeBox.CURSOR_LOC.BOX:
                        this.moveSelectedBoxes(hostPanel, mouseDiff);
                        return;

                    case TimeBox.CURSOR_LOC.HANDLE:
                    {
                        int handle = box.getHandle(prevMousePosition);
                        this.resizeSelectedBoxes(handle, mouseDiff);
                        return;
                    }
                }
                this.mouseDownAndMoveSelectedBoxes(hostPanel, mea, prevCursorLoc);
            }
        }

        private void mouseDownAndMoveSelectedBoxes(QueryPanel hostPanel, MouseEventArgs mea, TimeBox.CURSOR_LOC prevCursorLoc)
        {
            foreach (TimeBox box in this._selectedTimeBoxes)
            {
                if (box.onQP(hostPanel))
                {
                    box.onMouseDownAndMove(mea, prevCursorLoc);
                }
            }
        }

        public bool mouseMoveOnly(QueryPanel hostPanel, Point prevMousePosition, MouseEventArgs mea)
        {
            TimeBox tb = this.getTBmouseIsOverOnAmong(hostPanel, this._timeBoxes, new Point(mea.X, mea.Y));
            bool flag = this.updateTbOfPrevMouseCoord(tb);
            if (tb == null)
            {
                hostPanel.MainCursor = Cursors.Default;
                return flag;
            }
            tb.onMouseMoveOnly(mea, prevMousePosition, hostPanel.Bounds);
            return flag;
        }

        private void moveSelectedBoxes(QueryPanel hostPanel, Point mouseDiff)
        {
            foreach (TimeBox box in this._selectedTimeBoxes)
            {
                box.move(mouseDiff, hostPanel.Bounds);
            }
        }

        public bool noTimeBoxes()
        {
            return (this._timeBoxes.Count == 0);
        }

        public void paintAllBoxes(Graphics clientDC, QueryPanel hostPanel)
        {
            foreach (TimeBox box in this._timeBoxes)
            {
                box.Paint(clientDC, hostPanel);
            }
        }

        public void removeSelectedTimeBoxes()
        {
            while (this._selectedTimeBoxes.Count > 0)
            {
                this.removeTimeBox((TimeBox) this._selectedTimeBoxes[0]);
                this._selectedTimeBoxes.RemoveAt(0);
            }
        }

        public void removeTimeBox(TimeBox timeBox)
        {
            if (timeBox is DisablingEntity)
            {
                this._dataSet.DisablingManager.RemoveEntity((DisablingEntity) timeBox);
            }
            else if (timeBox is SearchBox)
            {
                ((SearchBox) timeBox).undoEffects();
            }
            this._timeBoxes.Remove(timeBox);
        }

        public void reset()
        {
            this.clearTimeBoxes();
            this._selectedTimeBoxes.Clear();
        }

        public void resizeRelocateAllBoxes(QueryPanel hostPanel)
        {
            foreach (TimeBox box in this._timeBoxes)
            {
                if (box.onQP(hostPanel))
                {
                    box.onResizeRelocate();
                }
            }
        }

        private void resizeSelectedBoxes(int handle, Point mouseDiff)
        {
            foreach (TimeBox box in this._selectedTimeBoxes)
            {
                box.resizeUsingHandle(handle, mouseDiff);
            }
        }

        public bool SelectedTBsContainDisablingEntities()
        {
            foreach (TimeBox box in this._selectedTimeBoxes)
            {
                if (box is FilterBox)
                {
                    return true;
                }
            }
            return false;
        }

        public void selectTimeBox(TimeBox tBox)
        {
            tBox.isSelected = true;
            if (!this._selectedTimeBoxes.Contains(tBox))
            {
                this._selectedTimeBoxes.Add(tBox);
            }
        }

        private void unselectSelectedBoxes()
        {
            foreach (TimeBox box in this._selectedTimeBoxes)
            {
                box.isSelected = false;
            }
            this._selectedTimeBoxes.Clear();
        }

        public void unselectTimeBox(TimeBox tBox)
        {
            tBox.isSelected = false;
            this._selectedTimeBoxes.Remove(tBox);
        }

        public bool UpdateSelectionOfTBs(QueryPanel qPanel, bool isControlKeyDown, MouseEventArgs mea)
        {
            if (this.noTimeBoxes())
            {
                return false;
            }
            if (!isControlKeyDown)
            {
                this.unselectSelectedBoxes();
            }
            foreach (TimeBox box in this._timeBoxes)
            {
                if (box.onQP(qPanel) && box.containsPoint(new Point(mea.X, mea.Y)))
                {
                    box.onMouseUp(mea);
                    if (box.isSelected && isControlKeyDown)
                    {
                        this.unselectTimeBox(box);
                    }
                    else
                    {
                        this.selectTimeBox(box);
                    }
                }
            }
            return true;
        }

        private bool updateTbOfPrevMouseCoord(TimeBox tb)
        {
            if (tb == this.tbOfPrevMouseCoord)
            {
                return false;
            }
            if (this.tbOfPrevMouseCoord != null)
            {
                this.tbOfPrevMouseCoord.onMouseLeft();
            }
            this.tbOfPrevMouseCoord = tb;
            return true;
        }

        public int SelectedBoxesCount
        {
            get
            {
                return this._selectedTimeBoxes.Count;
            }
        }

        public int TimeBoxesCount
        {
            get
            {
                return this._timeBoxes.Count;
            }
        }
        public ArrayList return_selectBox()
        {
            return _selectedTimeBoxes;
        }
    }
}

