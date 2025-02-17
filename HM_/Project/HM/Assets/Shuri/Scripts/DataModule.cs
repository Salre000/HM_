using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataModule
{
    // 保存
    public static void Save<T>(T data,string filePath)
    {
        // json変換
        string json = JsonUtility.ToJson(data);

        // 書き込み指定
        StreamWriter wr = new(filePath, false);

        // 書き込み
        wr.WriteLine(json);

        // ファイルを閉じる
        wr.Close();
    }

    // jsonファイル読み込み
    public static T Load<T>(string path)
    {
        // 読み込み指定
        StreamReader rd = new(path);

        // ファイル内容全て読み込む
        string json = rd.ReadToEnd();

        // ファイルを閉じる
        rd.Close();

        T aaa = JsonUtility.FromJson<T>(json);

        // jsonファイルを型に戻して返す
        return JsonUtility.FromJson<T>(json);
    }
}
