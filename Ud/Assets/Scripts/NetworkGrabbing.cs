using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkGrabbing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSelectEntered()
    {
        Debug.Log("Object is Grabbed");
    }

    public void OnSelectedExited()
    {
        Debug.Log("Object is Released");
    }
}
