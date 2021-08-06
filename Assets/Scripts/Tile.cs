using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case1
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileType tileType;
        private TileManager tileManager;
        public bool active = false;
        public Transform up;
        public Transform down;
        public Transform left;
        public Transform right;
        void Start()
        {
            gameObject.AddComponent<BoxCollider2D>();
            gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            GameObject child = new GameObject();
            child.transform.position = transform.position;
            child.SetActive(false);
            child.transform.SetParent(transform);
            child.gameObject.AddComponent<SpriteRenderer>().sprite = tileType.spriteX;
            child.transform.localScale /= 8;
            child.GetComponent<SpriteRenderer>().sortingOrder = 1;
            child.name = "XSprite";
        }
        private void OnMouseDown()
        {
            if (!active)
            {
                active = true;
                transform.GetChild(0).gameObject.SetActive(true);
                tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
                tileManager.SelectedTiles.Add(transform);
                tileManager.Selected();
            }
        }
    }
}