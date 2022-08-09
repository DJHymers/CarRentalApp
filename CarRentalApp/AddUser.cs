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
    public partial class AddUser : Form
    {

        private readonly CarRentalEntities carRentalEntities;
        private ManageUsers _manageUsers;
        public AddUser(ManageUsers manageUsers)
        {
            InitializeComponent();
            carRentalEntities = new CarRentalEntities();
            _manageUsers = manageUsers;
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            var roles = carRentalEntities.Roles.ToList();
            cbRole.DataSource = roles;
            cbRole.ValueMember = "id";
            cbRole.DisplayMember = "name";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var username = tbUsername.Text;
                var roleId = (int)cbRole.SelectedValue;
                var password = Utils.DefaultHashedPassword();
                var user = new User
                {
                    username = username,
                    password = password,
                    isActive = true
                };
                carRentalEntities.Users.Add(user);
                carRentalEntities.SaveChanges();

                var userId = user.id;

                var userRole = new UserRole
                {
                    roleid = roleId,
                    userid = userId
                };

                carRentalEntities.UserRoles.Add(userRole);
                carRentalEntities.SaveChanges();

                MessageBox.Show("New User Added Successfully.");
                Close();
            }
            catch (Exception)
            {

                MessageBox.Show("An Error Has Occured.");
            }
        }
    }
}
