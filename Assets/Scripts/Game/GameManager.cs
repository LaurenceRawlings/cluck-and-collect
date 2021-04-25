using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CluckAndCollect
{
    public class GameManager : MonoBehaviour
    {
        private GameState _state = GameState.Menu;

        private void Update()
        {
            MoveCommand command = null;
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                command = new MoveCommand(Vector3.forward);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                command = new MoveCommand(Vector3.back);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                command = new MoveCommand(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                command = new MoveCommand(Vector3.right);
            }

            command?.Execute();
        }
    }
}
