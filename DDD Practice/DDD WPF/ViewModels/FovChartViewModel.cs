using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Media3D;
using DDD.Domain.Entities.FieldOfView;
using DDD.Domain.Repositories.FieldOfView;
using DDD.Domain.ValueObjects;
using Reactive.Bindings;

namespace DDD_WPF.ViewModels
{
    public class FovChartViewModel : INotifyPropertyChanged
    {
        private FovRepository fovRepository;

        public Dictionary<LensType, List<FovSegment>> FovSegmentListDic { get; set; } =
            new Dictionary<LensType, List<FovSegment>>();
        public ReactiveProperty<string> InputValue { get; set; }
        public ReactiveProperty<string> OutputValue { get; set; }

        public ReactiveProperty<int> UpdateCounter { get; set; } = new ReactiveProperty<int>(0);
        public IObservable<int> Observable => UpdateCounter;

        public ReactiveProperty<CameraType> CameraType { get; set; }
        public Dictionary<LensType, FovSegment> FovSegmentDic;

        public FovChartViewModel() : this(new FovRepositoryImpl())
        {
            CameraType camType = new CameraType("048C");
            LensType lensType = new LensType("CA-HL");

            this.fovRepository.TryGetFovSegments(camType, lensType, out List<FovSegment> fovSegmentList);
            if (fovSegmentList != null && 0 < fovSegmentList.Count)
            {
                FovSegmentListDic.Add(lensType, fovSegmentList);
            }
        }

        public FovChartViewModel(IFovRepository fovRepos)
        {
            this.fovRepository = new FovRepository(fovRepos);
            InputValue = new ReactiveProperty<string>();
            OutputValue = InputValue.ToReactiveProperty();
        }

        public bool TryGetFovSegmentList(CameraType cam, LensType lens, out List<FovSegment> fovSegmentList)
        {
            fovSegmentList = null;
            return this.fovRepository.TryGetFovSegments(cam, lens, out fovSegmentList);
        }

        public class FovGraph
        {

        }
//public WeatherLatestViewModel(
        //    IWeatherRepository weather,
        //    IAreasRepository areas)
        //{
        //    _weather = weather;
        //    _areasRepository = areas;

        //    foreach (var area in _areasRepository.GetData())
        //    {
        //        Areas.Add(new AreaEntity(area.AreaId, area.AreaName));
        //    }

        //    LatestButton = new DelegateCommand(LatestButtonExecute);
        //}

        //private ObservableCollection<AreaEntity> _areas = new ObservableCollection<AreaEntity>();
        //public ObservableCollection<AreaEntity> Areas
        //{
        //    get { return _areas; }
        //    set
        //    {
        //        SetProperty(ref _areas, value);
        //    }
        //}
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
