using UnityEngine;
using System.Collections;

public class CityController : MonoBehaviour
{
    public GameObject CitySprite;

    public GameObject BrokeCitySprite;
    public GameObject BurningCityParticleSystem;

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        RazeCity();
    }

    private void RazeCity()
    {
        CitySprite.SetActive(false);
        GetComponent<PolygonCollider2D>().enabled = false;

        BrokeCitySprite.SetActive(true);
        BurningCityParticleSystem.SetActive(true);
    }
}
