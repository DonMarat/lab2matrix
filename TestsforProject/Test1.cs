using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatrixLabor2;
using Matrix;
using System;

namespace MatrixTests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void MatrixInitializationTest()
        {
            var matrix = new Matrix<double>(3, 3);
            Assert.AreEqual(3, matrix.RowCount);
            Assert.AreEqual(3, matrix.ColumnCount);
        }

        [TestMethod]
        public void MatrixIndexerTest()
        {
            var matrix = new Matrix<int>(2, 2);
            matrix[0, 0] = 5;
            matrix[0, 1] = 10;
            matrix[1, 0] = 15;
            matrix[1, 1] = 20;

            Assert.AreEqual(5, matrix[0, 0]);
            Assert.AreEqual(10, matrix[0, 1]);
            Assert.AreEqual(15, matrix[1, 0]);
            Assert.AreEqual(20, matrix[1, 1]);
        }

        [TestMethod]
        public void MatrixAdditionTest()
        {
            var matrixA = new Matrix<int>(2, 2);
            matrixA[0, 0] = 1;
            matrixA[0, 1] = 2;
            matrixA[1, 0] = 3;
            matrixA[1, 1] = 4;

            var matrixB = new Matrix<int>(2, 2);
            matrixB[0, 0] = 5;
            matrixB[0, 1] = 6;
            matrixB[1, 0] = 7;
            matrixB[1, 1] = 8;

            Matrix<int> result = matrixA + matrixB;

            Assert.AreEqual(6, result[0, 0]);
            Assert.AreEqual(8, result[0, 1]);
            Assert.AreEqual(10, result[1, 0]);
            Assert.AreEqual(12, result[1, 1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatrixAdditionSizeMismatchTest()
        {
            var matrixA = new Matrix<int>(2, 3);
            var matrixB = new Matrix<int>(3, 3);
            var result = matrixA + matrixB; // Должно выбросить исключение
        }

        [TestMethod]
        public void MatrixMultiplicationTest()
        {
            var matrixA = new Matrix<int>(2, 3);
            matrixA[0, 0] = 1;
            matrixA[0, 1] = 2;
            matrixA[0, 2] = 3;
            matrixA[1, 0] = 4;
            matrixA[1, 1] = 5;
            matrixA[1, 2] = 6;

            var matrixB = new Matrix<int>(3, 2);
            matrixB[0, 0] = 7;
            matrixB[0, 1] = 8;
            matrixB[1, 0] = 9;
            matrixB[1, 1] = 10;
            matrixB[2, 0] = 11;
            matrixB[2, 1] = 12;

            Matrix<int> result = matrixA * matrixB;

            Assert.AreEqual(58, result[0, 0]);
            Assert.AreEqual(64, result[0, 1]);
            Assert.AreEqual(139, result[1, 0]);
            Assert.AreEqual(154, result[1, 1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatrixMultiplicationSizeMismatchTest()
        {
            var matrixA = new Matrix<int>(2, 2);
            var matrixB = new Matrix<int>(3, 2); // Ошибка: не совместимы для умножения
            var result = matrixA * matrixB; // Должно выбросить исключение
        }

        [TestMethod]
        public void EmptyMatrixTest()
        {
            var matrix = new Matrix<int>(0, 0);
            Assert.AreEqual(0, matrix.RowCount);
            Assert.AreEqual(0, matrix.ColumnCount);
        }

        [TestMethod]
        public void MatrixPopulateTest()
        {
            var matrix = new Matrix<int>(2, 2);
            matrix.Populate((i, j) => i * j); // Заполняем матрицу значениями: (0,0), (0,1), (1,0), (1,1)

            Assert.AreEqual(0, matrix[0, 0]);
            Assert.AreEqual(0, matrix[0, 1]);
            Assert.AreEqual(0, matrix[1, 0]);
            Assert.AreEqual(1, matrix[1, 1]);
        }

        [TestMethod]
        public void MatrixExportToCsvTest()
        {
            var matrix = new Matrix<double>(2, 2);
            matrix[0, 0] = 1.1;
            matrix[0, 1] = 2.2;
            matrix[1, 0] = 3.3;
            matrix[1, 1] = 4.4;

            string filePath = "test.csv";
            matrix.ExportToCsv(filePath);

            Assert.IsTrue(System.IO.File.Exists(filePath));

            var lines = System.IO.File.ReadAllLines(filePath);
            Assert.AreEqual("1,10;2,20", lines[0]);
            Assert.AreEqual("3,30;4,40", lines[1]);

            System.IO.File.Delete(filePath); // Удаление файла после теста
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatrixPopulateWithInvalidFunctionTest()
        {
            var matrix = new Matrix<int>(2, 2);
            matrix.Populate((i, j) => throw new InvalidOperationException()); // Невозможность заполнить из-за ошибки в функции
        }
    }
}
