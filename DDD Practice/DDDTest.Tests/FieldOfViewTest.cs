using DDD.Domain.Entities.FieldOfView;
using DDD.Domain.Logics.FieldOfView;
using DDD.Domain.Repositories.FieldOfView;
using DDD.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace DDDTest.Tests
{
    [TestClass]
    public class FieldOfViewTest
    {
        [TestMethod]
        public void 視野表データ取得()
        {
            var moqFovRepository = new Mock<IFovRepository>();
            FovRepository fov = new FovRepository(moqFovRepository.Object);
            var camType = new CameraType("H048MX");
            var lensType = new LensType("CA-HL4");
            moqFovRepository.Setup(x => x.GetCcdSize(camType)).Returns(2.304);
            moqFovRepository.Setup(x => x.GetFocalLength(lensType)).Returns(12.40);
            moqFovRepository.Setup(x => x.GetMaxExtension(lensType)).Returns(1.2716);
            moqFovRepository.Setup(x => x.GetPrimaryPosition(lensType)).Returns(29.49+3.84);
            moqFovRepository.Setup(x => x.GetSelectableCloseUpRingThick(camType, lensType)).Returns(new List<double>() { 0.5, 3, 4 });

            fov.TryGetFovSegments(camType, lensType, out List<FovSegment> fovSegmentList);
            Assert.AreEqual(fovSegmentList.Count, 4);
            foreach(var segment in fovSegmentList)
            {
                Console.WriteLine("---------------");
                Console.WriteLine("Start: T: " + segment.ThicknessOfRing.ToString());
                Console.WriteLine("Start: V; " + segment.Start.V.ToString());
                Console.WriteLine("Start: W; " + segment.Start.Wd.ToString());
                Console.WriteLine("End  : V; " + segment.End.V.ToString());
                Console.WriteLine("End  : W; " + segment.End.Wd.ToString());
            }

           

        }
    }
}
