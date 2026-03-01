using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAbility", menuName = "Scriptable Objects/CharacterAbility")]
public class CharacterAbility : ScriptableObject {
	public string AbilityName = "Name";
	public int Damage = 10;
}
