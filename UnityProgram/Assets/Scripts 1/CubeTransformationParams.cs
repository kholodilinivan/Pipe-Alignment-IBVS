using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeTransformationParams: MonoBehaviour {
	[SerializeField] private InputField[] positionZone;
	[SerializeField] private InputField[] rotationZone;
	[SerializeField] private InputField[] sizeZone;

	private string[] positionZoneDefaultValues;
	private string[] rotationZoneDefaultValues;
	private string[] sizeZoneDefaultValues;
	
	private void Start() {
		positionZoneDefaultValues = new string[positionZone.Length];
		rotationZoneDefaultValues = new string[rotationZone.Length];
		sizeZoneDefaultValues = new string[sizeZone.Length];

		InputField[][] allZones = new InputField[][] { positionZone, rotationZone, sizeZone};
		string[][] defaultValuesForAllZones = new string[][] { positionZoneDefaultValues, rotationZoneDefaultValues, sizeZoneDefaultValues };

		for(int i = 0; i < allZones.Length; i++) {
			InputField[] currentZone = allZones[i];
			string[] currentZoneDefaultValues = defaultValuesForAllZones[i];

			for(int j = 0; j < currentZone.Length; j++) {
				currentZoneDefaultValues[j] = currentZone[j].text;
			}
		}
	}

	public void RestartPositionZone() {
		RestartZone(positionZone, positionZoneDefaultValues);
	}
	public void RestartRotationZone() {
		RestartZone(rotationZone, rotationZoneDefaultValues);
	}
	public void RestartSizeZone() {
		RestartZone(sizeZone, sizeZoneDefaultValues);
	}

	private void RestartZone(InputField[] zone, string[] defaultValues) {
		for (int i = 0; i < zone.Length; i++) {
			zone[i].text = defaultValues[i];
			zone[i].onEndEdit.Invoke(defaultValues[i]);
			zone[i].onValueChanged.Invoke(defaultValues[i]);
		}
	}
}
