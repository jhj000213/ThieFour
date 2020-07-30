using UnityEngine;
using System.Collections;

public class SleepBullet : MonoBehaviour {

    public float _Speed;
    public int _PlayerNumber;
    public GameObject _UIRoot;
    public GameObject _Effect;

    public int _Damage;

    public void Init(int playernum,float speed,int damage)
    {
        _PlayerNumber = playernum;
        _Speed = speed;
        _Damage = damage;
    }

    void Update()
    {
        transform.Translate(Vector3.right*Time.smoothDeltaTime*_Speed);
        
    }

    public void Hit()
    {
        Debug.Log("Hit");
        GameObject effect = NGUITools.AddChild(transform.parent.gameObject, _Effect);
        effect.transform.localPosition = transform.localPosition;
        effect.transform.localEulerAngles = transform.localEulerAngles + new Vector3(0,0,90);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "wall")
        {
            Hit();
        }
        
    }
}
