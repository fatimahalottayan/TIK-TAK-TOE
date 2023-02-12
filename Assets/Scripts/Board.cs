using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



namespace SAMI.TIKTAKTEO
{
    public class Board : MonoBehaviour
    {
        static public Board instance;
        static private int BoardSize;
        public string[,] board;
        private bool isX = true;
        public GameObject GameOver;
        public Text winner;
        public int EndCount = 0;
        public GameObject btn;
        public GameObject BOARD;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(instance);
            }
        }

        void Start()
        {

            //validate board size 
            if (BoardSize <= 2 || BoardSize > 10)
            {
                BoardSize = 3;
            }

            //set grid size
            BOARD.GetComponent<GridLayoutGroup>().constraintCount= BoardSize;

            //generate the btns in the scene
            GenerateBtns(BoardSize);


            //insilize array size
            board = new string[BoardSize, BoardSize];
       

            //inisialize the array values (needs to be enhanced)
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    board[i, j] = "_";
                }
            }
        }

        //will be called from the main menu
        public void SetArraySize(string size)
        { 

            BoardSize = int.Parse(size);
        
           
        }
     
        void GenerateBtns(int size)
        {

            for (int i = 0; i < size; i++)
            {    
                for (int j = 0; j < size; j++)
                {
                    GameObject BTN = Instantiate(btn);
                    BTN.GetComponent<Call>().index = new Vector2Int(i, j);
                    BTN.transform.SetParent(BOARD.transform, false);  
                }
                
            }
        }

        public void ButtonClicked(GameObject button, Vector2Int pos)
        {
            // check if is it a valid button?
            if (!(button.GetComponentInChildren<TextMeshProUGUI>().text).Equals("_"))
            {
                return;
            }

            //incress the board counter
            EndCount++;


            //change TXT value & assign the index value in the 2D array
            if (isX)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = "X";
                board[pos.x, pos.y] = "X";
            }
            else
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = "O";
                board[pos.x, pos.y] = "O";
            }


            //change the player
            isX = !isX;


            //check for win --> if zero returned then no winner in this round
            int winvalue = CheckForWin(pos);
            if (winvalue == 1)
            {
                EndGame("Player X wins");

            }
            else if (winvalue == -1)
            {

                EndGame("Player O wins");
            }


            //check if the board full
            if (!IsMovesLeft())
            {
                EndGame("No winner");
            }

        }

       
        void EndGame(string winstate)
        {
            winner.text = winstate;
            GameOver.transform.localScale = Vector3.zero;
            GameOver.SetActive(true);
            LeanTween.scale(GameOver, Vector3.one, 1f).setEaseInSine();
        }
       

        bool IsMovesLeft()
        {
            if (EndCount < board.Length)
                return true;

            return false;
        }



        int CheckForWin(Vector2Int pos)
        {
            int length = board.GetLength(0);
           
            //check coulmn of the clicked btn
            string temp = board[pos.x, 0];
            bool flag = true;
            for (int col = 1; col < length; col++)
            {
                if (!temp.Equals(board[pos.x, col]))
                {
                    flag = false;
                    break;
                }
                temp = board[pos.x, col];
            }
            if (flag)
            {
                if (temp.Equals("X"))
                    return 1;
                else if (temp.Equals("O"))
                    return -1;
            }



            //check row of the clicked btn
            temp = board[0, pos.y];
            flag = true;
            for (int row = 1; row < length; row++)
            {
                if (!temp.Equals(board[row, pos.y]))
                {
                    flag = false;
                    break;
                }
                temp = board[row, pos.y];
            }
            if (flag)
            {
                if (temp.Equals("X"))
                    return 1;
                else if (temp.Equals("O"))
                    return -1;
            }


            //check dieognaly of the clicked btn

            string leftEdge = board[0, 0];
            string rightEdge = board[board.GetLength(0) - 1, 0];
            string leftEdge2 = board[0, board.GetLength(0) - 1];
            string rightEdge2 = board[board.GetLength(0) - 1, board.GetLength(0) - 1];
            bool flag2 = true;


            //left diognal
            if (pos.x == pos.y)
            {
                if (leftEdge.Equals(rightEdge2) && !leftEdge.Equals("_"))
                {
                    for (int i = 1; i < length - 1; i++)
                    {
                        if (!board[i, i].Equals(leftEdge))
                        {
                            flag2 = false;
                            break;
                        }
                    }
                    if (flag2)
                    {
                        if (leftEdge.Equals("X"))
                            return 1;
                        else if (leftEdge.Equals("O"))
                            return -1;

                    }
                }
            }

            //right diognal
            if (pos.x + pos.y == length - 1)
            {
                flag2 = true;
                if (rightEdge.Equals(leftEdge2) && !rightEdge.Equals("_"))
                {
                    int counter = length - 2;
                    for (int i = 1; i < length - 1; i++)
                    {
                        if (!board[i, counter].Equals(rightEdge))
                        {
                            flag2 = false;
                            break;
                        }
                        counter--;
                    }
                    if (flag2)
                    {
                        if (rightEdge.Equals("X"))
                            return 1;
                        else if (rightEdge.Equals("O"))
                            return -1;

                    }

                }
            }

            // Else if none of them have won then return 0
            return 0;

        }
    }
}