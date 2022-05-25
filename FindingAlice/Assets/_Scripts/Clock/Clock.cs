﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Dd");
            GameObject.Find("ClockManager").SendMessage("clockReset");
        }
    }
}
