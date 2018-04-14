using System;
using Puzzles.PuzzlePattern;

namespace Puzzles.Multidot
{
    class MultidotController : PuzzleController
    {
        public override void MakeChange_SolveMode(MouseButton click, string type, int x, int y, object additionalArg = null)
        {
            if (type == "Edge")
            {
                Int32 toChange;
                if ((string)additionalArg == "True")
                    toChange = ((MultidotEdge)puzzle.Edges).VerticalData[x][y];
                else
                     toChange = ((MultidotEdge)puzzle.Edges).HorisontalData[x][y];

                if(toChange == 1)
                {
                    if (click == MouseButton.Left)
                    {
                        toChange = 0;
                        EdgeChange(x, y, Convert.ToBoolean(additionalArg), false);
                    }
                    else if (click == MouseButton.Right)
                    {
                        toChange = 2;
                        EdgeChange(x, y, Convert.ToBoolean(additionalArg), false);
                    }
                }
                else if (toChange == 2)
                {
                    if (click == MouseButton.Left)
                    {
                        toChange = 1;
                        EdgeChange(x, y, Convert.ToBoolean(additionalArg), true);
                    }
                    else if (click == MouseButton.Right)
                    {
                        toChange = 0;
                    }
                }
                else
                {
                    if (click == MouseButton.Left)
                    {
                        toChange = 1;
                        EdgeChange(x, y, Convert.ToBoolean(additionalArg), true);
                    }
                    else if (click == MouseButton.Right)
                    {
                        toChange = 2;
                    }
                }
                if ((string)additionalArg == "True")
                    ((MultidotEdge)puzzle.Edges).VerticalData[x][y] = toChange;
                else
                    ((MultidotEdge)puzzle.Edges).HorisontalData[x][y] = toChange;

                RefreshButton(new ElementClickArgs(x, y, "Edge", additionalArg));
            }
        }

        public override void MakeChange_EditMode(MouseButton click, string type, int x, int y, object additionalArg = null)
        {
            if (type == "Field")
            {
                if (((MultidotField)puzzle.Fields).Data[x][y] == 5)
                    ((MultidotField)puzzle.Fields).Data[x][y] = ((MultidotField)puzzle.Fields).Used[x][y];
                else
                    ((MultidotField)puzzle.Fields).Data[x][y] = 5;
                RefreshButton(new ElementClickArgs(x, y, "Field", null));
            }
            else if (type == "Edge")
            {
                bool newVal;
                if ((string)additionalArg == "True")
                {
                    newVal = !((MultidotEdge)puzzle.Edges).VerticalDataSolved[x][y];
                    ((MultidotEdge)puzzle.Edges).VerticalDataSolved[x][y] = newVal;
                }
                else
                {
                    newVal = !((MultidotEdge)puzzle.Edges).HorisontalDataSolved[x][y];
                    ((MultidotEdge)puzzle.Edges).HorisontalDataSolved[x][y] = newVal;
                }
                RefreshButton(new ElementClickArgs(x, y, "Edge", additionalArg));
                EdgeChange(x, y, Convert.ToBoolean(additionalArg), newVal);
            }
        }

        
        private void EdgeChange(int x, int y, bool vertical, bool newEdgeVal)
        {
            int val;
            if (newEdgeVal)
                val = -1;
            else
                val = 1;
            if (EditMode)
                val *= -1;
            if (vertical)
            {
                FieldChange(x, y, val);
                FieldChange(x, y - 1, val);
            }
            else
            {
                FieldChange(x, y, val);
                FieldChange(x - 1, y, val);
            }
        }

        private void FieldChange(int x, int y, int val)
        {
            if (x < 0 || x > n-1 || y < 0 || y > m-1)
                return;
            Puzzle puzzle = Puzzle.Instance;

            ((MultidotField)puzzle.Fields).Used[x][y] += val;
            RefreshButton(new ElementClickArgs(x, y, "Field", null));
        }

        
        private bool[][] verticalData;
        private bool[][] horisontalData;

        public override bool IsSolved()
        {
            if (!EditMode)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (((MultidotField)puzzle.Fields).Data[i][j] != 5 && ((MultidotField)puzzle.Fields).Used[i][j] != 0)
                            return false;
                    }
                }
            }

            verticalData = new bool[n][];
            horisontalData = new bool[n + 1][];
            if (!EditMode)
            {
                for (int i = 0; i < n; i++)
                {
                    verticalData[i] = new bool[m + 1];
                    for (int j = 0; j < m + 1; j++)
                        verticalData[i][j] = ((MultidotEdge)puzzle.Edges).VerticalData[i][j] == 1 ? true : false;
                }
                for (int i = 0; i < n + 1; i++)
                {
                    horisontalData[i] = new bool[m];
                    for (int j = 0; j < m; j++)
                        horisontalData[i][j] = ((MultidotEdge)puzzle.Edges).HorisontalData[i][j] == 1 ? true : false;
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    verticalData[i] = new bool[m + 1];
                    for (int j = 0; j < m + 1; j++)
                        verticalData[i][j] = ((MultidotEdge)puzzle.Edges).VerticalDataSolved[i][j];
                }
                for (int i = 0; i < n + 1; i++)
                {
                    horisontalData[i] = new bool[m];
                    for (int j = 0; j < m; j++)
                        horisontalData[i][j] = ((MultidotEdge)puzzle.Edges).HorisontalDataSolved[i][j];
                }
            }
            bool found = false;
            for (int i = 0; i < n && !found; i++)
            {
                for (int j = 0; j < m + 1 && !found; j++)
                {
                    if (verticalData[i][j])
                    {
                        if (!GoThroughTable(i, j, true))
                            return false;
                        else
                            found = true;
                    }
                }
            }
            for (int i = 0; i < n + 1 && !found; i++)
            {
                for (int j = 0; j < m && !found; j++)
                {
                    if (horisontalData[i][j])
                    {
                        if (!GoThroughTable(i, j, false))
                            return false;
                        else
                            found = true;
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m + 1; j++)
                {
                    if (verticalData[i][j])
                        return false;
                }
            }
            for (int i = 0; i < n + 1; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (horisontalData[i][j])
                        return false;
                }
            }

            return true;
        }

        private bool GoThroughTable(int bx, int by, bool bVertical)
        {
            int x = bx;
            int y = by;
            bool vertical = bVertical;
            int prev_x = x;
            int prev_y = y;

            do
            {
                if (vertical && (vertical != bVertical || x != bx || y != by))
                    verticalData[x][y] = false;
                else if  (vertical != bVertical || x != bx || y != by)
                    horisontalData[x][y] = false;

                int next_x = -1, next_y = -1;
                bool next_vert = false;

                if (x == prev_x && y == prev_y)
                {
                    if(vertical)
                    {
                        if (x+1 < n  && verticalData[x+1][y])
                        {
                            next_x = x+1;
                            next_y = y;
                            next_vert = vertical;
                        }
                        if (x + 1 <= n && y < m && horisontalData[x+1][y])
                        {
                            if (next_x != -1)
                                return false;

                            next_x = x+1;
                            next_y = y;
                            next_vert = !vertical;
                        }
                        if (x + 1 <= n && y - 1 >= 0 && horisontalData[x + 1][y-1])
                        {
                            if (next_x != -1)
                                return false;

                            next_x = x + 1;
                            next_y = y-1;
                            next_vert = !vertical;
                        }
                        prev_x = x + 1;
                        prev_y = y;
                    }
                    else
                    {
                        if (y + 1 < m && horisontalData[x][y + 1])
                        {
                            next_x = x;
                            next_y = y + 1;
                            next_vert = vertical;
                        }
                        if (y + 1 <= m && x < n && verticalData[x][y + 1])
                        {
                            if (next_x != -1)
                                return false;

                            next_x = x;
                            next_y = y + 1;
                            next_vert = !vertical;
                        }
                        if (y + 1 <= m && x - 1 >= 0 && verticalData[x - 1][y + 1])
                        {
                            if (next_x != -1)
                                return false;

                            next_x = x - 1;
                            next_y = y + 1;
                            next_vert = !vertical;
                        }
                        prev_x = x;
                        prev_y = y + 1;
                    }
                }
                else
                {
                    if (vertical)
                    {
                        if (x - 1 >= 0 && verticalData[x - 1][y])
                        {
                            next_x = x - 1;
                            next_y = y;
                            next_vert = vertical;
                        }
                        if (x < n && y < m && horisontalData[x][y])
                        {
                            if (next_x != -1)
                                return false;

                            next_x = x;
                            next_y = y;
                            next_vert = !vertical;
                        }
                        if (x < n && y - 1 >= 0 && horisontalData[x][y - 1])
                        {
                            if (next_x != -1)
                                return false;

                            next_x = x;
                            next_y = y - 1;
                            next_vert = !vertical;
                        }
                        prev_x = x;
                        prev_y = y;
                    }
                    else
                    {
                        if (y - 1 >= 0 && horisontalData[x][y - 1])
                        {
                            next_x = x;
                            next_y = y - 1;
                            next_vert = vertical;
                        }
                        if (y < m && x < n && verticalData[x][y])
                        {
                            if (next_x != -1)
                                return false;

                            next_x = x;
                            next_y = y;
                            next_vert = !vertical;
                        }
                        if (y < m && x - 1 >= 0 && verticalData[x - 1][y])
                        {
                            if (next_x != -1)
                                return false;

                            next_x = x - 1;
                            next_y = y;
                            next_vert = !vertical;
                        }
                        prev_x = x;
                        prev_y = y;
                    }
                }


                if (next_x == -1)
                    return false;
                x = next_x;
                y = next_y;
                vertical = next_vert;
            } while (vertical != bVertical || x != bx || y != by);

                if (bVertical)
                    verticalData[bx][by] = false;
                else
                    horisontalData[bx][by] = false;
                return true;
        }
    }
}