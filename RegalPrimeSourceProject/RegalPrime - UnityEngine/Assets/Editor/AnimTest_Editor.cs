using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AnimatorTest))]
public class AnimTest_Editor : Editor
{
	AnimatorTest thisScript;

	void Awake()
	{
		thisScript = target as AnimatorTest;
	}


	public override void OnInspectorGUI()
	{
//		base.OnInspectorGUI ();

		if(GUILayout.Button ("ResetActions"))
		{
			thisScript.ResetToDefault();
		}

		EditorGUILayout.LabelField ("");
		EditorGUILayout.LabelField ("");

		thisScript.VSpeed	= EditorGUILayout.FloatField("vSpeed",thisScript.VSpeed);
		if(GUILayout.Button ("InAir"))
		{
			thisScript.InAir();
		}
		if(GUILayout.Button ("OnGround"))
		{
			thisScript.LandOnGround();
		}

		EditorGUILayout.LabelField ("");
		EditorGUILayout.LabelField ("");

		if(GUILayout.Button ("ResetMovement"))
		{
			thisScript.Reset_Movement();
		}
		if(GUILayout.Button ("MoveHorizontal"))
		{
			thisScript.Move_Horizontal();
		}
		if(GUILayout.Button ("MoveVertical"))
		{
			thisScript.Move_Vertical();
		}

		EditorGUILayout.LabelField ("");
		EditorGUILayout.LabelField ("");

		if(GUILayout.Button ("StartClimbing"))
		{
			thisScript.StartClimbing();
		}
		if(GUILayout.Button ("StopClimbing"))
		{
			thisScript.StopClimbing();
		}
	}






}