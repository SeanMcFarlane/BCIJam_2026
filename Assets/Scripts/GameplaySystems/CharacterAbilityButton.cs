using TMPro;
using UnityEngine;

public class CharacterAbilityButton : MonoBehaviour {
	public TextMeshPro ButtonText;
	public BattleCharacter TargetBattleCharacter;
	public int AbilityIndex;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		if(TargetBattleCharacter.EquippedAbilities.Length > AbilityIndex) {
			ButtonText.text = TargetBattleCharacter.EquippedAbilities[AbilityIndex].AbilityName;
		}
	}

	void Initialize() {

	}

	void HandlePress() {

	}
}
