using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using DG.Tweening;
using System.Linq;
using Newtonsoft.Json;
using Best.SocketIO;
using Best.SocketIO.Events;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using Best.HTTP.Shared;

public class SocketIOManager : MonoBehaviour
{
    [SerializeField]
    private SlotBehaviour slotManager;

    [SerializeField]
    private UIManager uiManager;

    internal GameData InitialData = null;
    internal UiData UIData = null;
    internal Root ResultData = null;
    internal Player PlayerData = null;
    [SerializeField]
    internal List<string> bonusdata = null;
    //WebSocket currentSocket = null;
    internal bool isResultdone = false;

    // protected string nameSpace="game"; //BackendChanges
    protected string nameSpace = "playground"; //BackendChanges
    private Socket gameSocket; //BackendChanges
    private SocketManager manager;

    [SerializeField]
    internal JSHandler _jsManager;

    [SerializeField] internal JSFunctCalls JSManager;

    protected string SocketURI = null;
    //protected string TestSocketURI = "https://game-crm-rtp-backend.onrender.com/";
    protected string TestSocketURI = "https://frnp4zmn-5000.inc1.devtunnels.ms/";

    [SerializeField]
    private string testToken;

    protected string gameID = "SL-GOE";
   // protected string gameID = "";

    internal bool isLoaded = false;

    internal bool SetInit = false;

    private const int maxReconnectionAttempts = 6;
    private readonly TimeSpan reconnectionDelay = TimeSpan.FromSeconds(10);

    private void Awake()
    {
        //Debug.unityLogger.logEnabled = false;
        isLoaded = false;
        SetInit = false;
    }

    private void Start()
    {
        //OpenWebsocket();
        OpenSocket();
    }

    void ReceiveAuthToken(string jsonData)
    {
        Debug.Log("Received data: " + jsonData);

        // Parse the JSON data
        var data = JsonUtility.FromJson<AuthTokenData>(jsonData);
        SocketURI = data.socketURL;
        myAuth = data.cookie;

        nameSpace = data.nameSpace; //BackendChanges
        // Proceed with connecting to the server using myAuth and socketURL
    }

    string myAuth = null;

    private void OpenSocket()
    {
        // Create and setup SocketOptions
        SocketOptions options = new SocketOptions();
        options.ReconnectionAttempts = maxReconnectionAttempts;
        options.ReconnectionDelay = reconnectionDelay;
        options.Reconnection = true;
        options.ConnectWith = Best.SocketIO.Transports.TransportTypes.WebSocket; //BackendChanges

        // Application.ExternalCall("window.parent.postMessage", "authToken", "*");

#if UNITY_WEBGL && !UNITY_EDITOR
    string url = Application.absoluteURL;
    Debug.Log("Unity URL : " + url);
    ExtractUrlAndToken(url);

    Func<SocketManager, Socket, object> webAuthFunction = (manager, socket) =>
    {
      return new
      {
        token = testToken,
      };
    };
    options.Auth = webAuthFunction;
#else
        Func<SocketManager, Socket, object> authFunction = (manager, socket) =>
        {
            return new
            {
                token = testToken,
            };
        };
        options.Auth = authFunction;
#endif
        // Proceed with connecting to the server
        SetupSocketManager(options);
    }

    public void ExtractUrlAndToken(string fullUrl)
    {
        Uri uri = new Uri(fullUrl);
        string query = uri.Query; // Gets the query part, e.g., "?url=http://localhost:5000&token=e5ffa84216be4972a85fff1d266d36d0"

        Dictionary<string, string> queryParams = new Dictionary<string, string>();
        string[] pairs = query.TrimStart('?').Split('&');

        foreach (string pair in pairs)
        {
            string[] kv = pair.Split('=');
            if (kv.Length == 2)
            {
                queryParams[kv[0]] = Uri.UnescapeDataString(kv[1]);
            }
        }

        if (queryParams.TryGetValue("url", out string extractedUrl) &&
            queryParams.TryGetValue("token", out string token))
        {
            Debug.Log("Extracted URL: " + extractedUrl);
            Debug.Log("Extracted Token: " + token);
            testToken = token;
            SocketURI = extractedUrl;
        }
        else
        {
            Debug.LogError("URL or token not found in query parameters.");
        }
    }
    private IEnumerator WaitForAuthToken(SocketOptions options)
    {
        // Wait until myAuth is not null
        while (myAuth == null)
        {
            Debug.Log("My Auth is null");
            yield return null;
        }
        while (SocketURI == null)
        {
            Debug.Log("My Socket is null");
            yield return null;
        }

        Debug.Log("My Auth is not null");
        // Once myAuth is set, configure the authFunction
        Func<SocketManager, Socket, object> authFunction = (manager, socket) =>
        {
            return new
            {
                token = myAuth,
                gameId = gameID
            };
        };
        options.Auth = authFunction;

        Debug.Log("Auth function configured with token: " + myAuth);

        // Proceed with connecting to the server
        SetupSocketManager(options);
    }

    private void SetupSocketManager(SocketOptions options)
    {
        // Create and setup SocketManager
#if UNITY_EDITOR
        this.manager = new SocketManager(new Uri(TestSocketURI), options);
#else
        this.manager = new SocketManager(new Uri(SocketURI), options);
#endif
        if (string.IsNullOrEmpty(nameSpace))
        {  //BackendChanges Start
            gameSocket = this.manager.Socket;
        }
        else
        {
            print("nameSpace: " + nameSpace);
            gameSocket = this.manager.GetSocket("/" + nameSpace);
        }
        // Set subscriptions
        gameSocket.On<ConnectResponse>(SocketIOEventTypes.Connect, OnConnected);
        gameSocket.On<string>(SocketIOEventTypes.Disconnect, OnDisconnected);
        gameSocket.On<string>(SocketIOEventTypes.Error, OnError);
        gameSocket.On<string>("game:init", OnListenEvent);
        gameSocket.On<string>("spin:result", OnResult);
        gameSocket.On<bool>("socketState", OnSocketState);
        gameSocket.On<string>("internalError", OnSocketError);
        gameSocket.On<string>("alert", OnSocketAlert);
        gameSocket.On<string>("AnotherDevice", OnSocketOtherDevice); //BackendChanges Finish
    }

    // Connected event handler implementation
    void OnConnected(ConnectResponse resp)
    {
        Debug.Log("Connected!");
        SendPing();
    }
    void OnResult(string data)
    {
        ParseResponse(data);
    }
    private void OnDisconnected(string response)
    {
        Debug.Log("Disconnected from the server");
        StopAllCoroutines();
    }

    private void OnError(string response)
    {
        Debug.LogError("Error: " + response);
    }

    private void OnListenEvent(string data)
    {
        Debug.Log("Received some_event with data: " + data);
        ParseResponse(data);
    }

    private void OnSocketState(bool state)
    {
        if (state)
        {
            Debug.Log("my state is " + state);
        }
        else
        {

        }
    }
    private void OnSocketError(string data)
    {
        Debug.Log("Received error with data: " + data);
    }
    private void OnSocketAlert(string data)
    {
        Debug.Log("Received alert with data: " + data);
    }

    private void OnSocketOtherDevice(string data)
    {
        Debug.Log("Received Device Error with data: " + data);
        //uiManager.ADfunction();
    }

    private void SendPing()
    {
        InvokeRepeating("AliveRequest", 0f, 3f);
    }

    private void AliveRequest()
    {
        SendDataWithNamespace("YES I AM ALIVE");
    }

    private void SendDataWithNamespace(string eventName, string json = null)
    {
        // Send the message
        if (gameSocket != null && gameSocket.IsOpen) //BackendChanges
        {
            if (json != null)
            {
                gameSocket.Emit(eventName, json);
                Debug.Log("JSON data sent: " + json);
            }
            else
            {
                gameSocket.Emit(eventName);
            }
        }
        else
        {
            Debug.LogWarning("Socket is not connected.");
        }
    
    }

    internal void closeSocketReactnativeCall()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    JSManager.SendCustomMessage("onExit");
#endif
    }

    internal void CloseSocket()
    {
        SendDataWithNamespace("EXIT");
    }

    private void ParseResponse(string jsonObject)
    {
        Debug.Log("jjx: "+jsonObject);
        Root myData = JsonConvert.DeserializeObject<Root>(jsonObject);

        string id = myData.id;

        switch (id)
        {
            case "initData":
                {
                    InitialData = myData.gameData;
                    UIData = myData.uiData;
                    PlayerData = myData.player;
                    bonusdata = GetBonusData(myData.gameData.spinBonus);
               
                    if (!SetInit)
                    {
                        Debug.Log(jsonObject);
                        List<string> LinesString = ConvertListListIntToListString(InitialData.lines);
                     //   List<string> InitialReels = ConvertListOfListsToStrings(initialData.Reel);
                       
                        PopulateSlotSocket(LinesString);
                        SetInit = true;
                    }
                    else
                    {
                        RefreshUI();
                    }
                    break;
                }
            case "ResultData":
                {
                    Debug.Log(jsonObject);
                    //myData.message.GameData.FinalResultReel = ConvertListOfListsToStrings(myData.message.GameData.ResultReel);
                    // myData.message.GameData.FinalsymbolsToEmit = TransformAndRemoveRecurring(myData.message.GameData.symbolsToEmit);
                    ResultData = myData;
                    PlayerData = myData.player;
                    isResultdone = true;
                    break;
                }
            case "ExitUser":
                {
                    gameSocket.Disconnect();
                    if (this.manager != null)
                    {
                        Debug.Log("Dispose my Socket");
                        this.manager.Close();
                    }
                   // Application.ExternalCall("window.parent.postMessage", "onExit", "*");
#if UNITY_WEBGL && !UNITY_EDITOR
                        JSManager.SendCustomMessage("onExit");
#endif
                    break;
                }
        }
    }

    List<string> GetBonusData(List<int> bonusData)
    {
        List<string> bonusDataString = new List<string>();
        foreach (int data in bonusData)
        {
            bonusDataString.Add(data.ToString());
        }
        return bonusDataString;
    }

    private void RefreshUI()
    {
        //uiManager.InitialiseUIData(initUIData.AbtLogo.link, initUIData.AbtLogo.logoSprite, initUIData.ToULink, initUIData.PopLink, initUIData.paylines);
    }

    private void PopulateSlotSocket( List<string> LineIds)
    {
        slotManager.shuffleInitialMatrix();
        for (int i = 0; i < LineIds.Count; i++)
        {
            slotManager.FetchLines(LineIds[i], i);
        }

        slotManager.SetInitialUI();

        isLoaded = true;
#if UNITY_WEBGL && !UNITY_EDITOR
        JSManager.SendCustomMessage("OnEnter");
#endif
    }

    internal void AccumulateResult(double currBet)
    {
        isResultdone = false;
        MessageData message = new MessageData();
        message.currentBet = slotManager.BetCounter;
        // Serialize message data to JSON
        string json = JsonUtility.ToJson(message);
        SendDataWithNamespace("spin:request", json);
    }

    private List<string> RemoveQuotes(List<string> stringList)
    {
        for (int i = 0; i < stringList.Count; i++)
        {
            stringList[i] = stringList[i].Replace("\"", ""); // Remove inverted commas
        }
        return stringList;
    }

    private List<string> ConvertListListIntToListString(List<List<int>> listOfLists)
    {
        List<string> resultList = new List<string>();

        foreach (List<int> innerList in listOfLists)
        {
            // Convert each integer in the inner list to string
            List<string> stringList = new List<string>();
            foreach (int number in innerList)
            {
                stringList.Add(number.ToString());
            }

            // Join the string representation of integers with ","
            string joinedString = string.Join(",", stringList.ToArray()).Trim();
            resultList.Add(joinedString);
        }

        return resultList;
    }

    private List<string> ConvertListOfListsToStrings(List<List<string>> inputList)
    {
        List<string> outputList = new List<string>();

        foreach (List<string> row in inputList)
        {
            string concatenatedString = string.Join(",", row);
            outputList.Add(concatenatedString);
        }

        return outputList;
    }

    private List<string> TransformAndRemoveRecurring(List<List<string>> originalList)
    {
        // Flattened list
        List<string> flattenedList = new List<string>();
        foreach (List<string> sublist in originalList)
        {
            flattenedList.AddRange(sublist);
        }

        // Remove recurring elements
        HashSet<string> uniqueElements = new HashSet<string>(flattenedList);

        // Transformed list
        List<string> transformedList = new List<string>();
        foreach (string element in uniqueElements)
        {
            transformedList.Add(element.Replace(",", ""));
        }

        return transformedList;
    }
}

[Serializable]
public class MessageData
{
    public int currentBet;
}

[Serializable]
public class GameData
{
    public List<List<int>> lines { get; set; }
    public List<double> bets { get; set; }
    public List<int> spinBonus { get; set; }
}



[Serializable]
public class FreeSpins
{
    public int count { get; set; }
    public bool isFreeSpin { get; set; }
}

[SerializeField]
public class Bonus
{
    public int BonusSpinStopIndex { get; set; }
    public double amount { get; set; }
}

[Serializable]
public class Root
{
    //Result Data
    public bool success { get; set; }
    public List<List<string>> matrix { get; set; }
    public string name { get; set; }
    public Payload payload { get; set; }
    public Bonus bonus { get; set; }
    public Jackpot jackpot { get; set; }
    public Scatter scatter { get; set; }
    public FreeSpins freeSpin { get; set; }
    //Initial Data
    public string id { get; set; }
    public GameData gameData { get; set; }
    public UiData uiData { get; set; }
    public Player player { get; set; }
}
[Serializable]
public class Scatter
{
    public double amount { get; set; }
}
[Serializable]
public class Jackpot
{
    public bool isTriggered { get; set; }
    public double amount { get; set; }
}
[Serializable]
public class Payload
{
    public double winAmount { get; set; }
    public List<Win> wins { get; set; }
}

[Serializable]
public class Win
{
    public int line { get; set; }
    public List<int> positions { get; set; }
    public double amount { get; set; }
}

[Serializable]
public class UiData
{
    public Paylines paylines { get; set; }
}

[Serializable]
public class Paylines
{
    public List<Symbol> symbols { get; set; }
}

[Serializable]
public class Symbol
{
    public int id { get; set; }
    public string name { get; set; }
    public List<int> multiplier { get; set; }
    public string description { get; set; }
}

[Serializable]
public class Player
{
    public double balance { get; set; }
}

[Serializable]
public class AuthTokenData
{
    public string cookie;
    public string socketURL;
    public string nameSpace; //BackendChanges
}

