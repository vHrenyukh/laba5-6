using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba5
{
    public partial class Form1 : Form
    {
        List<IAnimal> animals;

        public Form1()
        {
            InitializeComponent();
            animals = new List<IAnimal>();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IAnimal animal = new Animal("Wolf", 50, 3, 60);
                animals.Add(animal);

                textBox3.Text = animal.Type;
                textBox4.Text = animal.Weight.ToString();
                textBox6.Text = animal.Age.ToString();
                textBox5.Text = animal.Price.ToString();

                textBox1.AppendText($"Added Animal: {animal.Type} - {animal.Weight}kg, {animal.Age} years, ${animal.Price}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                IAnimal wolf = new Wolf("Wolf", 50, 3, 60, "Gray wolf", "Europe");
                animals.Add(wolf);

                textBox7.Text = ((Wolf)wolf).Poroda;
                textBox8.Text = ((Wolf)wolf).Habitat;

                textBox1.AppendText($"Added Wolf: {((Wolf)wolf).Poroda} - {((Wolf)wolf).Habitat}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (animals == null || animals.Count == 0)
                {
                    MessageBox.Show("No animals to clone.");
                    return;
                }

                int halfCount = animals.Count / 2;
                for (int i = 0; i < halfCount; i++)
                {
                    IAnimal clone = (IAnimal)animals[i].Clone();
                    animals.Add(clone);
                    textBox1.AppendText($"Cloned: {clone.Type}{Environment.NewLine}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (animals == null || animals.Count == 0)
                {
                    MessageBox.Show("No animals to sort.");
                    return;
                }

                animals.Sort();

                textBox1.AppendText($"Sorted Animals:{Environment.NewLine}");
                foreach (IAnimal animal in animals)
                {
                    textBox1.AppendText($"{animal.Type} - {animal.Weight}kg, {animal.Age} years, ${animal.Price}{Environment.NewLine}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred: " + ex.Message);
            }
        }
    }

    public interface IAnimal : IComparable<IAnimal>, ICloneable
    {
        string Type { get; set; }
        int Weight { get; set; }
        int Age { get; set; }
        int Price { get; set; }
    }

    public class Animal : IAnimal
    {
        public string Type { get; set; }
        public int Weight { get; set; }
        public int Age { get; set; }
        public int Price { get; set; }

        public Animal(string type, int weight, int age, int price)
        {
            Type = type;
            Weight = weight;
            Age = age;
            Price = price;
        }

        public int CompareTo(IAnimal other)
        {
            if (other == null)
                return 1;

            return Type.CompareTo(other.Type);
        }

        public object Clone()
        {
            return new Animal(Type, Weight, Age, Price);
        }
    }

    public class Wolf : Animal
    {
        public string Poroda { get; set; }
        public string Habitat { get; set; }

        public Wolf(string type, int weight, int age, int price, string poroda, string habitat)
            : base(type, weight, age, price)
        {
            Poroda = poroda;
            Habitat = habitat;
        }

        public new int CompareTo(IAnimal other)
        {
            if (other == null)
                return 1;

            if (other is Wolf otherWolf)
                return Poroda.CompareTo(otherWolf.Poroda);

            return base.CompareTo(other);
        }

        public new object Clone()
        {
            return new Wolf(Type, Weight, Age, Price, Poroda, Habitat);
        }
    }
}

