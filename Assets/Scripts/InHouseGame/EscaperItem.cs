using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EscaperItem : MonoBehaviour {

    public List<Vector3> _CreatePosList = new List<Vector3>();

    void Start()
    {
        int num = Random.Range(0, _CreatePosList.Count);
        transform.localPosition = _CreatePosList[num];
    }

	public void GetItem()
    {
        Destroy(gameObject);
    }
}
