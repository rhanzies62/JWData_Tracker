using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Congregation> CongregationRepository { get; }
        IGenericRepository<CongregationUser> CongregationUserRepository { get; }
        IGenericRepository<MidWeekSchedule> MidWeekScheduleRepository { get; }
        IGenericRepository<MidWeekScheduleItem> MidWeekScheduleItemRepository { get; }
        IGenericRepository<Publisher> PublisherRepository { get; }
        IGenericRepository<ServiceReport> ServiceReportRepository { get; }
        IDataGridRepository DataGridRepository { get; }
        void Save();
        DataTrackerContext Database { get; }
    }
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DataTrackerContext context = new DataTrackerContext();
        private IGenericRepository<Congregation> congregationRepository;
        private IGenericRepository<CongregationUser> conregationUserRepository;
        private IGenericRepository<MidWeekSchedule> midWeekScheduleRepository;
        private IGenericRepository<MidWeekScheduleItem> midWeekScheduleItemRepository;
        private IGenericRepository<Publisher> publisherRepository;
        private IGenericRepository<ServiceReport> serviceReportRepository;
        private IDataGridRepository dataGridRepository;

        public IGenericRepository<Congregation> CongregationRepository
        {
            get
            {

                if (this.congregationRepository == null)
                {
                    this.congregationRepository = new GenericRepository<Congregation>(context);
                }
                return congregationRepository;
            }
        }
        public IGenericRepository<CongregationUser> CongregationUserRepository
        {
            get
            {

                if (this.conregationUserRepository == null)
                {
                    this.conregationUserRepository = new GenericRepository<CongregationUser>(context);
                }
                return conregationUserRepository;
            }
        }
        public IGenericRepository<MidWeekSchedule> MidWeekScheduleRepository
        {
            get
            {

                if (this.midWeekScheduleRepository == null)
                {
                    this.midWeekScheduleRepository = new GenericRepository<MidWeekSchedule>(context);
                }
                return midWeekScheduleRepository;
            }
        }
        public IGenericRepository<MidWeekScheduleItem> MidWeekScheduleItemRepository
        {
            get
            {

                if (this.midWeekScheduleItemRepository == null)
                {
                    this.midWeekScheduleItemRepository = new GenericRepository<MidWeekScheduleItem>(context);
                }
                return midWeekScheduleItemRepository;
            }
        }
        public IGenericRepository<Publisher> PublisherRepository
        {
            get
            {

                if (this.publisherRepository == null)
                {
                    this.publisherRepository = new GenericRepository<Publisher>(context);
                }
                return publisherRepository;
            }
        }
        public IGenericRepository<ServiceReport> ServiceReportRepository
        {
            get
            {

                if (this.serviceReportRepository == null)
                {
                    this.serviceReportRepository = new GenericRepository<ServiceReport>(context);
                }
                return serviceReportRepository;
            }
        }
        public IDataGridRepository DataGridRepository
        {
            get
            {

                if (this.dataGridRepository == null)
                {
                    this.dataGridRepository = new DataGridRepository(context);
                }
                return dataGridRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DataTrackerContext Database
        {
            get
            {
                return context;
            }
        }
    }
}
