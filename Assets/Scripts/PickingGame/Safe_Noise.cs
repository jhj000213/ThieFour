using UnityEngine;
using System.Collections;

public class Safe_Noise : MonoBehaviour {



	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "machinenoise")
        {
            Destroy(transform.parent.gameObject);
        }
        if(other.tag == "guard")
        {
            other.GetComponent<SequrityGuard>().HeardOfPickingSound();
        }
    }
}
