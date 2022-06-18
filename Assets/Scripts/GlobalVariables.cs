using UnityEngine;

public class GlobalVariables : MonoBehaviour {
    // Turn true if you want to show debug lines in game
    public bool showDebugLinesGame;

    private static GlobalVariables _instance;
    public static GlobalVariables Instance { get { return _instance; } }
    
    private void Awake() {
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
}