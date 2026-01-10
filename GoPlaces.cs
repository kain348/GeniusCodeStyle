using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPlaces : MonoBehaviour
{
    [Serialized] private float _speed;

    public Transform AllPlacesPoint;
    public Transform[] arrayPlaces;
    public int IndexPlaces;

    private void Start()
    {
        if (AllPlacespoint == null)
            throw new NullReferenceException(nameof(AllPlacesPoint));

        arrayPlaces = new Transform[AllPlacesPoint.childCount];

        for (int index = 0; index < AllPlacesPoint.childCount; index++)
        {
            arrayPlaces[index] = AllPlacesPoint.GetChild(index);
        }
    }

    private void Update()
    {
        if (IndexPlaces == null)
            throw new NullReferenceException(nameof(IndexPlaces));

        var pointByNumberInArray = arrayPlaces[IndexPlaces];
        transform.position = Vector3.MoveTowards(transform.position, pointByNumberInArray.position, _speed * Time.deltaTime);

        if (transform.position == pointByNumberInArray.position) NextPlaceTakerLogic();
    }

    private Vector3 NextPlaceTakerLogic()
    {
        IndexPlaces++;

        if (IndexPlaces == arrayPlaces.Length)
            IndexPlaces = 0;

        var thisPointVector = arrayPlaces[IndexPlaces];
        transform.forward = thisPointVector - transform.position;
        return thisPointVector;
    }
}