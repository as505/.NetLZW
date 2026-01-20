
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

    // Compress given input with LZW algorithm, compressor must be initialized with 'initDict()' before use
    public string compressInput(string input)
    {
        // Will get populated by indexes into the compressor dictionary
        string outputString = "";

        // Iter for input string
        int strIter = 0;
        // Current input length, starts at 1 and grows
        int strSelectLen = 1;
        // Substring of input, used to fetch index codes from dictionary
        string currentCode = "";

        while(strIter + strSelectLen < input.Length+1)
        {
            currentCode = input.Substring(strIter, strSelectLen);
            int foundIDX = this.searchDict(currentCode);
            int prevIDX = foundIDX;
            // Loop until a code is not in dict
            while (foundIDX != -1)
            {
                if (strIter + strSelectLen > input.Length-1)
                {
                    break;
                }
                // Check longer code
                strSelectLen += 1;
                currentCode = input.Substring(strIter, strSelectLen);
                // Store last index
                prevIDX = foundIDX;
                // Find index for longer code string
                foundIDX = this.searchDict(currentCode);
            }
            // Add new code to dict
            this.CodeDictionary.Add(currentCode);

            // Reset
            strSelectLen = 1;
            // Append previous index to output string
            outputString += prevIDX;

            strIter++;
        }

        // Return compressed string
        return outputString;
    }

    

    // Prints each dictionary entry in terminal
    public void printDict()
    {
        foreach(string code in this.CodeDictionary)
        {
            Console.WriteLine(code);
        }

        return;
    }

    // Returns Code Dictionary as a list
    public List<String> returnDict()
    {
        return this.CodeDictionary;
    }
}
