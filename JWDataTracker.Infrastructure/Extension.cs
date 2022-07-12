using JWDataTracker.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Infrastructure
{
    public static class Extension
    {
        public static string ParseQuery(this string query, GridFilter filter, string defaultOrderBy)
        {
            if (filter.Searchs != null && filter.Searchs.Any())
            {
                string where = "Where ";
                filter.Searchs.ForEach((search) =>
                {
                    string appendType = string.IsNullOrEmpty(search.AppendType) ? "or" : search.AppendType;
                    if (search.Operator == "like")
                    {
                        where += $" {search.Field} {search.Operator} '%{search.Value}%' {appendType}";
                    }
                    else
                    {
                        where += $" {search.Field} {search.Operator} '{search.Value}' {appendType}";
                    }
                });
                where = where.Substring(0, where.Length - 3);
                query = query.Replace(Resource.QueryToken_Where, where);
            }
            else { query = query.Replace(Resource.QueryToken_Where, string.Empty); }

            if (!string.IsNullOrEmpty(filter.Field) && !string.IsNullOrEmpty(filter.Direction))
            {
                query = query.Replace(Resource.QueryToken_OrderBy, $"{filter.Field} {filter.Direction}");
            }
            else
            {
                query = query.Replace(Resource.QueryToken_OrderBy, defaultOrderBy);
            }

            query = query.Replace(Resource.QueryToken_Take, filter.Take.ToString());
            query = query.Replace(Resource.QueryToken_Skip, filter.Skip.ToString());

            return query;
        }

        public static List<T> DataReaderMapToList<T>(this System.Data.Common.DbDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (HasColumn(dr, prop.Name))
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
