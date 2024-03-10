using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    //private Transform highlight;
    //private RaycastHit raycastHit;

    //void Update()
    //{
    //    // Highlight
    //    if (highlight != null)
    //    {
    //        highlight.gameObject.GetComponent<Outline>().enabled = false;
    //        highlight = null;
    //    }
    //    if (Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
    //    {
    //        highlight = raycastHit.transform;
    //        if (highlight.CompareTag("Grabbable"))
    //        {
    //            if (highlight.gameObject.GetComponent<Outline>() != null)
    //            {
    //                highlight.gameObject.GetComponent<Outline>().enabled = true;
    //            }
    //            else
    //            {
    //                Outline outline = highlight.gameObject.AddComponent<Outline>();
    //                outline.enabled = true;
    //                highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
    //                highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
    //            }
    //        }
    //        else
    //        {
    //            highlight = null;
    //        }
    //    }
    //}
}
