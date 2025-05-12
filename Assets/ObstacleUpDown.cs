using UnityEngine;

public class ObstacleUpDown : MonoBehaviour
{
    public float MoveDistance;
    public Transform MyTransform;
    public float MoveSpeed;
    private int _random;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _random = Random.Range(0, 80);
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Sin(Time.time*MoveSpeed + _random ) * MoveDistance;
        Vector3 pos = MyTransform.position;
        pos.y = y;
        MyTransform.position = pos;
    }
}
