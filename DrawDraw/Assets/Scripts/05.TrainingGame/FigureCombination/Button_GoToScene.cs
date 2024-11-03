using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_GoToScene : MonoBehaviour
{
    // 공통
    public void ChangeScene_map()
    {
        SceneManager.LoadScene("MapScene");
        Debug.Log("맵 화면으로 이동합니다.");
    }

    // 1단계
    public void ChangeScene_Sun()
    {
        SceneManager.LoadScene("1Sun");
        Debug.Log("해 도형 조합을 진행합니다.");
    }

    public void ChangeScene_Pinwheel()
    {
        SceneManager.LoadScene("1Pinwheel");
        Debug.Log("바람개비 도형 조합을 진행합니다.");
    }

    //2단계
    public void ChangeScene_Rocket()
    {
        SceneManager.LoadScene("2Rocket");
        Debug.Log("로켓 도형 조합을 진행합니다.");
    }

    public void ChangeScene_Ship()
    {
        SceneManager.LoadScene("2Ship");
        Debug.Log("배 도형 조합을 진행합니다.");
    }

    //3단계
    public void ChangeScene_Person()
    {
        SceneManager.LoadScene("3Person");
        Debug.Log("사람 도형 조합을 진행합니다.");
    }

    public void ChangeScene_TheTrain()
    {
        SceneManager.LoadScene("3TheTrain");
        Debug.Log("기차 도형 조합을 진행합니다.");
    }

    //reset
    public GameObject[] shapePrefabs; // 프리팹 배열
    public GameObject FinishButton; // 완료 버튼 참조

    // 프리팹별 초기 컬러 코드 저장
    private Dictionary<GameObject, string> prefabOriginalColorCodes = new Dictionary<GameObject, string>();


    void Start()
    {
        // 각 프리팹의 초기 컬러 코드를 저장
        foreach (GameObject prefab in shapePrefabs)
        {
            if (prefab != null)
            {
                SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // Color 객체를 HEX 컬러 코드로 변환하여 저장
                    string colorCode = "#" + ColorUtility.ToHtmlStringRGB(spriteRenderer.color);
                    prefabOriginalColorCodes[prefab] = colorCode;
                }
            }
        }
    }

    public void OnRestartButtonClick()
    {

        if (FinishButton != null && FinishButton.activeSelf)
        {
            ResetClonedShapesColor();
        }

        else
        {
            // 현재 활성화된 씬을 다시 로드
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("처음부터 다시 시작합니다.");

        }
    }
    private void ResetClonedShapesColor()
    {
        // 'shape' 태그를 가진 모든 오브젝트를 찾음
        GameObject[] clonedShapes = GameObject.FindGameObjectsWithTag("shape");

        foreach (GameObject shape in clonedShapes)
        {
            SpriteRenderer spriteRenderer = shape.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // 프리팹과 동일한 초기 컬러 코드를 찾고 적용
                foreach (var prefab in shapePrefabs)
                {
                    if (prefab != null && shape.name.StartsWith(prefab.name))
                    {
                        if (prefabOriginalColorCodes.TryGetValue(prefab, out string colorCode))
                        {
                            // 명시적으로 UnityEngine.Color를 사용
                            if (UnityEngine.ColorUtility.TryParseHtmlString(colorCode, out UnityEngine.Color color))
                            {
                                spriteRenderer.color = color;
                            }
                        }
                        break; // 일치하는 프리팹을 찾았으면 더 이상 반복할 필요 없음
                    }
                }
            }
        }

        //Debug.Log("모든 'shape' 태그 오브젝트의 색상이 초기 상태로 되돌아갔습니다.");
    }
}
