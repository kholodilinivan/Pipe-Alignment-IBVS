using Assets.ImageSynthesis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotsEffectsToggle: MonoBehaviour {
	public CameraEffectType effectForToggle;

	public void ToggleEffect() {
		ScreenshotsEffectsManager.ToggleEffect(effectForToggle);
	}
}
