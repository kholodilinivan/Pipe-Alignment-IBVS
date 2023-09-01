using Scripts_1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.ImageSynthesis;
using System.Linq;
using UnityEngine.Events;
using System.Collections.ObjectModel;

public static class ScreenshotsEffectsManager {
	private static List<CameraEffectType> cameraEffectsForScreenshots = new List<CameraEffectType>(6);
	public static void ToggleEffect(CameraEffectType effect) {
		if (cameraEffectsForScreenshots.Contains(effect)) {
			cameraEffectsForScreenshots.Remove(effect);
		}
		else {
			cameraEffectsForScreenshots.Add(effect);
		}
	}
	public static ReadOnlyCollection<CameraEffectType> getAllEffectsForScreenshots() {
		return new ReadOnlyCollection<CameraEffectType>(cameraEffectsForScreenshots); ;
	}
	public static ReadOnlyCollection<CameraEffectType> GetDefaultEffects() {
		List<CameraEffectType> result = new List<CameraEffectType>() { CameraEffectType.Img};

		return new ReadOnlyCollection<CameraEffectType>(result);
	}
}
