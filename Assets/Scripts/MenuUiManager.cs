using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUiManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;   // INPUT
    public TextMeshProUGUI playerNameText;   // ANZEIGE

    private void Start()
    {
        // Gespeicherten Namen laden und anzeigen
        if (!string.IsNullOrEmpty(PersistanceManager.Instance.TeamName))
        {
            playerNameInput.text = PersistanceManager.Instance.TeamName;
            playerNameText.text = PersistanceManager.Instance.TeamName;
        }
    }

    public void NewNameGiven()
    {
        string name = playerNameInput.text;

        if (!string.IsNullOrEmpty(name))
        {
            PersistanceManager.Instance.TeamName = name;
            PersistanceManager.Instance.SaveData(); // direkt speichern
            playerNameText.text = name;
        }
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        PersistanceManager.Instance.SaveData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}