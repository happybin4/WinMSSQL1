using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSSQL_VO1;


namespace WinMSSQL1
{
    class CommonUtil
    {
        public static void ComboBinding(ComboBox cbo, List<ComboItemVO> list, string gubun, bool blankItem = true, string blankText = "")
        {
            var codeList = (from item in list
                            where item.Gubun.Equals(gubun)
                            select item).ToList();
            if (blankItem)
            {
                ComboItemVO blank = new ComboItemVO() { Code = "", CodeName = blankText };
                codeList.Insert(0, blank);
            }

            cbo.DisplayMember = "CodeName"; //"속성" 눈에보이는 값
            cbo.ValueMember = "Code"; //눈에안보이는 값
            cbo.DataSource = codeList;
        }
    }
}
