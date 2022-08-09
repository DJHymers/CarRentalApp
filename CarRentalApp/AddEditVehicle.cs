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
    public partial class AddEditVehicle : Form
    {
        private bool isEditMode;
        private ManageVehicleListing _manageVehicleListing;
        private readonly CarRentalEntities carRentalEntities;
        public AddEditVehicle(ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Add New Vehicle";
            this.Text = "Add New Vehicle";
            isEditMode = false;
            _manageVehicleListing = manageVehicleListing;
            carRentalEntities = new CarRentalEntities();
        }

        public AddEditVehicle(TypesOfCar carToEdit, ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle";
            this.Text = "Edit Vehicle";
            _manageVehicleListing = manageVehicleListing;
            if (carToEdit == null)
            {
                MessageBox.Show("Please ensure that you have selected a valid record to edit.");
                Close();
            }
            else
            {
                isEditMode = true;
                carRentalEntities = new CarRentalEntities();
                PopulateFields(carToEdit);
            }
        }

        private void PopulateFields(TypesOfCar car)
        {
            lblId.Text = car.Id.ToString();
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbVIN.Text = car.VIN;
            tbRegNo.Text = car.VehicleReg;
            tbYear.Text = car.Year.ToString();
        }

        private void AddEditVehicle_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbMake.Text) ||
                         string.IsNullOrWhiteSpace(tbModel.Text))
                {
                    MessageBox.Show("Please ensure that you provide a make and model.");
                }
                else
                {
                    //if(isEditMode == true)
                    if (isEditMode)
                    {
                        //Edit code here
                        var id = int.Parse(lblId.Text);
                        var car = carRentalEntities.TypesOfCars.FirstOrDefault(q => q.Id == id);
                        car.Make = tbMake.Text;
                        car.Model = tbModel.Text;
                        car.VIN = tbVIN.Text;
                        car.VehicleReg = tbRegNo.Text;
                        car.Year = int.Parse(tbYear.Text);
                    }
                    else
                    {
                        //Add code here
                        var newCar = new TypesOfCar
                        {
                            Make = tbMake.Text,
                            Model = tbModel.Text,
                            VIN = tbVIN.Text,
                            VehicleReg = tbRegNo.Text,
                            Year = int.Parse(tbYear.Text),
                        };

                        carRentalEntities.TypesOfCars.Add(newCar);
                    }
                    carRentalEntities.SaveChanges();
                    _manageVehicleListing.PopulateGrid();
                    MessageBox.Show("Operation Completed. Refresh Grid To See Changes.");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
