using System.Globalization;
using System.IO;

namespace Matrix
{
    public class Matrix<T> where T : struct
    {
        private T[,] _elements;
        public int RowCount { get; }
        public int ColumnCount { get; }

        public Matrix(int rows, int cols)
        {
            RowCount = rows;
            ColumnCount = cols;
            _elements = new T[rows, cols];
        }

        public T this[int row, int col]
        {
            get => _elements[row, col];
            set => _elements[row, col] = value;
        }

        public void Populate(Func<int, int, T> valueGenerator)
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    try
                    {
                        _elements[i, j] = valueGenerator(i, j);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException("Ошибка при заполнении матрицы сгенерированными значениями.", ex);
                    }
                }
            }
        }

        public static Matrix<T> operator +(Matrix<T> first, Matrix<T> second)
        {
            if (first.RowCount != second.RowCount || first.ColumnCount != second.ColumnCount)
                throw new ArgumentException("Размеры матриц должны совпадать для сложения.");

            var result = new Matrix<T>(first.RowCount, first.ColumnCount);
            for (int i = 0; i < first.RowCount; i++)
                for (int j = 0; j < first.ColumnCount; j++)
                    result[i, j] = (dynamic)first[i, j] + second[i, j];

            return result;
        }

        public static Matrix<T> operator *(Matrix<T> first, Matrix<T> second)
        {
            if (first.ColumnCount != second.RowCount)
                throw new ArgumentException("Число столбцов первой матрицы должно быть равно числу строк второй матрицы.");

            var result = new Matrix<T>(first.RowCount, second.ColumnCount);
            for (int i = 0; i < first.RowCount; i++)
                for (int j = 0; j < second.ColumnCount; j++)
                {
                    dynamic total = default(T);
                    for (int k = 0; k < first.ColumnCount; k++)
                        total += (dynamic)first[i, k] * second[k, j];
                    result[i, j] = total;
                }

            return result;
        }

        public void ExportToCsv(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < RowCount; i++)
                {
                    var row = new List<string>();
                    for (int j = 0; j < ColumnCount; j++)
                    {
                        double value = Convert.ToDouble(this[i, j]);
                        row.Add(value.ToString("F2", CultureInfo.InvariantCulture).Replace('.', ','));
                    }
                    writer.WriteLine(string.Join(";", row));
                }
            }
        }
    }
}
