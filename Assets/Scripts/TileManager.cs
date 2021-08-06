using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Case1
{
    public class TileManager : MonoBehaviour
    {
        //public GameObject TilegameObject;
        public int Size;
        public Transform prefab;
        public List<Transform> Tiles = new List<Transform>();
        public List<Transform> SelectedTiles = new List<Transform>();
        public Text InputField;
        public Text MatchText;
        private void Start()
        {
            CreateGrid(3);
        }
        private bool ScreenIsVertical()
        {
            if (Camera.main.scaledPixelHeight > Camera.main.scaledPixelWidth)
            {
                return true;
            }
            return false;
        }
        public void Selected()
        {
            if (SelectedTiles.Count > 2)
            {
                for (int i = 0; i < SelectedTiles.Count; i++)
                {
                    var item = SelectedTiles[i];
                    var temp = Tiles[Tiles.IndexOf(item)].GetComponent<Tile>();
                    int counter = 0;

                    if (temp.right != null) { if (temp.right.GetComponent<Tile>().active != false) { counter++; } }
                    if (temp.up != null) { if (temp.up.GetComponent<Tile>().active != false) { counter++; } }
                    if (temp.down != null) { if (temp.down.GetComponent<Tile>().active != false) { counter++; } }
                    if (temp.left != null) { if (temp.left.GetComponent<Tile>().active != false) { counter++; } }

                    if (counter >= 2)
                    {
                        DeleteTiles(item);
                        MatchText.text = (int.Parse(MatchText.text) + 1).ToString();
                    }
                }
            }
        }
        void DeleteTiles(Transform _transform)
        {
            _transform.GetChild(0).gameObject.SetActive(false);
            _transform.GetComponent<Tile>().active = false;
            if (_transform.GetComponent<Tile>().right != null)
            {
                if (_transform.GetComponent<Tile>().right.GetComponent<Tile>().active == true)
                { DeleteTiles(_transform.GetComponent<Tile>().right); }
            }

            if (_transform.GetComponent<Tile>().down != null)
            {
                if (_transform.GetComponent<Tile>().down.GetComponent<Tile>().active == true)
                { DeleteTiles(_transform.GetComponent<Tile>().down); }
            }

            if (_transform.GetComponent<Tile>().up != null)
            {
                if (_transform.GetComponent<Tile>().up.GetComponent<Tile>().active == true)
                { DeleteTiles(_transform.GetComponent<Tile>().up); }
            }
            if (_transform.GetComponent<Tile>().left != null)
            {
                if (_transform.GetComponent<Tile>().left.GetComponent<Tile>().active == true)
                { DeleteTiles(_transform.GetComponent<Tile>().left); }
            }
            SelectedTiles.Remove(_transform);
        }
        public void CreateGrid(int size)
        {
            for (float i = -size + 1; i < 1; i++)
            {
                for (float j = -size + 1; j < 1; j++)
                {
                    GameObject g = Instantiate(prefab.gameObject, new Vector3(i + transform.position.x, j + transform.position.y, 0), Quaternion.identity);
                    g.name = "Tile" + i + " " + j;
                    g.transform.parent = transform;
                    Tiles.Add(g.transform);
                }
            }
            foreach (var item in Tiles)
            {
                if (Tiles.IndexOf(item) >= size)
                {
                    item.GetComponent<Tile>().left = Tiles[Tiles.IndexOf(item) - size];
                }
                else { item.GetComponent<Tile>().left = null; }

                if (Tiles.Count - Tiles.IndexOf(item) > size)
                {
                    item.GetComponent<Tile>().right = Tiles[Tiles.IndexOf(item) + size];
                }
                else { item.GetComponent<Tile>().right = null; }

                if (Tiles.IndexOf(item) % size != size - 1)
                {
                    item.GetComponent<Tile>().up = Tiles[Tiles.IndexOf(item) + 1];
                }
                else { item.GetComponent<Tile>().up = null; }

                if (Tiles.IndexOf(item) % size != 0)
                {
                    item.GetComponent<Tile>().down = Tiles[Tiles.IndexOf(item) - 1];
                }
                else { item.GetComponent<Tile>().down = null; }
            }
            if (ScreenIsVertical())
            {
                Camera.main.orthographicSize = prefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x * size * Screen.height / Screen.width * 0.5f;
            }
            else
            {
                Camera.main.orthographicSize = (float)size / 2;
            }
            Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            transform.position = new Vector3(screenBounds.x - 0.5f, screenBounds.y - 0.5f);
        }
        public void ClearGrid()
        {
            foreach (var item in Tiles)
            {
                Destroy(item.gameObject);
            }
            Tiles.Clear();
            SelectedTiles.Clear();
        }
    }
}