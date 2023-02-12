using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SAMI.TIKTAKTEO
{
    public class Call : MonoBehaviour
    {

       
        public Vector2Int index;
      

        public Call(Vector2Int place)
        {
            index.x = place.x;
            index.y=place.y;
        }

        public void CallButtonClicked()
        {
            Board.instance.ButtonClicked(gameObject, index);
        }
    }
}