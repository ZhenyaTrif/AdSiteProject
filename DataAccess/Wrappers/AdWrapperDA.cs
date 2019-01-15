using Common.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Wrappers
{
    public class AdWrapperDA : BaseConnection, IAdWrapper
    {
        public IEnumerable<Ad> GetAll()
        {
            var parameters = new List<SqlParameter>();
            var dataReader = sqlHelper.GetDataReader("Ad_GetAll",
                CommandType.StoredProcedure, null, out connection);
            try
            {
                var ads = new List<Ad>();
                while (dataReader.Read())
                {
                    var ad = new Ad
                    {
                        Id = Convert.ToInt32(dataReader["Id"]),
                        AdName = dataReader["AdName"].ToString(),
                        Info = dataReader["Info"].ToString(),
                        CreateDate = Convert.ToDateTime(dataReader["CreateDate"]).Date,
                        PicPath = dataReader["PicPath"].ToString(),
                        CategoryId = Convert.ToInt32(dataReader["CategoryId"]),
                        AuthorName = dataReader["AuthorName"].ToString(),
                        AuthorEmail = dataReader["AuthorEmail"].ToString(),
                        AuthorPhone = dataReader["AuthorPhone"].ToString(),
                        ProductPlacement = dataReader["ProductPlacement"].ToString(),
                        ProductState = dataReader["ProductState"].ToString(),
                        ProductType = dataReader["ProductType"].ToString(),
                        UserId = dataReader["UserId"].ToString()
                    };
                    ads.Add(ad);
                }
                return ads;
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

            sqlHelper.Delete("Ad_Delete", CommandType.StoredProcedure, parameters.ToArray());
        }

        public void Update(Ad ad)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(sqlHelper.CreateParameter("@Id", ad.Id, DbType.Int32));
            parameters.Add(sqlHelper.CreateParameter("@AdName", ad.AdName, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@Info", ad.Info, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@CreateDate", ad.CreateDate, DbType.Date));
            parameters.Add(sqlHelper.CreateParameter("@PicPath", ad.PicPath, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@CategoryId", ad.CategoryId, DbType.Int32));
            parameters.Add(sqlHelper.CreateParameter("@AuthorName", ad.AuthorName, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@AuthorEmail", ad.AuthorEmail, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@AuthorPhone", ad.AuthorPhone, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@ProductPlacement", ad.ProductPlacement, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@ProductType", ad.ProductType, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@ProductState", ad.ProductState, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@UserId", ad.UserId, DbType.String));

            sqlHelper.Update("Ad_Update", CommandType.StoredProcedure, parameters.ToArray());
        }

        public void Insert(Ad ad)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(sqlHelper.CreateParameter("@AdName", ad.AdName, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@Info", ad.Info, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@CreateDate", ad.CreateDate, DbType.Date));
            parameters.Add(sqlHelper.CreateParameter("@PicPath", ad.PicPath, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@CategoryId", ad.CategoryId, DbType.Int32));
            parameters.Add(sqlHelper.CreateParameter("@AuthorName", ad.AuthorName, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@AuthorEmail", ad.AuthorEmail, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@AuthorPhone", ad.AuthorPhone, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@ProductPlacement", ad.ProductPlacement, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@ProductType", ad.ProductType, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@ProductState", ad.ProductState, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@UserId", ad.UserId, DbType.String));

            sqlHelper.Insert("Ad_Insert", CommandType.StoredProcedure, parameters.ToArray());
        }
    }
}
