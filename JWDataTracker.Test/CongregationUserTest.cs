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
            dbContext = new DataTrackerContext("DataSource=DataTracker.db");
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
                Username = "adelacruz",
                Password = "ferndale"
            });

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void Login_Failed()
        {
            var result = congregationUserService.GetUserByUsernameAndPassword(new CongregationUserDto
            {
                Username = "adelacruz",
                Password = "ferndale1"
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
                CreatedDate = DateTime.UtcNow,
                Email = "TestEmail@email.com",
                Password = "ferndale",
                PublisherId = 1,
                RoleId = 1,
                Username = "mgutierrez",
                Salt = "ferndale67",
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
                CreatedDate = DateTime.UtcNow,
                Email = "TestEmail@email.com",
                Password = "ferndale",
                PublisherId = 1,
                RoleId = 1,
                Username = "mgutierrez",
                Salt = "ferndale67",
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
                CreatedDate = DateTime.UtcNow,
                Email = "TestEmail@email.com",
                Password = "ferndale",
                PublisherId = 1,
                RoleId = 1,
                Username = "adelacruz",
                Salt = "ferndale67",
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
                CreatedDate = DateTime.UtcNow,
                Email = "TestEmail@email.com",
                Password = "ferndale",
                PublisherId = 999,
                RoleId = 1,
                Username = "mgutierrez",
                Salt = "ferndale67",
            });

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void UpdateExisting_User_Success()
        {
            congregationUserRepoMoq.Setup(m => m.GetByID(It.IsAny<long>())).Returns(new entity.CongregationUser());
            unitOfWorkMoq.Setup(m => m.CongregationUserRepository).Returns(congregationUserRepoMoq.Object);
            congregationUserServiceMoq = new CongregationUserService(unitOfWorkMoq.Object);

            var result = congregationUserServiceMoq.Edit(new CongregationUserDto { 
                 CongregationUserId = 1,
                 Username = "adelacruz",
                 Email = "adelacruz@gmail.com"
            });

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void UpdateExisting_User_Failed_User_Not_Existing()
        {
            unitOfWorkMoq.Setup(m => m.CongregationUserRepository).Returns(congregationUserRepo);
            congregationUserServiceMoq = new CongregationUserService(unitOfWorkMoq.Object);

            var result = congregationUserServiceMoq.Edit(new CongregationUserDto
            {
                CongregationUserId = 999,
                Username = "adelacruz",
                Email = "adelacruz@gmail.com"
            });

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void CreateNew_User_PassingNull()
        {
            unitOfWorkMoq.Setup(m => m.CongregationUserRepository).Returns(congregationUserRepoMoq.Object);
            unitOfWorkMoq.Setup(m => m.PublisherRepository).Returns(publisherRepo);
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepo);
            congregationUserServiceMoq = new CongregationUserService(unitOfWorkMoq.Object);

            var result = congregationUserServiceMoq.Add(null);

            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void CreateNew_User_NoUsernam()
        {
            unitOfWorkMoq.Setup(m => m.CongregationUserRepository).Returns(congregationUserRepoMoq.Object);
            unitOfWorkMoq.Setup(m => m.PublisherRepository).Returns(publisherRepo);
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepo);
            congregationUserServiceMoq = new CongregationUserService(unitOfWorkMoq.Object);

            Assert.IsFalse(congregationUserServiceMoq.Add(new CongregationUserDto
            {
                CongregationId = 1,
                PublisherId = 1
            }).IsSuccess);
        }

        [Test]
        public void CreateNew_User_NoPassword()
        {
            unitOfWorkMoq.Setup(m => m.CongregationUserRepository).Returns(congregationUserRepoMoq.Object);
            unitOfWorkMoq.Setup(m => m.PublisherRepository).Returns(publisherRepo);
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepo);
            congregationUserServiceMoq = new CongregationUserService(unitOfWorkMoq.Object);

            Assert.IsFalse(congregationUserServiceMoq.Add(new CongregationUserDto
            {
                CongregationId = 1,
                PublisherId = 1,
                Username = "johndoe"
            }).IsSuccess);
        }

        [Test]
        public void CreateNew_User_NoRole()
        {
            unitOfWorkMoq.Setup(m => m.CongregationUserRepository).Returns(congregationUserRepoMoq.Object);
            unitOfWorkMoq.Setup(m => m.PublisherRepository).Returns(publisherRepo);
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepo);
            congregationUserServiceMoq = new CongregationUserService(unitOfWorkMoq.Object);
            Assert.IsFalse(congregationUserServiceMoq.Add(new CongregationUserDto
            {
                CongregationId = 1,
                PublisherId = 1,
                Username = "johndoe",
                Password = "ferndale"
            }).IsSuccess);
        }
    }
}