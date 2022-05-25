using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //this Position
    Vector3 m_Position;

    public GameObject player;
    //Player Position
    Vector3 p_Position;


    // Update is called once per frame
    void FixedUpdate()
    {
        m_Position = gameObject.transform.position;
        p_Position = player.transform.position;
        p_Position.z = m_Position.z;
        gameObject.transform.localPosition = Vector3.Lerp(m_Position, p_Position, Time.deltaTime * 6);
    }
}
