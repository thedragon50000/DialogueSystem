using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.SceneManagement;
using Zenject;

public class menuScript_ : MonoBehaviour
{
    [Inject] private ZenjectSceneLoader _sceneLoader;

    public void PlayGame(int LevelNumber)
    {
        var s = E_ZenjectID.Level.ToString();
        _sceneLoader.LoadScene(1, LoadSceneMode.Single,
            container => container.BindInstance(LevelNumber).WithId(s));
    }

    public void OpenLink(string URL)
    {
        Application.OpenURL(URL);
    }
}

public enum E_ZenjectID
{
    Level
}

/*


*/
