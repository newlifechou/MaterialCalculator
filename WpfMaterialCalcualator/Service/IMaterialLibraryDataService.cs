using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMaterialCalcualator.Model;

namespace WpfMaterialCalcualator.Service
{
    /// <summary>
    /// 材料库数据服务
    /// </summary>
    public interface IMaterialLibraryDataService
    {
        IList<MaterialItem> GetAllMaterialItems();

        bool AddMaterialItem(MaterialItem item);
        bool UpdateMaterialItem(MaterialItem item);
        bool DeleteMaterialItem(MaterialItem item);




    }
}
