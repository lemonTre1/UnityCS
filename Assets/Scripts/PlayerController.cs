using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerController : NetworkBehaviour {


    enum PlayerStatus {
        Idle,
        Run,
        Jump_01,
        Jump_02,
       
    }


	enum AttackStatus{
		NoAction,
		Shoot,
		ThrowBomb,
		R
	}

    public GameObject BulletPrefab;
    public GameObject BombPrefab;
    public GameObject RPrefab;
    public GameObject GameUIPrefab;



    public const float cameraHeight = 3f;
    public const float camreaDistance = 10f;


    public const float MoveSpeed = 10f;
    public const float RotateSpeed = 120f;


    public const float ShootTime = 0.1f;
    public const float BoomTime = 1f;
    public const float RTime = 5f;


    bool Shoot = true;
    bool Boom = true;
    bool R = true;

    SkillsNumber SkillsScript;
    GameObject mainCamera;
    Transform playerGun;
    Transform playerBomb;
    Transform playerR;
    PlayerStatus playerStatus;
	AttackStatus attackStatus;

    public override void OnStartLocalPlayer(){
        ShowGameUI();
        GetComponent<MeshRenderer>().material.color = Color.black;
    }


    void Start() {
  
        Define();
        playerStatus = PlayerStatus.Idle;
		attackStatus=AttackStatus.NoAction;
    }


	void Update () {

        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.J)) {
            Fire();
        }
           
        if (Input.GetKeyDown(KeyCode.K))
            ThrowBomb();

        if (Input.GetKeyDown(KeyCode.L))
            BigR();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
         

        //Player移动控制
        CameraFollow(); 

        //Player视角控制
        PlayerMover();
	}




    void OnCollisionEnter(Collision collider)
    {
        if (collider.transform.tag.Equals("Terrain"))
            playerStatus = PlayerStatus.Idle;
    }



    /// <summary>
    /// 初始化
    /// </summary>
    void Define() {
        playerGun = transform.FindChild("Gun");
        playerBomb = transform.FindChild("Bomb");
        playerR = transform.FindChild("R");

        mainCamera = GameObject.Find("Main Camera");
    }


    void ShowGameUI() {
        if (!GameUIPrefab)
            return;

        GameObject[] gameUIs = GameObject.FindGameObjectsWithTag("GameUI");

        if (gameUIs.Length > 0)
            for (int i = 0; i < gameUIs.Length; i++)
                gameUIs[i].SetActive(false);

        GameObject gameUI = Instantiate(GameUIPrefab,Vector3.zero,Quaternion.identity) as GameObject;
        SkillsScript = gameUI.GetComponent<SkillsNumber>();
    }

    /// <summary>
    /// 设置动作前摇  
    /// </summary>
    /// <param name="What">哪个动作</param>
    /// <param name="When">多长时间</param>
    /// <returns></returns>
	IEnumerator ActionTime(AttackStatus What, float When)
    {
        switch (What) {
            case AttackStatus.Shoot: Shoot = false; yield return new WaitForSeconds(When); Shoot = true; break;
			case AttackStatus.ThrowBomb: Boom = false; yield return new WaitForSeconds(When); Boom = true; break;
			case AttackStatus.R: R = false; yield return new WaitForSeconds(When); R = true; break;
        }
    }



#region  player控制

    /// <summary>
    /// J
    /// </summary>
    void Fire() {
        if (!BulletPrefab)
            return;

        if (!Shoot)
            return;

		attackStatus=AttackStatus.Shoot;
		StartCoroutine(ActionTime(attackStatus, ShootTime));
        CmdFire();
        SkillsScript.ShootChange();
    }

    /// <summary>
    /// K
    /// </summary>
    void ThrowBomb()
    {

        if (!BombPrefab)
            return;

        if (!Boom)
            return;

		attackStatus=AttackStatus.ThrowBomb;
		StartCoroutine(ActionTime(attackStatus, BoomTime));
        CmdThrowBomb();
        SkillsScript.BombChange();
    }


    /// <summary>
    /// L
    /// </summary>
    void BigR()
    {
        if (!RPrefab)
            return;

        if (!R)
            return;

		attackStatus=AttackStatus.R;
		StartCoroutine(ActionTime(attackStatus, RTime));

        CmdBigR();
        SkillsScript.RChange();
    }



    [Command]
    void CmdBigR() { 
        GameObject r = Instantiate(RPrefab, playerR.position, playerR.transform.rotation) as GameObject;
        NetworkServer.Spawn(r);
    }

    [Command]
    void CmdThrowBomb() {
        GameObject bomb = Instantiate(BombPrefab, playerBomb.position, playerBomb.transform.rotation) as GameObject;
        NetworkServer.Spawn(bomb);
    }


    [Command]
    void CmdFire() {
        GameObject bullet=Instantiate(BulletPrefab, playerGun.position, playerGun.transform.rotation) as GameObject;
        NetworkServer.Spawn(bullet);
    }


    void Jump() {
        bool isJump = true;
        switch (playerStatus)
        {
            case PlayerStatus.Jump_02: isJump = false; break;
            case PlayerStatus.Jump_01: playerStatus = PlayerStatus.Jump_02; break;
            default: playerStatus = PlayerStatus.Jump_01; break;
        }

        if(isJump)
            GetComponent<Rigidbody>().velocity = Vector3.up * 8f;
    }


    void PlayerMover() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");



        transform.Rotate(new Vector3(0, x * RotateSpeed * Time.deltaTime, 0));
        transform.Translate(Vector3.forward * MoveSpeed * y * Time.deltaTime);
    }

    void CameraFollow() {
        if (!mainCamera)
            return;

        mainCamera.transform.position = transform.position;
        mainCamera.transform.position -= transform.forward * camreaDistance;
        mainCamera.transform.position += transform.up * cameraHeight;

        mainCamera.transform.LookAt(transform.position);
    }

#endregion
}
