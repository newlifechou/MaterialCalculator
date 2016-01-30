using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfMaterialCalculator.Control
{
    public class NumberTextBox:TextBox
    {
        //用来储存输入前的值
        private string tmpStr = string.Empty;
        private string pattern = @"^[0-9]\d*\.?\d*$";

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            Match m = Regex.Match(this.Text, pattern);   // 匹配正则表达式
            if (m.Success)
            {
                tmpStr = this.Text;
            }
            else
            {
                this.Text = tmpStr;
                this.SelectionStart = this.Text.Length;
            }
        }
    }
}
