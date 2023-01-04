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
                this.DrawChart(fovSegmentList, Color.FromRgb(0,0,0));
            }
        }

        private void DrawChart(List<FovSegment> fovSegmentList, Color color)
        {

            foreach (var fovSegment in fovSegmentList)
            {
                int numDivisionPoints = 30;
                var fovPointList = this.DivideToPoints(fovSegment, numDivisionPoints);

                double ratioV = FovCanvas.ActualHeight / Math.Log10(1000);
                double ratioWd = FovCanvas.ActualWidth / Math.Log10(10000);

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

        private Point ToCanvasPoint(FovPoint fovPoint)
        {
            Debug.Assert(1 <= fovPoint.V && 10 <= fovPoint.Wd);

            double ratioV = FovCanvas.ActualHeight / Math.Log10(1000);
            double ratioWd = FovCanvas.ActualWidth / Math.Log10(10000);

            double v = FovCanvas.ActualHeight - Math.Log10(fovPoint.V)* ratioV;
            double wd = Math.Log10(fovPoint.Wd)  * ratioWd;

            return new Point(wd, v);
        }

        
        private List<FovPoint> DivideToPoints(FovSegment segment, int numDivision)
        {
            var pointList = new List<FovPoint>();

            pointList.Add(segment.Start);

            double wdStep = (segment.End.Wd - segment.Start.Wd) / numDivision;
            double vStep = (segment.End.V - segment.Start.V) / numDivision;

         
                
            for (int i = 1; i < numDivision; i++)
            {
                var wd = segment.Start.Wd + wdStep * i;
                var v = segment.Start.V + vStep * i;
                pointList.Add(new FovPoint(v, wd));
            }
            pointList.Add(segment.End);

            return pointList;
        }

    }
}
