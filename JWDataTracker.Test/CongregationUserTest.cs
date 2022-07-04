using JWDataTracker.Application.CongregationUser;
using JWDataTracker.Infrastructure.Repository;
using Moq;
using Newtonsoft.Json;
using entity = JWDataTracker.Infrastructure;
namespace JWDataTracker.Test
{
    public class Tests
    {
        //For Retrieving
        private DataTrackerContext dbContext;
        private IGenericRepository<entity.Publisher> publisherRepo;
        private IGenericRepository<entity.CongregationUser> congregationUserRepo;
        private IGenericRepository<entity.Congregation> congregationRepo;
        private ICongregationUserService congregationUserService;
        private readonly Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        //For Adding
        private readonly Mock<IGenericRepository<entity.CongregationUser>> congregationUserRepoMoq = new Mock<IGenericRepository<entity.CongregationUser>>();
        private readonly Mock<IGenericRepository<entity.Publisher>> publisherRepoMoq = new Mock<IGenericRepository<entity.Publisher>>();
        private ICongregationUserService congregationUserServiceMoq;
        private readonly Mock<IUnitOfWork> unitOfWorkMoq = new Mock<IUnitOfWork>();

        [SetUp]
        public void Setup()
        {
            dbContext = new DataTrackerContext();
            publisherRepo = new GenericRepository<entity.Publisher>(dbContext);
            congregationUserRepo = new GenericRepository<entity.CongregationUser>(dbContext);
            congregationRepo = new GenericRepository<entity.Congregation>(dbContext);

            unitOfWork.Setup(m => m.PublisherRepository).Returns(publisherRepo);
            unitOfWork.Setup(m => m.CongregationUserRepository).Returns(congregationUserRepo);
            congregationUserService = new CongregationUserService(unitOfWork.Object);
        }

        [Test]
        public void Login_Success()
        {
            var result = congregationUserService.GetUserByUsernameAndPassword(new CongregationUserDto
            {
                Username = "rdelacruz",
                Password = "12345"
            });

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void Login_Failed()
        {
            var result = congregationUserService.GetUserByUsernameAndPassword(new CongregationUserDto
            {
                Username = "rdelacruz",
                Password = "123451"
            });

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void CreateNew_User_Success()
        {
            unitOfWorkMoq.Setup(m => m.CongregationUserRepository).Returns(congregationUserRepoMoq.Object);
            unitOfWorkMoq.Setup(m => m.PublisherRepository).Returns(publisherRepo);
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepo);
            congregationUserServiceMoq = new CongregationUserService(unitOfWorkMoq.Object);

            var result = congregationUserServiceMoq.Add(new CongregationUserDto
            {
                CongregationId = 1,
                CreatedDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                Email = "TestEmail@email.com",
                Password = "12345",
                PublisherId = 1,
                RoleId = 1,
                Username = "mgutierrez",
                Salt = "1234567",
            });

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void CreateNew_User_Failed_CongregationIsNotExisting()
        {
            unitOfWorkMoq.Setup(m => m.CongregationUserRepository).Returns(congregationUserRepoMoq.Object);
            unitOfWorkMoq.Setup(m => m.PublisherRepository).Returns(publisherRepo);
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepo);
            congregationUserServiceMoq = new CongregationUserService(unitOfWorkMoq.Object);

            var result = congregationUserServiceMoq.Add(new CongregationUserDto
            {
                CongregationId = 2,
                CreatedDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                Email = "TestEmail@email.com",
                Password = "12345",
                PublisherId = 1,
                RoleId = 1,
                Username = "mgutierrez",
                Salt = "1234567",
            });

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void CreateNew_User_Failed_UsernameExisting()
        {
            unitOfWorkMoq.Setup(m => m.CongregationUserRepository).Returns(congregationUserRepo);
            unitOfWorkMoq.Setup(m => m.PublisherRepository).Returns(publisherRepo);
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepo);
            congregationUserServiceMoq = new CongregationUserService(unitOfWorkMoq.Object);

            var result = congregationUserServiceMoq.Add(new CongregationUserDto
            {
                CongregationId = 1,
                CreatedDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                Email = "TestEmail@email.com",
                Password = "12345",
                PublisherId = 1,
                RoleId = 1,
                Username = "rdelacruz",
                Salt = "1234567",
            });

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void CreateNew_User_Failed_PublisherNotExisting()
        {
            unitOfWorkMoq.Setup(m => m.CongregationUserRepository).Returns(congregationUserRepoMoq.Object);
            unitOfWorkMoq.Setup(m => m.PublisherRepository).Returns(publisherRepo);
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepo);
            congregationUserServiceMoq = new CongregationUserService(unitOfWorkMoq.Object);

            var result = congregationUserServiceMoq.Add(new CongregationUserDto
            {
                CongregationId = 1,
                CreatedDate = JsonConvert.SerializeObject(DateTime.UtcNow),
                Email = "TestEmail@email.com",
                Password = "12345",
                PublisherId = 2,
                RoleId = 1,
                Username = "mgutierrez",
                Salt = "1234567",
            });

            Assert.IsFalse(result.IsSuccess);
        }
    }
}