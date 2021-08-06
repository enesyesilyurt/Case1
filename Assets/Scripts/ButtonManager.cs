using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case1
{
    public class ButtonManager : MonoBehaviour
    {
        public TileManager tileManager;
        public void RebuildButton()
        {
            if (tileManager.InputField.text != null)
            {
                tileManager.MatchText.text = "0";
                tileManager.ClearGrid();

                int size = int.Parse(tileManager.InputField.text);
                if (size < 3)
                    tileManager.Size = 3;
                else
                    tileManager.Size = size;
                tileManager.CreateGrid(tileManager.Size);
            }
        }
    }
}