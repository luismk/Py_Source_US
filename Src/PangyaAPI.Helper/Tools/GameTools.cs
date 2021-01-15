using System;
namespace PangyaAPI.Helper.Tools
{
    public static class GameTools
    {
        public static int[] _THole18 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
        public static int[] _TMap19 = { 0x14, 0x12, 0x13, 0x10, 0x0F, 0x0E, 0x0D, 0x0B, 0x08, 0x0A, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x09 };

        public static ushort GetMap()
        {
            var Map = _TMap19;
            byte I;
            byte S;
            byte A;
            byte B;

            for (I = 0; I <= _TMap19.Length - 1; I++)
            {
                S = (byte)new Random().Next(_TMap19.Length);
                A = (byte)Map[S];
                B = (byte)Map[I];
                Map[I] = A;
                Map[S] = B;
            }
            return (ushort)(Map[new Random().Next(Map.Length)]);
        }

        public static int[] RandomHole()
        {
            int I;
            var Values = _THole18;
            for (I = 0; I <= _THole18.GetUpperBound(0); I++)
            {
                var Rnd = new Random();
                if (I != _THole18.Length)
                {
                    SwapX(ref Values[I], ref Values[I + Rnd.Next(Values.Length - I)]);
                }
            }
            return Values;
        }

        public static int[] RandomMap()
        {
            int I;
            var Values = _TMap19;
            for (I = 0; I <= _TMap19.GetUpperBound(0); I++)
                SwapX(ref Values[I], ref Values[I + new Random().Next(Values.Length - I)]);
            return Values;
        }

        public static void SwapX(ref int lhs, ref int rhs)
        {
            int tmp;
            tmp = lhs;
            lhs = rhs;
            rhs = tmp;
        }

    }  
}
