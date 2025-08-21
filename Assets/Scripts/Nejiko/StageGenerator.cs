using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    //ターゲットキャラクターの保持用変数
    public Transform character;

    //ステージのPrefabを配置する変数
    public GameObject[] stageChip;

    //Sceneに実体化させたステージのPrefabを管理する配列
    public List<GameObject> generateStageList = new List<GameObject>();
    public int preInstance = 5;
    public int currentChipIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //キャラクターの現在位置から現在のステージチップのインデックスを計算
        int characterPositionIndex = (int)(character.position.z / 30f);
        Debug.Log(characterPositionIndex);

        //キャラクターが進んだらステージチップを追加で生成する
        for (int i = preInstance + characterPositionIndex; i >= preInstance; i++)
        {
            //最初に作ったステージ数＋自分の通過したステージ数だけステージを生成する
            if(generateStageList.Count > preInstance + characterPositionIndex)
            {
                return;
            }
            //乱数を生成する
            int randomValue = Random.Range(0, stageChip.Length);

            //ここで作るステージをランダムに変更する
            GameObject stageObject = Instantiate(stageChip[randomValue]);
            stageObject.transform.position = new Vector3(0, 0, currentChipIndex * 30f);
            //生成したステージチップを管理リストに追加
            generateStageList.Add(stageObject);
            currentChipIndex++;
        } 
    }
}
