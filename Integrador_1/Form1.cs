using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Integrador_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            empresa= new Empresa();
        }
        Empresa empresa;
        Regex re;
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.MultiSelect=false;
            dataGridView1.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect=false;
            dataGridView2.SelectionMode=DataGridViewSelectionMode.FullRowSelect;

            empresa.AgregarAuto(new Auto("AF123BB", "Ford", "Ranger", "2023", 40000m));
            empresa.AgregarAuto(new Auto("AE998BA", "Fiat", "Argo", "2022", 20000m));
            empresa.AgregarAuto(new Auto("AD123BB", "BMW", "i325", "2021", 30000m));

            Mostrar(dataGridView2,empresa.RotornaListaAutos());



        }
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string dni = Interaction.InputBox("DNI: ");
              
                re= new Regex(@"\d{2}[.]\d{3}[.]\d{3}");
                if (!(re.IsMatch(dni) && dni.Length==10 )) throw new Exception("DNI fuera de formato !!!");
                Persona persona = new Persona(dni);
                if (empresa.ValidaDNI(persona)) throw new Exception("DNI existente !!!");
                persona.Nombre= Interaction.InputBox("Nombre: ");
                persona.Apellido = Interaction.InputBox("Apellido: ");
                empresa.AgregarPersona(persona);
                Mostrar(dataGridView1, empresa.RotornaListaPersonas());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        private void Mostrar(DataGridView pDGV, object pO)
        { pDGV.DataSource=null;pDGV.DataSource=pO; }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.Rows.Count==0) { dataGridView1_RowEnter(null, null); throw new Exception("Nada para borrar !!!"); }
                    empresa.BorrarPersona(new Persona(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),"",""));
                Mostrar(dataGridView1, empresa.RotornaListaPersonas());
                dataGridView1_RowEnter(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                Persona p = new Persona();
                p.DNI=row.Cells[0].Value.ToString();
                p.Nombre=Interaction.InputBox("Nombre: ", "Modificando Nombre ...", row.Cells[1].Value.ToString().Split(new string[] { ", "},StringSplitOptions.None)[1]);
                p.Apellido=Interaction.InputBox("Apellido: ", "Modificando Apellido ...", row.Cells[1].Value.ToString().Split(new string[] { ", " }, StringSplitOptions.None)[0]);
                empresa.ModificarPersona(p);
                Mostrar(dataGridView1, empresa.RotornaListaPersonas());

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                string dni = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string patente = (dataGridView2.SelectedRows[0].DataBoundItem as Auto).Patente;
                empresa.AsignaAutoAPersona(new Auto(patente),new Persona(dni));
                Mostrar(dataGridView3, empresa.RetornaListaDeAutosDePersona(new Persona(dni)));
                dataGridView2_RowEnter(null,null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
                if(dataGridView1.Rows.Count>0) Mostrar(dataGridView3, empresa.RetornaListaDeAutosDePersona(new Persona(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())));
                else { dataGridView3.DataSource=null; }
                dataGridView2_RowEnter(null,null);
            }
            catch (Exception)
            {

              
            }
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               if(dataGridView2.Rows.Count>0) Mostrar(dataGridView4, empresa.RetornaListaDeAutosGrilla4());
            }
            catch (Exception)
            {

               
            }
        }
    }
}
