using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace CarInventory
{
    public partial class Form1 : Form
    {
        List<Car> carsList = new List<Car>();

        public Form1()
        {
            InitializeComponent();
            loadDB();
            DisplayCars();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string year, make, colour, mileage;

            year = yearInput.Text;
            make = makeInput.Text;
            colour = colourInput.Text;
            mileage = mileageInput.Text;

            Car car = new Car(year, make, colour, mileage);

            carsList.Add(car);

            DisplayCars();
        }

        public void DisplayCars()
        {
            outputLabel.Text = "";

            foreach (Car c in carsList)
            {
                outputLabel.Text += c.year + " "
                    + c.make + " "
                    + c.colour + " "
                    + c.mileage + "\n";
            }
        }

        public void loadDB()
        {
            string year, make, colour, mileage;

            XmlReader reader = XmlReader.Create("Resources/CarData.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                   // reader.ReadToNextSibling("year");
                    year = reader.ReadString();

                    reader.ReadToNextSibling("make");
                    make = reader.ReadString();

                    reader.ReadToNextSibling("colour");
                    colour = reader.ReadString();
                    
                    reader.ReadToNextSibling("mileage");
                    mileage = reader.ReadString();

                    Car newCar = new Car(year, make, colour, mileage);
                    carsList.Add(newCar);


                }
            }

            reader.Close();
        }

        public void saveDB()
        {
            XmlWriter writer = XmlWriter.Create("Resources/CarData.xml", null);

            writer.WriteStartElement("Car");

            foreach (Car e in carsList)
            {
                writer.WriteStartElement("Car");

                writer.WriteElementString("year", e.year);
                writer.WriteElementString("make", e.make);
                writer.WriteElementString("colour", e.colour);
                writer.WriteElementString("mileage", e.mileage);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();
        }

        private void ClearLabels()
        {
            yearInput.Text = "";
            makeInput.Text = "";
            colourInput.Text = "";
            mileageInput.Text = "";
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            saveDB();
            Application.Exit();
        }
    }
}
