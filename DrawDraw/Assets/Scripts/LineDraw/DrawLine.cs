using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab; // 선을 그리기 위한 프리팹
    public float lineWidth = 0.1f; // 선의 너비

    private LineRenderer currentLineRenderer; // 현재 그려지는 선의 라인 렌더러
    private Vector2 previousPosition; // 이전 위치
    private Vector2 previousPosition2; // 이전전 위치

    [SerializeField]
    private MonoBehaviour LineDrawManager; // 활성화를 판단할 스크립트

    public GameObject finishButton;
    //public Sprite defaultSprite;
    public Sprite activeSprite;

    void Update()
    {
        // 그리기 영역 안에 있어야 그리기 가능
        if (LineDrawManager.GetComponent<LineDrawManager>().DrawActivate)
        { // 마우스 클릭 또는 터치가 시작되면 새로운 선을 그린다.
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                CreateNewLine();
            }
            // 현재 선을 그리는 중이고 마우스가 클릭되었거나 터치가 이동 중인 경우, 새로운 점을 선에 추가한다.
            else if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
            {
                Vector2 currentPosition = GetInputPosition();
                
                if (Vector2.Distance(currentPosition, previousPosition) > 0.1f) // 선을 그리기 위한 최소한의 거리
                {
                    if (currentLineRenderer == null)
                    {
                        CreateNewLine();
                    }
                    AddPointToLine(currentPosition);

                    // 선이 그려지면 버튼 이미지 변경
                    finishButton.GetComponent<Image>().sprite = activeSprite;
                    finishButton.GetComponent<Image>().preserveAspect = true; // 비율 유지
                                                                              // 버튼 이미지의 부모 UI 오브젝트의 RectTransform을 가져옵니다.
                    RectTransform buttonRectTransform = finishButton.GetComponent<RectTransform>();

                    // 원하는 스케일로 버튼 이미지의 크기를 조절합니다.
                    float desiredScaleX = 1.67f; // x축 스케일
                    float desiredScaleY = 1.67f; // y축 스케일
                    buttonRectTransform.localScale = new Vector2(desiredScaleX, desiredScaleY);
                }
            }
            // 마우스 클릭 또는 터치가 끝나면 현재 선을 비운다.
            else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                currentLineRenderer = null; // 현재 그리는 선 종료
            }
        }

        else //영역 밖을 경우
        {
            previousPosition = previousPosition2; // 이전 위치 초기화
        }
        //*********************************************************************************************************************
       
        void CreateNewLine()
        {
            // 새로운 선을 그릴 GameObject 생성
            GameObject newLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            currentLineRenderer = newLine.GetComponent<LineRenderer>();

            // 라인 렌더러 설정
            currentLineRenderer.startWidth = lineWidth;
            currentLineRenderer.endWidth = lineWidth;
            currentLineRenderer.positionCount = 0; // 선의 위치를 초기화합니다.

            // 현재 위치를 이전 위치로 설정
            previousPosition = GetInputPosition();
            AddPointToLine(previousPosition);
        }

        void AddPointToLine(Vector2 newPoint)
        {
            // 새로운 점을 선에 추가
            currentLineRenderer.positionCount++;
            currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, newPoint);

            // 현재 위치를 이전 위치로 설정
            previousPosition2 = previousPosition;
            previousPosition = newPoint;

        }

        // 마우스 클릭 또는 터치의 위치를 세계 좌표로 변환하여 반환
        Vector2 GetInputPosition()
        {
            Vector2 inputPosition = Vector2.zero;
            if (Input.touchCount > 0)
            {
                inputPosition = Input.GetTouch(0).position;
            }
            else
            {
                inputPosition = Input.mousePosition;
            }

            return Camera.main.ScreenToWorldPoint(inputPosition);
        }
    }
}
