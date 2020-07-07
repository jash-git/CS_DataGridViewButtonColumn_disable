using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //---
                //設定&清空

                dataGridView1.RowHeadersVisible = false;//DataGridView 最前面指示選取列所在位置的箭頭欄位
                dataGridView1.AllowUserToAddRows = false;//是否允許使用者新增資料
                dataGridView1.AllowUserToDeleteRows = false;//是否允許使用者刪除資料
                dataGridView1.AllowUserToOrderColumns = false;//是否允許使用者調整欄位位置
                //所有表格欄位寬度全部變成可調 dgvSub0000_01.AllowUserToResizeColumns = false;//是否允許使用者改變欄寬
                dataGridView1.AllowUserToResizeRows = false;//是否允許使用者改變行高
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;//單一欄位禁止編輯
                }
                dataGridView1.AllowUserToAddRows = false;//刪除空白列
                dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;//整列選取

                do
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        DataGridViewRow r1 = this.dataGridView1.Rows[i];//取得DataGridView整列資料
                        this.dataGridView1.Rows.Remove(r1);//DataGridView刪除整列
                    }
                } while (dataGridView1.Rows.Count > 0);

                dataGridView1.Rows.Add("000", "123");
                dataGridView1.Rows.Add("456", "789");

                ((CustomDGVButtonCell)dataGridView1.Rows[1].Cells[1]).Enabled = false;
            }
            catch { }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                CustomDGVButtonCell cell = (CustomDGVButtonCell)dataGridView1.Rows[e.RowIndex].Cells[1];

                if (e.ColumnIndex == 1 && cell.Enabled)
                {
                    try
                    {
                        MessageBox.Show(cell.Value.ToString());
                    }
                    catch { }
                    
                }
            }
        }
    }
}
