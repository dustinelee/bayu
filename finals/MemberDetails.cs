using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finals
{
    public partial class MemberDetails : Form
    {
        public MemberDetails()
        {
            InitializeComponent();
        }

        public void LoadData(string name, string age, string gender, string Contact, string? contact, string? email,string member ,string start, string end ,byte[] image)
        {
            txtFullName.Text = name;
            txtAge.Text = age;
            txtGender.Text = gender;
            txtContact.Text = Contact;
            txtEmail.Text = email;
            txtMember.Text = member;
            txtStart.Text = start;
            txtEnd.Text = end;

            if (image != null && image.Length > 0)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(image))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                catch
                {
                    pictureBox1.Image = null; // or default image
                }
            }
        }



        private void MemberDetails_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
