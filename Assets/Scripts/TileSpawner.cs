using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RunRun3
{
    public class TileSpawner : MonoBehaviour
    {
        [SerializeField] private int tileStartCount = 10;
        //[SerializeField] private int minimumStraightTiles = 3;
        //[SerializeField] private int maximumStraightTiles = 15;
        //[SerializeField] private GameObject startingTile;
        [SerializeField] private List<GameObject> startingTile;
        [SerializeField] private List<GameObject> normalTiles;
        [SerializeField] private List<GameObject> gapTiles;


        private Vector3 currentDownTileLocation = Vector3.zero;
        private Vector3 currentRightTileLocation = Vector3.zero;
        private Vector3 currentLeftTileLocation = Vector3.zero;
        private Vector3 currentUpTileLocation = Vector3.zero;
        
        private Vector3 currentDownTileDirection = Vector3.forward;
        //private Vector3 currentTileRotation = Vector3.zero;
        private GameObject prevTile;
        
        private List<GameObject> currentTiles;
        private List<GameObject> currentObstacles;

        public int indexCounter;

        private void Update()
        {
            Debug.Log(currentDownTileLocation);
        }

        private void Start()
        {
            currentTiles = new List<GameObject>();
            currentObstacles = new List<GameObject>();
            
            Random.InitState(System.DateTime.Now.Millisecond);
            
                for (int i = 0; i < tileStartCount; ++i)
                {
                    SpawnDownTile(startingTile[0], false);
                    SpawnRightTile(startingTile[1], false);
                    SpawnLeftTile(startingTile[2], false);
                    SpawnUpTile(startingTile[3], false);
                }

                //End tile thing
            //SpawnDownTile(SelectRandomGameObjectFromList(endTiles).GetComponent<Tile>(), false);
        }
        private void SpawnDownTile(GameObject tile, bool spawnObstacle)
        {
            prevTile = GameObject.Instantiate(tile.gameObject, currentDownTileLocation, Quaternion.Euler(0,0,0));
            currentTiles.Add(prevTile);
            
            prevTile.GetComponent<Tile>().index = indexCounter;

            if (indexCounter > 9)
            {
                indexCounter = 0;
            }
            else indexCounter++;
            
            currentDownTileLocation += Vector3.Scale(prevTile.GetComponent<Renderer>().bounds.size, currentDownTileDirection);
        }
        
        private void SpawnRightTile(GameObject tile, bool spawnObstacle)
        {
            currentRightTileLocation = new Vector3(2, 2, currentRightTileLocation.z);
            
            prevTile = GameObject.Instantiate(tile.gameObject, currentRightTileLocation, Quaternion.Euler(0,0,90));
            currentTiles.Add(prevTile);
            
            prevTile.GetComponent<Tile>().index = indexCounter;

            if (indexCounter > 9)
            {
                indexCounter = 0;
            }
            else indexCounter++;
            
            currentRightTileLocation += Vector3.Scale(prevTile.GetComponent<Renderer>().bounds.size, currentDownTileDirection);
        }
        
        private void SpawnLeftTile(GameObject tile, bool spawnObstacle)
        {
            currentLeftTileLocation = new Vector3(-2,2, currentLeftTileLocation.z);
            
            prevTile = GameObject.Instantiate(tile.gameObject, currentLeftTileLocation, Quaternion.Euler(0,0,90));
            currentTiles.Add(prevTile);
            
            prevTile.GetComponent<Tile>().index = indexCounter;

            if (indexCounter > 9)
            {
                indexCounter = 0;
            }
            else indexCounter++;
            
            currentLeftTileLocation += Vector3.Scale(prevTile.GetComponent<Renderer>().bounds.size, currentDownTileDirection);
        }
        
        private void SpawnUpTile(GameObject tile, bool spawnObstacle)
        {
            currentUpTileLocation = new Vector3(0,4,currentUpTileLocation.z);
            
            prevTile = GameObject.Instantiate(tile.gameObject, currentUpTileLocation, Quaternion.Euler(0,0,0));
            currentTiles.Add(prevTile);
            
            prevTile.GetComponent<Tile>().index = indexCounter;

            if (indexCounter > 9)
            {
                indexCounter = 0;
            }
            else indexCounter++;
            
            currentUpTileLocation += Vector3.Scale(prevTile.GetComponent<Renderer>().bounds.size, currentDownTileDirection);
        }

        public void AddNewTiles()
        {
            DeletePreviousTiles();
            SpawnDownTile(startingTile[0], false);
            SpawnRightTile(startingTile[1], false);
            SpawnLeftTile(startingTile[2], false);
            SpawnUpTile(startingTile[3], false);
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