using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CsvReader
{
    Queue<string> mLines;
    Dictionary<string, int> mHeaders;
    string[] currentLine;
    public CsvReader(string file)
    {
        mHeaders = new Dictionary<string, int>();

        mLines = new Queue<string>(File.ReadAllLines(file));
        string[] headers = mLines.Dequeue().Split(',');
        int idx = 0;
        foreach(var header in headers)
        {
            mHeaders.Add(header, idx++);
        }
    }

    public bool Read()
    {
        if (mLines.Count > 0)
        {
            currentLine = mLines.Dequeue().Split(',');
            return true;
        }
        return false;
    }

    public int GetHeaderIndex(string head)
    {
        if (!mHeaders.ContainsKey(head))
            return -1;
        return mHeaders[head];
    }

    public string GetFieldOrEmpty(string head)
    {
        if (!mHeaders.ContainsKey(head))
            return "";
        return currentLine[mHeaders[head]];
    }
}
