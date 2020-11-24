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
    public partial class Form1 : Form
    {
        List<ProductInfoVO> listAll = null;
        DataTable dtAll = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //콤보박스에 카테고리 정보를 바인딩
            ProductDB db = new ProductDB();
            DataTable dtCode = db.GetCategory();
            //빈(전체)를 위해서 한 행을 추가
            DataRow dr = dtCode.NewRow();
            dr["CategoryID"] = 0;
            dr["CategoryName"] = "전체";
            dtCode.Rows.InsertAt(dr,0);
            dtCode.AcceptChanges();

            comboBox3.DisplayMember = "CategoryName"; //"속성" 눈에보이는 값
            comboBox3.ValueMember = "CategoryID"; //눈에안보이는 값
            comboBox3.DataSource = dtCode;

            //GetAllDataTable();

            GetAllListVO();
        }

        private void GetAllListVO()
        {
            ProductDB db = new ProductDB();
            listAll = db.GetProductList2();
            dataGridView1.DataSource = listAll;
        }

        private void GetAllDataTable()
        {
            ProductDB db = new ProductDB();
            dtAll = db.GetProductList();
            dataGridView1.DataSource = dtAll;//dtAll
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetAllDataTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetAllListVO();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex < 1)
            {                
                return;
            }
            int value = Convert.ToInt32(comboBox3.SelectedValue);
            string text = comboBox3.Text;

            MessageBox.Show($"값:{value} / 텍스트:{text}");

            //선택된 카테고리에 해당하는 제품목록만 표시
            //1. DataTable을 바인딩한 경우
            //   DataView를 생성해서 RowFilter를 적용
           /*
            DataView dv = new DataView(dtAll);//dt.DefaultView;
            dv.RowFilter = $"CategoryName = '{text}'";
            dataGridView1.DataSource = dv.ToTable(); 
           // dv.ToTable();, dv.Table; 뷰 > 테이블
           */

            //2. List<VO>을 바인딩한 경우
            //   LINQ 를 사용해서 필터링 적용
            var prodlist = from  product in listAll
                           where product.CategoryName.Equals(text)
                           select product;
            dataGridView1.DataSource = prodlist.ToList();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
