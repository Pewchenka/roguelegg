using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControl : MonoBehaviour
{
    public void PlayGame()
    {
        PlayerMove.Php = 4;
        RoomCreate.dif = 1;
        SceneManager.LoadScene(1);
    }
    public void Qut()
    {
        Debug.Log("apoP");
        Application.Quit();
    }
}
