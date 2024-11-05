using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class CharacterConfig
{
  [SerializeField]
  int id;
  public int Id { get {return id; } set { this.id = value;} }
  
  [SerializeField]
  string name;
  public string Name { get {return name; } set { this.name = value;} }
  
  [SerializeField]
  int[] cardids = new int[0];
  public int[] Cardids { get {return cardids; } set { this.cardids = value;} }
  
  [SerializeField]
  int sex;
  public int Sex { get {return sex; } set { this.sex = value;} }
  
  [SerializeField]
  string traits;
  public string Traits { get {return traits; } set { this.traits = value;} }
  
  [SerializeField]
  string concepts;
  public string Concepts { get {return concepts; } set { this.concepts = value;} }
  
  [SerializeField]
  string aiprompt;
  public string Aiprompt { get {return aiprompt; } set { this.aiprompt = value;} }
  
}