namespace finals
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide(); // Itatago ang kasalukuyang form
        }

        private void button3_Click(object sender, EventArgs e)
        {
            membership memberForm = new membership();
            memberForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TargetProgram programForm = new TargetProgram();
            programForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Trainer trainerForm = new Trainer();
            trainerForm.Show();
            this.Hide();
        }
    }
}
