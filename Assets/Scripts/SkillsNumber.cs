using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillsNumber : MonoBehaviour {


    public const int MaxShootNumber = 20;
    public const int MaxBombNumber = 6;
    public const int MaxRNumber = 3;

    int currentShoot;
    int currentBomb;
    int currentR;
    public Text shootText;
    public Text boomText;
    public Text rText;

	// Use this for initialization
	void Start () {
        currentShoot = MaxShootNumber;
        currentBomb = MaxBombNumber;
        currentR = MaxRNumber;
        ShowNumber();
	}

  

    public void ShootChange() {
        currentShoot--;
        if (currentShoot == 0)
            currentShoot = MaxShootNumber;

        ShowNumber();
    }


    public void BombChange()
    {
        currentBomb--;
        if (currentBomb == 0)
            currentBomb = MaxBombNumber;

        ShowNumber();
    }


    public void RChange()
    {
        currentR--;
        if (currentR == 0)
            currentR = MaxRNumber;

        ShowNumber();
    }


  
    void ShowNumber() {

        if (shootText)
            shootText.text = currentShoot.ToString();

        if (boomText)
            boomText.text = currentBomb.ToString();

        if (rText)
            rText.text = currentR.ToString();
    }
}
