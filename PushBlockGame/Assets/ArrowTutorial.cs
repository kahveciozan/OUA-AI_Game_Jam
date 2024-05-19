using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTutorial : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            gameObject.SetActive(false);
        }
    }
}
