using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{  
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerUpIndicator, restartButton;

    private float speed = 1.0f;
    private float powerUpStrength = 20.0f;
    public bool hasPowerup = false;

    // Start is called before the first frame update
    void Start()
    {
        startMethod();
    }

    // Update is called once per frame
    void Update()
    {
        updateMethod();
    }

    public void startMethod()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    public void updateMethod()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerUpIndicator.transform.position = playerRb.transform.position + new Vector3(0, -0.5f, 0);
    }

    public void OnTriggerEnter(Collider other)                                                
    {                                          
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerUpIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }

        if (other.CompareTag("DestroyLimit"))
        {
            restartButton.SetActive(true);
            Time.timeScale = 0;
        }
    }

    IEnumerator PowerupCountdownRoutine() 
    {
        yield return new WaitForSeconds(5);
        hasPowerup = false;
        powerUpIndicator.SetActive(false);
    }

    public void OnCollisionEnter(Collision collision) 
    {                                                 
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>(); 
            Vector3 awayFromPlayer = collision.gameObject.transform.position - playerRb.transform.position;

            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            Debug.Log("Collided with: " + collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }

    public void restartButtonMethod()
    {
        SceneManager.LoadScene("Prototype 4");
        Time.timeScale = 1;
    }
}