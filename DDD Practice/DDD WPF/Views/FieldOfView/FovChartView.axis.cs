using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using DDD.Domain.Entities.FieldOfView;

namespace DDD_WPF.Views.FieldOfView
{
    public partial class FovChartView : UserControl
    {
        private const double ThickOfAxisLine = 0.1;
        private Color _colorOfAxisLine = Color.FromRgb(0, 0, 0);
        private const int ScaleLabelFontSize = 8;

        private void DrawAxis()
        {
            DrawHorizontalAxis();
            DrawVerticalAxis();
        }

        private void DrawHorizontalAxis()
        {
            var logMaxV = Math.Log10(MAX_V);
            for (int i = 0; i <= (int)logMaxV; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    var v = Math.Pow(10, i) * j;

                    if (MAX_V < v)
                    {
                        break;
                    }

                    var vStart = ToCanvasPoint(new FovPoint(v, MIN_WD));
                    var vEnd = ToCanvasPoint(new FovPoint(v, MAX_WD));

                    Line line = new Line
                    {
                        Stroke = new SolidColorBrush(_colorOfAxisLine),
                        StrokeThickness = ThickOfAxisLine,
                        X1 = vStart.X,
                        Y1 = vStart.Y,
                        X2 = vEnd.X,
                        Y2 = vEnd.Y
                    };
                    FovCanvas.Children.Add(line);

                    if (j == 1)
                    {
                        Label scaleLabel = new Label
                        {
                            FontSize = ScaleLabelFontSize,
                            Content = v.ToString(),
                            VerticalAlignment = VerticalAlignment.Bottom,
                            HorizontalAlignment = HorizontalAlignment.Left
                        };
                        scaleLabel.SetValue(System.Windows.Controls.Canvas.LeftProperty, vStart.X - 17 - i*2);//-iは桁数が大きいほど左に配置するため
                        scaleLabel.SetValue(System.Windows.Controls.Canvas.TopProperty, vStart.Y - 12); 
                        FovCanvas.Children.Add(scaleLabel);
                    }

                }
            }


        }

        private void DrawVerticalAxis()
        {
            var logMaxWd = Math.Log10(MAX_WD);
            for (int i = 1; i <= logMaxWd; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    var v = Math.Pow(10, i) * j;
                    if (MAX_WD < v)
                    {
                        break;
                    }

                    var vStart = ToCanvasPoint(new FovPoint(MIN_V, v));
                    var vEnd = ToCanvasPoint(new FovPoint(MAX_V, v));

                    Line line = new Line
                    {
                        Stroke = new SolidColorBrush(_colorOfAxisLine),
                        StrokeThickness = ThickOfAxisLine,
                        X1 = vStart.X,
                        Y1 = vStart.Y,
                        X2 = vEnd.X,
                        Y2 = vEnd.Y
                    };
                    FovCanvas.Children.Add(line);

                    if (j == 1)
                    {
                        //DrawScaleLabel(new Point(vStart.X - 6, vStart.Y - 6), (int)v);
                        Label scaleLabel = new Label
                        {
                            FontSize = ScaleLabelFontSize,
                            Content = v.ToString(),
                            VerticalAlignment = VerticalAlignment.Bottom,
                            HorizontalAlignment = HorizontalAlignment.Right
                        };
                        scaleLabel.SetValue(System.Windows.Controls.Canvas.LeftProperty, vStart.X - 6);
                        scaleLabel.SetValue(System.Windows.Controls.Canvas.TopProperty, vStart.Y - 6); 
                        FovCanvas.Children.Add(scaleLabel);
                    }

                }
            }

        }

        void DrawScaleLabel(Point point, int scale)
        {
            Label scaleLabel = new Label
            {
                FontSize = 8,
                Content = scale.ToString(),
                
            };
            scaleLabel.SetValue(System.Windows.Controls.Canvas.LeftProperty, point.X);
            scaleLabel.SetValue(System.Windows.Controls.Canvas.TopProperty, point.Y);            
            FovCanvas.Children.Add(scaleLabel);
        }
    }
}
