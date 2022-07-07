using JWDataTracker.Application.Congregation;
using JWDataTracker.Application.CongregationUser;
using JWDataTracker.Infrastructure.Repository;
using Moq;
using Newtonsoft.Json;
using entity = JWDataTracker.Infrastructure;

namespace JWDataTracker.Test
{
    public class CongregationTest
    {
        private DataTrackerContext dbContext;
        private IGenericRepository<entity.Congregation> congregationRepo;
        private readonly Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();

        private readonly Mock<IGenericRepository<entity.Congregation>> congregationRepoMoq = new Mock<IGenericRepository<entity.Congregation>>();
        private readonly Mock<IUnitOfWork> unitOfWorkMoq = new Mock<IUnitOfWork>();
        private ICongregationService congregationService;

        [SetUp]
        public void Setup()
        {
            dbContext = new DataTrackerContext();
            congregationRepo = new GenericRepository<entity.Congregation>(dbContext);
        }

        [Test]
        public void AddCongregation_Success()
        {
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepoMoq.Object);
            congregationService = new CongregationService(unitOfWorkMoq.Object);

            Assert.IsTrue(congregationService.Add(new CongregationDto { 
                Name = "Ligaya"
            }).IsSuccess);
        }

        [Test]
        public void AddCongregation_Name_Existing()
        {
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepo);
            congregationService = new CongregationService(unitOfWorkMoq.Object);

            Assert.IsFalse(congregationService.Add(new CongregationDto
            {
                Name = "Ferndale"
            }).IsSuccess);
        }

        [Test]
        public void AddCongregation_Passing_Null()
        {
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepo);
            congregationService = new CongregationService(unitOfWorkMoq.Object);

            Assert.IsFalse(congregationService.Add(null).IsSuccess);
        }

        [Test]
        public void AddCongregation_Empty_Object()
        {
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepoMoq.Object);
            congregationService = new CongregationService(unitOfWorkMoq.Object);

            Assert.IsFalse(congregationService.Add(new CongregationDto() { 
                Name = ""
            }).IsSuccess);
        }

        [Test]
        public void EditCongregation_Success()
        {
            congregationRepoMoq.Setup(m => m.GetByID(It.IsAny<long>())).Returns(new entity.Congregation());
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepoMoq.Object);
            congregationService = new CongregationService(unitOfWorkMoq.Object);

            Assert.IsTrue(congregationService.Edit(new CongregationDto
            {
                CongregationId = 1,
                Name = "Ligaya"
            }).IsSuccess);
        }

        [Test]
        public void EditCongregation_NotExisting()
        {
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepo);
            congregationService = new CongregationService(unitOfWorkMoq.Object);

            Assert.IsFalse(congregationService.Edit(new CongregationDto
            {
                CongregationId = 999,
                Name = "Ligaya"
            }).IsSuccess);
        }

        [Test]
        public void EditCongregation_NameNotExisting()
        {
            congregationRepoMoq.Setup(m => m.GetByID(It.IsAny<long>())).Returns(new entity.Congregation());
            unitOfWorkMoq.Setup(m => m.CongregationRepository).Returns(congregationRepoMoq.Object);
            congregationService = new CongregationService(unitOfWorkMoq.Object);

            Assert.IsFalse(congregationService.Edit(new CongregationDto
            {
                CongregationId = 1,
                Name = ""
            }).IsSuccess);
        }
    }
}
