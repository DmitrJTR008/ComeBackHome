using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class GPSTracker : MonoBehaviour
{
    public Transform FinishPosition;
    private Camera cam;

    [SerializeField] private Transform _worldPointer;
    [SerializeField] private Transform _pointerIco;
    [SerializeField] private Image _sprite;
    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 toFinish = FinishPosition.position - transform.position;
        Ray ray = new Ray(transform.position, toFinish);
        Debug.DrawRay(transform.position,toFinish, Color.green);

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < 4; i++)
        {
            if (planes[i].Raycast(ray, out float distance))
            {
                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }
        }

        minDistance = Mathf.Clamp(minDistance, 0, toFinish.magnitude);
        Vector3 worldPosition = ray.GetPoint(minDistance - 1);
        _pointerIco.position = Vector3.Lerp(_pointerIco.position, cam.WorldToScreenPoint(worldPosition),4 * Time.deltaTime);
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, (minDistance - 3) / 1);
    }

}
