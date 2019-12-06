using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NTD.DAO;
using NTD.BUS;

namespace NTD.GUI
{
    public partial class frmDangNhap : DevExpress.XtraEditors.XtraForm
    {
        public int RoleId;
        public frmDangNhap()
        {
            InitializeComponent();
            txtTaiKhoan.Focus();
        }

        public static string _username;
        User_BUS userBUS = new User_BUS();
        RoleForm_BUS roleFormBUS = new RoleForm_BUS();
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTaiKhoan.Text;
            string password = txtMK.Text;
            
            var rs = userBUS.Login(username, password);
            if (rs != null)
            {
                var listRoleForm = roleFormBUS.GetList(rs.RoleId);

                foreach (var roleform in listRoleForm)
                {
                    GlobalVar.dictRoleForm.Add(roleform.FormId, roleform);
                }

                var roleId = rs.RoleId;
                _username = rs.userName;
                Form1 form1 = new Form1(roleId);
                txtTaiKhoan.ResetText();
                txtMK.ResetText();
                username = string.Empty;
                password = string.Empty;
                this.Hide();
                form1.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
            }

        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            txtTaiKhoan.Text = "admin";
            txtMK.Text = "123";
        }
    }
}