using JWDataTracker.Application.CongregationUser;
using JWDataTracker.Infrastructure.Repository;
using Moq;
using entity = JWDataTracker.Infrastructure;
namespace JWDataTracker.Test
{
    public class Tests
    {
        private readonly Mock<IGenericRepository<entity.CongregationUser>> congregationRepoMoq = new Mock<IGenericRepository<entity.CongregationUser>>();
        private readonly Mock<IGenericRepository<entity.Publisher>> publisherRepoMoq = new Mock<IGenericRepository<entity.Publisher>>();
        private readonly Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            unitOfWork.Setup(m => m.PublisherRepository).Returns(new GenericRepository<entity.Publisher>(new DataTrackerContext()));
            unitOfWork.Setup(m => m.CongregationUserRepository).Returns(new GenericRepository<entity.CongregationUser>(new DataTrackerContext()));
            var service = new CongregationUserService(unitOfWork.Object);
            var result = service.GetUserByUsernameAndPassword(new CongregationUserDto {
                Username = "rdelacruz",
                Password = "12345"
            });

            Assert.IsTrue(result.IsSuccess);
        }
    }
}