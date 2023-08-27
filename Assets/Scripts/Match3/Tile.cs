using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;

    private Item _item;
    public Item Item
    {
        get => _item;

        set
        {
            if (_item == value) return;
            _item = value;
            icon.sprite = _item.sprite;
        }
    }
    public Image icon;
    public Button button;
    public Tile left => x > 0 ? Board.Instance.Tiles[x - 1, y] : null;
    public Tile Top => y > 0 ? Board.Instance.Tiles[x , y-1] : null;

    public Tile Right => x < Board.Instance.Width - 1 ? Board.Instance.Tiles[x + 1, y] : null;

    public Tile Bottom => y < Board.Instance.Width - 1 ? Board.Instance.Tiles[x , y+1] : null;

    public Tile[] NeighboursLR => new[]
    {
        left,
        Right,
    };
    public Tile[] NeighboursUD => new[]
   {
        Top,
        Bottom,
    };
    private void Start()
    {
        button.onClick.AddListener(call: () => Board.Instance.Select(tile: this));
    }



    public List<Tile> GetConnectedTiles(List<Tile> exclude = null,int dir =0)
    {
        var result = new List<Tile> { this, };
        if(exclude == null)
        {
            exclude = new List<Tile> { this, };
        }
        else
        {
            exclude.Add(this);
        }
        foreach(var neighbour in NeighboursLR)
        {

            if(neighbour == null || exclude.Contains(neighbour) || neighbour.Item != Item)continue;

            result.AddRange(neighbour.GetConnectedTiles(exclude));
        }
        
        return result;
    }

    public List<Tile> GetConnectedTilesUD(List<Tile> exclude = null, int dir = 0)
    {
        var result = new List<Tile> { this, };
        if (exclude == null)
        {
            exclude = new List<Tile> { this, };
        }
        else
        {
            exclude.Add(this);
        }
        foreach (var neighbour in NeighboursUD)
        {

            if (neighbour == null || exclude.Contains(neighbour) || neighbour.Item != Item) continue;

            result.AddRange(neighbour.GetConnectedTilesUD(exclude));
        }

        return result;
    }
}
