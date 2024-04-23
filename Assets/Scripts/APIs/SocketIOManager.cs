using System.Collections;
using System.Collections.Generic;
using BestHTTP.SocketIO;
using UnityEngine;
using UnityEngine.Events;
using System;
//using Newtonsoft.Json;
using BestHTTP.SocketIO.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using DG.Tweening;
using System.Linq;
using BestHTTP;

public class SocketIOManager : MonoBehaviour
{
    [SerializeField]
    private SlotBehaviour slotManager;
    [SerializeField]
    private TextAsset myJsonFile;
    [SerializeField]
    private TextAsset ResultJsonFile;

    internal ResultData tempresult;

    private void Start()
    {
        ParseMyJson(myJsonFile.ToString(), false);
    }

    private void ParseMyJson(string jsonObject, bool type)
    {
        try
        {
            jsonObject = jsonObject.Replace("\\", string.Empty);
            jsonObject = jsonObject.Trim();
            jsonObject = jsonObject.TrimStart('"').TrimEnd('"');
            if (!type)
            {
                InitialSlotData initialslots = JsonUtility.FromJson<InitialSlotData>(jsonObject);
                PopulateSlotSocket(initialslots.PopulateSlot, initialslots.X_values, initialslots.Y_values, initialslots.LineIDs);
            }
            else
            {
                ResultData slotResult = JsonUtility.FromJson<ResultData>(jsonObject);
                tempresult = slotResult;
            }
        }
        catch(Exception e)
        {
            Debug.Log("Error while parsing Json " + e.Message);
        }
    }

    private void PopulateSlotSocket(List<string> slotPop, List<string> x_val, List<string> y_val, List<int> LineIds)
    {
        for (int i = 0; i < slotPop.Count; i++)
        {
            List<int> points = slotPop[i]?.Split(',')?.Select(Int32.Parse)?.ToList();
            slotManager.PopulateInitalSlots(i, points);
        }

        for (int i = 0; i < slotPop.Count; i++)
        {
            slotManager.LayoutReset(i);
        }

        for (int i = 0; i < LineIds.Count; i++)
        {
            slotManager.FetchLines(x_val[i], y_val[i], LineIds[i], i);
        }

    }

    internal void AccumulateResult()
    {
        ParseMyJson(ResultJsonFile.ToString(), true);
    }
}

[Serializable]
public class InitialSlotData
{
    public List<string> PopulateSlot;
    public List<string> X_values;
    public List<string> Y_values;
    public List<int> LineIDs;
}

[Serializable]
public class ResultData
{
    public string StopList;
    public List<int> resultLine;
    public List<string> x_animResult;
    public List<string> y_animResult;
}
