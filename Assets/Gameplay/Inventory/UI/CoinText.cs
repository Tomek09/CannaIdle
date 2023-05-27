using TMPro;
using UnityEngine;

namespace Gameplay.Inventory.UI {
	public class CoinText : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private TMP_Text _text = null;

		private void OnEnable() {
			InventoryManager.OnCoinModify += ModifyText;
		}

		private void OnDisable() {
			InventoryManager.OnCoinModify -= ModifyText;
		}

		private void ModifyText(int coins) {
			_text.text = coins.ToString("D5");
		}

	}
}