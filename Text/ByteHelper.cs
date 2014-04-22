
using System;
using System.Collections;

namespace Softwarte.Helpers
{
    public static class ByteHelper
    {
        public static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1 == null)
            {
                throw new ArgumentNullException("a1");
            }

            if (a2 == null)
            {
                throw new ArgumentNullException("a2");
            }

            IStructuralEquatable eqa1 = a1;
            return eqa1.Equals(a2, StructuralComparisons.StructuralEqualityComparer);
        }

        public static byte[] CreateSpecialByteArray(int length)
        {
            if (length == 0)
            {
                throw new InvalidOperationException("length");
            }

            var arr = new byte[length];

            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = 0x20;
            }

            return arr;
        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            if (first == null)
            {
                throw new ArgumentNullException("first");
            }

            if (second == null)
            {
                throw new ArgumentNullException("second");
            }

            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }

        public static byte[] GetBytes(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                throw new ArgumentNullException(inputString);
            }

            var bytes = new byte[inputString.Length * sizeof(char)];
            Buffer.BlockCopy(inputString.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }

        public static string GetString(byte[] byteArray)
        {
            if (byteArray == null)
            {
                throw new ArgumentNullException("byteArray");
            }

            var chars = new char[byteArray.Length / sizeof(char)];
            Buffer.BlockCopy(byteArray, 0, chars, 0, byteArray.Length);
            return new string(chars);
        }
    }
}
