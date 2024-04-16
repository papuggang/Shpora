using System;
using System.Data;
using System.Windows;

namespace ggdfr
{

    public partial class MainWindow : Window
    {
        private int[,] matrix;
        private int rows, columns;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void EnterPoint(object sender, RoutedEventArgs e)
        {
            try
            {
                rows = int.Parse(txtRows.Text);
                columns = int.Parse(txtColumns.Text);

                if (rows < 0 || columns < 0)
                {
                    throw new Exception("Число строк и столбцов не может быть отрицательным.");
                }

                matrix = new int[rows, columns];
                Random rand = new Random();

                // Заполнение матрицы случайными числами
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        matrix[i, j] = rand.Next(0, 99);
                    }
                }

                // Отображение матрицы в DataGrid
                DataTable dt = new DataTable();
            for (int i = 0; i < columns; i++)
            {
                dt.Columns.Add(i.ToString());
            }
            for (int i = 0; i < rows; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < columns; j++)
                {
                    dr[j] = matrix[i, j];
                }
                dt.Rows.Add(dr);
            }
            dataGrid.ItemsSource = dt.DefaultView;
         }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите только числа в поля для строк и столбцов.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            // Поиск строки с максимальным и минимальным элементом
            int minRow = 0, maxRow = 0;
            int minVal = int.MaxValue, maxVal = int.MinValue;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (matrix[i, j] < minVal)
                    {
                        minVal = matrix[i, j];
                        minRow = i;
                    }
                    if (matrix[i, j] > maxVal)
                    {
                        maxVal = matrix[i, j];
                        maxRow = i;
                    }
                }
            }

            // Обмен строк
            for (int j = 0; j < columns; j++)
            {
                int temp = matrix[minRow, j];
                matrix[minRow, j] = matrix[maxRow, j];
                matrix[maxRow, j] = temp;
            }

            // Отображение матрицы в DataGrid2
            DataTable dt = new DataTable();
            for (int i = 0; i < columns; i++)
            {
                dt.Columns.Add(i.ToString());
            }
            for (int i = 0; i < rows; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < columns; j++)
                {
                    dr[j] = matrix[i, j];
                }
                dt.Rows.Add(dr);
            }
            dataGrid2.ItemsSource = dt.DefaultView;
        }
    }
}