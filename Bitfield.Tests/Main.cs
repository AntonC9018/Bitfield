namespace Bitfield.Tests
{
    using Bitfield.Tests.Generated;
    using Xunit;
    using BF = Kari.Plugins.Bitfield;

    public enum FlagsEnum { A = 1, B = 2, C = 4, D = 8 }

    [BF.Specification("MyBitfield"/*, DynamicallySized = false, Struct = true*/)]
    interface IMyBitfield
    {
        [BF.Bits(2)]       int Stuff2 { get; set; }
        [BF.Bits(4)]       int Stuff4 { get; set; }
        [BF.Range(10, 11)] int Ranged10_11 { get; set; }
        [BF.Bit]           bool IsSet { get; set; } 
        [BF.Flags]         FlagsEnum FlagsEnum { get; set; }
    }

    public class TestsStuff
    {
        [Fact]
        void Works()
        {
            unsafe { Assert.Equal(sizeof(MyBitfield), sizeof(int)); }

            MyBitfield bf = new MyBitfield();
            Assert.Equal(0, bf.Stuff2);
            Assert.Equal(0, bf.Stuff4);
            Assert.Equal(10, bf.Ranged10_11);
            Assert.False(bf.IsSet);
            Assert.Equal((FlagsEnum) 0, bf.FlagsEnum);

            bf.Stuff2 = 3;
            Assert.Equal(3, bf.Stuff2);

            // Overflow ignored by default.            
            bf.Stuff2 = 4;
            Assert.Equal(0, bf.Stuff2);

            bf.Stuff2++;
            Assert.Equal(1, bf.Stuff2);

            bf.Ranged10_11 = 11;
            Assert.Equal(11, bf.Ranged10_11);
            
            FlagsEnum f = FlagsEnum.A | FlagsEnum.D;
            bf.FlagsEnum |= f;
            Assert.Equal(f, bf.FlagsEnum);
        }
    }
}