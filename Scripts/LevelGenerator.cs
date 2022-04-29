using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator sharedInstance;
    public List <LevelBlock> allTheLevelBlocks = new List<LevelBlock>();//niveles creados
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();//niveles displayeados
    public Transform levelInitialPoint;//punto donde incia a crearel primer nivel

    private bool isGeneratingInitialBlocks = false;

    void Awake()
    {
        sharedInstance = this;
        GenerateInitialBlocks();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GenerateInitialBlocks()
    {
        isGeneratingInitialBlocks = true;
        for (int i = 0; i < 2; i++)
        {
            addNewBlock();
        }
        isGeneratingInitialBlocks = false;
    }

    public void addNewBlock()
    {
        //numeros random
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);
        if(isGeneratingInitialBlocks){
            randomIndex = 0;
        }
        LevelBlock block = (LevelBlock)Instantiate(allTheLevelBlocks [randomIndex]);
        block.transform.SetParent(this.transform, false);

        Vector3 blockPosition = Vector3.zero;

        if (currentLevelBlocks.Count == 0)
        {
            blockPosition = levelInitialPoint.position;
        }else 
        {
            blockPosition = currentLevelBlocks[currentLevelBlocks.Count -1].exitPoint.position;

        }

        block.transform.position = blockPosition;
        currentLevelBlocks.Add(block);

    }

    public void RemoveOldBlock()
    {
        LevelBlock block = currentLevelBlocks[0];
        currentLevelBlocks.Remove(block);
        Destroy(block.gameObject);
    }

    public void RemoveAllTheBlocks(){
        while(currentLevelBlocks.Count > 0){
            RemoveOldBlock();
        }
    }


}
