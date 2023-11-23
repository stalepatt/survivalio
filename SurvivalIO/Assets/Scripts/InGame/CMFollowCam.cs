using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMFollowCam : MonoBehaviour
{
    public void Init(GameObject targetObject)
    {
        CinemachineVirtualCamera cmVirtualCam = Utils.GetOrAddComponent<CinemachineVirtualCamera>(gameObject);

        cmVirtualCam.Follow = targetObject.transform;
    }
}
