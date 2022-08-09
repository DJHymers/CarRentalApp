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
    public partial class ManageVehicleListing : Form
    {
        private readonly CarRentalEntities carRentalEntities;
        public ManageVehicleListing()
        {
            InitializeComponent();
            carRentalEntities = new CarRentalEntities();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: { ex.Message}");
            }
        }

        public void PopulateGrid()
        {
            var cars = carRentalEntities.TypesOfCars
            .Select(q => new
            {
            Make = q.Make,
            Model = q.Model,
            VIN = q.VIN,
            VehicleReg = q.VehicleReg,
            Year = q.Year,
            Id = q.Id
            })
            .ToList();

            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[3].HeaderText = "Registration Number";
            gvVehicleList.Columns[5].Visible = false;
            //gvVehicleList.Columns[0].HeaderText = "ID";
            //gvVehicleList.Columns[1].HeaderText = "NAME";
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            AddEditVehicle addEditVehicle = new AddEditVehicle(this);
            addEditVehicle.ShowDialog();
            addEditVehicle.MdiParent = this.MdiParent;
            
        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            try
            {
                //get Id of selected row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;


                //query database for record
                var car = carRentalEntities.TypesOfCars.FirstOrDefault(q => q.Id == id);


                //launch AddEditVehicle window with data
                var addEditVehicle = new AddEditVehicle(car, this);
                addEditVehicle.ShowDialog();
                addEditVehicle.MdiParent = this.MdiParent;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                //get Id of selected row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                //query database for record
                var car = carRentalEntities.TypesOfCars.FirstOrDefault(q => q.Id == id);

                DialogResult dr = MessageBox.Show("Are You Sure You Want To Delete This Record?", "Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                
                if(dr == DialogResult.Yes)
                {
                    //delete vehicle from table
                    carRentalEntities.TypesOfCars.Remove(car);
                    carRentalEntities.SaveChanges();
                }
                PopulateGrid();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }
    }
}
