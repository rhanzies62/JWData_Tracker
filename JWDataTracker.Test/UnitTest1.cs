using JWDataTracker.Application.CongregationUser;
using JWDataTracker.Infrastructure.Repository;
using Moq;
using entity = JWDataTracker.Infrastructure;
namespace JWDataTracker.Test
{
    public class Tests
    {
        //For Retrieving
        private DataTrackerContext dbContext;
        private IGenericRepository<entity.Publisher> publisherRepo;
        private IGenericRepository<entity.CongregationUser> congregationRepo;

        //For Adding
        private readonly Mock<IGenericRepository<entity.CongregationUser>> congregationRepoMoq = new Mock<IGenericRepository<entity.CongregationUser>>();
        private readonly Mock<IGenericRepository<entity.Publisher>> publisherRepoMoq = new Mock<IGenericRepository<entity.Publisher>>();
        private readonly Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        [SetUp]
        public void Setup()
        {
            dbContext = new DataTrackerContext();
            publisherRepo = new GenericRepository<entity.Publisher>(dbContext);
            congregationRepo = new GenericRepository<entity.CongregationUser>(dbContext);
        }

        [Test]
        public void Login_Success()
        {
            unitOfWork.Setup(m => m.PublisherRepository).Returns(publisherRepo);
            unitOfWork.Setup(m => m.CongregationUserRepository).Returns(congregationRepo);
            var service = new CongregationUserService(unitOfWork.Object);
            var result = service.GetUserByUsernameAndPassword(new CongregationUserDto {
                Username = "rdelacruz",
                Password = "12345"
            });

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void Login_Failed()
        {

        }
    }
}