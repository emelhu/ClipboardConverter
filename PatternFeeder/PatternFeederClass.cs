using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eMeL.PatternFeeder
{
  public class PatternFeeder 
  {
    public string pattern { get; private set; }

    public List<CombineData[]> data1List { get; private set; }
    public List<CombineData[]> data2List { get; private set; }
    public List<CombineData[]> data3List { get; private set; }

    private static Char[]      splitChars = new Char[] { '\n' };

    public enum Type { Rtf, Html };

    public Type type { get; private set; }

    //

    public PatternFeeder(string pattern)
    {
      this.pattern = pattern;

      this.data1List = null;
      this.data2List = null;
      this.data3List = null;


      if (isRtfPattern())
      {
        type = Type.Rtf;
      }
      else if (isHtmPattern())
      {
        type = Type.Html;
      }
      else
      {
        throw new Exception("Invalid pattern format!\n[" + pattern.Substring(0, 1024) + "...] {0C546D06-47CC-4AFA-BD95-1FAF1B6B4E1D}");
      }
    }

    //

    public void buildData1List(string[] text)
    {
      data1List = buildCombineDataList(text);
    }

    public void buildData2List(string[] text)
    {
      data2List = buildCombineDataList(text);
    }

    public void buildData3List(string[] text)
    {
      data3List = buildCombineDataList(text);
    }


    public void buildData1List(string text)
    {
      buildData1List(text.Split(splitChars));
    }

    public void buildData2List(string text)
    {
      buildData2List(text.Split(splitChars));
    }

    public void buildData3List(string text)
    {
      buildData3List(text.Split(splitChars));
    }


    public void addToData1List(string[] text)
    {
      if (data1List == null)
      {
        data1List = new List<CombineData[]>();
      }

      data1List.Add(buildCombineData(text));
    }

    public void addToData2List(string[] text)
    {
      if (data2List == null)
      {
        data2List = new List<CombineData[]>();
      }

      data2List.Add(buildCombineData(text));
    }

    public void addToData3List(string[] text)
    {
      if (data3List == null)
      {
        data3List = new List<CombineData[]>();
      }

      data3List.Add(buildCombineData(text));
    }


    public void addToData1List(string text)
    {
      addToData1List(text.Split(splitChars));
    }

    public void addToData2List(string text)
    {
      addToData2List(text.Split(splitChars));
    }

    public void addToData3List(string text)
    {
      addToData3List(text.Split(splitChars));
    }

    //

    private static CombineData[] buildCombineData(string[] data)
    {
      List<CombineData> list = new List<CombineData>();

      foreach (string line in data)
      {
        if (line.Trim().Length > 0)
        { // empty line filtered.
          string[] parts = line.Split(new Char[] { ':' });

          if (parts.Length < 2)
          {
            throw new Exception("There isn't ':' separator in line '" + line + "' {C43B4471-0D21-40B4-B080-253881329062}");
          }

          string[] keyAndParam = parts[0].Split(new Char[] { '/' });

          CombineData cd = new CombineData(keyAndParam[0], parts[1]);

          if (keyAndParam.Length > 1)
          {
            string param = keyAndParam[1];

            if (param == "I")
            {
              cd.italics = true;
            }
            else
            {
              throw new Exception("There is an unrecognised parameter in key of line '" + line + "' {496C1AC8-1226-4D15-9E82-076305351547}");
            }
          }

          list.Add(cd);
        }
      }

      return list.ToArray();
    }


    private static List<CombineData[]> buildCombineDataList(string[] data)
    {
      var ret = new List<CombineData[]>();

      List<string> lines = null;


      foreach (string line in data)
      {
        if (line.Trim().Length > 0)
        { // Separator empty line filtered.
          if (line.StartsWith(">>>>>>>>>>"))
          { // Start definition of data of new page/document.
            if (lines != null)
            {
              ret.Add(buildCombineData(lines.ToArray()));
              lines = null;
            } 
          }
          else
          {
            if (lines == null)
            {
              lines = new List<string>();
            }

            lines.Add(line);
          }
        }
      }

      if (lines != null)
      {
        ret.Add(buildCombineData(lines.ToArray()));
      }

      return ret;
    }

    //

    public string getCombinedPattern(int ixData1, int ixData2 = -1, int ixData3 = -1)
    {
      // TODO: !!!

      string        ret   = null;
      CombineData[] data1 = null;
      CombineData[] data2 = null;
      CombineData[] data3 = null;

      if ((ixData1 >= 0) && (data1List != null) && (data1List.Count > ixData1))
      {
        data1 = data1List[ixData1];
      }

      if ((ixData2 >= 0) && (data2List != null) && (data2List.Count > ixData2))
      {
        data2 = data2List[ixData2];
      }

      if ((ixData3 >= 0) && (data3List != null) && (data3List.Count > ixData3))
      {
        data3 = data3List[ixData3];
      }

      //

      if ((data1 != null) || (data2 != null) || (data3 != null))
      {
        List<CombineData> fullCombineData = new List<CombineData>(); 

        if (data1 != null) 
        {
          fullCombineData.AddRange(data1);
        }
        
        if (data2 != null) 
        {
          fullCombineData.AddRange(data2);
        }
          
        if (data3 != null)
        {
          fullCombineData.AddRange(data3);
        }

        ret = getCombinedPattern(pattern, fullCombineData.ToArray());
      }

      //

      return ret;
    }
    //---------------------------------------------------------------------------------------------

    private string getCombinedPattern(string text, CombineData[] data)
    {
      bool doIt; 
      int  maxDepth = 100;             

      do
      { // for recursive use; if data contains other CombineData.key
        doIt = false; 

        foreach (CombineData cd in data)
        {
          if (text.Contains(cd.key))
          {
            doIt = true;
            text = text.Replace(cd.key, cd.getFormattedText(this.type));
          }
        }

        if (--maxDepth <= 0)
        {
          throw new Exception("Depth of recursion exceed the limit! {35046C35-64C5-4DAB-A017-6B9D8AB1D7FF}");
        }
      }
      while (doIt);

      return text;
    }

    //---------------------------------------------------------------------------------------------

    public Dictionary<string, string> getCombinedPatternAll()
    {
      var ret = new Dictionary<string, string>();                       // string1:name; string2:combinedtext

      if ((data1List != null) && (data2List != null) && (data3List != null))
      {
        for (int i = 0; i < data1List.Count; i++)
        {
          for (int j = 0; j < data2List.Count; j++)
          {
            for (int k = 0; k < data3List.Count; k++)
            {
              string n = getIdText(i, data1List.Count, j, data2List.Count, k, data3List.Count);
              string s = getCombinedPattern(i, j, k);

              ret.Add(n, s);
            }
          }
        }
      }
      else if ((data1List != null) && (data2List != null))
      {
        for (int i = 0; i < data1List.Count; i++)
        {
          for (int j = 0; j < data2List.Count; j++)
          {
            string n = getIdText(i, data1List.Count, j, data2List.Count, 0, 0);
            string s = getCombinedPattern(i, j, -1);

            ret.Add(n, s);
          }          
        }
      }
      else if ((data1List != null) && (data3List != null))
      {
        for (int i = 0; i < data1List.Count; i++)
        {
          for (int j = 0; j < data3List.Count; j++)
          {
            string n = getIdText(i, data1List.Count, 0, 0, j, data3List.Count);
            string s = getCombinedPattern(i, -1, j);

            ret.Add(n, s);
          }
        }
      }
      else if ((data2List != null) && (data3List != null))
      {
        for (int i = 0; i < data2List.Count; i++)
        {
          for (int j = 0; j < data3List.Count; j++)
          {
            string n = getIdText(0, 0, i, data2List.Count, j, data3List.Count);
            string s = getCombinedPattern(-1, i, j);

            ret.Add(n, s);
          }
        }
      }
      else if (data1List != null)  
      {
        for (int i = 0; i < data1List.Count; i++)
			  {
          string n = getIdText(i, data1List.Count, 0, 0, 0, 0);
          string s = getCombinedPattern(i, -1, -1);

          ret.Add(n, s);
			  }
      }
      else if (data2List != null)  
      {
        for (int i = 0; i < data2List.Count; i++)
        {
          string n = getIdText(0, 0, i, data2List.Count, 0, 0);
          string s = getCombinedPattern(-1, i, -1);

          ret.Add(n, s);
        }
      }
      else if (data3List != null)  
      {
        for (int i = 0; i < data3List.Count; i++)
        {
          string n = getIdText(0, 0, 0, 0, i, data3List.Count);
          string s = getCombinedPattern( -1, -1, i);

          ret.Add(n, s);
        }
      }

      return ret;
    }
    //---------------------------------------------------------------------------------------------

    private string getIdText(int ix1, int count1, int ix2, int count2, int ix3, int count3)
    {
      string ret = "";

      if (count1 > 0)
      {
        ret += "_" + ix1.ToString(intFormatter(count1));
      }

      if (count2 > 0)
      {
        ret += "_" + ix2.ToString(intFormatter(count2));
      }

      if (count3 > 0)
      {
        ret += "_" + ix3.ToString(intFormatter(count3));
      }

      return ret;
    }

    private string intFormatter(int max)
    {
      string ret = "D";
      string tmp = max.ToString(ret);

      return ret + tmp.Length.ToString();
    }
    //---------------------------------------------------------------------------------------------

    private bool isRtfPattern()
    {
      string start = pattern.Substring(0, 128).Trim().ToLower();

      return start.StartsWith(@"{\rtf");
    }

    private bool isHtmPattern()
    {
      if (isRtfPattern())
      {
        return false;
      }

      string start = pattern.Substring(0, 1024).Trim().ToLower();

      return start.Contains(@"<html");
    }
    //---------------------------------------------------------------------------------------------



  }

  //===============================================================================================
  public struct CombineData
  {
    public string key;
    public string text;
    public bool   italics;

    public CombineData(string key, string text)
    {
      this.key     = key;
      this.text    = text;

      this.italics = false;
    }

    public string getFormattedText(PatternFeeder.Type type)
    {
      if (this.italics)
      { // .... maybe more modifiers later
        string before = "";
        string after  = "";

        if (this.italics)
        {
          if (type == PatternFeeder.Type.Rtf)
          {
            before = before  + @"\i ";
            after  = @"\i0 " + after;
          }
          else if (type == PatternFeeder.Type.Html)
          {
            before = before + "<i>";
            after  = "</i>" + after;
          }
        }

        return before + this.text + after;
      }

      return this.text;
    }
  }
}
