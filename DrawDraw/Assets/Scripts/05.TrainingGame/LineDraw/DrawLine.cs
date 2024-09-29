using System.Collections.Generic;
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

    private List<GameObject> lines = new List<GameObject>();

    [SerializeField]
    private MonoBehaviour LineDrawManager; // 활성화를 판단할 스크립트

    public GameObject finishButton;
    //public Sprite defaultSprite;
    public Sprite activeSprite;

    private bool isDrawing = false;
    private float timer = 0f;
    public float timeLimit = 5f; // 선이 그려지지 않을 때 게임 오버가 되는 시간 (초 단위)

    public GameObject timeChar;
    public GameObject check; // 게임의 확인창 팝업
    public GameObject finish; // 게임의 결과창 팝업

    public ColorButtonManager ColorButtonManager; // ColorManager 스크립트 참조 (colorCode 값 가져오기)
    private Color lineColor;


    void Update()
    {
        // 타이머 업데이트
        timer += Time.deltaTime;
        if (!isDrawing)
        {
            if (timer >= timeLimit)
            {
                //Debug.Log("5초 지남");
                if (check.activeSelf == true || finish.activeSelf == true)
                {
                    timeChar.SetActive(false);
                    timer = 0f;
                }
                else
                {
                    timeChar.SetActive(true);
                    Invoke("timeEffect", 3f);
                }
            }
        }
        else
        {
            // 선이 그려지고 있을 때는 타이머를 초기화
            timer = 0f;
        }


        // 그리기 영역 안에 있어야 그리기 가능
        if (LineDrawManager.GetComponent<LineDrawManager>().DrawActivate)
        { // 마우스 클릭 또는 터치가 시작되면 새로운 선을 그린다.
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                CreateNewLine(lineColor);
            }
            // 현재 선을 그리는 중이고 마우스가 클릭되었거나 터치가 이동 중인 경우, 새로운 점을 선에 추가한다.
            else if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
            {
                Vector2 currentPosition = GetInputPosition();

                if (Vector2.Distance(currentPosition, previousPosition) > 0.1f) // 선을 그리기 위한 최소한의 거리
                {
                    if (currentLineRenderer == null)
                    {
                        CreateNewLine(lineColor);
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
                isDrawing = false;
            }
        }

        else //영역 밖을 경우
        {
            previousPosition = previousPosition2; // 이전 위치 초기화
        }
    }
    //*********************************************************************************************************************

    void CreateNewLine(Color newColor)
    {
        isDrawing = true;

        // 새로운 선을 그릴 GameObject 생성
        GameObject newLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        currentLineRenderer = newLine.GetComponent<LineRenderer>();

        // 생성된 선을 리스트에 추가
        lines.Add(newLine);

        // 라인 렌더러 설정
        // colorManager의 colorCode 값을 가져와서 LineRenderer의 색상을 변경
        if (ColorButtonManager.isActive)
        {
            lineColor = ColorButtonManager.selectedColor;
        }
        else
        {
            lineColor = Color.black;
        }

        ChangeLineColor(lineColor);

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

    public void ClearAllLines()
    {
        // 리스트에 저장된 모든 선(GameObject)을 삭제
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }

        // 리스트를 초기화
        lines.Clear();
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

    private void timeEffect()
    {
        timeChar.SetActive(false);
        timer = 0f;
    }

    // LineRenderer의 색상을 변경하는 함수
    void ChangeLineColor(Color newColor)
    {
        if (currentLineRenderer != null)
        {            
            // LineRenderer에 색상을 적용
            currentLineRenderer.startColor = newColor;
            currentLineRenderer.endColor = newColor;

            // 추가로 LineRenderer의 material 색상도 변경
            if (currentLineRenderer.material != null)
            {
                currentLineRenderer.material.color = newColor;
            }
            else
            {
                Debug.LogWarning("LineRenderer에 Material이 설정되지 않았습니다.");
            }
        }
        else
        {
            Debug.LogWarning("LineRenderer가 설정되지 않았습니다.");
        }
    }
}
