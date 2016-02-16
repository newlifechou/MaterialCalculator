using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareAuthorizeLib
{
    /// <summary>
    /// 软件许可类
    /// </summary>
    public class Permisson
    {
        /// <summary>
        /// 检查当前时间是否超过设定的许可时间
        /// </summary>
        /// <param name="permitTime">设定的许可时间</param>
        /// <returns></returns>
        public bool CheckTime(DateTime permitTime)
        {
            return DateTime.Now < permitTime;
        }
    }
}
