using UnityEngine;

public class ItemLocationHandler : MonoBehaviour
{
    public enum Location
    {
        University,
        Bar,
        Zone,
        UFO
    }

    [Header("Текущая локация")]
    public Location currentLocation;

    [Header("Позиции")]
    public Vector3 universityPosition;
    public Vector3 barPosition;
    public Vector3 zonePosition;
    public Vector3 ufoPosition;

    public Vector3 GetPositionForCurrentLocation()
    {
        switch (currentLocation)
        {
            case Location.University:
                return universityPosition;

            case Location.Bar:
                return barPosition;

            case Location.Zone:
                return zonePosition;

            case Location.UFO:
                return ufoPosition;
        }

        return transform.position;
    }
}