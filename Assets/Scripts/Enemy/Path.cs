using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Enemy
{
    public class Path : MonoBehaviour
    {
        [System.Serializable]
        public struct Way
        {
            public List<Transform> wayPoints;
        }

        [SerializeField]
        private List<Way> ways= new List<Way>();

        [SerializeField]
        private bool debug = false;

        public Way GetWay(int index){
            if(index > ways.Count-1 || index < 0){
                return ways[0];
            }else{
                return ways[index];
            }

        }

        public int GetWayCount(){
            return ways.Count;
        }

        public void Update(){
            if(debug){
                for(int i = 0; i < ways.Count; i++){
                    for(int k = 1; k < ways[i].wayPoints.Count; k++){
                        Debug.DrawLine(ways[i].wayPoints[k - 1].position, ways[i].wayPoints[k].position, Color.blue);
                    }
                }
            }
        }

    }
}
