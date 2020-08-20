using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ApplicationLogger;
using RESSWATCH;

namespace BL
{
    public class Product
    {
        #region Methods
        public static bool Add(string ParentIDP, string ShopifyID, string VarientID, string VarientSKU)
        {
            bool result = false;
            try
            {                
                if (DataAccess.ExecuteNonQuery(CommandType.Text, "INSERT INTO [Product] ( [ParentID],[ShopifyID],[VarientID],[VarientSKU])VALUES ('"+ParentIDP+"','"+ShopifyID+"','"+VarientID+"','"+VarientSKU+"')", null) > 0)
                    result = true;
                else
                    result = false;
            }
            catch (Exception Exp)
            {
                Logger.WriteException(Exp);
            }
            return result;
        }
        
        public static DataTable Get_ProductByparentID(string ParentID)
        {
            DataTable dTResult = new DataTable();
            try
            {
                DataSet dSet = ((DataSet)DataAccess.ExecuteDataSet(CommandType.Text, "Select * from Product where parentid='"+ParentID+"'", null));
                dTResult = dSet.Tables[0];
            }
            catch (Exception Exp)
            {
                Logger.WriteException(Exp);
            }
            return dTResult;
        }

        public static DataTable GetVarientBySKU(string ParentID, string VarientSKU)
        {
            DataTable dTResult = new DataTable();
            try
            {
                DataSet dSet = ((DataSet)DataAccess.ExecuteDataSet(CommandType.Text, "Select * from Product where  VarientSKU='" + VarientSKU + "'", null));
                dTResult = dSet.Tables[0];
            }
            catch (Exception Exp)
            {
                Logger.WriteException(Exp);
            }
            return dTResult;
        }
        #endregion Methods
    }
    public class Collection
    {
        public static bool Add(string ProductID, string CollectionID, string ShopifyCollectionID)
        {
            bool result = false;
            try
            {
                if (DataAccess.ExecuteNonQuery(CommandType.Text, "INSERT INTO [Collection] ( [ProductID],[CollectionID],[ShopifyCollectionID])VALUES ('" + ProductID + "','" + CollectionID + "','" + ShopifyCollectionID + "')", null) > 0)
                    result = true;
                else
                    result = false;
            }
            catch (Exception Exp)
            {
                Logger.WriteException(Exp);
            }
            return result;
        }


        public static string GetShopifyCollectionID(string CollectionID)
        {
            DataTable dTResult = new DataTable();
            try
            {
                DataSet dSet = ((DataSet)DataAccess.ExecuteDataSet(CommandType.Text, "Select * from Collection where CollectionID='" + CollectionID + "'", null));
                dTResult = dSet.Tables[0];
              if (dTResult.Rows.Count > 0 )
                    return dTResult.Rows[0]["ShopifyCollectionID"].ToString();

                return "";
            }
            catch (Exception Exp)
            {
                Logger.WriteException(Exp);
            }
            return "";
        }

        public static string GetShopifyCollectID(string CollectionID, string ProductID)
        {
            DataTable dTResult = new DataTable();
            try
            {
                DataSet dSet = ((DataSet)DataAccess.ExecuteDataSet(CommandType.Text, "Select * from Collection where CollectionID='" + CollectionID + $"' AND ProductID = '{ProductID}'", null));
                dTResult = dSet.Tables[0];
                if (dTResult.Rows.Count > 0)
                    return dTResult.Rows[0]["ShopifyCollectID"].ToString();

                return "";
            }
            catch (Exception Exp)
            {
                Logger.WriteException(Exp);
            }
            return "";
        }

        public static void UpdateShopifyCollectID(string CollectionID, string ProductID, string CollectID)
        {
            DataTable dTResult = new DataTable();
            try
            {
                DataAccess.ExecuteNonQuery(CommandType.Text, $"Update Collection Set ShopifyCollectID= '{CollectID}' where CollectionID = '{CollectionID}' AND ProductID = '{ProductID}'", null);
            }
            catch (Exception Exp)
            {
                Logger.WriteException(Exp);
            }
            return;
        }

        public static void UpdateProfitMargin(string CollectionID, string ProfitMargin)
        {
            DataTable dTResult = new DataTable();
            try
            {
                DataAccess.ExecuteNonQuery(CommandType.Text, $"Update Collection Set ProfitMargin= '{ProfitMargin}' where CollectionID = '{CollectionID}'", null);
            }
            catch (Exception Exp)
            {
                Logger.WriteException(Exp);
            }
            return;
        }

        public static DataTable GetDistinctCollection()
        {
            DataTable dTResult = new DataTable();
            try
            {
                DataSet dSet = ((DataSet)DataAccess.ExecuteDataSet(CommandType.Text, "Select Distinct CollectionID from Collection", null));
                dTResult = dSet.Tables[0];
            }
            catch (Exception Exp)
            {
                Logger.WriteException(Exp);
            }
            return dTResult;
        }
    }
}