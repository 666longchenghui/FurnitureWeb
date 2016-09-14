using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using System.Data;

namespace BLL.Goods
{
    public class GoodsInfo
    {
        Common.CommonClass Com = new Common.CommonClass();
        /// <summary>
        /// 商品信息分页展示
        /// </summary>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">当前页</param>
        /// <returns></returns>
        public Dictionary<string, object> GetGoodsList(string goodsname, string goodsmodel, string PageSize, string PageIndex)
        {
            string AddSQL = "";

            if (goodsname.Trim() != "")
            {
                AddSQL += " and Mname like '%" + goodsname + "%'";
            }
            //9.14
            if (goodsmodel == "请选择")
            {
                goodsmodel = "";
            }
            if (goodsmodel.Trim() != "")
            {
                AddSQL += " and d.DictionaryID ='" + goodsmodel + "'";
            }
            //每页显示2条
            //当前页
            //  string PageSql = @"SELECT TOP " + PageSize + " Mid,Mname,UnitName,Mnumber,m.Unitid,DataText,Msellingprice,Mnote,MCreateDate FROM Merchandise m inner join Unit u on m.Unitid=u.Unitid inner join Dictionary d on m.DictionaryID=d.DictionaryID WHERE MDelete=0 and(Mid NOT IN(SELECT TOP (" + PageSize + "*(" + PageIndex + "-1)) Mid FROM Merchandise ORDER BY Mid))ORDER BY Mid";
            string PageSQL = @" select * from (select Mid,Mname,UnitName,Mnumber,m.Unitid,DataText,Msellingprice,Mnote,MCreateDate ,ROW_NUMBER() OVER(ORDER BY Mid desc) as rank from Merchandise  m 
             inner join Unit u on m.Unitid=u.Unitid inner join Dictionary d on m.DictionaryID=d.DictionaryID where MDelete=0 " + AddSQL + " ) as t where t.rank  between (((" + PageIndex + " - 1) * " + PageSize + ")+1) and(" + PageIndex + " * " + PageSize + ") ORDER BY Mid desc";
            string PageCount = @"select count(*) from Merchandise  m 
             inner join Unit u on m.Unitid=u.Unitid inner join Dictionary d on m.DictionaryID=d.DictionaryID where MDelete=0 " + AddSQL + "";
            string ComStr = CommonClass.DataTableToJson(Com.Selcets(PageSQL));
            string ComCount = CommonClass.DataTableToJson(Com.Selcets(PageCount));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("ComStr", ComStr);
            dic.Add("ComCount", ComCount);
            return dic;
        }
        /// <summary>
        /// 查询单位表
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Dictionary<string, object> UnitPage(string index, string size, string unitname)
        {
            string AddUnitSQL = "";
            if (unitname.Trim() != "")
            {
                AddUnitSQL += " and UnitName like '%" + unitname + "%' ";
            }
            string Pagesql = "select * from (select *,ROW_NUMBER() OVER(ORDER BY Unitid)AS RANK FROM Unit WHERE UnitDelete=0 " + AddUnitSQL + " ) as t where t.rank between (((" + index + "-1)*" + size + ")+1) and (" + index + "*" + size + ") order by Unitid";
            string Countsql = "select count(*) from Unit where UnitDelete=0";
            string ComStr1 = CommonClass.DataTableToJson(Com.Selcets(Pagesql));
            string ComCount1 = CommonClass.DataTableToJson(Com.Selcets(Countsql));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("ComStr1", ComStr1);
            dic.Add("ComCount1", ComCount1);
            return dic;
        }
        /// <summary>
        /// 伪删除单位（unit）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DelUnit(string id)
        {
            string SelUnitName = "select Unitid from Merchandise  where Unitid=" + id + " and MDelete=0";
            Dictionary<string, object> dic = Common.CommonClass.Select(SelUnitName);
            if (dic != null)
            {
                return Utils.GetResult(300, "该单位已被使用", "false");
            }
            string Del = "Update Unit set UnitDelete=1 where Unitid=" + id + "";
            int count = Common.CommonClass.ExecutionSQL(Del);
            if (count > 0)
            {
                return Utils.GetResult(200, "删除成功");
            }
            else
            {
                return Utils.GetResult(300, "删除失败");
            }
        }
        public string GetUnitName()
        {
            string Selsql = "select Unitid,UnitName from Unit";
            string result = Common.CommonClass.DataTableToJson(Com.Selcets(Selsql));
            return result;
        }
        /// <summary>
        /// 添加单位名称
        /// </summary>
        /// <param name="unitname"></param>
        /// <returns></returns>
        public string AddUnit(string unitname)
        {
            string SelName = "select Unitid,UnitName,UnitDelete from Unit where UnitName='" + unitname + "' and UnitDelete=0";
            Dictionary<string, object> dic = Common.CommonClass.Select(SelName);
            if (dic != null)
            {
                return Utils.GetResult(300, "单位名称已存在，请重新添加新的单位名称");
            }
            else
            {
                SqlParameter[] par = new SqlParameter[]{
                    new SqlParameter ("UnitName",unitname),
                    new SqlParameter ("UnitDelete","0")
                };
                string InsertSql = @"INSERT INTO [Unit]([UnitName],[UnitDelete])VALUES(@UnitName,@UnitDelete)";
                int count = Com.ExecutionSqlPar(InsertSql, par);
                if (count > 0)
                {
                    return Utils.GetResult(200, "添加成功", "true");
                }
                else
                {
                    return Utils.GetResult(300, "添加失败");
                }
            }
        }
        /// <summary>
        /// 根据ID查找单位ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string UpdateUnit(string id)
        {
            string SelId = "select Unitid,UnitName from Unit where Unitid=" + id + "";
            string result = Common.CommonClass.DataTableToJson(Com.Selcets(SelId));
            return result;
        }
        /// <summary>
        /// 保存、修改单位名称 合二为一
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string SaveUnit(string id, string name)
        {
            string SelName = "select Unitid from Unit where UnitName='" + name + "' and UnitDelete=0";
            Dictionary<string, object> dic = Common.CommonClass.Select(SelName);
            if (dic != null)
            {
                return Utils.GetResult(300, "单位名称已存在，请重新添加新的单位名称");
            }
            SqlParameter[] par1;
            string StrSQL = "";
            if (id == null)
            {
                par1 = new SqlParameter[]
                {
                       new SqlParameter ("UnitName",name),new SqlParameter ("UnitDelete","0"),
                };
                StrSQL = @"INSERT INTO [Unit] ([UnitName],[UnitDelete]) VALUES(@UnitName,@UnitDelete)";
            }
            else
            {
                par1 = new SqlParameter[]
                {
                      new SqlParameter("Mid",id),  new SqlParameter ("UnitName",name), new SqlParameter ("UnitDelete","0"),
                };
                StrSQL = @"UPDATE [Unit] SET [UnitName] =@UnitName WHERE  Unitid=" + id + "";
            }
            int count = Com.ExecutionSqlPar(StrSQL, par1);
            if (count > 0)
            {
                return Utils.GetResult(200, "保存成功", "true");
            }
            else
            {
                return Utils.GetResult(300, "保存失败");
            }
        }
        /// <summary>
        /// 商品型号
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Dictionary<string, object> ModelPage(string index, string size, string modelname)
        {
            string AddModelSQL = "";
            if (modelname.Trim() != "")
            {
                AddModelSQL += " and DataText like '%" + modelname + "%' ";
            }
            string Pagesql = "select top " + size + " * from Dictionary where Ddelete=0  " + AddModelSQL + " and (DictionaryID not in(select top (" + size + "*(" + index + "-1)) DictionaryID from Dictionary order by DictionaryID)) order by DictionaryID ";
            string Countsql = "select count(*) from Dictionary where Ddelete=0";
            string ComStr1 = CommonClass.DataTableToJson(Com.Selcets(Pagesql));
            string ComCount1 = CommonClass.DataTableToJson(Com.Selcets(Countsql));
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("ComStr1", ComStr1);
            dic.Add("ComCount1", ComCount1);
            return dic;
        }
        /// <summary>
        /// 删除商品型号（伪删除，删除后Ddelete为1）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DelModel(string id)
        {
            string SelDictionary = "select DictionaryID from Merchandise where DictionaryID=" + id + " and MDelete=0";
            Dictionary<string, object> dic = Common.CommonClass.Select(SelDictionary);
            if (dic != null)
            {
                return Utils.GetResult(300, "型号已被使用");
            }
            string DelSQl = @"UPDATE [Dictionary] SET [Ddelete] =1 WHERE DictionaryID=" + id + "";
            int count = Common.CommonClass.ExecutionSQL(DelSQl);
            if (count > 0)
            {
                return Utils.GetResult(200, "删除成功");
            }
            else
            {
                return Utils.GetResult(300, "删除失败");
            }
        }
        /// <summary>
        /// 根据ID查找型号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string UpdateModel(string id)
        {
            string SelModel = "select DictionaryID,DataText from Dictionary where DictionaryID=" + id + "";
            string result = Common.CommonClass.DataTableToJson(Com.Selcets(SelModel));
            return result;
        }
        //修改商品型号
        public string SaveModel(string id, string ModelData)
        {
            string SelName = "select DictionaryID,DataText,Ddelete from Dictionary where DataText='" + ModelData + "' and Ddelete=0";
            Dictionary<string, object> dic = Common.CommonClass.Select(SelName);
            if (dic != null)
            {
                return Utils.GetResult(300, "型号已存在，请重新添加新的型号");
            }
            else
            {
                SqlParameter[] par = new SqlParameter[]
                {
                        new SqlParameter ("ModelData",ModelData),
                        new SqlParameter ("Ddelete","0"),
                 };
                string UpdateSql = @"UPDATE [Dictionary] SET [DataText] =@ModelData WHERE  DictionaryID=" + int.Parse(id) + "";
                int count = Com.ExecutionSqlPar(UpdateSql, par);
                if (count > 0)
                {
                    return Utils.GetResult(200, "修改成功", "true");
                }
                else
                {
                    return Utils.GetResult(300, "修改失败");
                }
            }
        }
        /// <summary>
        /// 添加型号
        /// </summary>
        /// <param name="modelText"></param>
        /// <returns></returns>
        public string AddModel(string modelText)
        {
            string SelModel = "select DictionaryID,DataText,Ddelete from Dictionary where DataText='" + modelText + "'";
            Dictionary<string, object> dic = Common.CommonClass.Select(SelModel);
            if (dic != null)
            {
                return Utils.GetResult(300, "单位名称已存在，请重新添加新的单位名称");
            }
            else
            {
                SqlParameter[] par = new SqlParameter[]
                {
                    new SqlParameter ("AddModel",modelText),
                    new SqlParameter ("Ddelete",int.Parse("0"))
               };
                string InsertSql = @"INSERT INTO [Dictionary]([DataText],[Ddelete])
                                         VALUES(@AddModel,@Ddelete)";
                int count = Com.ExecutionSqlPar(InsertSql, par);
                if (count > 0)
                {
                    return Utils.GetResult(200, "保存成功", "true");
                }
                else
                {
                    return Utils.GetResult(300, "添加失败");
                }
            }
        }
        public string SelModel()
        {
            string Selsql = "select DictionaryID,DataText from Dictionary where Ddelete=0";
            string result = Common.CommonClass.DataTableToJson(Com.Selcets(Selsql));
            return result;
        }
        //保存商品信息
        //public bool SaveMerchandise(string number, string name, string unit, string specification, string price, string note, string sum) {
        //    SqlParameter[] par = new SqlParameter[] {
        //        new SqlParameter ("MNumber",number),
        //        new SqlParameter ("MName",name),
        //        new SqlParameter ("MUnit",unit),
        //        new SqlParameter ("Mspecification",specification),
        //        new SqlParameter ("Msaleprice",price),
        //        new SqlParameter ("Mnote",note),
        //        new SqlParameter ("MDelete","0"),
        //        new SqlParameter ("Msum",sum),

        //    };
        //    string InsertSql = @"INSERT INTO [Merchandise]
        //                    ([Mnumber] ,
        //                       [Mname]                        
        //                       ,[Unitid]
        //                       ,[Mspecification]
        //                       ,[Msellingprice]
        //                       ,[Mnote]
        //                       ,[MDelete]
        //                       ,[Msum])
        //                 VALUES(
        //                       @MNumber
        //                       ,@MName
        //                       ,@MUnit
        //                       ,@Mspecification
        //                       ,@Msaleprice
        //                       ,@Mnote
        //                       ,@MDelete
        //                       ,@Msum);SELECT @@IDENTITY";

        //    int count = int.Parse(Com.ExecuteScalar(InsertSql, par).ToString());
        //    int mid = count;

        //    SqlParameter[] par2 = new SqlParameter[] {
        //     new SqlParameter("Mid",mid)
        //    };
        //    string InsertSql2 = @"INSERT INTO [StockIn]
        //                       ([Mid]) VALUES (@Mid)";
        //    int count2 = Com.ExecutionSqlPar(InsertSql2, par2);

        //    return false;

        //}

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="number"></param>
        /// <param name="name"></param>
        /// <param name="unit"></param>
        /// <param name="specification"></param>
        /// <param name="price"></param>
        /// <param name="note"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public string SaveMerchandise(string number, string name, string unit, string DictionaryID, string price, string note)
        {
            //判断如果用户输入的商品名称并且商品规格等于数据库已经存在的直接数量累加；
            string SelSQl = "select Mname,DictionaryID from Merchandise where Mname='" + name + "' and DictionaryID=" + DictionaryID + "";
            Dictionary<string, object> dic = Common.CommonClass.Select(SelSQl);
            if (dic != null)
            {
                return Utils.GetResult(300, "商品已存在");
            }
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter ("MNumber",number),
                new SqlParameter ("MName",name),
                new SqlParameter ("MUnit",unit),
                new SqlParameter ("DictionaryID",DictionaryID),
                new SqlParameter ("Msaleprice",price),
                new SqlParameter ("Mnote",note),
                new SqlParameter ("MDelete","0"),
                //new SqlParameter ("Msum",sum),
                new SqlParameter ("MCreateDate",DateTime.Now),

            };
            string InsertSql = @"INSERT INTO [Merchandise]([Mnumber] ,[Mname],[Unitid],[DictionaryID],[Msellingprice],[Mnote],[MDelete],MCreateDate)
                                             VALUES(@MNumber,@MName,@MUnit,@DictionaryID,@Msaleprice,@Mnote,@MDelete,@MCreateDate)";
            int count = int.Parse(Com.ExecutionSqlPar(InsertSql, par).ToString());
            if (count > 0)
            {
                return Utils.GetResult(200, "添加成功", "true");
            }
            else
            {
                return Utils.GetResult(300, "添加失败");
            }
        }

        /// <summary>
        /// 删除商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DelMerchandise(string id)
        {
            string DelSQL = @"UPDATE [Merchandise] SET [MDelete] = 1 WHERE Mid=" + id + "";
            int count = Common.CommonClass.ExecutionSQL(DelSQL);
            if (count > 0)
            {
                return Utils.GetResult(200, "删除成功");
            }
            else
            {
                return Utils.GetResult(300, "删除失败");
            }
        }
        //商品保存并入库     
        public string SaveStockIn(string innumber, string inname, string inunit, string dictionaryID, string insaleprice, string innote)
        {
            //总数量
            //int Totalvalue;
            //判断如果用户输入的商品名称并且商品规格等于数据库已经存在的直接数量累加；

            string SelSQl = "select Mname,DictionaryID from Merchandise where Mname='" + inname + "' and DictionaryID=" + dictionaryID + "";
            Dictionary<string, object> dic = Common.CommonClass.Select(SelSQl);
            //查询结果不等于NULL
            if (dic != null)
            {
                return Utils.GetResult(300, "商品已存在", "true");
            }
            //if (dic != null)
            //{
            //    string decount = string.Empty;
            //    foreach (var key in dic)
            //    {
            //        decount = key.Value.ToString();
            //    }
            //    Totalvalue =int.Parse(decount);
            //    string SetSum = "update Merchandise set Msum=" + Totalvalue + " where Mname='" + inname + "'";
            //    int result = Common.CommonClass.ExecutionSQL(SetSum);
            //    return false;
            //}
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter ("MNumber",innumber),
                new SqlParameter ("MName",inname),
                new SqlParameter ("MUnit",inunit),
                new SqlParameter ("DictionaryID",dictionaryID),
                new SqlParameter ("Msaleprice",insaleprice),
                new SqlParameter ("Mnote",innote),
                new SqlParameter ("MDelete","0"),
                new SqlParameter ("MCreateDate",DateTime.Now),

            };
            string InsertSql = @"INSERT INTO [Merchandise]([Mnumber],[Mname],[Unitid],[DictionaryID],[Msellingprice],[Mnote],[MDelete],MCreateDate)
                         VALUES(@MNumber,@MName,@MUnit,@DictionaryID,@Msaleprice,@Mnote,@MDelete,@MCreateDate)SELECT @@IDENTITY";

            int count = int.Parse(Com.ExecuteScalar(InsertSql, par).ToString());
            int mid = count;
            SqlParameter[] par2 = new SqlParameter[]
            {
                 new SqlParameter("Mid",mid),
                 new SqlParameter("InvertorySum","0"),
                 new SqlParameter("CreateTime",DateTime.Now)
            };
            string InsertSql2 = @"INSERT INTO [Inventory] ([Mid],InvertorySum,CreateTime) VALUES (@Mid,@InvertorySum,@CreateTime)";
            int count2 = Com.ExecutionSqlPar(InsertSql2, par2);
            if (count > 0 && count2 > 0)
            {
                return Utils.GetResult(200, "添加成功", "true");
            }
            else
            {
                return Utils.GetResult(300, "添加失败");
            }
        }
        public string SelGoodsInfo(string id)
        {
            string SelSQL = "select Mid ,Mname,Mnumber,Unitid,DictionaryID,Mnote,Msellingprice from Merchandise where Mid=" + id + "";
            string Str = Common.CommonClass.DataTableToJson(Com.Selcets(SelSQL));
            return Str;
        }
        public string SaveGoodInfo(string id, string SNumber, string SName, string SUnit, string SModel, string SSalePrice, string SNote)
        {
            ////总数量
            //int Sumvalue;
            ////判断如果用户输入的商品名称并且商品规格等于数据库已经存在的直接数量累加；

            //string SelSQl = "select Msum from Merchandise where Mname='" + SName + "' and DictionaryID=" + SModel + "";
            //Dictionary<string, object> dic = Common.CommonClass.Select(SelSQl);

            //if (dic != null)
            //{
            //    string decount = string.Empty;
            //    foreach (var key in dic)
            //    {
            //        decount = key.Value.ToString();
            //    }
            //    Sumvalue = int.Parse(SSum) + int.Parse(decount);
            //    string SetSum = "update Merchandise set Msum=" + Sumvalue + " where Mname='" + SName + "'";
            //    int result = Common.CommonClass.ExecutionSQL(SetSum);
            //    return "";
            //}
            SqlParameter[] par = new SqlParameter[] {
                       new SqlParameter("SNumber",SNumber),new SqlParameter("SName",SName),new SqlParameter("SUnit",SUnit),new SqlParameter("SModel",SModel),
                       new SqlParameter("SSalePrice",SSalePrice),new SqlParameter("SNote",SNote),new SqlParameter("MUpdateDate",DateTime.Now),
            };
            string SaveSQL = @"UPDATE [Merchandise]SET[Mname] = @SName,[Mnumber] = @SNumber,[Unitid] = @SUnit,[Msellingprice] =@SSalePrice,[Mnote] = @SNote,[MUpdateDate] =@MUpdateDate,[DictionaryID] = @SModel          
                             WHERE Mid=" + id + "";
            int count = Com.ExecutionSqlPar(SaveSQL, par);
            if (count > 0)
            {
                return Utils.GetResult(200, "修改成功", "true");
            }
            else
            {
                return Utils.GetResult(300, "修改失败");
            }
        }
    }
}
