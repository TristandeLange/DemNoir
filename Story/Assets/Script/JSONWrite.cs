using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using UnityEditorInternal;

public class JSONWrite : MonoBehaviour
{

    [System.Serializable]
    public class playerStats
    {
        public int nodeid;
        public int knowledge;
        public int charm;
        public int finesse;
        public int muscle;
        public int heart;
        public int sense;

        public playerStats(int v1, int v2, int v3, int v4, int v5, int v6, int v7)
        {
            this.nodeid = v1;
            this.knowledge = v2;
            this.charm = v3;
            this.finesse = v4;
            this.muscle = v5;
            this.heart = v6;
            this.sense = v7;
        }
    }


    public playerStats myPlayer = new playerStats(0,0,0,0,0,0,0);



    public void outputJSON() 
    {
        string strOutput = JsonUtility.ToJson(myPlayer);

        File.WriteAllText(Application.dataPath + "/resources/text.json", strOutput);
        

    }
    
}
