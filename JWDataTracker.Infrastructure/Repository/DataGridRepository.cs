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
                result.Message = "Error Occured";
            }

            return result;
        }

    }
}
