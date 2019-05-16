using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHelper : MonoBehaviour {

    public void OnExitBtnClick()
    {
        Application.Quit();
    }

    public void ResetSceneButt()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
