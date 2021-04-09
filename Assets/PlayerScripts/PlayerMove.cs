﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float acceleratePerSecond = 5f;
    public float deceleratePerSecond = 10f;
    public float maxMoveSpeed = 5f;

    protected Vector2 currVelocity;

    //Collision
    protected Rigidbody2D playerRigBody;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    //Get rigidbody component
    void OnEnable()
    {
        playerRigBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        contactFilter.useTriggers = false;
        //get filter of what layers can collide (get what layers to check collision against)
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;

    }

    // Update is called once per frame
    void Update()
    {
        updateVelocity();
        Move();

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


    //Check for collision and then move accordingly.
    void Move()
    {
        //Vector for the frame
        Vector2 newPosition = currVelocity * Time.deltaTime;
        Vector2 reducedPosition = newPosition*0;
        Vector2 collisionNormal = new Vector2(0, 0);
        Vector2 portionNormal;

        //Distance for the frame
        float distance = newPosition.magnitude;

        //Fraction of distance until the collision
        float distFraction = 0;

        if (distance > 0)
        {
            int collisionCount = playerRigBody.Cast(newPosition, contactFilter, hitBuffer, distance + 0.01f);
            if (collisionCount == 16)
            {
                Debug.Log("ERROR: Exceding maximum trackable collisions.  More than 16 collisions.  In PlayerMove.cs Script.");
            }

            //Track fraction of movement nessasary and collision wall angle
            for (int i = 0; i < collisionCount; i++)
            {
                
                distFraction = hitBuffer[i].fraction;
                collisionNormal = hitBuffer[i].normal;

                //If a collision occurs, get position of collision point.
                if (distFraction > 0)
                {

                    //Get distance to collision
                    reducedPosition.x = newPosition.x * distFraction;
                    reducedPosition.y = newPosition.y * distFraction;

                    //Move position slightly behind collision line
                    reducedPosition.x = reducedPosition.x * 0.999f;
                    reducedPosition.y = reducedPosition.y * 0.999f;

                    portionNormal = collisionNormal;
                    //x^2 + y^2 = 1 (get x to y ratio of normal)
                    if (portionNormal.x > 0)
                    {
                        portionNormal.x *= portionNormal.x;
                    }
                    else
                    {
                        portionNormal.x *= portionNormal.x * -1;
                    }
                    if (portionNormal.y > 0)
                    {
                        portionNormal.y *= portionNormal.y;
                    }
                    else
                    {
                        portionNormal.y *= portionNormal.y * -1;
                    }

                    //Calculate slant movement
                    // X Axis
                    if (collisionNormal.y < 0)
                    {
                        reducedPosition.x += newPosition.x * portionNormal.y;
                        reducedPosition.y += newPosition.x * portionNormal.x;
                    }
                    else if (collisionNormal.y > 0)
                    {
                        reducedPosition.x -= newPosition.x * portionNormal.y;
                        reducedPosition.y -= newPosition.x * portionNormal.x;
                    }
                    


                    // Y Axis
                    if (collisionNormal.x < 0)
                    {
                        reducedPosition.x += newPosition.y * portionNormal.y;
                        reducedPosition.y += newPosition.y * portionNormal.x;
                    }
                    else if (collisionNormal.x > 0)
                    {
                        reducedPosition.x -= newPosition.y * portionNormal.y;
                        reducedPosition.y -= newPosition.y * portionNormal.x;
                    }

                    if (collisionNormal.y == 0)
                    {
                        currVelocity.x = 0;
                        reducedPosition.x = 0;
                        reducedPosition.y = newPosition.y;
                    }
                    if (collisionNormal.x == 0)
                    {
                        currVelocity.y = 0;
                        reducedPosition.y = 0;
                        reducedPosition.x = newPosition.x;
                    }



                    newPosition.x = reducedPosition.x;
                    newPosition.y = reducedPosition.y;

                }

            

                
                //Debug.Log("Count: " + collisionCount);
                //Debug.Log("Normal: " + collisionNormal.x  + ", " + collisionNormal.y);

            }
            playerRigBody.position = playerRigBody.position + newPosition;
        }

        
    }
}


