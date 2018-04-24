using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

[CustomEditor(typeof(PlatformerCharacter2D), true)]
public class PlatformerCharacter2D_Editor : Editor
{
	PlatformerCharacter2D thisScript;

	void Awake()
	{
		thisScript = (PlatformerCharacter2D)target;
		thisScript.Set_MinPenetration ();
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI ();

		EditorGUILayout.LabelField("currentHorizontalSpeed: ", ""+thisScript.currentHorizontalSpeed*100);
		EditorGUILayout.LabelField("currentFallSpeed: ", ""+thisScript.currentFallSpeed*2);
		EditorGUILayout.LabelField("");

		EditorGUILayout.LabelField("gravityMultiplier: ", ""+thisScript.gravityMultiplier);
		EditorGUILayout.LabelField("movementMultiplier: ", ""+thisScript.movementMultiplier);
		EditorGUILayout.LabelField("jumpforceMultiplier: ", ""+thisScript.jumpforceMultiplier);

	}

}
