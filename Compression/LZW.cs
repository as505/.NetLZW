
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

    // Returns the code stored at the index in the code dict
    // If index is out of range, return empty string instead
    public string getCodeFromDict(int index)
    {
        if (index > this.CodeDictionary.Count -1)
        {
            return "";
        }

        string code = this.CodeDictionary[index];
        return code;

    }

    // Asserts that the code dictionary is initiated
    private void assertCodedictStatus()
    {
        Trace.Assert(this.CodeDictionary.Count() > 0);
        return;
    }

    // Compress given input with LZW algorithm, compressor must be initialized with 'initDict()' before use
    public List<int> compressInput(string input)
    {
        // Ensure dict is initiated
        this.assertCodedictStatus();

        // Each entry in list is one index to code dictionary
        List<int> outputList = [];

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
                outputList.Add(prevDictIDX);
                this.CodeDictionary.Add(currentCode);
                // Move iterator forward
                strIter += (strSelectLen-1);
                // Reset code length
                strSelectLen = 1;
            }
        }

        // Remember to add last Code index before returning result
        outputList.Add(prevDictIDX);

        // Return compressed string
        return outputList;
    }

    // Using only the initial LZW dictionary of all one-length codes, decompress the given string
    // Requires a new LZD object with an initialized code dictionary, otherwise behaviour is undefined
    public string decompressInput(List<int> input)
    {
        // Ensure dict is initiated
        this.assertCodedictStatus();

        string outputString = "";
        string prevEmitString = "";

        int strIter = 0;

        foreach(int IDX in input)
        {
            // Check code dict
            string dictString = this.getCodeFromDict(IDX);
            // Blank string means index is not in dict
            if (dictString != "")
            {
                // Emit to output
                outputString += dictString;
                // Concatenate previously emitted string with first symbol of current string, and add to code dict
                if (prevEmitString != "")
                {
                    string newDictEntry = prevEmitString + dictString.Substring(0, 1);
                    this.CodeDictionary.Add(newDictEntry);
                }

                // Replace previously emitted string with current string 
                prevEmitString = dictString;
            } else
            {
                // Concat previously emitted string with first symbol of current code
                string V = prevEmitString + currentCodeIDX.Substring(0, 1);
                // Add V to code dictionary and emit to output
                this.CodeDictionary.Add(V);
                outputString += V;
                prevEmitString = V;
            }

            strIter += 1;
        }
        

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
