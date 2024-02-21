using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBillBoardLobby : MonoBehaviour
{
    public GameObject ActorBody;
    
    public void SetNewMaterial(Material material)
    {
        ActorBody.GetComponent<MeshRenderer>().material = material;
    }
}
