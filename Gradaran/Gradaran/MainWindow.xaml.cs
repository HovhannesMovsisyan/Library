using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gradaran
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public GradaranEntities context;
       public int index;
       

        public MainWindow()
        {
            InitializeComponent();
            context = new GradaranEntities();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var data = (from item in context.Grqers
                        select new
                        {
                            vernagir = item.vernagir,
                            heghinak = item.heghinak,
                            gin = item.gin,
                            qanak = item.qanak,
                            
                        }).ToList();
            dataGrid.ItemsSource = data;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Insert w2 = new Insert();
            w2.Show();



            w2.Closing += (object senderr, System.ComponentModel.CancelEventArgs ee) =>
            {
                Grqer a = new Grqer();
                a.vernagir = w2.textBox.Text;
                a.heghinak = w2.textBox1.Text;
                a.gin = int.Parse(w2.textBox2.Text);
                a.qanak = int.Parse(w2.textBox3.Text);
                context.Grqers.Add(a);
                context.SaveChanges();
            };
        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = dataGrid.SelectedIndex;
            
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            
            Edit ed = new Edit();
            Grqer g = context.Grqers.ToList()[index];
            ed.textBox.Text = g.vernagir;
            ed.textBox1.Text = g.heghinak;
            ed.textBox2.Text = g.gin.ToString();
            ed.textBox3.Text = g.qanak.ToString();

            ed.Show();

            ed.Closing += (object senderr, System.ComponentModel.CancelEventArgs ee) =>
            {

                g.vernagir = ed.textBox.Text;
                g.heghinak = ed.textBox1.Text;
                g.gin += int.Parse(ed.textBox2.Text);
                g.qanak += int.Parse(ed.textBox3.Text);

                context.SaveChanges();
            };
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Grqer g = context.Grqers.ToList()[index];
            context.Grqers.Remove(g);
            context.SaveChanges();
        }

       

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = textBox.Text;
            var item = (from v in context.Grqers
                        where v.vernagir.StartsWith(text)
                        select v).ToList();
            
            dataGrid.ItemsSource = item;
            if (text == "")
            {
                this.dataGrid.ItemsSource = null;
                return;

            }

           

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = textBox1.Text;
            var item = (from v in context.Grqers
                        where v.heghinak.StartsWith(text)
                        select v).ToList();
            
            dataGrid.ItemsSource = item;
            if (text == "")
            {
                this.dataGrid.ItemsSource = null;
                return;

            }

        }
    }
}
