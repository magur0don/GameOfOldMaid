using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardGameStart : MonoBehaviour
{
   

    public void OnClockStartButton()
    {
        SceneManager.LoadScene("GameMain");
    }

}
