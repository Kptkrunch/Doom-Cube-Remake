// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class CutSprite : MonoBehaviour
// {
//     public LineRenderer lineRenderer;
//     public float burnTime = 1f;
//
//     private bool isCut = false;
//
//     void Update()
//     {
//         if (!isCut && Input.GetMouseButton(0))
//         {
//             Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
//
//             if (hit.collider != null && hit.collider.gameObject == gameObject)
//             {
//                 isCut = true;
//
//                 Vector2[] positions = new Vector2[2];
//                 positions[0] = new Vector2(-1f, 0f);
//                 positions[1] = new Vector2(1f, 0f);
//                 lineRenderer.SetPositions(positions);
//
//                 StartCoroutine(BurnEdges());
//             }
//         }
//     }
//
//     IEnumerator BurnEdges()
//     {
//         yield return new WaitForSeconds(burnTime);
//
//         GetComponent<D2dDestructible>().StampAll(lineRenderer);
//     }
// }