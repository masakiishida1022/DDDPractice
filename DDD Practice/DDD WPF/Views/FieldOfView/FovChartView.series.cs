using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using DDD.Domain.Entities.FieldOfView;
using DDD.Domain.ValueObjects;

namespace DDD_WPF.Views.FieldOfView
{
    public partial class FovChartView : UserControl
    {
        private void DrawSeries(Dictionary<LensType, List<FovSegment>> fovSegmentListDic)
        {
            var canvas = FovCanvas as Canvas;

            foreach (var fovSegmentListEntry in fovSegmentListDic)
            {
                var fovSegmentList = fovSegmentListEntry.Value;
                this.DrawSeries(fovSegmentList, Color.FromRgb(0,0,0));
            }
        }

        private void DrawSeries(List<FovSegment> fovSegmentList, Color color)
        {

            foreach (var fovSegment in fovSegmentList)
            {
                int numDivisionPoints = 4;
                var fovPointList = this.DivideToPoints(fovSegment, numDivisionPoints);

                int midPoint = fovPointList.Count / 2;

                var startPointList = new List<Point>();
                var endPointList = new List<Point>();

                for (int i = 0; i < fovPointList.Count - 1; i++)
                {
                    var start = ToCanvasPoint(fovPointList[i]);
                    var end = ToCanvasPoint(fovPointList[i + 1]);
                    startPointList.Add(start);
                    endPointList.Add(end);

                    Line line = new Line
                    {
                        Stroke = new SolidColorBrush(color),
                        X1 = start.X,
                        Y1 = start.Y,
                        X2 = end.X,
                        Y2 = end.Y
                    };



                    FovCanvas.Children.Add(line);
                }

                if (fovSegment.ThicknessOfRing != 0) 
                {
                    FovCanvas.Children.Add(MakeEndPointEllipse(startPointList[0], color));
                    FovCanvas.Children.Add(MakeEndPointEllipse(endPointList.Last(), color));

                    Point mid = new Point((startPointList[0].X + startPointList.Last().X) / 2,
                        (endPointList[0].Y + endPointList.Last().Y) / 2);
                    
                    FovCanvas.Children.Add(
                        MakeThicknessLabel(
                            new Point(mid.X -5, mid.Y - 20),
                            fovSegment.ThicknessOfRing, color));
                    

                }
            }
        }

        private Ellipse MakeEndPointEllipse(Point point, Color color)
        {
            //端に○を書く
            Ellipse ellipse = new Ellipse()
            {
                Stroke = new SolidColorBrush(color),
                Fill = new SolidColorBrush(color),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 4,
                Height = 4
            };

            ellipse.SetValue(System.Windows.Controls.Canvas.LeftProperty, point.X - ellipse.Width / 2);
            ellipse.SetValue(System.Windows.Controls.Canvas.TopProperty, point.Y - ellipse.Height / 2);

            return ellipse;
        }

        private Label MakeThicknessLabel(Point point, double thick, Color color)
        {
            Label ringThicknessLabel = new Label
            {
                FontSize = 12,
                Content = thick.ToString(),
            };
            ringThicknessLabel.SetValue(System.Windows.Controls.Canvas.LeftProperty, point.X);
            ringThicknessLabel.SetValue(System.Windows.Controls.Canvas.TopProperty, point.Y);
            return ringThicknessLabel;
        }

        private List<FovPoint> DivideToPoints(FovSegment segment, int numDivision)
        {
            var pointList = new List<FovPoint>();

            if (MAX_WD < segment.Start.Wd || MAX_V < segment.Start.V)
            {
                Debug.Assert(false, "startが範囲外は想定していない");
                return null;
            }

            var slope = (segment.End.V - segment.Start.V) / (segment.End.Wd - segment.Start.Wd);

           
            var endWd = segment.End.Wd;
            var endV = segment.End.V;

            #region //終端を範囲内に調整する処理
            if (MAX_WD < endWd)
            {
                endWd = MAX_WD;
                endV = segment.Start.V + (endWd - segment.Start.Wd) * slope;
            }

            if (MAX_V < endV)
            {
                endV = MAX_V;
                endWd = segment.Start.Wd + (endV - segment.Start.V) / slope;
            }
            #endregion //終端を範囲内に調整する処理

            var wdStep = (endWd - segment.Start.Wd) / numDivision;
            var vStep = (endV - segment.Start.V) / numDivision;

            pointList.Add(segment.Start);
                
            for (int i = 1; i < numDivision; i++)
            {
                var wd = segment.Start.Wd + wdStep * i;
                var v = segment.Start.V + vStep * i;
                pointList.Add(new FovPoint(v, wd));
            }
            pointList.Add(new FovPoint(endV, endWd));

            return pointList;
        }

    }
}
