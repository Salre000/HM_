using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataModule
{
    // �ۑ�
    public static void Save<T>(T data,string filePath)
    {
        // json�ϊ�
        string json = JsonUtility.ToJson(data);

        // �������ݎw��
        StreamWriter wr = new(filePath, false);

        // ��������
        wr.WriteLine(json);

        // �t�@�C�������
        wr.Close();
    }

    // json�t�@�C���ǂݍ���
    public static T Load<T>(string path)
    {
        // �ǂݍ��ݎw��
        StreamReader rd = new(path);

        // �t�@�C�����e�S�ēǂݍ���
        string json = rd.ReadToEnd();

        // �t�@�C�������
        rd.Close();

        T aaa = JsonUtility.FromJson<T>(json);

        // json�t�@�C�����^�ɖ߂��ĕԂ�
        return JsonUtility.FromJson<T>(json);
    }
}
