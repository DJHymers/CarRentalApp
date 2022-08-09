using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class Login : Form
    {
        private readonly CarRentalEntities carRentalEntities;
        public Login()
        {
            InitializeComponent();
            carRentalEntities = new CarRentalEntities();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SHA256 sha = SHA256.Create();


                var username = tbUsername.Text.Trim();
                var password = tbPassword.Text;

                var hashed_password = Utils.HashPassword(password);


                var user = carRentalEntities.Users.FirstOrDefault(q => q.username == username
                                                                       && q.password == hashed_password
                                                                       && q.isActive == true);
                if (user == null)
                {
                    MessageBox.Show("Incorrect username/password");
                }
                else
                {
                    var mainWindow = new MainWindow(this, user);
                    mainWindow.Show();
                    Hide();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Something went wrong. Please try again.");
            }
        }
    }
}
