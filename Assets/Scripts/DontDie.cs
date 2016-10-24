using UnityEngine;
using System.Collections;

public class DontDie : MonoBehaviour {
    public static DontDie Instance2;
    void Awake() {
        
        if (Instance2 == null)
        {
            Instance2 = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }
}
