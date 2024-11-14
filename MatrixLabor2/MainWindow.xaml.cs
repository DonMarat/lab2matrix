using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Matrix;

namespace MatrixApp
{
    public partial class MainWindow : Window
    {
        private Matrix<double> _firstMatrix;
        private Matrix<double> _secondMatrix;
        private Matrix<double> _resultMatrix;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateMatrices_Click(object sender, RoutedEventArgs e)
        {
            int rows1 = int.Parse(MatrixXRowsTextBox.Text);
            int cols1 = int.Parse(MatrixXColsTextBox.Text);
            int rows2 = int.Parse(MatrixYRowsTextBox.Text);
            int cols2 = int.Parse(MatrixYColsTextBox.Text);

            _firstMatrix = new Matrix<double>(rows1, cols1);
            _secondMatrix = new Matrix<double>(rows2, cols2);

            ShowMatrix(_firstMatrix, MatrixXDisplay);
            ShowMatrix(_secondMatrix, MatrixYDisplay);
        }

        private void FillWithRandomValues_Click(object sender, RoutedEventArgs e)
        {
            if (_firstMatrix == null || _secondMatrix == null)
            {
                MessageBox.Show("Сначала создайте матрицы.");
                return;
            }

            Random rand = new Random();
            _firstMatrix.Populate((i, j) => rand.NextDouble() * 10);
            _secondMatrix.Populate((i, j) => rand.NextDouble() * 10);

            ShowMatrix(_firstMatrix, MatrixXDisplay);
            ShowMatrix(_secondMatrix, MatrixYDisplay);
        }

        private void ShowMatrix(Matrix<double> matrix, ItemsControl control)
        {
            control.Items.Clear();
            var grid = new Grid();

            for (int i = 0; i < matrix.RowCount; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int j = 0; j < matrix.ColumnCount; j++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    var textBox = new TextBox
                    {
                        Text = matrix[i, j].ToString("F2"),
                        Margin = new Thickness(2),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(textBox, i);
                    Grid.SetColumn(textBox, j);
                    grid.Children.Add(textBox);
                }
            }
            control.Items.Add(grid);
        }

        private void UpdateMatrix(Matrix<double> matrix, ItemsControl control)
        {
            if (control.Items.Count == 0)
                return;

            var grid = control.Items[0] as Grid;
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    var textBox = grid.Children[i * matrix.ColumnCount + j] as TextBox;
                    string input = textBox.Text.Trim();

                    if (double.TryParse(input, NumberStyles.Any, CultureInfo.CurrentCulture, out double value))
                    {
                        matrix[i, j] = value;
                    }
                    else
                    {
                        MessageBox.Show("Введите корректные числовые значения в матрице.");
                        return;
                    }
                }
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (_firstMatrix == null || _secondMatrix == null)
            {
                MessageBox.Show("Сначала создайте и заполните матрицы.");
                return;
            }

            UpdateMatrix(_firstMatrix, MatrixXDisplay);
            UpdateMatrix(_secondMatrix, MatrixYDisplay);

            if (OperationSelector.SelectedIndex == 0)
            {
                if (_firstMatrix.RowCount == _secondMatrix.RowCount && _firstMatrix.ColumnCount == _secondMatrix.ColumnCount)
                {
                    var stopwatch = Stopwatch.StartNew();
                    _resultMatrix = _firstMatrix + _secondMatrix;
                    stopwatch.Stop();
                    ShowMatrix(_resultMatrix, ResultMatrixDisplay);
                    ElapsedTimeTextBlock.Text = $"Время расчета (сложение): {stopwatch.ElapsedMilliseconds} мс";
                }
                else
                {
                    MessageBox.Show("Для сложения размеры матриц должны совпадать. ");
                }
            }
            else if (OperationSelector.SelectedIndex == 1)
            {
                if (_firstMatrix.ColumnCount == _secondMatrix.RowCount)
                {
                    var stopwatch = Stopwatch.StartNew();
                    _resultMatrix = _firstMatrix * _secondMatrix;
                    stopwatch.Stop();
                    ShowMatrix(_resultMatrix, ResultMatrixDisplay);
                    ElapsedTimeTextBlock.Text = $"Время расчета (умножение): {stopwatch.ElapsedMilliseconds} мс";
                }
                else
                {
                    MessageBox.Show("Для умножения количество столбцов первой матрицы должно быть равно количеству строк второй матрицы.");
                }
            }
        }
        private void SaveResult_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = ".csv",
                Filter = "CSV files (*.csv)|*.csv"
            };

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                _resultMatrix.ExportToCsv(dialog.FileName);
                MessageBox.Show("Результат сохранен!");
            }
        }
    }
}
