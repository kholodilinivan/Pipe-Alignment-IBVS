using UnityEngine;
using UnityEngine.UI;

public class CubeTransformation: MonoBehaviour {
	public InputField leftInput;
	public InputField frontInput;
	public InputField borderInput;
	public InputField rotationInput;
	public InputField sizeInput;

	public GameObject robotGO;
	private Changes robotChangesScript;

	private Transform leftSide;
	private Transform rightSide;
	private Transform frontSide;
	private Transform bottomSide;

	Transform leftPlane;
	Transform rightPlane;
	Transform frontPlane;
	Transform bottomPlane;

	private float currentSize;
	private float currentAngle;

	private void Start() {
		Transform mainRectangle = transform.GetChild(0);

		leftSide   = mainRectangle.GetChild(0).GetChild(0).GetChild(0);
		rightSide  = mainRectangle.GetChild(1).GetChild(0).GetChild(0);
		frontSide  = mainRectangle.GetChild(2).GetChild(0).GetChild(0);
		bottomSide = mainRectangle.GetChild(3).GetChild(0).GetChild(0);

		leftPlane   = leftSide.GetChild(0);
		rightPlane  = rightSide.GetChild(1);
		frontPlane  = frontSide.GetChild(0);
		bottomPlane = bottomSide.GetChild(1);

		robotChangesScript = robotGO.GetComponent<Changes>();

		currentSize = normalizeInputValue(sizeInput.text);
		currentAngle = normalizeInputValue(rotationInput.text);
	}

	public void setNewPosRelativeToLeft(string newPositionStr) {
		float newPosition = normalizeInputValue(newPositionStr);
		float moveStep = newPosition - leftSide.localPosition.x;

		float previousAngle = currentAngle;
		Rotate("0");

		leftSide.transform.localPosition += new Vector3(moveStep, 0, 0);
		rightSide.transform.localPosition += new Vector3(moveStep, 0, 0);
		frontSide.transform.localPosition += new Vector3(moveStep, 0, 0);
		bottomSide.transform.localPosition += new Vector3(moveStep, 0, 0);

		Rotate(previousAngle.ToString());

		robotChangesScript.EffectButtons();
	}

	public void setNewPosRelativeToFront(string newPositionStr) {
		float newPosition = normalizeInputValue(newPositionStr);
		float moveStep = newPosition - frontSide.localPosition.z;

		float previousAngle = currentAngle;
		Rotate("0");

		leftSide.localPosition += new Vector3(0, 0, moveStep);
		rightSide.localPosition += new Vector3(0, 0, moveStep);
		frontSide.localPosition += new Vector3(0, 0, moveStep);
		bottomSide.localPosition += new Vector3(0, 0, moveStep);

		Rotate(previousAngle.ToString());

		robotChangesScript.EffectButtons();
	}

	public void setNewPosRelativeToBorder(string newPositionStr) {
		float newPosition = normalizeInputValue(newPositionStr);

		float previousAngle = currentAngle;
		Rotate("0");

		leftSide.localPosition  = new Vector3(leftSide.localPosition.x, newPosition, leftSide.localPosition.z);
		rightSide.localPosition = new Vector3(rightSide.localPosition.x, newPosition, rightSide.localPosition.z);
		frontSide.localPosition = new Vector3(frontSide.localPosition.x, newPosition, frontSide.localPosition.z);
		bottomSide.localPosition = new Vector3(bottomSide.localPosition.x, newPosition, bottomSide.localPosition.z);

		Rotate(previousAngle.ToString());

		robotChangesScript.EffectButtons();
	}

	public void Rotate(string newAngleStr) {
		float newAngle = float.Parse(newAngleStr);

		Transform leftParent   = leftSide.parent.parent;
		Transform rightParent  = rightSide.parent.parent;
		Transform frontParent  = frontSide.parent.parent;
		Transform bottomParent = bottomSide.parent.parent;

		Vector3 center = (leftPlane.position + rightPlane.position + frontPlane.position + bottomPlane.position) / 4;

		float rotateAngle = newAngle - currentAngle;

		leftParent.RotateAround(center,   new Vector3(0, 1, 0), rotateAngle);
		rightParent.RotateAround(center,  new Vector3(0, 1, 0), rotateAngle);
		frontParent.RotateAround(center,  new Vector3(0, 1, 0), rotateAngle);
		bottomParent.RotateAround(center, new Vector3(0, 1, 0), rotateAngle);

		robotChangesScript.EffectButtons();

		currentAngle = newAngle;
	}

	public void ChangeSize(string newSizeStr) {
		float newSize = normalizeInputValue(newSizeStr);

		float moveDistance = (newSize - (rightSide.localPosition.x - leftSide.localPosition.x)) * 0.5f;

		leftSide.localPosition   -= new Vector3(moveDistance, 0, 0);
		rightSide.localPosition  += new Vector3(moveDistance, 0, 0);
		frontSide.localPosition  += new Vector3(0, 0, moveDistance);
		bottomSide.localPosition -= new Vector3(0, 0, moveDistance);

		alignPlane(leftPlane, newSize);
		alignPlane(rightPlane, newSize);
		alignPlane(frontPlane, newSize);
		alignPlane(bottomPlane, newSize);

		leftInput.onEndEdit.Invoke(leftInput.text);
		frontInput.onEndEdit.Invoke(frontInput.text);
		borderInput.onEndEdit.Invoke(borderInput.text);

		leftInput.onValueChanged.Invoke(leftInput.text);
		frontInput.onValueChanged.Invoke(frontInput.text);
		borderInput.onValueChanged.Invoke(borderInput.text);

		robotChangesScript.EffectButtons();

		currentSize = newSize;
	}

	private void alignPlane(Transform plane, float newSize) {
		MeshRenderer meshR = plane.GetComponent<MeshRenderer>();

		float meshMaxPointYBeforeScale = meshR.bounds.max.y;
		plane.localScale = new Vector3(plane.localScale.x * (newSize / currentSize), plane.localScale.y, plane.localScale.z * (newSize / currentSize));
		float meshMaxPointYAfterScale = meshR.bounds.max.y;
		plane.position -= new Vector3(0, meshMaxPointYAfterScale - meshMaxPointYBeforeScale, 0); //move the side to where it was before scaling
	}

	private float normalizeInputValue(string value) {
		return float.Parse(value) / 1000;
	}
}
