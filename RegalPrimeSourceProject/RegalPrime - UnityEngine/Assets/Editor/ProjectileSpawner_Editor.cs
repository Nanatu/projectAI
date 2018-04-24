// RegalPrime 6-02-14 - ProjectileSpawner_Editor.cs

// Shows and hides variables based on the current selected attack type (from ProjectileSpawner)
// This sets all the vaiables and shows warnings / extra info if needed.

// Things look a lot nicer with this script. Its my first time making one so there might be bugs.

using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;

static class EqualExtensions
{
	public static bool In<T>(this T obj, params T[] args)
	{
		return args.Contains(obj);
	}
}


[CustomEditor(typeof(ProjectileSpawner))]
public class ProjectileSpawner_Editor : Editor
{
	[SerializeField] ProjectileSpawner thisScript;

	string[] SpawnTypes;
	int CurrentSpawnSelection = 0;

	string InfiniteSpawns;
	string InfiniteObjects;

	void Awake()
	{
		thisScript = target as ProjectileSpawner;
		SpawnTypes = thisScript.GetListOfSpawns ();

		if(SpawnTypes == null)
		SpawnTypes = new string[]{"There should be data here"};

		CurrentSpawnSelection = thisScript.CurrentSpawnSelection;



//		var testObj = ScriptableObject.CreateInstance<ProjectileSpawner>();
//		var serializedTestObject = new UnityEditor.SerializedObject (testObj);
//		var serializedINT = serializedTestObject.FindProperty ("CurrentSpawnSelection");
//		Debug.Log ("DOES THIS EVEN WORK " + serializedINT.intValue);

	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update ();

		if(thisScript.NumberOfSets <= 0)
		{
			thisScript.NumberOfSets = 0;
			InfiniteSpawns = "- Infinite Sets";
		}
		else
			InfiniteSpawns = "";

		if(thisScript.NumberOfObjects <= 0)
		{
			thisScript.NumberOfObjects = 0;
			InfiniteObjects = "- Infinite Spawns";
		}
		else
			InfiniteObjects = "";

		if(CurrentSpawnSelection == 7)
			InfiniteObjects = "per circle burst";

		thisScript.IsEnabled	= EditorGUILayout.Toggle("IsEnabled",thisScript.IsEnabled);
		thisScript.RandomColor	= EditorGUILayout.Toggle("RandomColor",thisScript.RandomColor);
		thisScript.Kinematic	= EditorGUILayout.Toggle("Kinematic",thisScript.Kinematic);
		thisScript.objectToSpawn = (GameObject) EditorGUILayout.ObjectField("Object To Spawn", thisScript.objectToSpawn, typeof(GameObject), true);

		EditorGUILayout.LabelField ("");

		CurrentSpawnSelection = EditorGUILayout.Popup ("Spawn Type ", CurrentSpawnSelection, SpawnTypes);
		thisScript.CurrentSpawnSelection = CurrentSpawnSelection;


		thisScript.InitialStartDelay	= EditorGUILayout.FloatField("InitialStartDelay",thisScript.InitialStartDelay);
		thisScript.NumberOfSets	= EditorGUILayout.IntField(new GUIContent("NumberOfSets "+InfiniteSpawns, "Choosing 0 will result in infinite spawn sets"),thisScript.NumberOfSets);
		thisScript.DelayBetweenSets	= EditorGUILayout.FloatField("DelayBetweenSets",thisScript.DelayBetweenSets);

		EditorGUILayout.LabelField ("");

		if(CurrentSpawnSelection.In (0,2))
		{
			thisScript.TargetPlayer	= EditorGUILayout.Toggle("TargetPlayer",thisScript.TargetPlayer);
			if(!thisScript.TargetPlayer)
				thisScript.TargetObject = (GameObject) EditorGUILayout.ObjectField("TargetObject", thisScript.TargetObject, typeof(GameObject), true);
		}
		if(CurrentSpawnSelection.In (1,4,5,6))
		{
			thisScript.TargetDirection = EditorGUILayout.Vector2Field("TargetDirection", thisScript.TargetDirection);
		}

		EditorGUILayout.LabelField ("");

		thisScript.NumberOfObjects	= EditorGUILayout.IntField	("NumberOfObjects "+InfiniteObjects, thisScript.NumberOfObjects);
		if(CurrentSpawnSelection != 7)
		{
			thisScript.AppearDelay	= EditorGUILayout.FloatField("AppearDelay",thisScript.AppearDelay);
		}
		thisScript.ObjectSpeed		= EditorGUILayout.FloatField("ObjectSpeed", 	thisScript.ObjectSpeed);
		thisScript.ObjectDeathTimer	= EditorGUILayout.FloatField("ObjectDeathTimer",thisScript.ObjectDeathTimer);

		if(CurrentSpawnSelection.In (0,1))
		{
			EditorGUILayout.LabelField ("");
			thisScript.SprayVariance	= EditorGUILayout.FloatField(new GUIContent("SprayVariance", "Will cause a degree variance (spray effect) when shooting in a direction"),thisScript.SprayVariance);
		}
		if(CurrentSpawnSelection == 4)
		{
			EditorGUILayout.LabelField ("");
			thisScript.DegreeChange	= EditorGUILayout.FloatField(new GUIContent("DegreeChange","Each consecutive object is incremented X degrees"),thisScript.DegreeChange);
			thisScript.DegreeCap	= EditorGUILayout.FloatField(new GUIContent("DegreeCap","Will cap the rotation to a certain degree. If 0 then it will not cap it"),thisScript.DegreeCap);
		}
		EditorGUILayout.LabelField ("");
		if(CurrentSpawnSelection == 5)
		{
			thisScript.Amplitude_X	= EditorGUILayout.FloatField("Amplitude_X",thisScript.Amplitude_X);
			thisScript.PeriodSpeed_X	= EditorGUILayout.FloatField("PeriodSpeed_X",thisScript.PeriodSpeed_X);
			thisScript.Amplitude_Y	= EditorGUILayout.FloatField("Amplitude_Y",thisScript.Amplitude_Y);
			thisScript.PeriodSpeed_Y	= EditorGUILayout.FloatField("PeriodSpeed_Y",thisScript.PeriodSpeed_Y);
			thisScript.LerpMovement	= EditorGUILayout.Toggle("LerpMovement",thisScript.LerpMovement);
		}

		if(thisScript.ObjectDeathTimer <= 0)
		{
			thisScript.ObjectDeathTimer = 0;
			EditorGUILayout.HelpBox("WARNING - Having ObjectDeathTimer = 0 will cause the objects to never die", MessageType.Warning);
		}
		if(thisScript.NumberOfSets <= 0 && thisScript.NumberOfObjects <= 0)
		{
			EditorGUILayout.HelpBox("WARNING - Infinite objects spawning with infinite sets. THIS IS NOT A GOOD IDEA", MessageType.Warning);
		}


		EditorUtility.SetDirty (thisScript);
//		EditorGUILayout.LabelField ("");
//		EditorGUILayout.LabelField ("");
//		EditorGUILayout.LabelField ("");
//		base.OnInspectorGUI ();
	}
}

/*
0 "AtTarget",
1 "InDirection",
2 "FollowTarget",
3 "RandomSprayOutwards",
4 "CircularSpray",
5 "WaveSpray",
6 "FromScreenEdge",
7 "CircleBurst"
*/
