using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [HideInInspector]
    public Bounds areaBounds;

    public GameObject ground;
    public GameObject area;

    public List<PlayerInfo> AgentsList = new List<PlayerInfo>();
    public List<BlockInfo> BlocksList = new List<BlockInfo>();

    [Header("Target")]
    public bool UseRandomTargetPosition = true;
    [Header("Agent")]
    public bool UseRandomAgentRotation = true;
    public bool UseRandomAgentPosition = true;
    [Header("Block")]
    public bool UseRandomBlockRotation = true;
    public bool UseRandomBlockPosition = true;

    private PushBlockSettings m_PushBlockSettings;
    private int m_NumberOfRemainingBlocks;
    private SimpleMultiAgentGroup m_AgentGroup;

    public ParticleSystem confettiEffect;
    // Start is called before the first frame update
    void Start()
    {
        m_NumberOfRemainingBlocks = BlocksList.Count;

        areaBounds = ground.GetComponent<Collider>().bounds;
        m_PushBlockSettings = FindObjectOfType<PushBlockSettings>();

        foreach (var item in BlocksList)
        {
            item.StartingPos = item.T.transform.position;
            item.StartingRot = item.T.transform.rotation;
            item.Rb = item.T.GetComponent<Rigidbody>();
        }

        m_AgentGroup = new SimpleMultiAgentGroup();
        foreach (var item in AgentsList)
        {
            item.StartingPos = item.Agent.transform.position;
            item.StartingRot = item.Agent.transform.rotation;
            item.Rb = item.Agent.GetComponent<Rigidbody>();
            m_AgentGroup.RegisterAgent(item.Agent);
        }

        ResetScene();
    }

    public Vector3 GetRandomSpawnPos()
    {
        var foundNewSpawnLocation = false;
        var randomSpawnPos = Vector3.zero;
        while (foundNewSpawnLocation == false)
        {
            var randomPosX = Random.Range(-areaBounds.extents.x * m_PushBlockSettings.spawnAreaMarginMultiplier,
                areaBounds.extents.x * m_PushBlockSettings.spawnAreaMarginMultiplier);

            var randomPosZ = Random.Range(-areaBounds.extents.z * m_PushBlockSettings.spawnAreaMarginMultiplier,
                areaBounds.extents.z * m_PushBlockSettings.spawnAreaMarginMultiplier);
            randomSpawnPos = ground.transform.position + new Vector3(randomPosX, 1f, randomPosZ);
            if (Physics.CheckBox(randomSpawnPos, new Vector3(1.5f, 0.01f, 1.5f)) == false)
            {
                foundNewSpawnLocation = true;
            }
        }
        return randomSpawnPos;
    }

    public void ScoredAGoal(Collider col, float score)
    {
        print($"Scored {score} on {gameObject.name}");

        //Decrement the counter
        m_NumberOfRemainingBlocks--;

        //Are we done?
        bool done = m_NumberOfRemainingBlocks == 0;

        //Disable the block
        //col.gameObject.SetActive(false);

        //Give Agent Rewards
        m_AgentGroup.AddGroupReward(score);

        if (done)
        {
            //Reset assets
            m_AgentGroup.EndGroupEpisode();


            //TODO: Confetti effect
            confettiEffect.Play();


            //Invoke("ResetScene", 5.0f);
            //ResetScene();
        }
    }

    Quaternion GetRandomRot()
    {
        return Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
    }

    public void ResetScene()
    {
        //Random platform rotation
        var rotation = Random.Range(0, 4);
        var rotationAngle = rotation * 90f;

        if (UseRandomTargetPosition) area.transform.Rotate(new Vector3(0f, rotationAngle, 0f));

        //Reset Agents
        foreach (var item in AgentsList)
        {
            var pos = UseRandomAgentPosition ? GetRandomSpawnPos() : item.StartingPos;
            var rot = UseRandomAgentRotation ? GetRandomRot() : item.StartingRot;

            item.Agent.transform.SetPositionAndRotation(pos, rot);
            item.Rb.velocity = Vector3.zero;
            item.Rb.angularVelocity = Vector3.zero;
        }

        //Reset Blocks
        foreach (var item in BlocksList)
        {
            var pos = UseRandomBlockPosition ? GetRandomSpawnPos() : item.StartingPos;
            var rot = UseRandomBlockRotation ? GetRandomRot() : item.StartingRot;

            item.T.transform.SetPositionAndRotation(pos, rot);
            item.Rb.velocity = Vector3.zero;
            item.Rb.angularVelocity = Vector3.zero;
            item.T.gameObject.SetActive(true);
        }

        //Reset counter
        m_NumberOfRemainingBlocks = BlocksList.Count;
    }
    


    
    // Info classes to store starting positions and rigidbodies
    [System.Serializable]
    public class PlayerInfo
    {
        public PushAgentCollab Agent;
        [HideInInspector]
        public Vector3 StartingPos;
        [HideInInspector]
        public Quaternion StartingRot;
        [HideInInspector]
        public Rigidbody Rb;
    }

    [System.Serializable]
    public class BlockInfo
    {
        public Transform T;
        [HideInInspector]
        public Vector3 StartingPos;
        [HideInInspector]
        public Quaternion StartingRot;
        [HideInInspector]
        public Rigidbody Rb;
    }
}
