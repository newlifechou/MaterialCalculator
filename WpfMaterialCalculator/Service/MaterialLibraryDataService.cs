using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMaterialCalculator.Model;
using WpfMaterialCalculator.CommonHelper;
using System.Data.SQLite;

namespace WpfMaterialCalculator.Service
{
    public class MaterialLibraryDataService : IMaterialLibraryDataService
    {
        public bool AddMaterialItem(MaterialItem item)
        {
            string cmdText = "insert into material (id,materialName,moleWeight,popRate) values  (@id,@materialName,@moleWeight,@popRate) ";
            SQLiteParameter[] cmdParameters =
            {
                new SQLiteParameter("@id",item.Id),
                new SQLiteParameter("@materialName",item.MaterialName),
                new SQLiteParameter("@moleWeight",item.MoleWeight),
                new SQLiteParameter("@popRate",item.PopRate)
            };

            return SqliteHelper.ExecuteNonQuery(cmdText, cmdParameters) > 0;
        }

        public bool DeleteMaterialItem(MaterialItem item)
        {
            string cmdText = "delete from Material where id=@id";
            SQLiteParameter[] cmdParameters =
            {
                new SQLiteParameter("@id",item.Id),
            };

            return SqliteHelper.ExecuteNonQuery(cmdText, cmdParameters) > 0;
        }

        public IList<MaterialItem> GetAllMaterialItems()
        {
            string cmdText = "select id,materialName,moleweight,poprate from material  order by poprate desc,materialname";
            SQLiteDataReader dr = SqliteHelper.ExecuteReader(cmdText, null);
            IList<MaterialItem> results = new List<MaterialItem>(); 

            while (dr.Read())
            {
                MaterialItem tmp = new MaterialItem();
                tmp.Id = dr.GetGuid(0);
                tmp.MaterialName = dr.GetString(1);
                tmp.MoleWeight = dr.GetDouble(2);
                tmp.PopRate = dr.GetInt32(3);

                results.Add(tmp);
            }
            dr.Close();

            return results;
        }

        public bool UpdateMaterialItem(MaterialItem item)
        {
            string cmdText = "update material set materialName=@materialName,moleWeight=@moleWeight,popRate=@popRate where id=@id";
            SQLiteParameter[] cmdParameters =
            {
                new SQLiteParameter("@materialName",item.MaterialName),
                new SQLiteParameter("@moleWeight",item.MoleWeight),
                new SQLiteParameter("@popRate",item.PopRate),
                new SQLiteParameter("@id",item.Id)
            };

            return SqliteHelper.ExecuteNonQuery(cmdText, cmdParameters) > 0;
        }
    }
}
