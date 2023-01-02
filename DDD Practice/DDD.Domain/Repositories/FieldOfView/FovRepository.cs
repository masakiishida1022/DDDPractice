using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DDD.Domain.Entities.FieldOfView;
using DDD.Domain.Logics.FieldOfView;
using DDD.Domain.ValueObjects;
using Microsoft.SqlServer.Server;

namespace DDD.Domain.Repositories.FieldOfView
{
    public class FovRepository
    {
        private readonly IFovRepository fovRepository;

        private static readonly Dictionary<Tuple<CameraType, LensType>, List<FovSegment>> DataCache
            = new Dictionary<Tuple<CameraType, LensType>, List<FovSegment>>();

        public FovRepository(IFovRepository repos)
        {
            this.fovRepository = repos;
        }

        public bool TryGetFovSegments(CameraType cam, LensType lens, out List<FovSegment> fovSegmentList)
        {
            var camAndLens = new Tuple<CameraType, LensType>(cam, lens);
            if (DataCache.TryGetValue(camAndLens, out fovSegmentList))
            {
                return true;
            }

            if (TryGenerateFovSegments(camAndLens, out fovSegmentList))
            {
                DataCache.Add(camAndLens, fovSegmentList);
                return true;
            }

            return false;
        }


        private bool TryGenerateFovSegments(Tuple<CameraType, LensType> camAndLens, out List<FovSegment> fovSegmentList)
        {
            fovSegmentList = new List<FovSegment>();

            var camType = camAndLens.Item1;
            var lensType = camAndLens.Item2;
            double focalPoint = fovRepository.GetFocalLength(lensType);
            double ccdSize = fovRepository.GetCcdSize(camType);
            double primaryPos = fovRepository.GetPrimaryPosition(lensType);
            double maxExtension = fovRepository.GetMaxExtension(lensType);
            var thickOfRingList = fovRepository.GetSelectableCloseUpRingThick(camType, lensType)
                .OrderByDescending(thick => thick).ToList();
            var startPointList = new List<FovPoint>();
            var endPointList = new List<FovPoint>();

            for (int i = 0; i < thickOfRingList.Count; i++)
            {
                //接写リング＋最大繰り出し量よりも一つ上のサイズの接写リングのほうが大きいのであれば、そちらを開始点に選ぶ
                double ext = (0 < i) ? Math.Min(thickOfRingList[i] + maxExtension, thickOfRingList[i - 1]) : thickOfRingList[0] + maxExtension;

                var begin = FovLogic.CalcFovPoint(focalPoint, ccdSize, ext, primaryPos);
                var end = FovLogic.CalcFovPoint(focalPoint, ccdSize, thickOfRingList[i], primaryPos);
                var fovSegment = new FovSegment(thickOfRingList[i], begin, end);
                fovSegmentList.Add(fovSegment);
            }

            //接写リングをつけないセグメントを追加する。
            var zeroBegin = FovLogic.CalcFovPoint(focalPoint, ccdSize, thickOfRingList.Last(), primaryPos);
            var zeroEnd = FovLogic.CalcFovPoint(focalPoint, ccdSize, 0.001, primaryPos);
            fovSegmentList.Add(new FovSegment(0, zeroBegin, zeroEnd));

            return true;

        }
    }
}
