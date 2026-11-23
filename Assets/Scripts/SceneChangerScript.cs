using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class SceneChangerScript : MonoBehaviour
{

    public SaveLoadScript saveLoadScript;
    public FadeScript fadeScript;


    public void CLoseGame()
    {
        StartCoroutine(Delay("quit", -1, "", -1));
    }

    public IEnumerator Delay(string action, int charIndex, string playerName, int playerCount)
    {
        if (string.Equals(action, "quit", StringComparison.OrdinalIgnoreCase))
        {
            yield return new WaitForSeconds(0.5f);
            PlayerPrefs.DeleteAll();

            if (UnityEditor.EditorApplication.isPlaying)
                UnityEditor.EditorApplication.isPlaying = false;
            else
            {
                yield return fadeScript.FadeOut(0.1f); // !!!
                Application.Quit();
            }

        }
        else if (string.Equals(action, "play", StringComparison.OrdinalIgnoreCase))
        {
            yield return fadeScript.FadeOut(0.1f); //!!!
            saveLoadScript.SavePlayerData(charIndex, playerName, playerCount);
            SceneManager.LoadScene(1, LoadSceneMode.Single);

        }
        else if (string.Equals(action, "menu", StringComparison.OrdinalIgnoreCase))
        {
            yield return fadeScript.FadeOut(0.1f);
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    public void ToMainMenu()
    {
        StartCoroutine(Delay("menu", -1, "", -1));
    }
}
