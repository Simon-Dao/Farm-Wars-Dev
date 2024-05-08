using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{   
    private PhotonView view;
    public float playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(!view.IsMine) return;

        float dx = Input.GetAxis("Horizontal");       
        float dy = Input.GetAxis("Vertical");

        Vector3 delta = new Vector3(dx,dy, 0);
        delta *= playerSpeed * Time.deltaTime;

        transform.position += delta;       
    }
}
