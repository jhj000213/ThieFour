using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SceneChangeMng : MonoBehaviour
{



    public GameObject _Parent;
    public GameObject _FadeIn;

    public int _One = 1;
    public int _Two = 2;

    public void SceneChange(int num)
    {
        GameObject obj = NGUITools.AddChild(_Parent, _FadeIn);
        StartCoroutine(SceneChangeDelay(1.5f, num));
    }

    IEnumerator SceneChangeDelay(float time, int num)
    {
        yield return new WaitForSeconds(time);

        if (num == 1)
            SceneManager.LoadScene("MainScene");
        else if (num == 2)
            SceneManager.LoadScene("GameScene");
    }


    public void Exit()
    {
        Application.Quit();
    }
}
