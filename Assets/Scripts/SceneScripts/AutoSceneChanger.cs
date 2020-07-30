using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoSceneChanger : MonoBehaviour {

    public float _DelayTime;
    [Range(1,3)]
    [Tooltip("1 = LoadScene \r\n2 = TitleScene/r/n3 = MainScene")]
    public int _SceneNumber;

    public GameObject _Parent;
    public GameObject _FadeIn;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(_DelayTime);

        GameObject obj = NGUITools.AddChild(_Parent, _FadeIn);
        StartCoroutine(SceneChangeDelay(1.5f, _SceneNumber));
    }


    IEnumerator SceneChangeDelay(float time, int num)
    {
        yield return new WaitForSeconds(time);

        if (num == 1)
            SceneManager.LoadScene("LoadScene");
        else if (num == 2)
            SceneManager.LoadScene("TitleScene");
        else if (num == 3)
            SceneManager.LoadScene("MainScene");
    }
}
