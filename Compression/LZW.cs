
using System.Diagnostics;

namespace Compression;

public class LZW
{
    /* 
    LZW Stores all codes of N length in a dictionary, going from all codes of length N, N+1, N+2...., N+M 
    Input stream is then converted into entry keys for this dictionary
    */
    List<String> CodeDictionary = [];
    public int testImport()
    {
        Console.WriteLine("Class imported!");
        return 1;
    }

    // First step of LZW is to initialize the dictionary with all possible codes of length 1
    public int initDict(char[] characters, int size)
    {
        // Dictionary index starts at 0
        int idx;
        
        for (idx = 0; idx < size; idx++)
        {
            // Convert char to string, as dictionary will contain multi-char strings
            this.CodeDictionary.Add(characters[idx].ToString());
        }

        Debug.Assert(this.CodeDictionary.Count != 0);
        // Success
        return 1;
    }

    // Search for given code in dict
    public int searchDict(string code)
    {
        int codeIDX = this.CodeDictionary.FindIndex(x=> x == code);
        return codeIDX;
    }

    // Asserts that the code dictionary is initiated
    private void assertCodedictStatus()
    {
        Trace.Assert(this.CodeDictionary.Count() > 0);
        return;
    }

    // Compress given input with LZW algorithm, compressor must be initialized with 'initDict()' before use
    public string compressInput(string input)
    {
        // Ensure dict is initiated
        this.assertCodedictStatus();
        // Will get populated by indexes into the compressor dictionary
        string outputString = "";

        // Iter for input string
        int strIter = 0;
        // Current input length, starts at 1 and grows
        int strSelectLen = 1;
        // Substring of input, used to fetch index codes from dictionary
        string currentCode = "";
        int prevDictIDX = 0;
        int dictResult = 0;

        while(strIter + strSelectLen <= input.Length)
        {
            // Get substring of input
            currentCode = input.Substring(strIter, strSelectLen);
            // Check if substring is stored in code dict
            dictResult = this.searchDict(currentCode);
            // Store dict index, or return last found index if the code is missing
            if (dictResult != -1)
            {
                // Code is found, store index for later
                prevDictIDX = dictResult;
                // Increase length of code string
                strSelectLen += 1;

            } else
            {
                // Code is not found in dict, send last found idx to output, and add code to dict
                outputString += prevDictIDX;
                this.CodeDictionary.Add(currentCode);
                // Move iterator forward
                strIter += (strSelectLen-1);
                // Reset code length
                strSelectLen = 1;
            }
        }

        // Remember to add last Code index before returning result
        outputString += prevDictIDX;

        // Return compressed string
        return outputString;
    }

    // Using only the initial LZW dictionary of all one-length codes, decompress the given string
    // Requires a new LZD object with an initialized code dictionary, otherwise behaviour is undefined
    public string decompressInput(string input)
    {
        // Ensure dict is initiated
        this.assertCodedictStatus();

        string outputString = "";
        return outputString;
    }
    

    // Prints each dictionary entry in terminal
    public void printDict()
    {
        foreach(string code in this.CodeDictionary)
        {
            int idx = this.searchDict(code);
            Console.WriteLine($"Code: {code}, Index: {idx}");
        }

        return;
    }

    // Returns Code Dictionary as a list
    public List<String> returnDict()
    {
        return this.CodeDictionary;
    }
}
