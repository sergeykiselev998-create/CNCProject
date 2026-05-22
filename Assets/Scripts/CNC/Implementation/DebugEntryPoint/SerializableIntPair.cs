using System;

namespace CNC.Implementation.DebugEntryPoint
{
    [Serializable]
    public class SerializableIntPair
    {
        public int Value1;
        public int Value2;

        public SerializableIntPair(int value1, int value2)
        {
            Value1 = value1;
            Value2 = value2;
        }
    }
}