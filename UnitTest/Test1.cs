using Compression;

namespace UnitTest;

[TestClass]
public sealed class TestLZW
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

    // Tests that a LZW object can be created and initialized for digits 0-9
    [TestMethod]
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
        List<int> compressedData = compressor.compressInput(inputString);
        

        // Compressing first string again should yield better results, since codes are now in code dict
        List<int> seccondCompData = compressor.compressInput(inputString);
        // Input should result in a different compressed string
        Assert.AreNotEqual(compressedData, seccondCompData);
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
        List<int> compressedString = compressor.compressInput(input);

    }

    [TestMethod]
    public void TestDecode()
    {
        // Init two LZW objects with the same starting code dictionary
        LZW compressor = new LZW();
        LZW decoder = new LZW();
        compressor.initDict(['A', 'B'], 2);
        decoder.initDict(['A', 'B'], 2);


        // Create an input message
        string input = "ABABABBAA";
        

        List<int> compressedData = compressor.compressInput(input);
        string decompressed = decoder.decompressInput(compressedData);

        Assert.AreEqual(input, decompressed);
    }

    [TestMethod]
    public void TestLongEncodeDecode()
    {
        char[] letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',];
        // Create LZW objects initialized to all digits 0-9
        LZW compressor = new LZW();
        LZW decoder = new LZW();
        compressor.initDict(letters, 8);
        decoder.initDict(letters, 8);

        // Generate long input
        int seed = 10;
        Random rnd = new Random(seed);

        string input = "";
        int i;
        int prev = 0;

        // Create random string with somewhat repeating patterns
        for (i = 0; i < 250; i++)
        {
            // 50% chance to repeat last letter
            if (rnd.Next(0, 1) == 1)
            {
                input += letters[prev];
                
            } else
            { 
                // 50% chance to chose a new letter
                int randNum = rnd.Next(0, 7);
                prev = randNum;
                input += letters[randNum];
            }
        }

        List<int> compressedData = compressor.compressInput(input);
        string decodedString = decoder.decompressInput(compressedData);

        Assert.AreEqual(input, decodedString);
    }
}
