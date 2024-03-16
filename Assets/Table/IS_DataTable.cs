using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class IS_DataTable : ScriptableObject
{
	 public List<Inside_Entity> Inside; // Replace 'EntityType' to an actual type that is serializable.
	 public List<Dialog_Entity> Dialogue; // Replace 'EntityType' to an actual type that is serializable.
}
