using UnityEngine;

public class FuelBarrel : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private int _fuel;

    private void NewBarrel()
    {
        Camera camera = Camera.main;
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0f,camera.pixelWidth ),
            (Random.Range(0f, camera.pixelHeight))));
        transform.position = (new Vector3(newPosition.x, newPosition.y, transform.position.z));
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Fuel>() != null)
        {
            col.GetComponent<Fuel>().AddFuel(_fuel);
            NewBarrel();
        }
    }
}
