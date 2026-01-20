using System.Diagnostics;
using Compression;

namespace UnitTest;

[TestClass]
public sealed class TestDict
{
    [TestMethod]
    // Tests that a LZW object can be created and initialized for digits 0-9
    public void TestDictInit()
    {
        // Create LZW compressor initialized with digits 0-9
        LZW compressor = new LZW();
        int res = compressor.initDict(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'], 10);
        // Assert init returns success
        Assert.AreEqual(1, res);

        // Search compressor dict for each digit 0-9, asserting that each digit is found
        int i;
        for (i = 0; i< 10; i++)
        {
            res = compressor.searchDict(i.ToString());
            Assert.AreNotEqual(-1, res);
        }   

        return;
    }
    
    [TestMethod]
    public void TestCompress()
    {
        // Create LZW compressor initialized with digits 0-9
        LZW compressor = new LZW();
        int res = compressor.initDict(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'], 10);
        // Assert init returns success
        Assert.AreEqual(1, res);

        // Test input that cant be compressed
        string incompressible = "0123456789";
        string compString = compressor.compressInput(incompressible);
        // Assert input and output are equal
        Assert.AreEqual(incompressible, compString);

        string seccondInput = "000111222333444555";
        string seccondCompString = compressor.compressInput(seccondInput);
        // Assert output has been compressed
        Assert.IsLessThan(seccondInput.Length, seccondCompString.Length);
    }
}
