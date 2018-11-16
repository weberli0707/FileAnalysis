using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace FileAnalysis
{
    public partial class FrmFileAnalysis : Form
    {
        public FrmFileAnalysis()
        {
            InitializeComponent();
        }
        string defaultPath = "";
        List<DirectoryInfo> ldr = new List<DirectoryInfo>();//目录明细表
        private void btnAdd_Click(object sender, EventArgs e)        {
            
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            //打开的文件夹浏览对话框上的描述  
            dialog.Description = "请选择一个文件夹";
            //是否显示对话框左下角 新建文件夹 按钮，默认为 true  
            dialog.ShowNewFolderButton = false;
            //首次defaultPath为空，按FolderBrowserDialog默认设置（即桌面）选择  
            if (defaultPath != "")
            {
                //设置此次默认目录为上一次选中目录  
                dialog.SelectedPath = defaultPath;
            }
            //按下确定选择的按钮  
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //记录选中的目录  
                defaultPath = dialog.SelectedPath;
                bool isExists = false;
                //在目录中是否存在
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                   if(defaultPath == checkedListBox1.GetItemText(checkedListBox1.Items[i]))
                    {
                        MessageBox.Show(this, "在目录中已存在!", "提示!");
                        return;
                    }
                }
                //在目录明细表中是否存在
                DirectoryInfo di = new DirectoryInfo(defaultPath);
                if (ldr.Count > 0)
                {
                    foreach(DirectoryInfo d in ldr)
                    {
                        if (d == di)
                        {
                            isExists = true;
                            break;
                        }
                    }
                }

                if(isExists==false)
                {
                    checkedListBox1.Items.Add(defaultPath);
                }
            }
            
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (int indexChecked in checkedListBox1.CheckedIndices)
            {
                checkedListBox1.Items.RemoveAt(indexChecked - i);
                i++; // 通过减去i来修正删除选项后的索引错误
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy == false)
            {
                bw.RunWorkerAsync("scan");
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {

            List<FileInfo> lfs = new List<FileInfo>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                string strFold = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                List<FileInfo> ilfi = FileHelper.getFile(strFold, "*");
                foreach(FileInfo fi in ilfi)
                {
                    if (lfs.Exists(v1=> v1 == fi) == false)
                    {
                        lfs.Add(fi);
                    }
                }
               
            }

            List<FileCol> lfo = new List<FileCol>();
            lfs.ForEach(p =>
            {
                lfo.Add(new FileCol(p.Name,p.FullName,p.CreationTime,p.LastWriteTime,p.Length));
            });
            e.Result = lfo;
        }

        private void DataGridViewTitle()
        {
            dataGridView1.Columns["ColFileName"].HeaderText = "文件名";
            dataGridView1.Columns["ColFilePath"].HeaderText = "路径";
            dataGridView1.Columns["ColCreateDate"].HeaderText = "创建时间";
            dataGridView1.Columns["ColAlterDate"].HeaderText = "修改时间";
            dataGridView1.Columns["ColFileSize"].HeaderText = "文件大小";
        }


        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    BindingList<FileCol> FcList = (BindingList<FileCol>)e.Result;
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = new BindingList<FileCol>(FcList);
                    DataGridViewTitle();

                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void FrmFileAnalysis_Load(object sender, EventArgs e)
        {
            BindingList<FileCol> FcList = new BindingList<FileCol>();
            FcList.Add(new FileCol("", "",DateTime.Now, DateTime.Now, 0));
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = FcList;
            DataGridViewTitle();
        }
    }
}
