using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(MachineController), true)]
public class MachineController_Editor : Editor
{
		MachineController thisScript;
		
		void Awake()
		{
			thisScript = target as MachineController;
		}
		
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI ();
			
			EditorGUILayout.LabelField ("");
			
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
			
			if(GUILayout.Button ("SpeedUp_Machine"))
			{
				thisScript.SpeedUp_Machine();
			}
			
			if(GUILayout.Button ("SlowDown_Machine"))
			{
				thisScript.SlowDown_Machine();
			}

			if(GUILayout.Button ("SetSpeed_Machine"))
			{
				thisScript.SetSpeed_Machine();
			}

		}
}
