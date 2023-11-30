using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.InputSystem;
using Unity.VisualScripting.Dependencies.NCalc;

public class DataManager
{
    public Dictionary<Define.CharacterType, CharacterData> CharacterDatas { get; private set; }
    public List<ChapterInfoData> ChapterInfos { get; private set; }
    public Dictionary<int, List<WaveInfoData>> WaveInfos { get; private set; }

    public void Init()
    {
        CharacterDatas = LoadToDictionary<Define.CharacterType, CharacterData>(Path.Combine("Data", "CharacterData"));

        WaveInfos = new();
        List<WaveInfoData> waveInfoDatas = LoadToList<WaveInfoData>(Path.Combine("Data", "WaveInfoData"));
        foreach (WaveInfoData data in waveInfoDatas)
        {
            if (false == WaveInfos.ContainsKey(data.ChapterID))
            {
                WaveInfos[data.ChapterID] = new List<WaveInfoData> { new WaveInfoData() };
                WaveInfos[data.ChapterID][0].ChapterID = data.ChapterID;
            }

            WaveInfos[data.ChapterID].Add(data);
        }

    }

    public List<T> LoadToList<T>(string path)
        where T : ICsvParsable, new()
    {
        var textAsset = Resources.Load<TextAsset>(path);
        CsvItem[][] items = null;
        items = ParseTextAsset(textAsset.text, items);

        var list = new List<T>();

        for (int row = 1; row < items.Length; ++row)
        {
            var data = new T();
            data.Parse(items[row]);

            list.Add(data);
        }

        return list;
    }

    public Dictionary<TKey, TValue> LoadToDictionary<TKey, TValue>(string path)
        where TValue : ICsvParsable, IKeyOwned<TKey>, new()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        CsvItem[][] items = null;
        items = ParseTextAsset(textAsset.text, items);

        Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

        for (int row = 1; row < items.Length; ++row)
        {
            var data = new TValue();
            data.Parse(items[row]);

            dict[data.Key] = data;
        }

        return dict;

    }

    CsvItem[][] ParseTextAsset(string data, CsvItem[][] items)
    {
        string[] rows = data.Split(Environment.NewLine);

        items = new CsvItem[rows.Length][];

        for (int row = 0; row < rows.Length; ++row)
        {
            string[] columns = rows[row].Split(',');
            items[row] = new CsvItem[columns.Length];

            for (int column = 0; column < columns.Length; ++column)
            {
                items[row][column].Data = columns[column];
            }
        }

        return items;
    }

    public struct CsvItem
    {
        public string Data { get; set; }

        public short ToShort() => Convert.ToInt16(Data);

        public int ToInt() => Convert.ToInt32(Data);

        public long ToLong() => Convert.ToInt64(Data);

        public float ToFloat() => Convert.ToSingle(Data);

        public double ToDouble() => Convert.ToDouble(Data);

        public override string ToString() => Data;
    }

    public interface IKeyOwned<T>
    {
        public T Key { get; }
    }

    public interface ICsvParsable
    {
        void Parse(CsvItem[] row);
    }
}