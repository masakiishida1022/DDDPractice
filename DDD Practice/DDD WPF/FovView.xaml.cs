using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DDD.Domain.Entities.FieldOfView;
using DDD_WPF.ViewModels;

namespace DDD_WPF
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class FovControl : UserControl
    {
        private readonly FovViewModel fovViewModel = new FovViewModel();
        public FovControl()
        {
            InitializeComponent();
            this.DataContext = fovViewModel;
            //DrawFovGraph();
        }

        private void DrawFovGraph()
        {
            var canvas = FovCanvas as Canvas;

            foreach (var fovSegmentListEntry in this.fovViewModel.FovSegmentListDic)
            {
                var lensType = fovSegmentListEntry.Key;
                var fovSegmentList = fovSegmentListEntry.Value;
                this.DrawGraph(fovSegmentList, Color.FromRgb(0,0,0));
            }
            //Rectangle rect = new Rectangle();
            //rect.Stroke = new SolidColorBrush(Colors.Black);
            //rect.StrokeThickness = 5;
            
            //rect.Width = 20;
            //rect.Height = 30;
            //Canvas.SetLeft(rect, 10);
            //Canvas.SetTop(rect, 10);
            //FovCanvas.Children.Add(rect);
        }

        private void DrawGraph(List<FovSegment> fovSegmentList, Color color)
        {

            foreach (var fovSegment in fovSegmentList)
            {
                int numDivisionPoints = 10;
                var fovPointList = this.DivideToPoints(fovSegment, numDivisionPoints);

                double ratioV = FovCanvas.ActualHeight / Math.Log10(1000);
                double ratioWd = FovCanvas.ActualWidth / Math.Log10(10000);


                for (int i = 0; i < fovPointList.Count - 1; i++)
                {
                    Line line = new Line
                    {
                        Stroke = new SolidColorBrush(color),
                        X1 = Math.Log10(fovPointList[i].Wd) * ratioWd,
                        Y1 = FovCanvas.ActualHeight - Math.Log10(fovPointList[i].V) * ratioV,
                        X2 = Math.Log10(fovPointList[i+1].Wd) * ratioWd,
                        Y2 = FovCanvas.ActualHeight - Math.Log10(fovPointList[i+1].V) * ratioV
                    };

                    FovCanvas.Children.Add(line);
                }
            }
        }

        private List<FovPoint> DivideToPoints(FovSegment segment, int numPoints)
        {
            var pointList = new List<FovPoint>();

            pointList.Add(segment.Start);

            double wdStep = (segment.End.Wd - segment.Start.Wd) / (numPoints + 1);
            double vStep = (segment.End.V - segment.Start.V) / (numPoints + 1);

         

            for (int i = 1; i <= numPoints; i++)
            {
                var wd = segment.Start.Wd + wdStep * i;
                var v = segment.Start.V + vStep * i;
                pointList.Add(new FovPoint(v, wd));
            }
            pointList.Add(segment.End);

            return pointList;
        }

        private void DrawFovSegment(FovSegment fovSegment)
        {
            var pointCollection = new PointCollection();

        }
        private void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DrawFovGraph();
        }
    }
}
