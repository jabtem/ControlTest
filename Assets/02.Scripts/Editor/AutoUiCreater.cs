using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public enum AnchorPresets
{
    TopLeft,
    TopCenter,
    TopRight,

    MiddleLeft,
    MiddleCenter,
    MiddleRight,

    BottomLeft,
    BottonCenter,
    BottomRight,
    BottomStretch,

    VertStretchLeft,
    VertStretchRight,
    VertStretchCenter,

    HorStretchTop,
    HorStretchMiddle,
    HorStretchBottom,

    StretchAll
}

public class AutoUiCreater : EditorWindow
{
    static EditorWindow window;
    GameObject canvas = null;
    GameObject eventSystem = null;
    bool setPivot;
    bool setPosition;
    

    //Screen Resolution
    float ScreenX = Screen.width;
    float ScreenY = Screen.height;
    float halfScreenX = Screen.width * 0.5f;
    float halfScreenY = Screen.height * 0.5f;

    AnchorPresets anchorPresets;

    [MenuItem("Create/UGUI")]
    static void Open()
    {
        window = EditorWindow.GetWindow(typeof(AutoUiCreater));
        window.Show();
    }

    void OnGUI()
    {

        setPivot = EditorGUILayout.Toggle("Set Pivot", setPivot);
        setPosition = EditorGUILayout.Toggle("Set Positon", setPosition);
        anchorPresets = (AnchorPresets)EditorGUILayout.EnumPopup("Anchor", anchorPresets);


        if (GUILayout.Button("Button"))
        {
            CanvasCheck();

            GameObject butt = new GameObject("Button");

            ///<summary> Add Text Compoenent
            GameObject txtObj = new GameObject("Text");
            txtObj.transform.SetParent(butt.transform);
            RectTransform txtRt = txtObj.AddComponent<RectTransform>();
            txtRt.sizeDelta = new Vector2(0f, 0f);
            txtRt.anchorMax = new Vector2(1f, 1f);
            txtRt.anchorMin = new Vector2(0f, 0f);
            Text txt = txtObj.AddComponent<Text>();
            txt.text = "Button";
            txt.alignment = TextAnchor.MiddleCenter;
            txt.color = Color.black;
            ///</summary>

            RectTransform buttRt = butt.AddComponent<RectTransform>();
            buttRt.sizeDelta = new Vector2(160f, 30f);
            SetAnchor(buttRt, anchorPresets);
            butt.AddComponent<CanvasRenderer>();
            Image buttImage = butt.AddComponent<Image>();
            buttImage.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            buttImage.type = Image.Type.Sliced;
            butt.AddComponent<Button>();
            butt.transform.SetParent(canvas.transform);
        }
    }

    void CanvasCheck()
    {
        canvas = GameObject.Find("Canvas");
        eventSystem = GameObject.Find("EventSystem");


        if (canvas == null && eventSystem == null)
        {
            canvas = new GameObject("Canvas");
            canvas.AddComponent<RectTransform>();
            Canvas can = canvas.AddComponent<Canvas>();
            can.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
        else if (canvas == null && eventSystem != null)
        {
            canvas = new GameObject("Canvas");
            canvas.AddComponent<RectTransform>();
            Canvas can = canvas.AddComponent<Canvas>();
            can.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
        }
        else if (canvas != null && eventSystem == null)
        {
            eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        else
            return;
    }

    public void SetAnchor(RectTransform rectTr, AnchorPresets allign)
    {

        //Defalut Position, Screen Center
        rectTr.anchoredPosition = new Vector3(halfScreenX, halfScreenY, 0f);
        float halfRectSizeX = rectTr.sizeDelta.x * 0.5f;
        float halfRectSizeY = rectTr.sizeDelta.y * 0.5f;
        Vector2 vec = Vector2.zero;

        switch (allign)
        {
            case (AnchorPresets.TopLeft):
                {
                    vec = new Vector2(0f, 1f);
                    rectTr.anchorMin = vec;
                    rectTr.anchorMax = vec;
                    if(setPivot&&!setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector2(rectTr.anchoredPosition.x - (rectTr.sizeDelta.x * 0.5f),
                                rectTr.anchoredPosition.y + (rectTr.sizeDelta.y * 0.5f));

                    }
                    else if (setPosition&&!setPivot)
                    {
                        rectTr.anchoredPosition = new Vector2(rectTr.sizeDelta.x * 0.5f, ScreenY - rectTr.sizeDelta.y *0.5f);
                    }
                    else if(setPivot && setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector2(0f, ScreenY);
                    }
                    
                    break;
                }
            case (AnchorPresets.TopCenter):
                {
                    vec = new Vector2(0.5f, 1f);
                    rectTr.anchorMin = vec;
                    rectTr.anchorMax = vec;
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x ,
                                                            rectTr.anchoredPosition.y + (rectTr.sizeDelta.y * 0.5f));
                    }
                    else if (setPosition && !setPivot)
                    {
                        rectTr.anchoredPosition = new Vector2(rectTr.anchoredPosition.x, ScreenY - rectTr.sizeDelta.y * 0.5f);
                    }
                    else if (setPivot && setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector2(halfScreenX,ScreenY);
                    }
                    break;
                }
            case (AnchorPresets.TopRight):
                {
                    vec = new Vector2(1f, 1f);
                    rectTr.anchorMin = vec;
                    rectTr.anchorMax = vec;
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x + (rectTr.sizeDelta.x * 0.5f),
                                                            rectTr.anchoredPosition.y + (rectTr.sizeDelta.y * 0.5f));
                    }
                    else if (setPosition && !setPivot)
                    {
                        rectTr.anchoredPosition = new Vector2(ScreenX- rectTr.sizeDelta.x * 0.5f, ScreenY - rectTr.sizeDelta.y * 0.5f);
                    }
                    else if (setPivot && setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector2(ScreenX, ScreenY);
                    }
                    break;
                }

            case (AnchorPresets.MiddleLeft):
                {
                    vec = new Vector2(0f, 0.5f);
                    rectTr.anchorMin = vec;
                    rectTr.anchorMax = vec;
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x - (rectTr.sizeDelta.x * 0.5f),
                                                            rectTr.anchoredPosition.y );
                    }
                    else if (setPosition && !setPivot)
                    {
                        rectTr.anchoredPosition = new Vector2(rectTr.sizeDelta.x * 0.5f, halfScreenY);
                    }
                    else if (setPivot && setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector2(0f, halfScreenY);
                    }
                    break;
                }
            case (AnchorPresets.MiddleCenter):
                {
                    vec = new Vector2(0.5f, 0.5f);
                    rectTr.anchorMin = vec;
                    rectTr.anchorMax = vec;
                    if (setPivot)
                    {
                        rectTr.pivot = vec;
                    }
                    break;
                }
            case (AnchorPresets.MiddleRight):
                {
                    vec = new Vector2(1f, 0.5f);
                    rectTr.anchorMin = vec;
                    rectTr.anchorMax = vec;
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x + (rectTr.sizeDelta.x * 0.5f),
                                                            rectTr.anchoredPosition.y);
                    }
                    else if (setPosition && !setPivot)
                    {
                        rectTr.anchoredPosition = new Vector2(ScreenX - rectTr.sizeDelta.x * 0.5f, halfScreenY);
                    }
                    else if (setPivot && setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector2(ScreenX, halfScreenY);
                    }
                    break;
                }

            case (AnchorPresets.BottomLeft):
                {
                    vec = new Vector2(0f, 0f);
                    rectTr.anchorMin = vec;
                    rectTr.anchorMax = vec;
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x - (rectTr.sizeDelta.x * 0.5f),
                                                            rectTr.anchoredPosition.y - (rectTr.sizeDelta.y * 0.5f));
                    }
                    else if (setPosition && !setPivot)
                    {
                        rectTr.anchoredPosition = new Vector2(rectTr.sizeDelta.x * 0.5f, rectTr.sizeDelta.y*0.5f);
                    }
                    else if (setPivot && setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector2(0f, 0f);
                    }
                    break;
                }
            case (AnchorPresets.BottonCenter):
                {
                    vec = new Vector2(0.5f, 0f);
                    rectTr.anchorMin = vec;
                    rectTr.anchorMax = vec;
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x - (rectTr.sizeDelta.x * 0.5f),
                                                            rectTr.anchoredPosition.y);
                    }
                    else if (setPosition && !setPivot)
                    {
                        rectTr.anchoredPosition = new Vector2(halfScreenX, rectTr.sizeDelta.y * 0.5f);
                    }
                    else if (setPivot && setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector2(halfScreenX, 0f);
                    }
                    break;
                }
            case (AnchorPresets.BottomRight):
                {
                    vec = new Vector2(1f, 0f);
                    rectTr.anchorMin = vec;
                    rectTr.anchorMax = vec;
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x - (rectTr.sizeDelta.x * 0.5f),
                                                            rectTr.anchoredPosition.y);
                    }
                    else if (setPosition && !setPivot)
                    {
                        rectTr.anchoredPosition = new Vector2(ScreenX - rectTr.sizeDelta.x *0.5f, rectTr.sizeDelta.y * 0.5f);
                    }
                    else if (setPivot && setPosition)
                    {
                        rectTr.pivot = vec;
                        rectTr.anchoredPosition = new Vector2(halfScreenX, 0f);
                    }
                    break;
                }

            //아직 처리안됨

            case (AnchorPresets.HorStretchTop):
                {
                    rectTr.anchorMin = new Vector2(0, 1);
                    rectTr.anchorMax = new Vector2(1, 1);

                    //Equal TopCenter
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = new Vector2(0.5f, 1);
                        rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x,
                                                            rectTr.anchoredPosition.y + (rectTr.sizeDelta.y * 0.5f));
                    }
                    break;
                }
            case (AnchorPresets.HorStretchMiddle):
                {
                    rectTr.anchorMin = new Vector2(0, 0.5f);
                    rectTr.anchorMax = new Vector2(1, 0.5f);
                    //Equal MiddleCenter
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = new Vector2(0.5f,0.5f);
                    }
                    break;
                }
            case (AnchorPresets.HorStretchBottom):
                {
                    rectTr.anchorMin = new Vector2(0, 0);
                    rectTr.anchorMax = new Vector2(1, 0);
                    //Equal BottomCenter
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = new Vector2(0.5f, 0);
                        rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x - (rectTr.sizeDelta.x * 0.5f),
                                                            rectTr.anchoredPosition.y);
                    }
                    break;
                }

            case (AnchorPresets.VertStretchLeft):
                {
                    rectTr.anchorMin = new Vector2(0, 0);
                    rectTr.anchorMax = new Vector2(0, 1);
                    //Equal MiddleLeft
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = new Vector2(0,0.5f);
                        rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x - (rectTr.sizeDelta.x * 0.5f),
                                                            rectTr.anchoredPosition.y);
                    }
                    break;
                }
            case (AnchorPresets.VertStretchCenter):
                {
                    rectTr.anchorMin = new Vector2(0.5f, 0);
                    rectTr.anchorMax = new Vector2(0.5f, 1);
                    //Equal MiddleCenter
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = new Vector2(0.5f,0.5f);
                    }
                    break;
                }
            case (AnchorPresets.VertStretchRight):
                {
                    //Equal MiddleRight
                    rectTr.anchorMin = new Vector2(1, 0);
                    rectTr.anchorMax = new Vector2(1, 1);
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = new Vector2(1.0f,0.5f);
                        rectTr.anchoredPosition = new Vector3(rectTr.anchoredPosition.x + (rectTr.sizeDelta.x * 0.5f),
                                                            rectTr.anchoredPosition.y);
                    }
                    break;
                }

            case (AnchorPresets.StretchAll):
                {
                    rectTr.anchorMin = new Vector2(0, 0);
                    rectTr.anchorMax = new Vector2(1, 1);
                    if (setPivot && !setPosition)
                    {
                        rectTr.pivot = new Vector2(0.5f, 0.5f);
                    }
                    break;
                }
        }
    }
}
