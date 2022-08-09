using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class ResetPassword : Form
    {
        private readonly CarRentalEntities carRentalEntities;
        private User _user;
        public ResetPassword(User user)
        {
            InitializeComponent();
            carRentalEntities = new CarRentalEntities();
            _user = user;
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                var password = tbNewPassword.Text;
                var confirm_password = tbConfirmPassword.Text;
                var user = carRentalEntities.Users.FirstOrDefault(q => q.id == _user.id);
                if(password != confirm_password)
                {
                    MessageBox.Show("Password does not match. Please try again.");
                }

                user.password = Utils.HashPassword(password);
                carRentalEntities.SaveChanges();
                MessageBox.Show("Your password has been reset.");
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("An error has occured, please try again.");               
            }
        }
    }
}
