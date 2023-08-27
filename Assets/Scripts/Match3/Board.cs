
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;

public sealed class Board : MonoBehaviour
{   
    public static Board Instance { get; private set; }
    private int UDorLR;
    private const float tweenDuration = 0.25f;
    
    public Row[] rows;

    public EssenceCounter ec;

    private readonly List<Tile> _selection = new List<Tile>();
    public Tile[,] Tiles { get; private set; }

    public int Width => Tiles.GetLength(dimension:0);
    public int Height => Tiles.GetLength(dimension: 1);

    private void Awake() => Instance = this;

    private void Start()
    {
        Tiles = new Tile[rows.Max(row => row.tiles.Length), rows.Length];

        for (var y = 0;  y < Height ; y++){
            for(var x = 0; x < Width; x++)
            {
                var tile = rows[y].tiles[x];

                tile.x = x;
                tile.y = y;



                tile.Item = ItemDatabase.items[Random.Range(0, ItemDatabase.items.Length)];



                Tiles[x, y] = rows[y].tiles[x];
            }
        }
     
    }
    private void Update()
    {
       

        if (!Input.GetKeyDown(KeyCode.A)) return;

        foreach (var connectedTile in Tiles[0, 0].GetConnectedTiles())
            connectedTile.icon.transform.DOScale(1.25f, tweenDuration).Play();

      
    }
    public async void Select(Tile tile)
    {
        if (!_selection.Contains(tile))
        {
            _selection.Add(tile);

        }
        if (_selection.Count < 2) return;
        Debug.Log(message: $"Selected tiles at ({_selection[0].x},{_selection[0].y}) and ({_selection[1].x},{_selection[1].y})");

        if (canPop())
        {
            if(UDorLR == 0)
            {
                PopUD();

            }
            else
            {
                PopLR();

            }
        }



        await Swap(_selection[0], _selection[1]);
        
        _selection.Clear();
    
    
    }
    public async Task Swap(Tile tile1, Tile tile2)
    {
        var icon1 = tile1.icon;
        var icon2 = tile2.icon;

        if(tile1.x == tile2.x)
        {
            UDorLR = 1;
        }
        else
        {
            UDorLR = 0;
        }




        var iconTransform = icon1.transform;
        var icon2Transform = icon2.transform;


        var sequence = DOTween.Sequence();
        sequence.Join(iconTransform.DOMove(icon2Transform.position, tweenDuration)).Join(icon2Transform.DOMove(iconTransform.position, tweenDuration));

        await sequence.Play().AsyncWaitForCompletion();

        iconTransform.SetParent(tile2.transform);
        icon2Transform.SetParent(tile1.transform);

        tile1.icon = icon2;
        tile2.icon = icon1;

        var tile1Item = tile1.Item;
        tile1.Item = tile2.Item;
        tile2.Item = tile1Item;






    }
    private bool canPop()
    {
        for (var y =0; y < Height; y++)
        {
            for(var x =0; x < Width; x++)
            {
                if (Tiles[x, y].GetConnectedTiles().Skip(1).Count() >= 2)
                    return true;
                if (Tiles[x, y].GetConnectedTilesUD().Skip(1).Count() >= 2)
                    return true;
            }
        }
        return false;
    }
    private async void PopLR()
    {
        for(var y = 0; y < Height; y++)
        {
            for(var x = 0; x < Width; x++)
            {
                var tile = Tiles[x, y];

                var connectedTiles = tile.GetConnectedTiles();




                if(connectedTiles.Skip(1).Count() < 2)
                {
                    continue;
                }
                var deflateSequence = DOTween.Sequence();

                foreach(var connectedTile in connectedTiles)
                {
                    deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, tweenDuration));
                }

                await deflateSequence.Play().AsyncWaitForCompletion();

                var inflateSequence = DOTween.Sequence();
                foreach(var  connectedTile in connectedTiles)
                {
                    connectedTile.Item = ItemDatabase.items[Random.Range(0, ItemDatabase.items.Length)];
                    inflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, tweenDuration));

                }
                await inflateSequence.Play().AsyncWaitForCompletion();
            }
        }
    }
    private async void PopUD()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var tile = Tiles[x, y];

                var connectedTiles = tile.GetConnectedTilesUD();




                if (connectedTiles.Skip(1).Count() < 2)
                {
                    continue;
                }
                var deflateSequence = DOTween.Sequence();

                foreach (var connectedTile in connectedTiles)
                {
                    deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, tweenDuration));
                    ec.getmatch(connectedTile.Item.value, connectedTiles.Count());
                }

                await deflateSequence.Play().AsyncWaitForCompletion();

                var inflateSequence = DOTween.Sequence();
                foreach (var connectedTile in connectedTiles)
                {
                    connectedTile.Item = ItemDatabase.items[Random.Range(0, ItemDatabase.items.Length)];
                    inflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, tweenDuration));

                }
                await inflateSequence.Play().AsyncWaitForCompletion();
            }
        }
    }
}
