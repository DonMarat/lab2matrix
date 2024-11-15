using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matrix;
using System;

namespace MatrixApp.Tests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void CorrectRowNumberInMatrixTest()
        {
            var matrixA = new Matrix<int>(2, 3);
            Assert.AreEqual(2, matrixA.RowCount);
        }

        [TestMethod]
        public void CorrectColumnNumberInMatrixTest()
        {
            var matrixA = new Matrix<int>(3, 3);
            Assert.AreEqual(3, matrixA.ColumnCount);
        }

        [TestMethod]
        public void MatrixAddition_ValidInput_ReturnsCorrectResult()
        {
            var matrixA = new Matrix<int>(2, 2);
            var matrixB = new Matrix<int>(2, 2);

            matrixA[0, 0] = 1;
            matrixA[0, 1] = 2;
            matrixA[1, 0] = 3;
            matrixA[1, 1] = 4;

            matrixB[0, 0] = 5;
            matrixB[0, 1] = 6;
            matrixB[1, 0] = 7;
            matrixB[1, 1] = 8;

            var expected = new Matrix<int>(2, 2);
            expected[0, 0] = 6;
            expected[0, 1] = 8;
            expected[1, 0] = 10;
            expected[1, 1] = 12;

            var result = matrixA + matrixB;

            for (int i = 0; i < expected.RowCount; i++)
            {
                for (int j = 0; j < expected.ColumnCount; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j]);
                }
            }
        }

        [TestMethod]
        public void MatrixMultiplication_ValidInput_ReturnsCorrectResult()
        {
            var matrixA = new Matrix<int>(2, 2);
            var matrixB = new Matrix<int>(2, 2);

            matrixA[0, 0] = 1;
            matrixA[0, 1] = 2;
            matrixA[1, 0] = 3;
            matrixA[1, 1] = 4;

            matrixB[0, 0] = 5;
            matrixB[0, 1] = 6;
            matrixB[1, 0] = 7;
            matrixB[1, 1] = 8;

            var expected = new Matrix<int>(2, 2);
            expected[0, 0] = 19;
            expected[0, 1] = 22;
            expected[1, 0] = 43;
            expected[1, 1] = 50;

            var result = matrixA * matrixB;

            for (int i = 0; i < expected.RowCount; i++)
            {
                for (int j = 0; j < expected.ColumnCount; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j]);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatrixAddition_DifferentDimensions_ThrowsException()
        {
            var matrixA = new Matrix<int>(1, 2);
            var matrixB = new Matrix<int>(2, 2);

            var result = matrixA + matrixB;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatrixMultiplication_IncompatibleDimensions_ThrowsException()
        {
            // Создаем первую матрицу размером 2x3
            var matrixA = new Matrix<int>(2, 3);

            // Заполняем первую матрицу
            matrixA[0, 0] = 1;
            matrixA[0, 1] = 2;
            matrixA[0, 2] = 3;
            matrixA[1, 0] = 4;
            matrixA[1, 1] = 5;
            matrixA[1, 2] = 6;

            // Создаем вторую матрицу размером 2x2
            var matrixB = new Matrix<int>(2, 2);

            // Заполняем вторую матрицу
            matrixB[0, 0] = 7;
            matrixB[0, 1] = 8;
            matrixB[1, 0] = 9;
            matrixB[1, 1] = 10;

            // Пытаемся выполнить умножение матриц с несовместимыми размерами
            var result = matrixA * matrixB;  
        }


        [TestMethod]
        public void MatrixIndexing_GetAndSetValues_ReturnsCorrectValues()
        {
            var matrix = new Matrix<int>(2, 2);
            matrix[0, 0] = 42;

            Assert.AreEqual(42, matrix[0, 0]);
        }

        [TestMethod]
        public void MatrixEquality_SameMatrices_ReturnsTrue()
        {
            var matrixA = new Matrix<int>(2, 2);
            var matrixB = new Matrix<int>(2, 2);

            matrixA[0, 0] = 1;
            matrixA[0, 1] = 2;
            matrixA[1, 0] = 3;
            matrixA[1, 1] = 4;

            matrixB[0, 0] = 1;
            matrixB[0, 1] = 2;
            matrixB[1, 0] = 3;
            matrixB[1, 1] = 4;

            for (int i = 0; i < matrixA.RowCount; i++)
            {
                for (int j = 0; j < matrixA.ColumnCount; j++)
                {
                    Assert.AreEqual(matrixA[i, j], matrixB[i, j]);
                }
            }
        }
        [TestMethod]
        public void MatrixAdd_WithFloatValues_ReturnsCorrectResult()
        {
            // Создание матриц размером 2x2
            var matrixA = new Matrix<float>(2, 2);
            var matrixB = new Matrix<float>(2, 2);

            // Заполнение значениями для первой матрицы
            matrixA[0, 0] = 4.5f;
            matrixA[0, 1] = 6.7f;
            matrixA[1, 0] = 2.3f;
            matrixA[1, 1] = 8.9f;

            // Заполнение значениями для второй матрицы
            matrixB[0, 0] = 1.1f;
            matrixB[0, 1] = 2.2f;
            matrixB[1, 0] = 3.3f;
            matrixB[1, 1] = 4.4f;

            // Ожидаемый результат после сложения
            var expected = new Matrix<float>(2, 2);
            expected[0, 0] = 5.6f;
            expected[0, 1] = 8.9f;
            expected[1, 0] = 5.6f;
            expected[1, 1] = 13.3f;

            // Выполнение операции сложения
            var result = matrixA + matrixB;

            // Проверка правильности сложения матриц
            for (int i = 0; i < expected.RowCount; i++)
            {
                for (int j = 0; j < expected.ColumnCount; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], 0.0001f);
                }
            }
        }

        [TestMethod]
        public void MatrixEquality_DifferentMatrices_ReturnsFalse()
        {
            var matrixA = new Matrix<int>(2, 2);
            var matrixB = new Matrix<int>(2, 2);

            matrixA[0, 0] = 1;
            matrixA[0, 1] = 2;
            matrixA[1, 0] = 3;
            matrixA[1, 1] = 4;

            matrixB[0, 0] = 5;
            matrixB[0, 1] = 6;
            matrixB[1, 0] = 7;
            matrixB[1, 1] = 8;

            for (int i = 0; i < matrixA.RowCount; i++)
            {
                for (int j = 0; j < matrixA.ColumnCount; j++)
                {
                    Assert.AreNotEqual(matrixA[i, j], matrixB[i, j]);
                }
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentExceptionInMultiplicationWhenOneMatrixIsEmptyTest()
        {
            // Создаем матрицу с нулевыми размерами (0x0)
            var matrixA = new Matrix<int>(0, 0);  // Передаем 0 строк и 0 столбцов

            // Создаем матрицу размером 2x2
            var matrixB = new Matrix<int>(2, 2);
            matrixB[0, 0] = 7;
            matrixB[0, 1] = 2;
            matrixB[1, 0] = 1;
            matrixB[1, 1] = 0;

            // Пытаемся умножить пустую матрицу на обычную, что должно вызвать исключение
            var result = matrixA * matrixB;
        }




    }
}

