using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_control : MonoBehaviour
{   
    private float horizontal_rotate_speed = 0.5f;
    private float character_velocity = 0f;
    private float accelerate_rate = 0.01f;
    private float slow_rate = 1f;
    private float cur_euler_y;
    private GameObject joystick;
    private RectTransform rect_transform;
    // Start is called before the first frame update
    void Start()
    {
        cur_euler_y = transform.rotation.eulerAngles.y;
        joystick = GameObject.Find("/Canvas/Fixed Joystick/Handle");
        rect_transform = joystick.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.A))
        //{
        //    turn_left();
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    turn_right();
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    accelerate();
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    slow();
        //}
        change_rotation();
        change_velocity();
        move_forward();
    }

    private void change_rotation()
    {
        cur_euler_y += horizontal_rotate_speed * rect_transform.localPosition.x * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, cur_euler_y, 0);
    }

    private void change_velocity()
    {
        character_velocity += accelerate_rate * rect_transform.localPosition.y * Time.deltaTime;
    }

    private void turn_left()
    {   
        cur_euler_y -= horizontal_rotate_speed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, cur_euler_y, 0);
    }

    private void turn_right()
    {
        cur_euler_y += horizontal_rotate_speed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, cur_euler_y, 0);
    }

    private void accelerate()
    {
        character_velocity += accelerate_rate * Time.deltaTime;
    }

    private void slow()
    {
        character_velocity -= slow_rate * Time.deltaTime;
        if (character_velocity < 0f) {character_velocity = 0f;}
    }

    private void move_forward()
    {
        transform.position += transform.forward * character_velocity * Time.deltaTime;
    }
}

