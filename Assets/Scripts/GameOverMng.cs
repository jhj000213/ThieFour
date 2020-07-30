using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverMng : MonoBehaviour {

    public GameObject _FailLabel;
    public GameObject _ToolTip;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.5f);

        _FailLabel.SetActive(true);
        StartCoroutine(Step1(1.0f));
    }

    IEnumerator Step1(float time)
    {
        yield return new WaitForSeconds(time);

        _ToolTip.SetActive(true);
    }

    void Update()
    {
        for (int i = 1; i < 12; i++)
        {
            if (Input.GetButtonDown("Player" + i.ToString() + "_O"))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}
