using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [field: SerializeField] public ENVManager currentEnvironment { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        GameMaster.Entity.BeginGameplay(this);
        SetUpCam();
    }

    private static void SetUpCam()
    {
        CinemachineVirtualCamera vCam = GameObject.Find("VCam").GetComponent<CinemachineVirtualCamera>();
        vCam.Follow = GameMaster.Entity.playerList[0].transform;
        vCam.LookAt = GameMaster.Entity.playerList[0].transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
