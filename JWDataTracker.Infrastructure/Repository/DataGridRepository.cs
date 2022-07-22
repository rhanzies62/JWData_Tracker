using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWDataTracker.Helper;
using JWDataTracker.Domain.Grid;
using Microsoft.EntityFrameworkCore;

namespace JWDataTracker.Infrastructure.Repository
{
    public interface IDataGridRepository
    {
        GridResultGeneric<PublisherGridDto> ListPublishers(GridFilter filter);
        GridResultGeneric<PublisherRecentPartGrid> ListPublisherRecentPart(GridFilter filter, int publisherId);
    }
    public class DataGridRepository : IDataGridRepository
    {
        internal DataTrackerContext context;

        public DataGridRepository(DataTrackerContext _context)
        {
            this.context = _context;
        }
        public GridResultGeneric<PublisherGridDto> ListPublishers(GridFilter filter)
        {
            var result = new GridResultGeneric<PublisherGridDto>();
            var cmd = context.Database.GetDbConnection().CreateCommand();
            try
            {
                context.Database.OpenConnection();
                cmd.CommandText = Resource.PublisherGrid.ParseQuery(filter, "PublisherId");
                var reader = cmd.ExecuteReader();
                result.Data = reader.DataReaderMapToList<PublisherGridDto>().ToList();
                reader.Dispose();
                context.Database.CloseConnection();

                context.Database.OpenConnection();
                cmd.CommandText = Resource.PublisherGridCount.ParseQuery(filter, "PublisherId"); ;
                var countReader = cmd.ExecuteReader();
                countReader.Read();
                result.TotalCount = int.Parse(countReader.GetValue(0).ToString());
                countReader.Dispose();
                context.Database.CloseConnection();
            }
            catch (Exception e)
            {
                result.TotalCount = 0;
                result.Data = new List<PublisherGridDto>();
                result.IsSuccess = false;
                result.Message = $"{e.GetBaseException().Message},{e.StackTrace.ToString()}";
            }

            return result;
        }

        public GridResultGeneric<PublisherRecentPartGrid> ListPublisherRecentPart(GridFilter filter,int publisherId)
        {
            var result = new GridResultGeneric<PublisherRecentPartGrid>();
            var cmd = context.Database.GetDbConnection().CreateCommand();
            try
            {
                context.Database.OpenConnection();
                cmd.CommandText = Resource.PublisherRecentParts.ParseQuery(filter, "ScheduledDate").Replace("##PublisherId##",publisherId.ToString());
                var reader = cmd.ExecuteReader();
                result.Data = reader.DataReaderMapToList<PublisherRecentPartGrid>().ToList();
                reader.Dispose();
                context.Database.CloseConnection();

                context.Database.OpenConnection();
                cmd.CommandText = Resource.PublisherRecentPartsCount.ParseQuery(filter, "ScheduledDate").Replace("##PublisherId##", publisherId.ToString());
                var countReader = cmd.ExecuteReader();
                countReader.Read();
                result.TotalCount = int.Parse(countReader.GetValue(0).ToString());
                countReader.Dispose();
                context.Database.CloseConnection();
            }
            catch (Exception e)
            {
                result.TotalCount = 0;
                result.Data = new List<PublisherRecentPartGrid>();
                result.IsSuccess = false;
                result.Message = $"{e.GetBaseException().Message},{e.StackTrace.ToString()}";
            }

            return result;
        }

    }
}
