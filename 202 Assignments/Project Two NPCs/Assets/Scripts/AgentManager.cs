using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AgentManager : MonoBehaviour
{
    [SerializeField]
    Human humanPrefab;

    [SerializeField]
    Zombie zombiePrefab;

    List<Human> humans;

    List<Zombie> zombies;

    [SerializeField]
    uint numHumans;

    [SerializeField]
    uint numZombies;

    public List<Obstacle> obstacles;

    Obstacle distraction;

    [SerializeField]
    Obstacle distractionPrefab;

    public List<Human> Humans
    {
        get
        {
            return humans;
        }
    }

    public List<Zombie> Zombies
    {
        get
        {
            return zombies;
        }
    }

    public Obstacle Distraction
    {
        get
        {
            return distraction;
        }
        set
        {
            distraction = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //The agent manager spawns in the correct amount of human agents
        humans = new List<Human>();
        for (uint i = 0; i < numHumans; i++)
        {
            SpawnWanderer();
        }

        //Then the manager spawns the correct amount of zombie agents
        zombies = new List<Zombie>();
        for (uint i = 0; i < numZombies; i++)
        {
            SpawnZombie();
        }
    }

    /// <summary>
    /// This method creates a human using the human prefab and its transform and then
    /// adds it to the humans list so that they can be read by other agents
    /// </summary>
    private void SpawnWanderer()
    {
        Human human = Instantiate(humanPrefab, transform.parent);
        human.AgentManager = this;
        humans.Add(human);
    }

    /// <summary>
    /// This method creates a zombie using the zombie prefab and iits transform and then 
    /// adds it to the zombies list so that they can be read by other agents
    /// </summary>
    private void SpawnZombie()
    {
        Zombie zombie = Instantiate(zombiePrefab, transform.parent);
        zombie.AgentManager = this;
        zombies.Add(zombie);
    }

    /// <summary>
    /// This method places a distraction object into the scene using the mouse's 
    /// position when the mouse is clicked
    /// </summary>
    /// <param name="context">This determines if the mouse is clicked</param>
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Vector3 placement = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            placement.z = 0;
            if (distraction != null)
            {
                Destroy(distraction.gameObject);
            }
            distraction = Instantiate(distractionPrefab, placement, Quaternion.identity);
        }
    }
}
