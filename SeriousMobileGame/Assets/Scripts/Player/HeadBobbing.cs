using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour {

    [SerializeField] private float amplitude = 0.015f;
    [SerializeField] private float frequency = 10.0f;
    [SerializeField] private DeerController controller;

    private float toggleSpeed = 3.0f;
    private Vector3 startPosition;

    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private Transform target;

    float defaultPosY = 0;
    float timer = 0;

    private void Awake() {
        controller = GetComponent<DeerController>();
        startPosition = _camera.localPosition;
    }


    private void Update() {
        CheckMotion();
        ResetPosition();
        _camera.LookAt(target.position);
    }

    private void PlayMotion(Vector3 motion) {
        _camera.localPosition += motion;
    }

    private Vector3 FootStepMotion() {
        Vector3 position = Vector3.zero;
        position.y += Mathf.Sin(Time.time * frequency) * amplitude;
        position.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return position;
    }

    private void CheckMotion() {
        //float speed = new Vector3(controller.moveValue, 0, controller.moveValue).magnitude;

        //if (speed < toggleSpeed) return;
        if (!controller.isMoving) return;

        PlayMotion(FootStepMotion());
    }
    private void ResetPosition() {
        if (_camera.localPosition == startPosition) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, startPosition, 1 * Time.deltaTime);
    }
}