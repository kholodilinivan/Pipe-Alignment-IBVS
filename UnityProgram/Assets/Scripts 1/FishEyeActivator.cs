using System;
using UnityEngine;

namespace Scripts_1
{
    public class FishEyeActivator : MonoBehaviour
    {
        [SerializeField]
        private Camera _fishEyeCamera;

        private void OnEnable()
        {
            if (_fishEyeCamera != null)
            {
                // _fishEyeCamera.gameObject.SetActive(false);
                // _fishEyeCamera.gameObject.SetActive(true);
            }
        }
    }
}