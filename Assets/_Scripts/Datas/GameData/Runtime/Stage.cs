using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class Stage
{
  [SerializeField]
  int id;
  public int Id { get {return id; } set { this.id = value;} }
  
  [SerializeField]
  int[] mapids = new int[0];
  public int[] Mapids { get {return mapids; } set { this.mapids = value;} }
  
  [SerializeField]
  int[] passiveskillids = new int[0];
  public int[] Passiveskillids { get {return passiveskillids; } set { this.passiveskillids = value;} }
  
}