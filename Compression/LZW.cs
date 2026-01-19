
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

    public void printDict()
    {
        foreach(string code in this.CodeDictionary)
        {
            Console.WriteLine(code);
        }

        return;
    }
}
