using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float acceleratePerSecond = 5f;
    public float deceleratePerSecond = 10f;
    public float maxMoveSpeed = 5f;

    protected Vector2 currVelocity;

    protected Rigidbody2D playerRigBody;
    //Get rigidbody component
    void OnEnable()
    {
        playerRigBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        updateVelocity();
        Vector2 newPosition = currVelocity * Time.deltaTime;
        playerRigBody.position = playerRigBody.position + newPosition;

    }

    void updateVelocity()
    {

        //Try to accelerate upwards if button is pressed
        if (Input.GetKey(KeyCode.W))
        {
            //Only if speed is below maximum
            if (currVelocity.y <= maxMoveSpeed)
            {
                //Make sure speed does not exceed maximum
                if ((currVelocity.y + (acceleratePerSecond * Time.deltaTime)) > maxMoveSpeed)
                {
                    currVelocity.y = maxMoveSpeed;

                } else
                {
                    currVelocity.y += acceleratePerSecond * Time.deltaTime;
                }
                

            //Decelerate if speed is over maximum
            } else
            {
                currVelocity.y -= deceleratePerSecond * Time.deltaTime;
            }
            
        //Decelerate if button is not being pressed
        } 
        else if (currVelocity.y > 0)
        {
            //Do not decelerate past 0
            if (currVelocity.y - (deceleratePerSecond * Time.deltaTime) < 0)
            {
                currVelocity.y -= currVelocity.y;
            } else
            {
                currVelocity.y -= deceleratePerSecond * Time.deltaTime;
            }
            
        }

        //Try to accelerate downwards if button is pressed
        if (Input.GetKey(KeyCode.S))
        {
            //Only if speed is below maximum
            if (currVelocity.y >= maxMoveSpeed * -1)
            {
                //Make sure speed does not exceed maximum
                if ((currVelocity.y - (acceleratePerSecond * Time.deltaTime)) < maxMoveSpeed*-1)
                {
                    currVelocity.y = maxMoveSpeed*-1;

                }
                else
                {
                    currVelocity.y -= acceleratePerSecond * Time.deltaTime;
                }


                //Decelerate if speed is over maximum
            }
            else
            {
                currVelocity.y += deceleratePerSecond * Time.deltaTime;
            }

         //Decelerate if button is not being pressed
        } 
        else if (currVelocity.y < 0)
        {
            //Do not decelerate past 0
            if (currVelocity.y + (deceleratePerSecond * Time.deltaTime) > 0)
            {
                currVelocity.y -= currVelocity.y;
            }
            else
            {
                currVelocity.y += deceleratePerSecond * Time.deltaTime;
            }

        }

        //Try to accelerate rightwards if button is pressed
        if (Input.GetKey(KeyCode.D))
        {
            //Only if speed is below maximum
            if (currVelocity.x <= maxMoveSpeed)
            {
                //Make sure speed does not exceed maximum
                if ((currVelocity.x + (acceleratePerSecond * Time.deltaTime)) > maxMoveSpeed)
                {
                    currVelocity.x = maxMoveSpeed;

                }
                else
                {
                    currVelocity.x += acceleratePerSecond * Time.deltaTime;
                }


                //Decelerate if speed is over maximum
            }
            else
            {
                currVelocity.x -= deceleratePerSecond * Time.deltaTime;
            }

            //Decelerate if button is not being pressed
        }
        else if (currVelocity.x > 0)
        {
            //Do not decelerate past 0
            if (currVelocity.x - (deceleratePerSecond * Time.deltaTime) < 0)
            {
                currVelocity.x -= currVelocity.x;
            }
            else
            {
                currVelocity.x -= deceleratePerSecond * Time.deltaTime;
            }

        }

        //Try to accelerate leftwards if button is pressed
        if (Input.GetKey(KeyCode.A))
        {
            //Only if speed is below maximum
            if (currVelocity.x * -1 <= maxMoveSpeed)
            {
                //Make sure speed does not exceed maximum
                if ((currVelocity.x - (acceleratePerSecond * Time.deltaTime)) < maxMoveSpeed*-1)
                {
                    currVelocity.x = maxMoveSpeed * -1;

                }
                else
                {
                    currVelocity.x -= acceleratePerSecond * Time.deltaTime;
                }


                //Decelerate if speed is over maximum
            }
            else
            {
                currVelocity.x += deceleratePerSecond * Time.deltaTime;
            }

            //Decelerate if button is not being pressed
        }
        else if (currVelocity.x < 0)
        {
            //Do not decelerate past 0
            if (currVelocity.x + (deceleratePerSecond * Time.deltaTime) > 0)
            {
                currVelocity.x -= currVelocity.x;
            }
            else
            {
                currVelocity.x += deceleratePerSecond * Time.deltaTime;
            }

        }
    }
}


