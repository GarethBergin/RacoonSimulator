using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public ItemManager im;
    public Transform Gun;
    Vector2 direction;
    public GameObject bullet;
    public float BulletSpeed;
    public Transform ShootPoint;
    public float fireRate;
    float ReadyForNextShot;
    public Text fireflyText;

    public float spotlightRadiusIncrease = 1f;
    public GameObject player;
    //public GameObject weaponToGive;


    public Animator animator;

    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)Gun.position;
        FaceMouse();
        
        if(Input.GetMouseButtonDown(1) && im.flyCount > 0)
        {

            //animator.SetTrigger("IsShooting");
            if(Time.time > ReadyForNextShot)
            {
                ReadyForNextShot = Time.time + 1/fireRate;
                shoot();
            }
            
        }
            
        
       
    }

    void FaceMouse()
    {
        Gun.transform.right = direction;

       

    }

    void shoot()
    {
        GameObject BulletIns = Instantiate(bullet, ShootPoint.position, ShootPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(ShootPoint.up * BulletSpeed, ForceMode2D.Impulse);
        //BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * BulletSpeed);

        Destroy(BulletIns, 3);
        DecreasePlayerSpotlightRadius();
    }

    void DecreasePlayerSpotlightRadius()
    {
        SpotlightPulse playerSpotlight = player.GetComponentInChildren<SpotlightPulse>();
        if (playerSpotlight != null)
        {
            // Increase min and max outer radius
            playerSpotlight.minOuterRadius -= spotlightRadiusIncrease;
            playerSpotlight.maxOuterRadius -= spotlightRadiusIncrease;

            Debug.Log("Min Outer Radius: " + playerSpotlight.minOuterRadius);
            Debug.Log("Max Outer Radius: " + playerSpotlight.maxOuterRadius);

            im.flyCount--;
            fireflyText.text = im.flyCount.ToString();

            


        }
        else
        {
            Debug.LogWarning("Player's spotlight not found!");
        }
    }


}