using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
public class Grid : MonoBehaviour {
		
		public Vector2 gridSize = new Vector2 (1, 1);

		Vector2 gridOffset;
		Vector3 lastGridSize;
	    Vector3 lastPosition;

		public List<Vector4> occupiedPositions;

		// Al iniciarse el script
		void Awake () {
				UpdateScale ();
		}

		// Actualizar cada frame
		void Update () {
				
				// Si el transform ha cambiado de valores
				if (transform.hasChanged) {

						// Actualizamos la escala del grid size y corregimos la posicion de los objetos arrastrables
						transform.hasChanged = false;
						UpdateScale ();
						FixPositions ();
				}

				// Si la cuadricula ha cambiado de escala
				if ((gridSize.x != lastGridSize.x) || (gridSize.y != lastGridSize.y)) {
						
						// Actualizamos la escala del objeto
						lastGridSize = gridSize;
						UpdateScale ();

				}
		}

		// Actualizar escala del objeto
		void UpdateScale () {
				transform.localScale = new Vector3 (gridSize.x, gridSize.y, 1);
				GetComponent<Renderer> ().material.mainTextureScale = gridSize;
		}

		// Corregir posiciones de los objetos arrastrables
		void FixPositions () {
				var objs = FindObjectsOfType<DragAndDrop> ();
				var diff = transform.localPosition - lastPosition;
				foreach (DragAndDrop i in objs) {
						i.FixPosition (diff);
				}
				lastPosition = transform.localPosition;
		}

		// Obtener el Offset de la cuadricula
		public Vector2 GetGridOffset () {
				gridOffset.x = transform.localPosition.x;
				gridOffset.y = transform.localPosition.y;
				return gridOffset;
		}
}
