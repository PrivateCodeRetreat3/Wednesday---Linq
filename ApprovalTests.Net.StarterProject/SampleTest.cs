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
        
        [TestMethod]
        public void TestThatASquareSurvives()
        {
            var board = new []{new Point(1, 1), new Point(1, 2) , new Point(2, 1) , new Point(2, 2) };
            var board2 = AdvanceTurn(board);
            Approvals.Verify(StringUtils.DisplayGrid(10,10, (x,y) => board2.Contains(new Point(x,y))? "x": "."));
        }


        [TestMethod]
        public void TestThatAGliderGlides()
        {
            var board = new[] { new Point(3, 3), new Point(4, 4), new Point(4, 5), new Point(5, 3), new Point(5,4) };
            var board2 = AdvanceTurn(board);
            Approvals.Verify(StringUtils.DisplayGrid(10, 10, (x, y) => board2.Contains(new Point(x, y)) ? "x" : "."));
        }


        private IEnumerable<Point> AdvanceTurn(Point[] board)
        {
            IEnumerable<IGrouping<Point, Point>> counts = board.SelectMany(p => GetNeighbours(p)).GroupBy(p => p);
            var alive = counts.Where(g => g.Count() == 3 || (g.Count() == 2 && board.Contains(g.Key))).Select(g => g.Key);
            return alive;
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
