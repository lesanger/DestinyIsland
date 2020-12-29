using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]

[Serializable]
public class ClothingSkins
{
	public Material[] Skins;
}

public class MannequinFemale : MonoBehaviour
{
	public static int TopType { get; set; }
	public static int BottomType { get; set; }
	public static int TopSkin { get; set; }
	public static int BottomSkin { get; set; }
	public static int MannequinType { get; set; }

	public GameObject mannequinMesh;

	public GameObject topMesh;
	public GameObject bottomMesh;

	public Material[]	mannequinTopMaskMaterials;
	public Material[]	mannequinBottomMaskMaterials;
	public Material		mannequinTopMaskOff;
	public Material		mannequinBottomOff;

	public MeshFilter[]	mannequinMeshes;
	public MeshFilter[] topMeshes;
	public MeshFilter[] bottomMeshes;

	public ClothingSkins[]	topSkins;
	public ClothingSkins[]	bottomSkins;



	[SerializeField]
	private int sTopType = 0; // Developer set value in the inspector, for testing purpose
	[SerializeField]
	private int sBottomType = 0;  
	[SerializeField]
	private int sTopSkin = 0; 
	[SerializeField]
	private int sBottomSkin = 0;
	[SerializeField]
	private int sMannequinType = 0; 

	void Start()
	{
		if (!Application.isEditor)
		{
			MannequinType = TopType = BottomType = TopSkin = BottomSkin = 0; // Normal value, used for the final game
		}
	}

	void OnValidate()
	{
		if ( sTopType < -1 )
			sTopType = -1;
		else if  ( sTopType > topMeshes.Length - 1 )
			sTopType = topMeshes.Length - 1;

		if ( sBottomType < -1 )
			sBottomType = -1;
		else if  ( sBottomType > bottomMeshes.Length - 1 )
			sBottomType = bottomMeshes.Length - 1;

		if ( sTopType >= 0 )
		{
			if ( sTopSkin < 0 )
				sTopSkin = 0;
			else if  ( sTopSkin > topSkins[ sTopType ].Skins.Length - 1 )
				sTopSkin = topSkins[ sTopType ].Skins.Length - 1;
		}

		if ( sBottomType >= 0 )
		{
			if ( sBottomSkin < 0 )
				sBottomSkin = 0;
			else if  ( sBottomSkin > bottomSkins[ sBottomType ].Skins.Length - 1 )
				sBottomSkin = bottomSkins[ sBottomType ].Skins.Length - 1;
		}

		if ( sMannequinType < 0 )
			sMannequinType = 0;
		else if  ( sMannequinType > mannequinMeshes.Length - 1 )
			sMannequinType = mannequinMeshes.Length - 1;


		TopType = sTopType;
		BottomType = sBottomType;
		TopSkin = sTopSkin;
		BottomSkin = sBottomSkin;
		MannequinType = sMannequinType;

		mannequinMesh.GetComponent<MeshFilter>().mesh = mannequinMeshes[ MannequinType ].sharedMesh;

		if ( topMeshes.Length > 0 )
		{
			if ( sTopType >= 0 )
			{
				topMesh.SetActive( true );
				topMesh.GetComponent<MeshFilter>().mesh = topMeshes[ TopType ].sharedMesh;
				topMesh.GetComponent<MeshRenderer>().material = topSkins[ TopType ].Skins[ TopSkin ];
			}
			else if ( sTopType < 0 )
			{
				topMesh.SetActive( false );
			}
		}

		if ( bottomMeshes.Length > 0 )
		{
			if ( sBottomType >= 0 )
			{
				bottomMesh.SetActive( true );
				bottomMesh.GetComponent<MeshFilter>().mesh = bottomMeshes[ BottomType ].sharedMesh;
				bottomMesh.GetComponent<MeshRenderer>().material = bottomSkins[ BottomType ].Skins[ BottomSkin ];
			}
			else if ( sBottomType < 0 )
				bottomMesh.SetActive( false );
		}

		Material[] newMannequinSkin;
		newMannequinSkin = mannequinMesh.GetComponent<MeshRenderer>().sharedMaterials;

		if ( sTopType < 0 )
			newMannequinSkin[0] = mannequinTopMaskOff;
		else if ( sTopType >= 0 )
			newMannequinSkin[0] = mannequinTopMaskMaterials[ TopType ];

		if ( newMannequinSkin.Length > 1 )
		{
			if ( sBottomType < 0 )
				newMannequinSkin[1] = mannequinBottomOff;
			else if ( sBottomType >= 0  &&   mannequinBottomMaskMaterials.Length > 0 )
				newMannequinSkin[1] = mannequinBottomMaskMaterials[ BottomType ];
		}

		// Set alpha masking of the mannequin so polygons dont stick out of the clothings
		mannequinMesh.GetComponent<MeshRenderer>().sharedMaterials = newMannequinSkin;


	}
}