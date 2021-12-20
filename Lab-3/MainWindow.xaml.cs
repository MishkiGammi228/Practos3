using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using LibMas;
using Lib_4;

namespace Lab_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] _matrix;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void создать_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(строк.Text, out int row) == true && Int32.TryParse(столбец.Text, out int column) == true)
            {
                _matrix = new int[row, column];
                dataGrid.ItemsSource = VisualArray.ToDataTable(_matrix).DefaultView;
            }
            else
            {
                MessageBox.Show("Данные введены неверно");
            }
        }

        private void Заполнить_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                int diapazon1 = Convert.ToInt32(diapazon.Text);
                int column = Convert.ToInt32(столбец.Text);
                int row = Convert.ToInt32(строк.Text);
                _matrix = new int[row, column];
                ArrayOperation.FillArrayRandom(_matrix, diapazon1);
                dataGrid.ItemsSource = VisualArray.ToDataTable(_matrix).DefaultView;
            }
            catch
            {
                MessageBox.Show("Данные ведены неверно");
            }
        }

        private void Решение_Click(object sender, RoutedEventArgs e)
        {
           otv.Text = Convert.ToString(Practic.ColumnOddNumber(_matrix));
        }

        private void Удалить_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            ArrayOperation.ClearArray(_matrix);
            otv.Clear();
        }

        private void Сброс_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            ArrayOperation.ClearArray(_matrix);
            otv.Clear();
            diapazon.Clear();
            строк.Clear();
            столбец.Clear();
        }

        private void dataGrid_SelectionChanged(object sender, DataGridCellEditEndingEventArgs e)
        {
            int indexStolb = e.Column.DisplayIndex;
            int indexRow = e.Row.GetIndex();
            _matrix[indexStolb, indexRow] = Convert.ToInt32(((TextBox)e.EditingElement).Text);
        }

        private void Выход(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Информация(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дана целочисленная матрица размера M × N. Найти номер первого из ее столбцов, " +
                "содержащих только нечетные числа.Если таких столбцов нет, то вывести 0. ");
        }

        private void SaveArray(object sender, RoutedEventArgs e)
        {
            if (_matrix == null)
            {
                MessageBox.Show("Таблица пуста");
                return;
            }
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";
            if (save.ShowDialog() == true)
            {
                ArrayOperation.SaveArray(_matrix, save.FileName);
            }
        }

        private void OpenArray(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";
            if (open.ShowDialog() == true)
            {
                if (open.FileName != string.Empty)
                {
                    ArrayOperation.OpenArray(out _matrix, open.FileName);
                    otv.Text = _matrix.Length.ToString();
                    dataGrid.ItemsSource = VisualArray.ToDataTable(_matrix).DefaultView;
                }
            }
        }

    }
}
