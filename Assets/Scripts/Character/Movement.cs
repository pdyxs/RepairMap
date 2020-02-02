using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public bool OppositeControls = false;
    public float SelectDelay = 1f;

    //public Input controls;


    public Vector3 offset;

    Grid _grid;

    public GridSquare DefaultGrid;
    public GridSquare CurrentGrid;

    public Transform characterMovementTrans;
    public Transform characterRotationTrans;

    public Animation anim;
    public string floatAnim; 
    public string wobbleAnim;


    bool currentlyMoving = false;


    Transform targetTransform;

    public float rotateSpeed = 0.1f;
    public float moveSpeed = 0.1f;


    //debug
    public bool start = false;


    PlayerInput playerInput;
    InputAction action;
    InputAction startAction;

    public StartGame startGame;
    public int playerNum = 0;

    private void Awake()
    {
       
    }


    private void StartAction_performed(InputAction.CallbackContext obj)
    {
        if (startGame != null)
        {
            startGame.StartPressed(playerNum);
            Debug.Log("ACTION");
        }
    }


    public void OnMovement(Vector2 movementVec)
    {
        //Vector2 movementVec = inputValue.Get<Vector2>();

        if (currentlyMoving == false)
        {
            if ((!OppositeControls && movementVec.y == -1) || (OppositeControls && movementVec.y == 1))//(Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (CurrentGrid.y < _grid.squares.Length - 1)
                {
                    CurrentGrid = _grid.squares[CurrentGrid.y + 1].squares[CurrentGrid.x];
                    MoveTo(CurrentGrid);
                }
            }
            else if ((!OppositeControls && movementVec.y == 1) || (OppositeControls && movementVec.y == -1))//(Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (CurrentGrid.y > 0)
                {
                    CurrentGrid = _grid.squares[CurrentGrid.y - 1].squares[CurrentGrid.x];
                    MoveTo(CurrentGrid);
                }
            }
            else if ((!OppositeControls && movementVec.x == 1) || (OppositeControls && movementVec.x == -1))//(Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (CurrentGrid.x < _grid.squares[CurrentGrid.y].squares.Length - 1)
                {
                    CurrentGrid = _grid.squares[CurrentGrid.y].squares[CurrentGrid.x + 1];
                    MoveTo(CurrentGrid);
                }
            }
            else if ((!OppositeControls && movementVec.x == -1) || (OppositeControls && movementVec.x == 1))//(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (CurrentGrid.x > 0)
                {
                    CurrentGrid = _grid.squares[CurrentGrid.y].squares[CurrentGrid.x - 1];
                    MoveTo(CurrentGrid);
                }
            }

            TimeUtils.RunAfter(()=>CurrentGrid.grid.SelectSquare(CurrentGrid), SelectDelay);
           
        }
    }

    public void OnEnable()
    {
        playerInput = this.GetComponent<PlayerInput>();
        action = playerInput.actions["Movement"];
        startAction = playerInput.actions["Action"];


        startAction.performed += StartAction_performed;
    }

    private void Start()
    {




        if (characterMovementTrans == null)
        {
            Debug.LogError("set characterMovementTrans in Movement");
        }

        if (characterRotationTrans == null)
        {
            Debug.LogError("set characterRotationTrans in Movement");
        }

        if (DefaultGrid != null)
        {
            setLocationInstant(DefaultGrid);
            CurrentGrid = DefaultGrid;

            _grid = DefaultGrid.grid;
        }
        else
        {
            Debug.LogError("no default grid in Movement");
        }
    }
    void setLocationInstant(GridSquare defaultGrid)
    {
        characterMovementTrans.position = defaultGrid.transform.position + offset;
    }




    private void Update()
    {
        if(start == true)
        {
            MoveTo(DefaultGrid);
            // MoveToPosition(Vector3.zero);
            start = false;
        }


        Vector2 movementValue = action.ReadValue<Vector2>();
        OnMovement(movementValue);

        

        /*if (currentlyMoving == false)+
        {
            if ( //(Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (CurrentGrid.y < _grid.squares.Length -1)
                {
                    CurrentGrid = _grid.squares[CurrentGrid.y + 1 ].squares[CurrentGrid.x ];
                    MoveTo(CurrentGrid);
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (CurrentGrid.y > 0)
                {
                    CurrentGrid = _grid.squares[CurrentGrid.y -1].squares[CurrentGrid.x];
                    MoveTo(CurrentGrid);
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (CurrentGrid.x < _grid.squares[CurrentGrid.y].squares.Length - 1)
                {
                    CurrentGrid = _grid.squares[CurrentGrid.y].squares[CurrentGrid.x + 1];
                    MoveTo(CurrentGrid);
                }
            }


            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (CurrentGrid.x > 0)
                {
                    CurrentGrid = _grid.squares[CurrentGrid.y].squares[CurrentGrid.x - 1];
                    MoveTo(CurrentGrid);
                }
            }
        }*/


    }

    void inputMoveRight()
    {
        if (currentlyMoving == false)
        {
            if (CurrentGrid.x < _grid.squares[CurrentGrid.y].squares.Length - 1)
            {
                CurrentGrid = _grid.squares[CurrentGrid.y].squares[CurrentGrid.x + 1];
                MoveTo(CurrentGrid);
            }
        }
    }


    public void MoveTo(GridSquare gridTarget)
    {
        StartCoroutine(moveNextLocation(gridTarget));
    }


    public IEnumerator moveNextLocation(GridSquare gridTarget)
    {
        currentlyMoving = true;

        //rotate first


        Vector3 targetDirection = gridTarget.transform.position - characterRotationTrans.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        while (Quaternion.Angle(characterRotationTrans.rotation, targetRotation) > 0.5f)
        {
            float rotateStep = rotateSpeed * Time.deltaTime;
            characterRotationTrans.rotation = Quaternion.Slerp(characterRotationTrans.rotation, targetRotation, rotateStep);
            yield return null;
        }


        //float
        //anim[floatAnim].speed = 1;
        //anim.Play(floatAnim);


        //move
        anim[wobbleAnim].wrapMode = WrapMode.Loop;
        anim.Play(wobbleAnim);

        //set down
        // anim[floatAnim].speed = -1;
        //anim.Play(floatAnim);

        yield return new WaitForSeconds(0.2f);

        while(Vector3.Distance(characterMovementTrans.position, gridTarget.transform.position) > 0.3f)
        {
            float moveStep = moveSpeed * Time.deltaTime;
            characterMovementTrans.transform.position = Vector3.MoveTowards(characterMovementTrans.transform.position, gridTarget.transform.position, moveStep);
            yield return null;
        }
        anim.Stop(wobbleAnim);

        currentlyMoving = false;
    }
}
