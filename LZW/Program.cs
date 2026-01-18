// Used for Assert
using System.Diagnostics;
// Import LZW compression code
using Compression;

class Program{
    // Returns a 3 char string between < 000 - 255 >
    private static string createAndPadRGBValue(Random rnd){
        // Get a seeded random number between 0 and 255
        string RGBval = rnd.Next(0, 255).ToString();
        
        // Pad left until 3 chars
        while (RGBval.Length < 3){
            RGBval = "0" + RGBval;
        }

        return RGBval;
    }


    // Returns a padded string of 9 chars defining one RGB color
    private static string createRgbColor(Random rnd){
        // Create one 3-char value for each field
        string R = createAndPadRGBValue(rnd);
        string G = createAndPadRGBValue(rnd);
        string B = createAndPadRGBValue(rnd);

        // Return as one string
        return (R+G+B);
    }

    // Create a data stream for N number of pixels, where N=pixelCount
    private static string createRgbDataStream(int pixelCount){
        // Seed for the same result each time
        Random rnd = new Random(10);

        // Init output
        string data = "";
        
        // Loop to write data stream
        int i;
        for(i = 0; i < pixelCount; i++){
            data += createRgbColor(rnd);
        }

        return data;
    }

    // Run
    static void Main() {
        Console.WriteLine("Starting...");
        // Sets number of pixels in datastream
        int amount = 100;
        // Create data stream as a string
        string RGB = createRgbDataStream(amount);

        // Assert correct string lenght, number of pixels * 3 color channels * 3 chars per channel
        Debug.Assert(RGB.Length == amount*3*3);

        
        LZW compressor = new LZW();
        compressor.testImport();
        Console.WriteLine("Finished!");
    }
}



