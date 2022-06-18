using UnityEngine;
public class SetTargetFrameRate : MonoBehaviour
{
    public int targetFrameRate = 30;

    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}