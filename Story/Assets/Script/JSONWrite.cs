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
        public int mind;
        public int heart;
        public int sneakiness;
        public int strength;

        public playerStats(int v1, int v2, int v3, int v4, int v5)
        {
            this.nodeid = v1;
            this.mind = v2;
            this.heart = v3;
            this.sneakiness = v4;
            this.strength = v5;
        }
    }


    public playerStats myPlayer = new playerStats(0,0,0,0,10);



    public void outputJSON() 
    {
        string strOutput = JsonUtility.ToJson(myPlayer);

        File.WriteAllText(Application.dataPath + "/resources/text.json", strOutput);
        

    }
    
}
