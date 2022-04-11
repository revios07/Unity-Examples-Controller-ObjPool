using System.Collections.Generic;
using UnityEngine;

public class BlockInitaliazer : MonoBehaviour
{
    private Vector3 spawnPosPooling = Vector3.zero;

    [Tooltip("Takes Last Blocks Position For Front Blocks")]
    protected Vector3 lastBlockPos = Vector3.zero;

    protected GameObject[] blockPrefabs;

    protected GameObject pointObject;
    [SerializeField]
    protected int blockCount;

    //1-> 2 -> 3 ->
    protected List<GameObject> blockPool = new List<GameObject>();

    [Header("Hardness For Blocks")]
    private float difficulty = 1;

    private int createBlockIndex;
    

    public void Initialize(int bCount, GameObject[] bPrefabs, GameObject pPrefab,int diffucultyScale)
    {
        difficulty = diffucultyScale;
        blockCount = bCount;
        blockPrefabs = bPrefabs;
        pointObject = pPrefab;
        InstantiateBlocks();
    }

    protected void InstantiateBlocks()
    {
        //10 Per Block Colour
        //Random Y Pos Down Side Block
        BlockCreate(blockPool, blockPrefabs, blockCount / 3);
        BlockPosSet(ref lastBlockPos, blockCount);
    }

    //First Instantiate Blocks
    protected void BlockCreate(List<GameObject> blocks, GameObject[] blockWithColor, int howMuchInstantiate)
    {
        for (int i = 0; i < howMuchInstantiate; ++i)
        {
            for (int j = 0; j < blockWithColor.Length; ++j)
            {
                blocks.Add(Instantiate(blockWithColor[j], spawnPosPooling, Quaternion.identity));
                Instantiate(blockWithColor[j], spawnPosPooling + new Vector3(0, Random.Range(-25f +difficulty, -28f +difficulty), 0f), Quaternion.identity, blocks[createBlockIndex].transform).transform.localScale = Vector3.one;

                ++createBlockIndex;
            }
        }

        pointObject = Instantiate(pointObject, new Vector3(0,500,0), Quaternion.identity);
    }

    
    protected void BlockPosSet(ref Vector3 pos, int blockCount)
    {
        blockCount -= 2;

        for (int i = 0; i < blockCount; i += 3)
        {
            blockPool[i].transform.position = lastBlockPos;
            lastBlockPos.y += Random.Range(1.0f, -1.0f);
            lastBlockPos.z += 1f;

            blockPool[i + 1].transform.position = lastBlockPos;
            lastBlockPos.y += Random.Range(1.0f, -1.0f);
            lastBlockPos.z += 1f;

            blockPool[i + 2].transform.position = lastBlockPos;
            lastBlockPos.y += Random.Range(1.0f, -1.0f);
            lastBlockPos.z += 1f;
        }
    }
}
