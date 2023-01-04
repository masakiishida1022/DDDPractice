using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DDD.Domain.Entities.FieldOfView;

namespace DDD_WPF.Views.FieldOfView
{
    public partial class FovChartView : UserControl
    {
        private const double MAX_V = 1000;
        private const double MAX_WD = 10000;
        private const double MIN_V = 1;
        private const double MIN_WD = 10;

        private const double ChartAreaMergeX = 50;
        private const double ChartAreaMergeY = 50;

        private Point ToCanvasPoint(FovPoint fovPoint)
        {
            Debug.Assert(MIN_V <= fovPoint.V && MIN_WD <= fovPoint.Wd);

            double ratioV = (FovCanvas.ActualHeight - ChartAreaMergeY) / (Math.Log10(MAX_V) - Math.Log10(MIN_V));
            double ratioWd = (FovCanvas.ActualWidth - ChartAreaMergeX) / (Math.Log10(MAX_WD) - Math.Log10(MIN_WD));

            double v = FovCanvas.ActualHeight - ChartAreaMergeY / 2- Math.Log10(fovPoint.V)* ratioV;
            double wd = ChartAreaMergeX / 2 + (Math.Log10(fovPoint.Wd) - Math.Log10(MIN_WD)) * ratioWd;

            return new Point(wd, v);
        }
    }
}
