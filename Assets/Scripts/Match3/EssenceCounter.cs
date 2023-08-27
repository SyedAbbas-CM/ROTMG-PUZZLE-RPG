using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceCounter : MonoBehaviour
{
    public int kingCount=0;
    public int warriorCount = 0;
    public int wizardCount = 0;
    public int loverCount = 0;

    public void getmatch(int value,int length)
    {
        if(value == 4)
        {
            warriorCount += length;
        }
        if (value == 3)
        {
            wizardCount += length;
        }
        if (value == 1)
        {
            kingCount += length;
        }
        if (value == 2)
        {
            loverCount += length;
        }
    }



}
