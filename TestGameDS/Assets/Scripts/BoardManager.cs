using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;     
    public List<Sprite> characters = new List<Sprite>();    
    public GameObject tile;
    public int xSize, ySize;
    private const float yBorder= 0.4f;
    private const float xBorder = 0.5f;
    private float SizeTile=1;
    private GameObject[,] tiles;
     public bool IsShifting { get; set; }     
    

    void Start()
    {
        instance = GetComponent<BoardManager>();
        while (SizeTile*0.256 * ySize/2 >= yBorder)
        {
            float step = 0.03f;
            SizeTile = SizeTile - step;
            tile.transform.localScale = new Vector3(SizeTile, SizeTile, SizeTile);
            if(SizeTile*0.256*xSize/2>= xBorder)
            {
                while (SizeTile * 0.256 * xSize / 2 >= xBorder)
                {
                    SizeTile = SizeTile - step;
                    tile.transform.localScale = new Vector3(SizeTile, SizeTile, SizeTile);
                }
            }
        }
        if (xSize >= 10 && ySize >= 10 && xSize <= 50 && ySize <= 50) {
            Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
            CreateBoard(offset.x, offset.y);

        }
    }
      private void CreateBoard(float xOffset, float yOffset)
      {
          tiles = new GameObject[xSize, ySize];

          float startX = transform.position.x - xSize*xOffset/2;     
          float startY = transform.position.y - ySize*yOffset/2;

          for (int x = 0; x < xSize; x++)
          {      
              for (int y = 0; y < ySize; y++)
              {
                  GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x),startY + (yOffset * y), 0), tile.transform.rotation);
                  tiles[x, y] = newTile;
                  newTile.transform.parent = transform;
                  Sprite newSprite = characters[Random.Range(0, characters.Count)];
                  newTile.GetComponent<SpriteRenderer>().sprite = newSprite; 
              }
          }
      }
    public IEnumerator FindNullTiles()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (tiles[x, y].GetComponent<SpriteRenderer>().sprite == null)
                {
                    yield return StartCoroutine(ShiftTilesDown(x, y));
                    break;
                }
            }
        }
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                tiles[x, y].GetComponent<Tile>().ClearAllMatches();
            }
        }
    }
    private IEnumerator ShiftTilesDown(int x, int yStart, float shiftDelay = .03f)
    {
        IsShifting = true;
        List<SpriteRenderer> renders = new List<SpriteRenderer>();
        int nullCount = 0;

        for (int y = yStart; y < ySize; y++)
        {
            SpriteRenderer render = tiles[x, y].GetComponent<SpriteRenderer>();
            if (render.sprite == null)
            {
                nullCount++;
            }
            renders.Add(render);
            yield return new WaitForSeconds(shiftDelay);
        }
        IsShifting = false;
    }
}
