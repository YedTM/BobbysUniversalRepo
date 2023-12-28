using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    // (Optional) Prevent non-singleton constructor use.
    [SerializeField]
    SpriteRenderer animalPrefab;

    [SerializeField]
    List<Sprite> animalImages = new List<Sprite>();

    List<SpriteRenderer> animals = new List<SpriteRenderer>();


    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private SpriteRenderer SpawnCreature()
    {
        return Instantiate(animalPrefab, transform);
    }

    private float Gaussian(float mean, float stdDev)
    {
        float val1 = Random.Range(0f, 1f);
        float val2 = Random.Range(0f, 1f);

        float gaussValue =
        Mathf.Sqrt(-2.0f * Mathf.Log(val1)) *
        Mathf.Sin(2.0f * Mathf.PI * val2);

        return mean + stdDev * gaussValue;
    }


    public void Spawn()
    {
        DestroyAnimals();

        // For the exercise, create a random int for the count
        for (int i = 0; i < Random.Range(25, 75); i++)
        {
            animals.Add(SpawnCreature());
            //Percent rates are not that of the exercise
            float randValue = Random.value;
            if (randValue < 0.3f) //Kangeroo
            {
                animals[i].sprite = animalImages[0];
            }
            else if (randValue < 0.55f) //Elephant
            {
                animals[i].sprite = animalImages[1];
            }
            else if (randValue < 0.75f) //Turtle
            {
                animals[i].sprite = animalImages[4];
            } 
            else if (randValue < 0.9f) //Snail
            {
                animals[i].sprite = animalImages[3];
            }
            else //Octopus
            {
                animals[i].sprite = animalImages[2];
            }

            //Randomize Positions
            //Use Guassian Random for exercise
            Vector2 spawnPosition = new Vector2(Gaussian(1, 2), Gaussian(1, 1));
            animals[i].transform.position = spawnPosition;

            animals[i].color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        }

        
    }

    private void DestroyAnimals()
    {
        foreach (SpriteRenderer animal in animals) 
        { 
            Destroy(animal.gameObject);
        }
        animals.Clear();
    }
}
