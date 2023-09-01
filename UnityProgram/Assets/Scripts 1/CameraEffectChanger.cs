using System;
using Assets.ImageSynthesis;
using UnityEngine;

namespace Scripts_1
{
    public class CameraEffectChanger : MonoBehaviour
    {
        public void ChangeCameraEffect(int effect)
        {
            var effectType = (CameraEffectType) effect;

            CameraEffectSettings.Instance.CurrentCameraEffect = effectType;
        }
    }
}