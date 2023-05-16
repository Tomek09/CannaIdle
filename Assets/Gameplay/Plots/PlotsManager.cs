using UnityEngine;

namespace Gameplay.Plots {
	public class PlotsManager : Singleton<PlotsManager> {

		[Header("Components")]
		[SerializeField] private Plot[] _plots = null;

		public Plot ActivePlot { get => _plots[0]; }

	}
}