using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace RunRun3
{
    public class TileSpawner : MonoBehaviour
    {
        [SerializeField] private int tileStartCount = 10;
        [SerializeField] private int minimumStraightTiles = 3;
        [SerializeField] private int maximumStraightTiles = 15;
        [SerializeField] private GameObject startingTile;
        [SerializeField] private List<GameObject> endTiles;
        [SerializeField] private List<GameObject> obstacles;
        

        private Vector3 currentTileLocation = Vector3.zero;
        private Vector3 currentTileDirection = Vector3.forward;
        private GameObject prevTile;

        private List<GameObject> currentTiles;
        private List<GameObject> currentObstacles;

        public int indexCounter;
        private void Start()
        {
            currentTiles = new List<GameObject>();
            currentObstacles = new List<GameObject>();
            
            Random.InitState(System.DateTime.Now.Millisecond);

            for (int i = 0; i < tileStartCount; ++i)
            {
                SpawnTile(startingTile.GetComponent<Tile>(), false);
            } 
            
            //End tile thing
            //SpawnTile(SelectRandomGameObjectFromList(endTiles).GetComponent<Tile>(), false);
        }
        private void SpawnTile(Tile tile, bool spawnObstacle)
        {
            prevTile = GameObject.Instantiate(tile.gameObject, currentTileLocation, Quaternion.identity);
            currentTiles.Add(prevTile);
            
            prevTile.GetComponent<Tile>().index = indexCounter;
            
            if (indexCounter > 9)
            {
                indexCounter = 0;
            }
            else indexCounter++;
            
            currentTileLocation += Vector3.Scale(prevTile.GetComponent<Renderer>().bounds.size, currentTileDirection);
        }

        public void AddNewTiles()
        {
            DeletePreviousTiles();
            SpawnTile(startingTile.GetComponent<Tile>(), false);
        }

        private void DeletePreviousTiles()
        {
            var maxCurrentTiles = currentTiles.Count - 1;
            while (currentTiles.Count != maxCurrentTiles)
            {
                GameObject tile = currentTiles[0];
                currentTiles.RemoveAt(0);
                Destroy(tile);
            }
        }
        
        

        private GameObject SelectRandomGameObjectFromList(List<GameObject> list)
        {
            if (list.Count == 0) return null;
            
            return list[Random.Range(0, list.Count)];
        }
    }

}

