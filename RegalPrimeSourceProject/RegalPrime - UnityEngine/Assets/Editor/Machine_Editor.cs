using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Machine), true)]
public class Machine_Editor : Editor
{
	Machine thisScript;
	[SerializeField] private float speedChange = 1;

	void Awake()
	{
		thisScript = target as Machine;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI ();

		EditorGUILayout.LabelField ("");
		EditorGUILayout.LabelField ("The following buttons are only useful for editor testing");
		EditorGUILayout.LabelField ("These are functions you can call via the parent Machine class to effect this object");

		if(GUILayout.Button ("Start_Machine"))
		{
			thisScript.Start_Machine();
		}

		if(GUILayout.Button ("Stop_Machine"))
		{
			thisScript.Stop_Machine();
		}

		if(GUILayout.Button ("Reset_Machine"))
		{
			thisScript.Reset_Machine();
		}

		if(GUILayout.Button ("Reverse_Machine"))
		{
			thisScript.Reverse_Machine();
		}


		speedChange	= EditorGUILayout.FloatField("Speed Change", speedChange);
		if(GUILayout.Button ("SpeedUp_Machine"))
		{
			thisScript.SpeedUp_Machine(speedChange);
		}
		if(GUILayout.Button ("SlowDown_Machine"))
		{
			thisScript.SlowDown_Machine(speedChange);
		}
		if(GUILayout.Button ("SetSpeed_Machine"))
		{
			thisScript.SetSpeed_Machine(speedChange);
		}
	}



}
