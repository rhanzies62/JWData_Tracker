using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        GenericRepository<Congregation> CongregationRepository { get; }
        GenericRepository<CongregationUser> CongregationUserRepository { get; }
        GenericRepository<MidWeekSchedule> MidWeekScheduleRepository { get; }
        GenericRepository<MidWeekScheduleItem> MidWeekScheduleItemRepository { get; }
        GenericRepository<Publisher> PublisherRepository { get; }
        GenericRepository<ServiceReport> ServiceReportRepository { get; }
        void Save();
    }
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DataTrackerContext context = new DataTrackerContext();
        private GenericRepository<Congregation> congregationRepository;
        private GenericRepository<CongregationUser> conregationUserRepository;
        private GenericRepository<MidWeekSchedule> midWeekScheduleRepository;
        private GenericRepository<MidWeekScheduleItem> midWeekScheduleItemRepository;
        private GenericRepository<Publisher> publisherRepository;
        private GenericRepository<ServiceReport> serviceReportRepository;

        public GenericRepository<Congregation> CongregationRepository
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
        public GenericRepository<CongregationUser> CongregationUserRepository
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
        public GenericRepository<MidWeekSchedule> MidWeekScheduleRepository
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
        public GenericRepository<MidWeekScheduleItem> MidWeekScheduleItemRepository
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
        public GenericRepository<Publisher> PublisherRepository
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
        public GenericRepository<ServiceReport> ServiceReportRepository
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
    }
}
