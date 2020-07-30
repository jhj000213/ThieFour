using UnityEngine;
using System.Collections;

public class EffectSoundMng : MonoBehaviour
{

    public AudioClip _Audio;

    void Start()
    {
        //GetComponent<RemoveSelf>()._DelayTime = _Audio.length;

        AudioSource.PlayClipAtPoint(_Audio, Vector2.zero, 1);
    }
}
