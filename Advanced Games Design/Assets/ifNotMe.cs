using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ifNotMe : MonoBehaviour
{

    void Start() {
        if (!GetComponent<PhotonView>().isMine)
        {
            Destroy(gameObject);
        }
            }
}
