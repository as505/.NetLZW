using System.Diagnostics;
using Compression;

namespace UnitTest;

[TestClass]
public sealed class TestDict
{
    private LZW initDigitsLZW()
    {
        // Create LZW compressor initialized with digits 0-9
        LZW compressor = new LZW();
        int res = compressor.initDict(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'], 10);
        // Success
        Assert.AreEqual(1, res);

        return compressor;
    }
    [TestMethod]
    // Tests that a LZW object can be created and initialized for digits 0-9
    public void TestDictInit()
    {
        // Create LZW compressor initialized with digits 0-9
        LZW compressor = this.initDigitsLZW();

        // Search compressor dict for each digit 0-9, asserting that each digit is found
        int i;
        for (i = 0; i< 10; i++)
        {
            int res = compressor.searchDict(i.ToString());
            Assert.AreNotEqual(-1, res);
        }   

        return;
    }
    
    [TestMethod]
    public void TestCompress()
    {
        // Create LZW compressor initialized with digits 0-9
        LZW compressor = this.initDigitsLZW();
        

        // Test input that cant be compressed
        string inputString = "0123456789";
        string compString = compressor.compressInput(inputString);
        // Assert input and output are equal
        Assert.AreEqual(inputString, compString);

        // Compressing first string again should yield better results, since codes are now in code dict
        string seccondCompString = compressor.compressInput(inputString);
        // Input should result in a different compressed string
        Assert.AreNotEqual(compString, seccondCompString);

        // The small input means it takes a few iterations before the string gets shorter, as multiple digits gets swapped with multi-digit indexes
        int i;
        // Initiate string as input string, this will get overwritten by progresively shorter compressed strings in the following loop
        string thirdCompString = compString;
        for (i = 0; i < 5; i++)
        {
            thirdCompString = compressor.compressInput(inputString);
        }
        
        Assert.IsLessThan(compString.Length, thirdCompString.Length);

        return;
    }

    [TestMethod]
    public void TestDifferentStartingDict()
    {
        // Create compressor initialized to A B C D
        LZW compressor = new LZW();
        compressor.initDict(['A', 'B', 'C', 'D'], 4);
        // Check dict contains only the four entries
        Assert.AreEqual(4, compressor.returnDict().Count());
        
        // Create input string using the four chars
        string input = "ABABABCDCDCDCDAABBAABB";
        string compressedString = compressor.compressInput(input);

        // Check that compression works
        Assert.AreNotEqual(input, compressedString);
        Assert.IsLessThan(input, compressedString);
    }
}
