// RegalPrime 6-02-14 - BaseAI_Editor.cs

// This provides a clean way to select the actions of an AI unit
// It asks the script the available actions and gives the user 3 dropdown boxes to create a custom AI (one for each AI state)


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

[CustomEditor(typeof(BaseAI), true)]
public class BaseAI_Editor : Editor
{
	BaseAI thisScript;

	string[] TA_Methods_String;

	public int AggressiveAINumber = 0;
	public int AlertAINumber = 0;
	public int PassiveAINumber = 0;
	
	void Awake()
	{
		thisScript = (BaseAI)target;

		// Get the current values of the AI unit
		AggressiveAINumber = thisScript.AggressiveAINumber;
		AlertAINumber = thisScript.AlertAINumber;
		PassiveAINumber = thisScript.PassiveAINumber;

		// Get the list of all available AI actions (a string)
		TA_Methods_String = thisScript.GetFunctionList_String(thisScript.GetFunctionList()).ToArray();


		// If any values are out of range then default to 0
		if(AggressiveAINumber > TA_Methods_String.Length-1 || AggressiveAINumber < 0)
			AggressiveAINumber = 0;

		if(AlertAINumber > TA_Methods_String.Length-1 || AlertAINumber < 0)
			AlertAINumber = 0;

		if(PassiveAINumber > TA_Methods_String.Length-1 || PassiveAINumber < 0)
			PassiveAINumber = 0;

	}


	public override void OnInspectorGUI()
	{

		AggressiveAINumber = EditorGUILayout.Popup("Aggressive State", AggressiveAINumber, TA_Methods_String);
		AlertAINumber = EditorGUILayout.Popup("Alerted State", AlertAINumber, TA_Methods_String);
		PassiveAINumber = EditorGUILayout.Popup("Passive State", PassiveAINumber, TA_Methods_String);

		thisScript.AggressiveAINumber = AggressiveAINumber;
		thisScript.AlertAINumber = AlertAINumber;
		thisScript.PassiveAINumber = PassiveAINumber;

		EditorGUILayout.LabelField (string.Format (" "));

//		EditorGUILayout.LabelField (string.Format ("{0} - {1} - {2}",AggressiveAINumber,AlertAINumber,PassiveAINumber));
//		EditorGUILayout.LabelField (string.Format ("{0} - {1} - {2}",thisScript.AggressiveAINumber,thisScript.AlertAINumber,thisScript.PassiveAINumber));

		EditorUtility.SetDirty (thisScript);

		base.OnInspectorGUI ();
	}

}
