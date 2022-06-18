using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFireFirghters : MonoBehaviour
{
    // Start is called before the first frame update
    private int xPosition = 0; 
    public GameObject myPrefab;
    public Transform parent;

    void Start() {
        for (int i = 0; i < 9; i++) {
            this.spawnFireFighter(new Vector3(xPosition,0,0));
            this.xPosition += 2;
        }
        
    }

    void spawnFireFighter (Vector3 position) {
        GameObject fireFighter = Instantiate(myPrefab, position, Quaternion.identity);
        fireFighter.transform.SetParent(parent);
        fireFighter.name = "FireFighter" + Random.Range(1, 100000);
    }
}
