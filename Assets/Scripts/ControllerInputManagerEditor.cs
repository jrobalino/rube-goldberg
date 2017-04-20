using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(ControllerInputManager))]
public class ControllerInputEditor : Editor
{

	override public void OnInspectorGUI()
	{
		var ControllerInputManager = target as ControllerInputManager;

		//EditorGUILayout.PrefixLabel("Walk Speed");
		ControllerInputManager.throwForce = EditorGUILayout.FloatField("Throw Force", ControllerInputManager.throwForce);
		ControllerInputManager.leftController = EditorGUILayout.Toggle("Left Controller", ControllerInputManager.leftController);

		using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(ControllerInputManager.leftController)))
		{
			if (group.visible == true)
			{
				EditorGUI.indentLevel++;
				EditorGUILayout.PrefixLabel("Teleport Aimer Object");
				ControllerInputManager.teleportAimerObject = (GameObject)EditorGUILayout.ObjectField(ControllerInputManager.teleportAimerObject, typeof(GameObject), true);
				EditorGUILayout.PrefixLabel("Disabled Aimer Object");
				ControllerInputManager.disabledAimerObject = (GameObject)EditorGUILayout.ObjectField(ControllerInputManager.disabledAimerObject, typeof(GameObject), true);
				EditorGUILayout.PrefixLabel("Laser");
				ControllerInputManager.laser = (LineRenderer)EditorGUILayout.ObjectField(ControllerInputManager.laser, typeof(LineRenderer), true);
				EditorGUILayout.PrefixLabel("Player");
				ControllerInputManager.player = (GameObject)EditorGUILayout.ObjectField(ControllerInputManager.player, typeof(GameObject), true);
				ControllerInputManager.laserMask = EditorTools.LayerMaskField("Laser Mask", ControllerInputManager.laserMask);
				EditorGUILayout.PrefixLabel("yNudgeAmount");
				ControllerInputManager.yNudgeAmount = EditorGUILayout.FloatField(ControllerInputManager.yNudgeAmount);
				EditorGUILayout.PrefixLabel("Horizontal Range");
				ControllerInputManager.teleporterMaxHorizontal = EditorGUILayout.FloatField(ControllerInputManager.teleporterMaxHorizontal);
				EditorGUILayout.PrefixLabel("Vertical Range");
				ControllerInputManager.teleporterMaxVertical = EditorGUILayout.FloatField(ControllerInputManager.teleporterMaxVertical);
				EditorGUILayout.PrefixLabel("Player Height");
				ControllerInputManager.playerHeight = EditorGUILayout.FloatField(ControllerInputManager.playerHeight);
				EditorGUILayout.PrefixLabel("Allow Dash");
				ControllerInputManager.useDash = EditorGUILayout.Toggle(ControllerInputManager.useDash);
				EditorGUILayout.PrefixLabel("Dash Speed");
				ControllerInputManager.dashSpeed = EditorGUILayout.FloatField(ControllerInputManager.dashSpeed);
				EditorGUILayout.PrefixLabel("Allow Walking");
				ControllerInputManager.allowWalking = EditorGUILayout.Toggle(ControllerInputManager.allowWalking);
				EditorGUILayout.PrefixLabel("Player Cam");
				ControllerInputManager.playerCam = (Transform)EditorGUILayout.ObjectField(ControllerInputManager.playerCam, typeof(Transform), true);
				EditorGUILayout.PrefixLabel("Walk Speed");
				ControllerInputManager.moveSpeed = EditorGUILayout.FloatField(ControllerInputManager.moveSpeed);
				EditorGUI.indentLevel--;
			}
		}
	}
}


