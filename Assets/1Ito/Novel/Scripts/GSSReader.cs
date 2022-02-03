using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class GSSReader : MonoBehaviour
{
    public string SheetID = "�ǂݍ��ރV�[�g��ID";
    public string SheetName = "�ǂݍ��ރV�[�g";
    public UnityEvent OnLoadEnd;

    public bool IsLoading { get; private set; }
    public string[][] Datas { get; private set; }

    IEnumerator GetFromWeb()
    {
        IsLoading = true;

        var tqx = "tqx=out:csv";
        var url = "https://docs.google.com/spreadsheets/d/" + SheetID + "/gviz/tq?" + tqx + "&sheet=" + SheetName;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        IsLoading = false;

        var protocol_error = request.result == UnityWebRequest.Result.ProtocolError;
        var connection_error = request.result == UnityWebRequest.Result.ConnectionError;
        if (protocol_error || connection_error)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Datas = ConvertCSVtoJaggedArray(request.downloadHandler.text);
            OnLoadEnd.Invoke();
        }

    }

    public void Reload() => StartCoroutine(GetFromWeb());

    static string[][] ConvertCSVtoJaggedArray(string t)
    {
        var reader = new StringReader(t);
        reader.ReadLine();  //�w�b�_�ǂݔ�΂�
        var rows = new List<string[]>();
        while (reader.Peek() >= 0)
        {
            var line = reader.ReadLine();        // ��s���ǂݍ���
            var elements = line.Split(',');    // �s�̃Z����,�ŋ�؂���
            for (var i = 0; i < elements.Length; i++)
            {
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
            }
            rows.Add(elements);
        }
        return rows.ToArray();
    }
}