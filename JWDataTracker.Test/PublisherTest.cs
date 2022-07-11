using JWDataTracker.Application.CongregationUser;
using JWDataTracker.Application.Publisher;
using JWDataTracker.Infrastructure.Repository;
using Moq;
using Newtonsoft.Json;
using entity = JWDataTracker.Infrastructure;

namespace JWDataTracker.Test
{
    public class PublisherTest
    {
        //For Retrieving
        private DataTrackerContext dbContext;
        private IGenericRepository<entity.Publisher> publisherRepo;
        private IGenericRepository<entity.Congregation> congregationRepo;
        private readonly Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        //For Adding
        private readonly Mock<IGenericRepository<entity.Congregation>> congregationRepoMoq = new Mock<IGenericRepository<entity.Congregation>>();
        private readonly Mock<IGenericRepository<entity.Publisher>> publisherRepoMoq = new Mock<IGenericRepository<entity.Publisher>>();
        private IPublisherService publisherService;
        private readonly Mock<IUnitOfWork> unitOfWorkMoq = new Mock<IUnitOfWork>();

        [SetUp]
        public void Setup()
        {
            dbContext = new DataTrackerContext();
            publisherRepo = new GenericRepository<entity.Publisher>(dbContext);
            congregationRepo = new GenericRepository<entity.Congregation>(dbContext);


        }

        [Test]
        public void Add_Publisher_Success()
        {
            congregationRepoMoq.Setup(m => m.GetByID(It.IsAny<long>())).Returns(new entity.Congregation { });

            unitOfWork.Setup(m => m.PublisherRepository).Returns(publisherRepoMoq.Object);
            unitOfWork.Setup(m => m.CongregationRepository).Returns(congregationRepoMoq.Object);
            publisherService = new PublisherService(unitOfWork.Object);

            var publisherDto = new PublisherDto {
                CongregationId = 1,
                FirstName = "Francis",
                LastName = "Cebu",
                IsElder = false,
                IsMs = false,
                IsRp = false,
                IsUnBaptized = false,
                GroupNumber = 1
            };
            var result = publisherService.Add(publisherDto);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void Add_Publisher_CongregationNotExisting()
        {
            var _congregationRepoMoq = new Mock<IGenericRepository<entity.Congregation>>();
            unitOfWork.Setup(m => m.PublisherRepository).Returns(publisherRepoMoq.Object);
            unitOfWork.Setup(m => m.CongregationRepository).Returns(_congregationRepoMoq.Object);
            publisherService = new PublisherService(unitOfWork.Object);

            var publisherDto = new PublisherDto
            {
                CongregationId = 1,
                FirstName = "Francis",
                LastName = "Cebu",
                IsElder = false,
                IsMs = false,
                IsRp = false,
                IsUnBaptized = false,
                GroupNumber = 1
            };
            var result = publisherService.Add(publisherDto);
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void Add_Publisher_No_FirstName_LastName()
        {
            congregationRepoMoq.Setup(m => m.GetByID(It.IsAny<long>())).Returns(new entity.Congregation { });

            unitOfWork.Setup(m => m.PublisherRepository).Returns(publisherRepoMoq.Object);
            unitOfWork.Setup(m => m.CongregationRepository).Returns(congregationRepoMoq.Object);
            publisherService = new PublisherService(unitOfWork.Object);

            var publisherDto = new PublisherDto
            {
                CongregationId = 1,
                FirstName = "",
                LastName = "",
                IsElder = false,
                IsMs = false,
                IsRp = false,
                IsUnBaptized = false,
                GroupNumber = 1
            };
            var result = publisherService.Add(publisherDto);
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void Add_Publisher_Both_Elder_MS()
        {
            congregationRepoMoq.Setup(m => m.GetByID(It.IsAny<long>())).Returns(new entity.Congregation { });

            unitOfWork.Setup(m => m.PublisherRepository).Returns(publisherRepoMoq.Object);
            unitOfWork.Setup(m => m.CongregationRepository).Returns(congregationRepoMoq.Object);
            publisherService = new PublisherService(unitOfWork.Object);

            var publisherDto = new PublisherDto
            {
                CongregationId = 1,
                FirstName = "Francis",
                LastName = "Cebu",
                IsElder = true,
                IsMs = true,
                IsRp = false,
                IsUnBaptized = false,
                GroupNumber = 1
            };
            var result = publisherService.Add(publisherDto);
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void Add_Publisher_Both_RP_Unbaptize()
        {
            congregationRepoMoq.Setup(m => m.GetByID(It.IsAny<long>())).Returns(new entity.Congregation { });

            unitOfWork.Setup(m => m.PublisherRepository).Returns(publisherRepoMoq.Object);
            unitOfWork.Setup(m => m.CongregationRepository).Returns(congregationRepoMoq.Object);
            publisherService = new PublisherService(unitOfWork.Object);

            var publisherDto = new PublisherDto
            {
                CongregationId = 1,
                FirstName = "Francis",
                LastName = "Cebu",
                IsElder = false,
                IsMs = false,
                IsRp = true,
                IsUnBaptized = true,
                GroupNumber = 1
            };
            var result = publisherService.Add(publisherDto);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
