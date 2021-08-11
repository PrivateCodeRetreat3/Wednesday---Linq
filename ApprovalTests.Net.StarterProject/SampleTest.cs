using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ApprovalTests.Core;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalTests.Net.StarterProject
{
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class SampleTest
    {
        [TestMethod]
        public void TestThatASingleLonelyCellDies()
        {
            var board = new []{new Point(1, 1)};
            var board2 = AdvanceTurn(board);
            Approvals.Verify(StringUtils.DisplayGrid(10,10, (x,y) => board2.Contains(new Point(x,y))? "x": "."));
        }

        private IEnumerable<Point> AdvanceTurn(Point[] board)
        {
            return board.SelectMany(p => GetNeighbours(p)).GroupBy(p => p);
        }

        private IEnumerable<Point> GetNeighbours(Point point)
        {
            yield return new Point(point.X - 1, point.Y - 1);
            yield return new Point(point.X - 0, point.Y - 1); 
            yield return new Point(point.X + 1 , point.Y - 1); 
            yield return new Point(point.X - 1, point.Y - 0);
            yield return new Point(point.X + 1 , point.Y - 0);
            yield return new Point(point.X - 1, point.Y + 1);
            yield return new Point(point.X - 0, point.Y + 1); 
            yield return new Point(point.X + 1 , point.Y + 1); 

        }
    }
}
