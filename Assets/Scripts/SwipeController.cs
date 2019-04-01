using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour {


    private const float DEADZONE = 100.0f;

    public static SwipeController Instance { set; get; }

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;

    public bool Tap { get { return tap; } }

    public bool SwipeLeft { get { return swipeLeft; } }

    public bool SwipeRight { get { return swipeRight; } }

    public bool SwipeUp { get { return swipeUp; } }

    public bool SwipeDown { get { return swipeDown; } }


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // Reset all the booleans
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        
       
        if (Input.GetMouseButtonDown(0)) // funkcja wykrywa klikniecie myszka
        {
 
            tap = true;
            startTouch = Input.mousePosition; // funkcja start touch ustawia aktualna pozycja myszy we współrzędnych pikseli.
        }
        else if (Input.GetMouseButtonUp(0)) // wykrywa koniec klikniecia, myszka Zwraca wartość true podczas kadru, w którym użytkownik zwolnił dany przycisk myszy. 
        {
            startTouch = swipeDelta = Vector2.zero;
              
        }
     

        
        if (Input.touches.Length != 0)                                                                              //Zwraca listę obiektów reprezentujących status wszystkich dotknięć w ostatniej klatce. (Tylko do odczytu) (Przydziela zmienne tymczasowe).

           
        {
            if (Input.touches[0].phase == TouchPhase.Began)                                                     // Ta sekcja tylko aktualizuje ostatnie dotkniecie do nowej lokalizacji "touch.phase  informuje program, że palec został przesunięty z jego ostatniej lokalizacji, a tym samym aktualizuje do nowej touch.position.
            {
                tap = true;
                startTouch = Input.mousePosition;
                Debug.Log(startTouch);
            }

            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)   // sprawdzanie czy palec usunieto z ekranu

            {
                startTouch = swipeDelta = Vector2.zero;
              
            }
        }
       


        swipeDelta = Vector2.zero;
        if (startTouch != Vector2.zero)
        {

           
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }

            else if (Input.GetMouseButtonDown(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
                
            }


         
            if (swipeDelta.magnitude > DEADZONE)
            {
               
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    // Horizontal
                    if (x < 0)
                    {
                        swipeLeft = true;
                    }
                    else
                    {
                        swipeRight = true;
                    }
                }
                else
                {
                    // Vertical
                    if (y < 0)
                    {
                        swipeDown = true;
                    }
                    else
                    {
                        swipeUp = true;
                    }
                }

                startTouch = swipeDelta = Vector2.zero;
            }
        }

    }
}
