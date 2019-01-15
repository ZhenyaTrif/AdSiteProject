using Common.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Wrappers
{
    public class CategoryWrapperDA : BaseConnection, ICategoryWrapper
    {
        public IEnumerable<Category> GetAll()
        {
            var parameters = new List<SqlParameter>();
            var dataReader = sqlHelper.GetDataReader("Category_GetAll",
                CommandType.StoredProcedure, null, out connection);
            try
            {
                var categories = new List<Category>();
                while (dataReader.Read())
                {
                    var category = new Category
                    {
                        Id = Convert.ToInt32(dataReader["Id"].ToString()),
                        CategoryName = dataReader["CategoryName"].ToString()
                    };
                    categories.Add(category);
                }
                return categories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
        }

        public void Delete(int id)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(sqlHelper.CreateParameter("@Id", id, DbType.Int32));

            sqlHelper.Delete("Category_Delete", CommandType.StoredProcedure, parameters.ToArray());
        }

        public void Update(Category category)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(sqlHelper.CreateParameter("@Id", category.Id, DbType.Int32));
            parameters.Add(sqlHelper.CreateParameter("@CategoryName", category.CategoryName, DbType.String));

            sqlHelper.Update("Category_Update", CommandType.StoredProcedure, parameters.ToArray());
        }

        public int Insert(Category category)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(sqlHelper.CreateParameter("@CategoryName", category.CategoryName, DbType.String));
            int lastId = 0;

            sqlHelper.Insert("Category_Insert", CommandType.StoredProcedure, parameters.ToArray(), out lastId);
            return lastId;
        }
    }
}
