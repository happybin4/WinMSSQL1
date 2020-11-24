using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSSQL_DAC1;
using MSSQL_VO1;

namespace WinMSSQL1
{
    public partial class Form2 : Form
    {
        List<RegionInfoVO> regionList = null;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Region 정보 조회해서 콤보박스에 바인딩
            ProductDB db = new ProductDB();
            List<ComboItemVO> lsit = db.GetCodeList();
            CommonUtil.ComboBinding(comboBox2, lsit, "Region",true,"전체");
            CommonUtil.ComboBinding(comboBox3, lsit, "Category", true, "=========");


            //전체 지역정보 조회해서 데이터그리드뷰에 바인딩
            regionList = db.GetRegionList();
            dataGridView1.DataSource = regionList;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex < 1) return;

                // 선택된 Region에 따라서 지역정보 필터링해서 데이터그리드뷰에 바인딩
                dataGridView1.DataSource = (from region in regionList
             where region.RegionID == comboBox2.SelectedValue.ToString()
             select region).ToList();
        }
    }
}
