using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Android;
using System.Collections;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [BoxGroup("File Storage Config")]
    [SerializeField] private string _fileName;
    [BoxGroup("File Storage Config")]
    [SerializeField] private bool _isUsingEncryption;
    public static DataPersistenceManager instance { get; private set; }

    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager");
        }
        instance = this;
        _dataPersistenceObjects = new List<IDataPersistence>();
    }

    private void Start()
    {
        StartCoroutine(AskForPermissions());
        _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _isUsingEncryption);
        FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        _gameData = new GameData();
    }

    public void LoadGame()
    {
        FindAllDataPersistenceObjects();
        _gameData = _dataHandler.Load();
        //If no data can be loaded, initialize to a new game
        if (_gameData == null)
        {
            Debug.Log("No data was found to be loaded. Initializing data to defaults.");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        FindAllDataPersistenceObjects();
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(ref _gameData);
        }
        _dataHandler.Save(_gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        dataPersistenceObjects.ToList().ForEach(i => { _dataPersistenceObjects.Add(i); Debug.Log("Adding: " + i + " to dataPersistenceArray"); });
    }

    public GameData GetGameData()
    {
        return _gameData;
    }
    private IEnumerator AskForPermissions()
    {
#if UNITY_ANDROID
        List<bool> permissions = new List<bool>() { false, false};
        List<bool> permissionsAsked = new List<bool>() { false, false};
        List<Action> actions = new List<Action>()
    {
        new Action(() => {
            permissions[0] = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite);
            if (!permissions[0] && !permissionsAsked[2])
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                permissionsAsked[0] = true;
                return;
            }
        }),
        new Action(() => {
            permissions[1] = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead);
            if (!permissions[1] && !permissionsAsked[3])
            {
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
                permissionsAsked[1] = true;
                return;
            }
        })
    };
        for (int i = 0; i < permissionsAsked.Count;)
        {
            actions[i].Invoke();
            if (permissions[i])
            {
                ++i;
            }
            yield return new WaitForEndOfFrame();
        }
#endif
    }

}
