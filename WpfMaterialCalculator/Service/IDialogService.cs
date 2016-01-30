using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalculator.Service
{
    public interface IDialogService
    {
        bool ShowDialog(string content, string caption);
    }
}
