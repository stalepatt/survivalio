using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMFollowCam : MonoBehaviour
{
    public void Init(Transform target)
    {
        CinemachineVirtualCamera cmVirtualCam = Utils.GetOrAddComponent<CinemachineVirtualCamera>(gameObject);

        cmVirtualCam.Follow = target;
    }
}
