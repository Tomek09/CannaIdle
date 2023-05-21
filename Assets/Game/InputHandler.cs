using UnityEngine;

namespace Game {
	public class InputHandler : Singleton<InputHandler> {

		public static System.Action<Vector3> OnMouseButtonDown;
		public static System.Action<Vector3> OnMouseButtonHold;
		public static System.Action<Vector3> OnMouseButtonUp;

		[Header("Info")]
		private Camera _mainCamera = null;

		private void OnDrawGizmos() {
			Vector3 point = GetMouseWorldPosition();
			Gizmos.DrawSphere(point, .125f);
		}

		private void Update() {
			if (Input.GetMouseButtonDown(0)) {
				OnMouseButtonDown?.Invoke(GetMouseWorldPosition());
			}

			if (Input.GetMouseButton(0)) {
				OnMouseButtonHold?.Invoke(GetMouseWorldPosition());
			}

			if (Input.GetMouseButtonUp(0)) {
				OnMouseButtonUp?.Invoke(GetMouseWorldPosition());
			}
		}

		private Vector3 GetMouseWorldPosition() {
			if (_mainCamera == null) {
				_mainCamera = Camera.main;
			}

			Plane plane = new Plane(Vector3.up, Vector3.zero);
			Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			if (plane.Raycast(ray, out float enter))
				return ray.GetPoint(enter);
			return new Vector3(-999, -999, -999);

		}
	}
}