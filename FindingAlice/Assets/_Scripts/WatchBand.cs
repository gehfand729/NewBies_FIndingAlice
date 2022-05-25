using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchBand : MonoBehaviour
{
    public Transform clock_t;
    public Transform player_t;
    Material mat;
    Vector3 clock_v;
    Vector3 player_v;
    Vector3 scale;
    float x = 0f;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        clock_v = clock_t.position;
        player_v = player_t.position;
        Vector3 vec = new Vector3((clock_v.x + player_v.x)/2, (clock_v.y + player_v.y)/2, 0);
        transform.position = vec;

        float dx = clock_v.x - player_v.x;
        float dy = clock_v.y - player_v.y;
        Vector3 angle = new Vector3(0, 0, Mathf.Atan2(dy, dx) * (180f / Mathf.PI));
        transform.rotation = Quaternion.Euler(angle);

        Vector3 scale = new Vector3(Vector3.Distance(clock_v, player_v), 0.3f, 0f);
        transform.localScale = scale;
        if (ClockManager.C.isPressKeyClock)
        {
            x += 0.003f;
            mat.mainTextureScale = new Vector2(x, 1f);
        }
        else
        {
            x = 0f;
            mat.mainTextureScale = Vector2.zero;
        }
    }
}
