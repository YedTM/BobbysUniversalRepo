using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour
{
    [SerializeField]
    TagPlayer tagPrefab;

    List<Agent> agents;

    [SerializeField]
    uint numPlayers;

    [SerializeField]
    float countTimer;

    public Sprite[] tagSprites;

    public Agent itPlayer;

    public List<Agent> Agents
    {
        get
        {
            return agents;
        }
    }

    public float CountTimer
    {
        get
        {
            return countTimer;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agents = new List<Agent>();
        for (uint i = 0; i < numPlayers; i++)
        {
            SpawnPlayer();
        }
        if (agents.Count > 0) 
        {
            ((TagPlayer)agents[0]).SetState(TagStates.Counting);
            itPlayer = agents[0];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnPlayer()
    {
        TagPlayer player = Instantiate(tagPrefab, transform.parent);
        player.TagManager = this;
        agents.Add(player);
    }
}
