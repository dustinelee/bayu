using System;
using System.Data;
using System.Windows.Forms;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace finals
{
    public partial class membership : Form
    {
        public membership()
        {
            InitializeComponent();
        }

        private byte[] ImageToByteArray(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        // ✅ LOAD DATA FUNCTION
        void LoadData()
        {
            DBConnect db = new DBConnect();
            try
            {
                db.Open();
                string query = "SELECT * FROM members";

                MySql.Data.MySqlClient.MySqlDataAdapter da =
                    new MySql.Data.MySqlClient.MySqlDataAdapter(query, db.Connection);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView5.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.Close();
            }
        }

        private void membership_Load(object sender, EventArgs e)
        {
            LoadData(); // ✅ auto load pag open
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string fullname = txtFullName.Text.Trim();
            string gender = cmbGender.Text.Trim();
            string contactnumber = txtContact.Text.Trim();
            string email = txtEmail.Text.Trim();
            string membershipplan = cmbMembershipplan.Text.Trim();

            int age;

            // dates
            DateTime startdate = dtpStartdate.Value;
            DateTime enddate = dtpEndate.Value;

            // ✅ image convert
            byte[] img = null;

            if (pictureBox6.Image != null)
            {
                img = ImageToByteArray(pictureBox6.Image);
            }

            // ✅ validation
            if (string.IsNullOrWhiteSpace(fullname) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(gender) ||
                string.IsNullOrWhiteSpace(membershipplan))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out age) || age <= 0)
            {
                MessageBox.Show("Please enter a valid age");
                return;
            }

            if (enddate < startdate)
            {
                MessageBox.Show("End date cannot be earlier than start date");
                return;
            }

            DBConnect db = new DBConnect();

            try
            {
                db.Open();

                string query = @"INSERT INTO members 
(fullname, age, gender, contactnumber, email, membershipplan, startdate, enddate, image) 
VALUES 
(@fullname, @age, @gender, @contactnumber, @email, @membershipplan, @startdate, @enddate, @image)";

                using (MySql.Data.MySqlClient.MySqlCommand cmd =
                    new MySql.Data.MySqlClient.MySqlCommand(query, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@fullname", fullname);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@contactnumber", contactnumber);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@membershipplan", membershipplan);
                    cmd.Parameters.AddWithValue("@startdate", startdate);
                    cmd.Parameters.AddWithValue("@enddate", enddate);

                    // ✅ FIXED (ito ang tamang image save)
                    cmd.Parameters.AddWithValue("@image", img ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Client added successfully");

                LoadData();

                // clear fields
                txtFullName.Clear();
                txtAge.Clear();
                txtContact.Clear();
                txtEmail.Clear();
                cmbMembershipplan.SelectedIndex = -1;
                cmbGender.SelectedIndex = -1;
                dtpStartdate.Value = DateTime.Now;
                dtpEndate.Value = DateTime.Now;
                pictureBox1.Image = null; // ✅ clear image
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                db.Close();
            }
        }

        private void dataGridView5_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView5.Rows[e.RowIndex];

                string name = row.Cells["fullname"].Value.ToString();
                string age = row.Cells["age"].Value.ToString();
                string gender = row.Cells["gender"].Value.ToString();
                string contact = row.Cells["contactnumber"].Value.ToString();
                string email = row.Cells["email"].Value.ToString();
                string member = row.Cells["membershipplan"].Value.ToString();
                string start = row.Cells["startdate"].Value.ToString();
                string end = row.Cells["enddate"].Value.ToString();
                byte[] image = (byte[])row.Cells["image"].Value;

                MemberDetails form = new MemberDetails();
                form.LoadData(name, age, gender, gender, contact, email, member, start, end, image);
                form.ShowDialog();
            }
        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide(); // Itatago ang kasalukuyang form
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            membership memberForm = new membership();
            memberForm.Show();
            this.Hide();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            TargetProgram programForm = new TargetProgram();
            programForm.Show();
            this.Hide();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Trainer trainerForm = new Trainer();
            trainerForm.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox6.Image = Image.FromFile(ofd.FileName);
                pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

