
using UnityEngine;

public class GameFrameLimiter : MonoBehaviour
{
    private const int MAXFPS = 60;

    void Start()
    {
        Application.targetFrameRate = MAXFPS;
        Cursor.visible = false;
    }

}
