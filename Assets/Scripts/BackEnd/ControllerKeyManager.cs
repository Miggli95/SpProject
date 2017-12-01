using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerKeyManager : MonoBehaviour {

    float horizontal ;
    float vertical;
    float triggerInput;

    string[] controllers = { "a", "", "", "" };
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        controllers = Input.GetJoystickNames();

      /*  for(int i = 0; i< controllers.Length;i++)
        {
            print("name " + controllers[i]);
        }*/

    }
    public void getKeyCode(string s, Controller2D a, ref bool XBOX)
    {
        controllers = Input.GetJoystickNames();
       
            
            a.JumpKey = KeyCode.Space;
            a.DashKey = KeyCode.LeftShift;
            a.UseKey = KeyCode.E;
            a.InteractKey = KeyCode.Q;
            a.PickUpKey = KeyCode.C;
            a.DieKey = KeyCode.P;
        

        if(a.consoleControlls && controllers.Length>0)
        {
            switch (s)
            {
                case "P1":
                    if (controllers.Length > 0)
                    {
                        if (controllers[0].ToUpper().Contains("XBOX"))
                        {
                            a.JumpKey = KeyCode.Joystick1Button0;

                            //a.DashKey = KeyCode.Joystick1Button5;

                            a.InteractKey = KeyCode.Joystick1Button3;

                            a.PickUpKey = KeyCode.Joystick1Button2;
                            a.UseKey = KeyCode.Joystick1Button1;
                            //a.DieKey = KeyCode.Joystick1Button4; //tempkey please remove it if you need the key
                            a.CycleLeftKey = KeyCode.Joystick1Button4;
                            a.CycleRightKey = KeyCode.Joystick1Button5;
                            XBOX = true;
                        }

                        else if (controllers[0].ToUpper().Contains("WIRELESS"))
                        {
                            a.JumpKey = KeyCode.Joystick1Button1;

                            //a.DashKey = KeyCode.Joystick1Button7;

                            a.InteractKey = KeyCode.Joystick1Button3;

                            a.PickUpKey = KeyCode.Joystick1Button0;
                            a.UseKey = KeyCode.Joystick1Button2;
                            //a.DieKey = KeyCode.Joystick1Button4; //tempkey please remove it if you need the key
                            a.CycleLeftKey = KeyCode.Joystick1Button4;
                            a.CycleRightKey = KeyCode.Joystick1Button7;
                        }

                    }


                    break;
                case "P2":

                    if (controllers.Length > 1)
                    {
                        if (controllers[1].ToUpper().Contains("XBOX"))
                        {
                            a.JumpKey = KeyCode.Joystick2Button0;

                            //a.DashKey = KeyCode.Joystick2Button5;

                            a.InteractKey = KeyCode.Joystick2Button3;

                            a.PickUpKey = KeyCode.Joystick2Button2;
                            a.UseKey = KeyCode.Joystick2Button1;
                            //a.DieKey = KeyCode.Joystick2Button4; //tempkey please remove it if you need the key
                            a.CycleLeftKey = KeyCode.Joystick2Button4;
                            a.CycleRightKey = KeyCode.Joystick2Button5;
                            XBOX = true;
                        }
                        else if (controllers[1].ToUpper().Contains("WIRELESS"))
                        {
                            a.JumpKey = KeyCode.Joystick2Button1;

                            //a.DashKey = KeyCode.Joystick2Button7;

                            a.InteractKey = KeyCode.Joystick2Button3;

                            a.PickUpKey = KeyCode.Joystick2Button0;
                            a.UseKey = KeyCode.Joystick2Button2;
                            //a.DieKey = KeyCode.Joystick2Button4; //tempkey please remove it if you need the key
                            a.CycleLeftKey = KeyCode.Joystick2Button4;
                            a.CycleRightKey = KeyCode.Joystick2Button7;
                        }
                    }
                    break;
                case "P3":
                    if (controllers.Length > 2)
                    {
                        if (controllers[2].ToUpper().Contains("XBOX"))
                        {
                            a.JumpKey = KeyCode.Joystick3Button0;

                            //a.DashKey = KeyCode.Joystick3Button5;

                            a.InteractKey = KeyCode.Joystick3Button3;

                            a.PickUpKey = KeyCode.Joystick3Button2;
                            a.UseKey = KeyCode.Joystick3Button1;
                            //a.DieKey = KeyCode.Joystick3Button4; //tempkey please remove it if you need the key
                            a.CycleLeftKey = KeyCode.Joystick3Button4;
                            a.CycleRightKey = KeyCode.Joystick3Button5;
                            XBOX = true;
                        }
                        else if (controllers[2].ToUpper().Contains("WIRELESS"))
                        {
                            a.JumpKey = KeyCode.Joystick3Button1;

                            //a.DashKey = KeyCode.Joystick3Button7;

                            a.InteractKey = KeyCode.Joystick3Button3;

                            a.PickUpKey = KeyCode.Joystick3Button0;
                            a.UseKey = KeyCode.Joystick3Button2;
                            //a.DieKey = KeyCode.Joystick3Button4; //tempkey please remove it if you need the key
                            a.CycleLeftKey = KeyCode.Joystick3Button4;
                            a.CycleRightKey = KeyCode.Joystick3Button7;
                        }
                    }
                    break;

                case "P4":
                    if (controllers.Length > 3)
                    {
                        if (controllers[3].ToUpper().Contains("XBOX"))
                        {
                            a.JumpKey = KeyCode.Joystick4Button0;

                            //a.DashKey = KeyCode.Joystick4Button5;

                            a.InteractKey = KeyCode.Joystick4Button3;

                            a.PickUpKey = KeyCode.Joystick4Button2;
                            a.UseKey = KeyCode.Joystick4Button1;
                            //a.DieKey = KeyCode.Joystick4Button4; //tempkey please remove it if you need the key
                            a.CycleLeftKey = KeyCode.Joystick4Button4;
                            a.CycleRightKey = KeyCode.Joystick4Button5;
                            XBOX = true;
                        }
                        else if (controllers[3].ToUpper().Contains("WIRELESS"))
                        {
                            a.JumpKey = KeyCode.Joystick4Button1;

                            //a.DashKey = KeyCode.Joystick4Button7;

                            a.InteractKey = KeyCode.Joystick4Button3;

                            a.PickUpKey = KeyCode.Joystick4Button0;
                            a.UseKey = KeyCode.Joystick4Button2;
                            //a.DieKey = KeyCode.Joystick4Button4; //tempkey please remove it if you need the key
                            a.CycleLeftKey = KeyCode.Joystick4Button4;
                            a.CycleRightKey = KeyCode.Joystick4Button7;
                        }
                    }
                    break;
            }

            a.XBOX = XBOX;
        }

    }
    public Vector2 getcharInput(string s, bool a, bool c)
    {
        if (!a && c)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else if(c)
        {
            switch (s)
            {
                case "P1":
                    horizontal = Input.GetAxis("Horizontal1");
                    vertical = Input.GetAxis("Vertical1");
                    break;
                case "P2":
                    horizontal = Input.GetAxis("Horizontal2");
                    vertical = Input.GetAxis("Vertical2");
                    break;
                case "P3":
                    horizontal = Input.GetAxis("Horizontal3");
                    vertical = Input.GetAxis("Vertical3");
                    break;
                case "P4":
                    horizontal = Input.GetAxis("Horizontal4");
                    vertical = Input.GetAxis("Vertical4");
                    break;
                default:
                    horizontal = Input.GetAxis("Horizontal");
                    vertical = Input.GetAxis("Vertical");
                    break;
            }
        }
        return new Vector2(horizontal, vertical);
    }

    public float getTriggerInput(string s, bool c)
    { 
        if (c)
        {
            switch (s)
            {
                case "P1":
                    triggerInput = Input.GetAxis("Trigger1");
                    break;
                case "P2":
                    triggerInput = Input.GetAxis("Trigger2");
                    break;
                case "P3":
                    triggerInput = Input.GetAxis("Trigger3");
                    break;
                case "P4":
                    triggerInput = Input.GetAxis("Trigger4");
                    break;

                default:
                    triggerInput = 0;
                    break;
            }
        }

        return triggerInput;
    }
}
