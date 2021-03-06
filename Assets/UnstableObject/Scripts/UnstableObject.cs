using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnstableStyle
{
    Rotation, Vibration, RotationAndVibration
}

public class UnstableObject : MonoBehaviour
{
    public  uint WaitFrame => mWaitFrame;
    public float Rotation  => mRotation;
    public float Vibration => mVibration;
    public UnstableStyle STYLE => mSTYLE;

    [SerializeField] private  uint mWaitFrame;
    [SerializeField] private float mRotation;
    [SerializeField] private float mVibration;
    [SerializeField] private UnstableStyle mSTYLE;

    public Vector2 PivotPoint;

    private IEnumerator mEUpdate;

    public UnstableObject(uint waitFrame, float rotation, float vibration, UnstableStyle style) {
        mWaitFrame = waitFrame; mVibration = vibration; mRotation = rotation; mSTYLE = style;
    }

    public void Setting(uint waitFrame, float vibration, float rotation, UnstableStyle style) {
        mWaitFrame = waitFrame; mVibration = vibration; mRotation = rotation; mSTYLE = style;
    }
    private void OnEnable()
    {
        StartCoroutine(mEUpdate = EUpdate());

        PivotPoint = transform.localPosition;
    }
    private void OnDisable()
    {
        if (mEUpdate != null) {
            StopCoroutine(mEUpdate);
        }
        mEUpdate = null;
    }
    private IEnumerator EUpdate()
    {
        while (gameObject.activeSelf)
        {
            for (uint i = 0; i < mWaitFrame; i++) { yield return null; }

            switch (mSTYLE)
            {
                case UnstableStyle.Rotation:
                    transform.localRotation = Quaternion.Euler(Vector3.forward * mRotation * Random.Range(-1f, 1f));
                    break;

                case UnstableStyle.Vibration:
                    transform.localPosition = PivotPoint + Random.insideUnitCircle * mVibration;
                    break;

                case UnstableStyle.RotationAndVibration:
                    transform.localPosition = PivotPoint + Random.insideUnitCircle * mVibration;
                    transform.localRotation = Quaternion.Euler(Vector3.forward * mRotation * Random.Range(-1f, 1f));
                    break;
                default:
                    break;
            }
            
        }
    }
}
