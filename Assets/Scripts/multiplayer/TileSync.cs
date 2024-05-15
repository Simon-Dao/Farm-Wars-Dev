using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TileSync : MonoBehaviourPun, IPunObservable 
{
    private SpriteRenderer spriteRenderer;
    private Vector3 color;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

        if(stream.IsWriting) {

        } else if(stream.IsReading) {
        }
    }
}
