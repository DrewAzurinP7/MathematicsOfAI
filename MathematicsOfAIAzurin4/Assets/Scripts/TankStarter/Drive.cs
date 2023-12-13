using UnityEngine;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour 
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public GameObject fuel;

    void Start() 
    {

    }

    void CalculateAngle() 
    {

        Vector3 tankForwardVector = this.transform.up;
        Vector3 fuelVector = fuel.transform.position - this.transform.position;

        float dot = tankForwardVector.x * fuelVector.x + tankForwardVector.y * fuelVector.y;
        float angle = Mathf.Acos(dot / (tankForwardVector.magnitude * fuelVector.magnitude));

        Debug.Log("Angle: " + angle * Mathf.Rad2Deg);
        Debug.Log("Unity Angle: " + Vector3.Angle(tankForwardVector, fuelVector));

        Debug.DrawRay(this.transform.position, tankForwardVector * 10.0f, Color.green, 2.0f);
        Debug.DrawRay(this.transform.position, fuelVector, Color.red, 2.0f);

        int clockwise = 1;
        if (Cross(tankForwardVector, fuelVector).z < 0)
            clockwise = -1;

        float unityAngle = Vector3.SignedAngle(tankForwardVector, fuelVector, this.transform.forward);

        this.transform.Rotate(0, 0, unityAngle);
    }

     Vector3 Cross(Vector3 v, Vector3 w)
    {
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float zMult = v.x * w.y - v.y * w.x;

        Vector3 crossProd = new Vector3 (xMult, yMult, zMult);
        return crossProd;
    }

    void CalculateDistance() 
    {

        Vector3 tankPosition = this.transform.position;
        Vector3 fuelPosition = fuel.transform.position;

        float distance = Mathf.Sqrt(Mathf.Pow(tankPosition.x - fuelPosition.x, 2.0f) + Mathf.Pow(tankPosition.y - fuelPosition.y, 2.0f) + Mathf.Pow(tankPosition.z - fuelPosition.z, 2.0f));
        float unityDistance = Vector3.Distance(tankPosition, fuelPosition);

        Debug.Log("Distance: " + distance);
        Debug.Log("Unity Distance: " + unityDistance);
    }

    void Update() 
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);

        if (Input.GetKeyDown(KeyCode.Space)) 
        {

            CalculateDistance();
            CalculateAngle();
        }

    }
}