using JWDataTracker.Application.MidWeekMeetingSchedule;
using JWDataTracker.Application.CongregationUser;
using JWDataTracker.Infrastructure.Repository;
using Moq;
using Newtonsoft.Json;
using System.Linq.Expressions;
using entity = JWDataTracker.Infrastructure;
namespace JWDataTracker.Test
{
    public class MidWeekScheduleTest
    {
        private IMidWeekMeetingScheduleService service;
        private readonly Mock<IGenericRepository<entity.MidWeekSchedule>> midWeekScheduleRepo = new Mock<IGenericRepository<entity.MidWeekSchedule>>();
        private readonly Mock<IGenericRepository<entity.MidWeekScheduleItem>> midWeekScheduleItemRepo = new Mock<IGenericRepository<entity.MidWeekScheduleItem>>();
        private readonly Mock<IUnitOfWork> unitOfWorkMoq = new Mock<IUnitOfWork>();

        [SetUp]
        public void Setup()
        {
            midWeekScheduleRepo.Setup(m => m.GetByID(It.IsAny<long>())).Returns(new entity.MidWeekSchedule());
            midWeekScheduleItemRepo.Setup(m => m.GetByID(It.IsAny<long>())).Returns(new entity.MidWeekScheduleItem());
            unitOfWorkMoq.Setup(m => m.MidWeekScheduleRepository).Returns(midWeekScheduleRepo.Object);
            unitOfWorkMoq.Setup(m => m.MidWeekScheduleItemRepository).Returns(midWeekScheduleItemRepo.Object);
            service = new MidWeekMeetingScheduleService(unitOfWorkMoq.Object);
        }

    }
}
