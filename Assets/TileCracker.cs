using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCracker : MonoBehaviour
{
    public SpriteRenderer tileSprite;
    public Sprite[] cracks;

    int tileCrackIndex = 0;

    public void NextCrack()
    {
        if(tileCrackIndex < cracks.Length - 1)
            tileCrackIndex++;

        tileSprite.sprite = cracks[tileCrackIndex];
    }
}
