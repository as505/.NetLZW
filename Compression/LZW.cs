
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
        string output = "";

        // Iter for input string
        int strIter = 0;
        // Current input length, starts at 1 and grows
        int strSelectLen = 1;
        // Substring of input, used to fetch index codes from dictionary
        string currentCode = "";

        while(strIter + strSelectLen < input.Length+1)
        {
            currentCode = input.Substring(strIter, strSelectLen);
            int found = this.searchDict(currentCode);
            // If we find the subsring in the code dict, replace substring with code index
            if (found != -1)
            {
                output += found.ToString();
            }
            strIter++;
        }

        // Return compressed string
        return output;
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
