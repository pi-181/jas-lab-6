using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jaslab1
{
    public partial class Form1 : Form
    { 
        private DataGridViewColumn _dataGridViewColumn1 = new DataGridViewTextBoxColumn();
        private DataGridViewColumn _dataGridViewColumn2 = new DataGridViewTextBoxColumn();
        private DataGridViewColumn _dataGridViewColumn3 = new DataGridViewTextBoxColumn();
        private Stack<House> _houses = new Stack<House>();

        public Form1()
        {
            InitializeComponent();
            
            _dataGridViewColumn1.Name = "";
            _dataGridViewColumn1.HeaderText = "Адреса";
            _dataGridViewColumn1.ValueType = typeof(string);
            _dataGridViewColumn1.Width = dataGridView1.Width / 3;

            _dataGridViewColumn2.Name = "";
            _dataGridViewColumn2.HeaderText = "Житлова площа";
            _dataGridViewColumn2.ValueType = typeof(string);
            _dataGridViewColumn2.Width = dataGridView1.Width / 3;
            
            _dataGridViewColumn3.Name = "";
            _dataGridViewColumn3.HeaderText = "Кількість поверхів";
            _dataGridViewColumn3.ValueType = typeof(string);
            _dataGridViewColumn3.Width = dataGridView1.Width / 3;
            
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Add(_dataGridViewColumn1);
            dataGridView1.Columns.Add(_dataGridViewColumn2);
            dataGridView1.Columns.Add(_dataGridViewColumn3);
            dataGridView1.AutoResizeColumns();
        }

        private void AddHouse(string address, int houseroom, int floors)
        {
            House house = new House(address, houseroom, floors);
            _houses.Push(house);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            ShowListInGrid();
        }
        
        private void DeleteHouse(int elementIndex)
        {
            PopByIndex(elementIndex);
            ShowListInGrid();
        }

        private bool PopByIndex(int index)
        {
            var stack = new Stack<House>();
            bool dropped = false;
            
            int i = 0;
            foreach (House h in _houses)
            {
                if (i != index) stack.Push(h);
                else dropped = true;
                i++;
            }

            _houses = stack;
            return dropped;
        }
        
        private void ShowListInGrid()
        {
            dataGridView1.Rows.Clear();
            foreach (House h in _houses)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell surnameCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell bookCell = new DataGridViewTextBoxCell();
                
                nameCell.ValueType = typeof(string);
                nameCell.Value = h.address;
                surnameCell.ValueType = typeof(string);
                surnameCell.Value = h.houseroom;
                bookCell.ValueType = typeof(string);
                bookCell.Value = h.floors;
                
                row.Cells.Add(nameCell);
                row.Cells.Add(surnameCell);
                row.Cells.Add(bookCell);
                
                dataGridView1.Rows.Add(row);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddHouse(textBox1.Text, int.Parse(textBox2.Text), int.Parse(textBox3.Text));
        }
        
        private void RemoveButtonClick(object sender, EventArgs e)
        {
            var rowIndex = dataGridView1.SelectedCells[0].RowIndex;
            var result = MessageBox.Show(
                "Видалити дім?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    DeleteHouse(rowIndex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}