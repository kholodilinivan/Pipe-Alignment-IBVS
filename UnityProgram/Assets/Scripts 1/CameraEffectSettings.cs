using Assets.ImageSynthesis;

namespace Scripts_1
{
    public class CameraEffectSettings
    {
        private CameraEffectSettings()
        {
        }

        private static CameraEffectSettings _instance;

        public static CameraEffectSettings Instance => _instance ?? (_instance = new CameraEffectSettings());

        public CameraEffectType CurrentCameraEffect { get; set; }
    }
}