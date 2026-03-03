using TMPro;
using UnityEngine;

public class CharacterAbilityButton : MonoBehaviour {
	//Set Manually in scene
	public RoboticlashGameManagerComponent GameManager;

	public TextMeshPro ButtonText;
	public BattleCharacter PlayerBattleCharacter;
	public BattleCharacter EnemyBattleCharacter;
	public int AbilityIndex;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start() {
		if(PlayerBattleCharacter.EquippedAbilities.Length > AbilityIndex) {
			ButtonText.text = PlayerBattleCharacter.EquippedAbilities[AbilityIndex].AbilityName;
		}
	}

	public void HandlePress() {
		if(GameManager.IsPlayerTurn) {
			PlayerBattleCharacter.StartUsingAbility(PlayerBattleCharacter.EquippedAbilities[AbilityIndex], EnemyBattleCharacter);
		}
	}
}
