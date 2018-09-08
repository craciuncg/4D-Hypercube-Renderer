using System;

namespace DimensionRenderer
{
    /*
       Author: Craciun Cristian-George
       Version: 1.0
       Description: This is the main code for rendering the 4D Hypercube using .NET library with some additional settings to it.
   */
    class MatMul
    {
        public static float[,] MatrixMult(float[,] a, float[,] b)
        {
            int rowsA = a.GetLength(0);
            int colsA = a.GetLength(1);

            int rowsB = b.GetLength(0);
            int colsB = b.GetLength(1);

            if (colsA != rowsB)
            {
                Console.WriteLine("Cols of A does not match rows of B!");
                return null;
            }

            float[,] result = new float[rowsA, colsB];


            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += a[i, k] * b[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }

        public static void LogMatrix(float[,] m)
        {
            Console.WriteLine(m.GetLength(0) + " X " + m.GetLength(1));
            Console.WriteLine("---------------------------------------------------------------------------------");
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    Console.Write(m[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------------------------------------------------------------------");
        }

        public static float[,] Vec2toMatrix(Vector2 v)
        {
            float[,] result = new float[2, 1];
            result[0, 0] = v.x;
            result[1, 0] = v.y;
            return result;
        }

        public static Vector2 MatrixtoVec2(float[,] m)
        {
            return new Vector2(m[0, 0], m[1, 0]);
        }

        public static float[,] MatrixMult(float[,] a, Vector2 v)
        {
            return MatrixMult(a, Vec2toMatrix(v));
        }

        public static float[,] Vec3toMatrix(Vector3 v)
        {
            float[,] result = new float[3, 1];
            result[0, 0] = v.x;
            result[1, 0] = v.y;
            result[2, 0] = v.z;
            return result;
        }

        public static Vector3 MatrixtoVec3(float[,] m)
        {
            return new Vector3(m[0, 0], m[1, 0], m[2, 0]);
        }

        public static float[,] MatrixMult(float[,] a, Vector3 v)
        {
            return MatrixMult(a, Vec3toMatrix(v));
        }

        public static float[,] Vec4toMatrix(Vector4 v)
        {
            float[,] result = new float[4, 1];
            result[0, 0] = v.x;
            result[1, 0] = v.y;
            result[2, 0] = v.z;
            result[3, 0] = v.w;
            return result;
        }

        public static Vector4 MatrixtoVec4(float[,] m)
        {
            if (m.Length != 4)
            {
                Console.WriteLine("Matrix to Vec4 conversion failed, rows of the matrix are != 4!");
                return null;
            }

            return new Vector4(m[0, 0], m[1, 0], m[2, 0], m[3, 0]);
        }
        public static float[,] MatrixMult(float[,] a, Vector4 v)
        {
            return MatrixMult(a, Vec4toMatrix(v));
        }
    }
}
