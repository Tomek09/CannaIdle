using UnityEngine;

namespace Gameplay.Plots {
	public class PlotTile : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private Patches.Patch _patch = null;
		[SerializeField] private Plants.Plant _plant = null;

		public Patches.Patch Patch { get => _patch; }
		public Plants.Plant Plant { get => _plant; }



		public void Initialize() {
			_patch.Initialize(this);
			_plant.Initialize(this);
		}

	}
}