using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
public class Grid : MonoBehaviour {
		
		public Vector2 gridSize = new Vector2 (1, 1);

		Vector2 gridOffset;
		Vector3 lastGridSize;
	    Vector3 lastPosition;

		public List<Vector4> occupiedPositions;

		void Awake () {
				UpdateScale ();
		}

		void Update () {

				// If the transform has changed
				// Si el transform ha cambiado de valores
				if (transform.hasChanged) {

						// Update the grid texture size and fix positions of the draggable gameobjects
						// Actualizamos la escala de la textura de la cuadricula y corregimos la posicion de los objetos arrastrables
						transform.hasChanged = false;
						occupiedPositions.Clear ();
						GetComponent<Renderer> ().material.mainTextureScale = gridSize;
						FixPositions ();
						
				}

				// If grid size has changed
				// Si la cuadricula ha cambiado de escala
				if ((gridSize.x != lastGridSize.x) || (gridSize.y != lastGridSize.y)) {
						
						// Actualizamos la escala del objeto
						lastGridSize = gridSize;
						UpdateScale ();

				}
		}
		
		// Update transform and texture size
		// Actualizar escala del objeto y su textura
		void UpdateScale () {
				transform.localScale = new Vector3 (gridSize.x, gridSize.y, 1);
				GetComponent<Renderer> ().material.mainTextureScale = gridSize;
		}

		// Fix draggable gameobjects positions
		// Corregir posiciones de los objetos arrastrables
		void FixPositions () {
				var objs = FindObjectsOfType<DragAndDrop> ();
				var diff = transform.localPosition - lastPosition;
				foreach (DragAndDrop i in objs) {
						i.FixPosition (diff);
						i.UpdateAll ();
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
