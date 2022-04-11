using UnityEngine;

public class BlockCreator : BlockInitaliazer
{
    private static BlockCreator instance;

    //Takes Blocks at Behind
    private int unbusyBlockIndex = 0;

    public static BlockCreator GetSingleton()
    {
        if (instance == null)
        {
            instance = new GameObject("_BlockCreator").AddComponent<BlockCreator>();
        }

        return instance;
    }

    //Forward Block
    public Transform GetRelativeBlock(int playerPosZ)
    {
        return blockPool[playerPosZ].transform;
    }

    //Object Pooling Method
    public void UpdateBlockPosition()
    {
        for (int i = 0; i < 10; ++i)
        {
            //Positions Update
            lastBlockPos.y += Random.Range(-1.2f, 1.2f);

            //Behinds Blocks Goes Front
            blockPool[unbusyBlockIndex].transform.position = lastBlockPos;

            ++unbusyBlockIndex;

            if (unbusyBlockIndex > blockPool.Count - 1)
            {
                PointCreator(lastBlockPos);
                unbusyBlockIndex = 0;
            }

            lastBlockPos.z += 1;
        }
    }

    private void PointCreator(Vector3 pos)
    {
        pos.y -= Random.Range(9.5f, 10.5f);
        pointObject.transform.position = pos;

        pointObject.SetActive(true);
    }
}
