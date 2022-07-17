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

        [Test]
        public void Add_Schedule_Meeting_Success()
        {
            var midWeekSchedule = new MidWeekMeetingScheduleDto()
            {
                Attendance = 0,
                CongregationId = 1,
                CreatedBy = 1,
                MidWeekScheduleId = 0,
                ScheduledDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                MidWeekScheduleItems = new List<MidWeekScheduleItemDto>()
                      {
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,2,"Sample 2",Constant.MainHall,Constant.OpeningPrayer),
                          new MidWeekScheduleItemDto(Constant.OPENING,3,"Sample 3",Constant.MainHall,Constant.ClosingPrayer),

                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,4,"Sample 4",Constant.MainHall,Constant.InstructionalTalk),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,5,"Sample 5",Constant.MainHall,Constant.SpiritualGem),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,6,"Sample 6",Constant.MainHall,Constant.BibleReading),

                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 7",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,8,"Sample 8",Constant.MainHall,Constant.FirstPart,88,"sample 88"),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,9,"Sample 9",Constant.MainHall,Constant.SecondPart,99,"sample 99"),

                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,10,"Sample 10",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,11,"Sample 11",Constant.MainHall,Constant.FirstPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,12,"Sample 12",Constant.MainHall,Constant.SecondPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,13,"Sample 13",Constant.MainHall,Constant.CBSConductor),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,14,"Sample 14",Constant.MainHall,Constant.CBSReader),

                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,15,"Sample 15",Constant.MainHall,Constant.AudioVideo,115, "Sample 115"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,16,"Sample 16",Constant.MainHall,Constant.Zoom,116, "Sample 116"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,17,"Sample 17",Constant.MainHall,Constant.MicRoving,117, "Sample 117"),
                      }
            };
            var response = service.Add(midWeekSchedule);
            Assert.IsTrue(response.IsSuccess);
        }

        [Test]
        public void Add_Schedule_Meeting_Validate_1()
        {
            var midWeekSchedule = new MidWeekMeetingScheduleDto()
            {
                Attendance = 0,
                CongregationId = 1,
                CreatedBy = 1,
                MidWeekScheduleId = 0,
                ScheduledDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                MidWeekScheduleItems = new List<MidWeekScheduleItemDto>()
                      {
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,2,"Sample 2",Constant.MainHall,Constant.OpeningPrayer),
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample 3",Constant.MainHall,Constant.ClosingPrayer),

                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,4,"Sample 4",Constant.MainHall,Constant.InstructionalTalk),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,5,"Sample 5",Constant.MainHall,Constant.SpiritualGem),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,6,"Sample 6",Constant.MainHall,Constant.BibleReading),

                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 7",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,8,"Sample 8",Constant.MainHall,Constant.FirstPart,88,"sample 88"),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,9,"Sample 9",Constant.MainHall,Constant.SecondPart,99,"sample 99"),

                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,10,"Sample 10",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,11,"Sample 11",Constant.MainHall,Constant.FirstPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,12,"Sample 12",Constant.MainHall,Constant.SecondPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,13,"Sample 13",Constant.MainHall,Constant.CBSConductor),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,14,"Sample 14",Constant.MainHall,Constant.CBSReader),

                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,15,"Sample 15",Constant.MainHall,Constant.AudioVideo,115, "Sample 115"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,16,"Sample 16",Constant.MainHall,Constant.Zoom,116, "Sample 116"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,17,"Sample 17",Constant.MainHall,Constant.MicRoving,117, "Sample 117"),
                      }
            };
            var response = service.Add(midWeekSchedule);
            Assert.IsFalse(response.IsSuccess);
        }

        [Test]
        public void Add_Schedule_Meeting_Validate_2()
        {
            var midWeekSchedule = new MidWeekMeetingScheduleDto()
            {
                Attendance = 0,
                CongregationId = 1,
                CreatedBy = 1,
                MidWeekScheduleId = 0,
                ScheduledDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                MidWeekScheduleItems = new List<MidWeekScheduleItemDto>()
                      {
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,2,"Sample 2",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,3,"Sample 3",Constant.MainHall,Constant.ClosingPrayer),

                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,4,"Sample 4",Constant.MainHall,Constant.InstructionalTalk),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,5,"Sample 5",Constant.MainHall,Constant.SpiritualGem),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,6,"Sample 6",Constant.MainHall,Constant.BibleReading),

                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 7",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,8,"Sample 8",Constant.MainHall,Constant.FirstPart,88,"sample 88"),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,9,"Sample 9",Constant.MainHall,Constant.SecondPart,99,"sample 99"),

                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,10,"Sample 10",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,11,"Sample 11",Constant.MainHall,Constant.FirstPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,12,"Sample 12",Constant.MainHall,Constant.SecondPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,13,"Sample 13",Constant.MainHall,Constant.CBSConductor),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,14,"Sample 14",Constant.MainHall,Constant.CBSReader),

                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,15,"Sample 15",Constant.MainHall,Constant.AudioVideo,115, "Sample 115"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,16,"Sample 16",Constant.MainHall,Constant.Zoom,116, "Sample 116"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,17,"Sample 17",Constant.MainHall,Constant.MicRoving,117, "Sample 117"),
                      }
            };
            var response = service.Add(midWeekSchedule);
            Assert.IsFalse(response.IsSuccess);
        }

        [Test]
        public void Add_Schedule_Meeting_Validate_3()
        {
            var midWeekSchedule = new MidWeekMeetingScheduleDto()
            {
                Attendance = 0,
                CongregationId = 1,
                CreatedBy = 1,
                MidWeekScheduleId = 0,
                ScheduledDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                MidWeekScheduleItems = new List<MidWeekScheduleItemDto>()
                      {
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,2,"Sample 2",Constant.MainHall,Constant.OpeningPrayer),
                          new MidWeekScheduleItemDto(Constant.OPENING,3,"Sample 3",Constant.MainHall,Constant.ClosingPrayer),

                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,4,"Sample 4",Constant.MainHall,Constant.InstructionalTalk),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,5,"Sample 5",Constant.MainHall,Constant.SpiritualGem),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,6,"Sample 6",Constant.MainHall,Constant.BibleReading),

                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 7",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,8,"Sample 8",Constant.MainHall,Constant.FirstPart,88,"sample 88"),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,9,"Sample 9",Constant.MainHall,Constant.SecondPart,99,"sample 99"),

                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,4,"Sample 10",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,11,"Sample 11",Constant.MainHall,Constant.FirstPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,12,"Sample 12",Constant.MainHall,Constant.SecondPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,13,"Sample 13",Constant.MainHall,Constant.CBSConductor),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,14,"Sample 14",Constant.MainHall,Constant.CBSReader),

                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,15,"Sample 15",Constant.MainHall,Constant.AudioVideo,115, "Sample 115"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,16,"Sample 16",Constant.MainHall,Constant.Zoom,116, "Sample 116"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,17,"Sample 17",Constant.MainHall,Constant.MicRoving,117, "Sample 117"),
                      }
            };
            var response = service.Add(midWeekSchedule);
            Assert.IsFalse(response.IsSuccess);
        }

        [Test]
        public void Add_Schedule_Meeting_Validate_4_5()
        {
            var midWeekSchedule = new MidWeekMeetingScheduleDto()
            {
                Attendance = 0,
                CongregationId = 1,
                CreatedBy = 1,
                MidWeekScheduleId = 0,
                ScheduledDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                MidWeekScheduleItems = new List<MidWeekScheduleItemDto>()
                      {
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,2,"Sample 2",Constant.MainHall,Constant.OpeningPrayer),
                          new MidWeekScheduleItemDto(Constant.OPENING,3,"Sample 3",Constant.MainHall,Constant.ClosingPrayer),

                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,4,"Sample 4",Constant.MainHall,Constant.InstructionalTalk),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,5,"Sample 5",Constant.MainHall,Constant.SpiritualGem),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,6,"Sample 6",Constant.MainHall,Constant.BibleReading),

                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 7",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,8,"Sample 8",Constant.MainHall,Constant.FirstPart,88,"sample 88"),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,9,"Sample 9",Constant.MainHall,Constant.SecondPart,99,"sample 99"),

                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,4,"Sample 10",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,11,"Sample 11",Constant.MainHall,Constant.FirstPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,12,"Sample 12",Constant.MainHall,Constant.SecondPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,13,"Sample 13",Constant.MainHall,Constant.CBSConductor),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,14,"Sample 14",Constant.MainHall,Constant.CBSReader),

                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,15,"Sample 15",Constant.MainHall,Constant.AudioVideo,115, "Sample 115"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,16,"Sample 16",Constant.MainHall,Constant.Zoom,116, "Sample 116"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,17,"Sample 17",Constant.MainHall,Constant.MicRoving,117, "Sample 117"),
                      }
            };
            var response = service.Add(midWeekSchedule);
            Assert.IsFalse(response.IsSuccess);
        }

        [Test]
        public void Add_Schedule_Meeting_Validate_6()
        {
            var midWeekSchedule = new MidWeekMeetingScheduleDto()
            {
                Attendance = 0,
                CongregationId = 1,
                CreatedBy = 1,
                MidWeekScheduleId = 0,
                ScheduledDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                MidWeekScheduleItems = new List<MidWeekScheduleItemDto>()
                      {
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,2,"Sample 2",Constant.MainHall,Constant.OpeningPrayer),
                          new MidWeekScheduleItemDto(Constant.OPENING,3,"Sample 3",Constant.MainHall,Constant.ClosingPrayer),

                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,4,"Sample 4",Constant.MainHall,Constant.InstructionalTalk),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,5,"Sample 5",Constant.MainHall,Constant.SpiritualGem),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,6,"Sample 6",Constant.MainHall,Constant.BibleReading),

                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 7",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 8",Constant.MainHall,Constant.FirstPart,88,"sample 88"),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,9,"Sample 9",Constant.MainHall,Constant.SecondPart,99,"sample 99"),

                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,10,"Sample 10",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,11,"Sample 11",Constant.MainHall,Constant.FirstPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,12,"Sample 12",Constant.MainHall,Constant.SecondPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,13,"Sample 13",Constant.MainHall,Constant.CBSConductor),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,14,"Sample 14",Constant.MainHall,Constant.CBSReader),

                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,15,"Sample 15",Constant.MainHall,Constant.AudioVideo,115, "Sample 115"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,16,"Sample 16",Constant.MainHall,Constant.Zoom,116, "Sample 116"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,17,"Sample 17",Constant.MainHall,Constant.MicRoving,117, "Sample 117"),
                      }
            };
            var response = service.Add(midWeekSchedule);
            Assert.IsFalse(response.IsSuccess);
        }

        [Test]
        public void Edit_Schedule_Meeting_Success()
        {
            var midWeekSchedule = new MidWeekMeetingScheduleDto()
            {
                Attendance = 0,
                CongregationId = 1,
                CreatedBy = 1,
                MidWeekScheduleId = 0,
                ScheduledDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                MidWeekScheduleItems = new List<MidWeekScheduleItemDto>()
                      {
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,2,"Sample 2",Constant.MainHall,Constant.OpeningPrayer),
                          new MidWeekScheduleItemDto(Constant.OPENING,3,"Sample 3",Constant.MainHall,Constant.ClosingPrayer),

                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,4,"Sample 4",Constant.MainHall,Constant.InstructionalTalk),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,5,"Sample 5",Constant.MainHall,Constant.SpiritualGem),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,6,"Sample 6",Constant.MainHall,Constant.BibleReading),

                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 7",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,8,"Sample 8",Constant.MainHall,Constant.FirstPart,88,"sample 88"),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,9,"Sample 9",Constant.MainHall,Constant.SecondPart,99,"sample 99"),

                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,10,"Sample 10",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,11,"Sample 11",Constant.MainHall,Constant.FirstPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,12,"Sample 12",Constant.MainHall,Constant.SecondPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,13,"Sample 13",Constant.MainHall,Constant.CBSConductor),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,14,"Sample 14",Constant.MainHall,Constant.CBSReader),

                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,15,"Sample 15",Constant.MainHall,Constant.AudioVideo,115, "Sample 115"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,16,"Sample 16",Constant.MainHall,Constant.Zoom,116, "Sample 116"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,17,"Sample 17",Constant.MainHall,Constant.MicRoving,117, "Sample 117"),
                      }
            };
            long id = 1;
            foreach (var item in midWeekSchedule.MidWeekScheduleItems) {
                item.MidWeekScheduleItemId = id;
                id++;
            }
            var response = service.Edit(midWeekSchedule);
            Assert.IsTrue(response.IsSuccess);
        }

        [Test]
        public void Edit_Schedule_Meeting_Validate_1()
        {
            var midWeekSchedule = new MidWeekMeetingScheduleDto()
            {
                Attendance = 0,
                CongregationId = 1,
                CreatedBy = 1,
                MidWeekScheduleId = 0,
                ScheduledDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                MidWeekScheduleItems = new List<MidWeekScheduleItemDto>()
                      {
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,2,"Sample 2",Constant.MainHall,Constant.OpeningPrayer),
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample 3",Constant.MainHall,Constant.ClosingPrayer),

                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,4,"Sample 4",Constant.MainHall,Constant.InstructionalTalk),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,5,"Sample 5",Constant.MainHall,Constant.SpiritualGem),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,6,"Sample 6",Constant.MainHall,Constant.BibleReading),

                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 7",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,8,"Sample 8",Constant.MainHall,Constant.FirstPart,88,"sample 88"),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,9,"Sample 9",Constant.MainHall,Constant.SecondPart,99,"sample 99"),

                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,10,"Sample 10",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,11,"Sample 11",Constant.MainHall,Constant.FirstPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,12,"Sample 12",Constant.MainHall,Constant.SecondPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,13,"Sample 13",Constant.MainHall,Constant.CBSConductor),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,14,"Sample 14",Constant.MainHall,Constant.CBSReader),

                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,15,"Sample 15",Constant.MainHall,Constant.AudioVideo,115, "Sample 115"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,16,"Sample 16",Constant.MainHall,Constant.Zoom,116, "Sample 116"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,17,"Sample 17",Constant.MainHall,Constant.MicRoving,117, "Sample 117"),
                      }
            };
            long id = 1;
            foreach (var item in midWeekSchedule.MidWeekScheduleItems)
            {
                item.MidWeekScheduleItemId = id;
                id++;
            }
            var response = service.Edit(midWeekSchedule);
            Assert.IsFalse(response.IsSuccess);
        }

        [Test]
        public void Edit_Schedule_Meeting_Validate_2()
        {
            var midWeekSchedule = new MidWeekMeetingScheduleDto()
            {
                Attendance = 0,
                CongregationId = 1,
                CreatedBy = 1,
                MidWeekScheduleId = 0,
                ScheduledDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                MidWeekScheduleItems = new List<MidWeekScheduleItemDto>()
                      {
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,2,"Sample 2",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,3,"Sample 3",Constant.MainHall,Constant.ClosingPrayer),

                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,4,"Sample 4",Constant.MainHall,Constant.InstructionalTalk),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,5,"Sample 5",Constant.MainHall,Constant.SpiritualGem),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,6,"Sample 6",Constant.MainHall,Constant.BibleReading),

                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 7",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,8,"Sample 8",Constant.MainHall,Constant.FirstPart,88,"sample 88"),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,9,"Sample 9",Constant.MainHall,Constant.SecondPart,99,"sample 99"),

                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,10,"Sample 10",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,11,"Sample 11",Constant.MainHall,Constant.FirstPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,12,"Sample 12",Constant.MainHall,Constant.SecondPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,13,"Sample 13",Constant.MainHall,Constant.CBSConductor),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,14,"Sample 14",Constant.MainHall,Constant.CBSReader),

                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,15,"Sample 15",Constant.MainHall,Constant.AudioVideo,115, "Sample 115"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,16,"Sample 16",Constant.MainHall,Constant.Zoom,116, "Sample 116"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,17,"Sample 17",Constant.MainHall,Constant.MicRoving,117, "Sample 117"),
                      }
            };
            long id = 1;
            foreach (var item in midWeekSchedule.MidWeekScheduleItems)
            {
                item.MidWeekScheduleItemId = id;
                id++;
            }
            var response = service.Edit(midWeekSchedule);
            Assert.IsFalse(response.IsSuccess);
        }

        [Test]
        public void Edit_Schedule_Meeting_Validate_3()
        {
            var midWeekSchedule = new MidWeekMeetingScheduleDto()
            {
                Attendance = 0,
                CongregationId = 1,
                CreatedBy = 1,
                MidWeekScheduleId = 0,
                ScheduledDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                MidWeekScheduleItems = new List<MidWeekScheduleItemDto>()
                      {
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,2,"Sample 2",Constant.MainHall,Constant.OpeningPrayer),
                          new MidWeekScheduleItemDto(Constant.OPENING,3,"Sample 3",Constant.MainHall,Constant.ClosingPrayer),

                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,4,"Sample 4",Constant.MainHall,Constant.InstructionalTalk),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,5,"Sample 5",Constant.MainHall,Constant.SpiritualGem),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,6,"Sample 6",Constant.MainHall,Constant.BibleReading),

                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 7",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,8,"Sample 8",Constant.MainHall,Constant.FirstPart,88,"sample 88"),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,9,"Sample 9",Constant.MainHall,Constant.SecondPart,99,"sample 99"),

                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,4,"Sample 10",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,11,"Sample 11",Constant.MainHall,Constant.FirstPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,12,"Sample 12",Constant.MainHall,Constant.SecondPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,13,"Sample 13",Constant.MainHall,Constant.CBSConductor),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,14,"Sample 14",Constant.MainHall,Constant.CBSReader),

                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,15,"Sample 15",Constant.MainHall,Constant.AudioVideo,115, "Sample 115"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,16,"Sample 16",Constant.MainHall,Constant.Zoom,116, "Sample 116"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,17,"Sample 17",Constant.MainHall,Constant.MicRoving,117, "Sample 117"),
                      }
            };
            long id = 1;
            foreach (var item in midWeekSchedule.MidWeekScheduleItems)
            {
                item.MidWeekScheduleItemId = id;
                id++;
            }
            var response = service.Edit(midWeekSchedule);
            Assert.IsFalse(response.IsSuccess);
        }

        [Test]
        public void Edit_Schedule_Meeting_Validate_4_5()
        {
            var midWeekSchedule = new MidWeekMeetingScheduleDto()
            {
                Attendance = 0,
                CongregationId = 1,
                CreatedBy = 1,
                MidWeekScheduleId = 0,
                ScheduledDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                MidWeekScheduleItems = new List<MidWeekScheduleItemDto>()
                      {
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,2,"Sample 2",Constant.MainHall,Constant.OpeningPrayer),
                          new MidWeekScheduleItemDto(Constant.OPENING,3,"Sample 3",Constant.MainHall,Constant.ClosingPrayer),

                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,4,"Sample 4",Constant.MainHall,Constant.InstructionalTalk),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,5,"Sample 5",Constant.MainHall,Constant.SpiritualGem),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,6,"Sample 6",Constant.MainHall,Constant.BibleReading),

                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 7",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,8,"Sample 8",Constant.MainHall,Constant.FirstPart,88,"sample 88"),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,9,"Sample 9",Constant.MainHall,Constant.SecondPart,99,"sample 99"),

                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,4,"Sample 10",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,11,"Sample 11",Constant.MainHall,Constant.FirstPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,12,"Sample 12",Constant.MainHall,Constant.SecondPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,13,"Sample 13",Constant.MainHall,Constant.CBSConductor),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,14,"Sample 14",Constant.MainHall,Constant.CBSReader),

                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,15,"Sample 15",Constant.MainHall,Constant.AudioVideo,115, "Sample 115"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,16,"Sample 16",Constant.MainHall,Constant.Zoom,116, "Sample 116"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,17,"Sample 17",Constant.MainHall,Constant.MicRoving,117, "Sample 117"),
                      }
            };
            long id = 1;
            foreach (var item in midWeekSchedule.MidWeekScheduleItems)
            {
                item.MidWeekScheduleItemId = id;
                id++;
            }
            var response = service.Edit(midWeekSchedule);
            Assert.IsFalse(response.IsSuccess);
        }

        [Test]
        public void Edit_Schedule_Meeting_Validate_6()
        {
            var midWeekSchedule = new MidWeekMeetingScheduleDto()
            {
                Attendance = 0,
                CongregationId = 1,
                CreatedBy = 1,
                MidWeekScheduleId = 0,
                ScheduledDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                MidWeekScheduleItems = new List<MidWeekScheduleItemDto>()
                      {
                          new MidWeekScheduleItemDto(Constant.OPENING,1,"Sample",Constant.MainHall,Constant.Chairman),
                          new MidWeekScheduleItemDto(Constant.OPENING,2,"Sample 2",Constant.MainHall,Constant.OpeningPrayer),
                          new MidWeekScheduleItemDto(Constant.OPENING,3,"Sample 3",Constant.MainHall,Constant.ClosingPrayer),

                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,4,"Sample 4",Constant.MainHall,Constant.InstructionalTalk),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,5,"Sample 5",Constant.MainHall,Constant.SpiritualGem),
                          new MidWeekScheduleItemDto(Constant.TREASUREFROMGODSWORD,6,"Sample 6",Constant.MainHall,Constant.BibleReading),

                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 7",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,7,"Sample 8",Constant.MainHall,Constant.FirstPart,88,"sample 88"),
                          new MidWeekScheduleItemDto(Constant.APPLYYOURSELFTOTHEFIELDMINISTRY,9,"Sample 9",Constant.MainHall,Constant.SecondPart,99,"sample 99"),

                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,10,"Sample 10",Constant.MainHall,Constant.VideoDiscussion),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,11,"Sample 11",Constant.MainHall,Constant.FirstPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,12,"Sample 12",Constant.MainHall,Constant.SecondPart),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,13,"Sample 13",Constant.MainHall,Constant.CBSConductor),
                          new MidWeekScheduleItemDto(Constant.LIVINGASACHRISTIAN,14,"Sample 14",Constant.MainHall,Constant.CBSReader),

                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,15,"Sample 15",Constant.MainHall,Constant.AudioVideo,115, "Sample 115"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,16,"Sample 16",Constant.MainHall,Constant.Zoom,116, "Sample 116"),
                          new MidWeekScheduleItemDto(Constant.ATTENDANTS,17,"Sample 17",Constant.MainHall,Constant.MicRoving,117, "Sample 117"),
                      }
            };
            long id = 1;
            foreach (var item in midWeekSchedule.MidWeekScheduleItems)
            {
                item.MidWeekScheduleItemId = id;
                id++;
            }
            var response = service.Edit(midWeekSchedule);
            Assert.IsFalse(response.IsSuccess);
        }
    }
}
