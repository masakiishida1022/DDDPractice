using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DDD.Domain.Entities.FieldOfView;
using DDD_WPF.ViewModels;

namespace DDD_WPF.Views.FieldOfView
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class FovChartView : UserControl
    {
        private readonly FovChartViewModel fovViewModel = new FovChartViewModel();

        public FovChartView()
        {
            InitializeComponent();
            this.DataContext = fovViewModel;
            fovViewModel.Observable.Skip(1).Subscribe(count => Update());
            //DrawFovGraph();
        }

        
      
        private void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fovViewModel.UpdateCounter.Value = 10;
        }

        private void Update()
        {
            this.FovCanvas.Children.Clear();
            this.DrawAxis();
            this.DrawSeries(this.fovViewModel.FovSegmentListDic);
        }
    }
}
