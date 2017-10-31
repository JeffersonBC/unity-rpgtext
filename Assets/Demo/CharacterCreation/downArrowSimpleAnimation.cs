using UnityEngine;

public class downArrowSimpleAnimation : MonoBehaviour {
      
    public float magnitude = 3f;
    public float speed = 3f;

    float x0, y0, t0;
    RectTransform rt;

    void Start() {
        rt = (RectTransform)transform;
        x0 = rt.anchoredPosition.x;
        y0 = rt.anchoredPosition.y;
    }

    void OnEnable() {
        t0 = Time.time;
    }

	void Update () {
        rt.anchoredPosition = new Vector3(x0, y0 + Mathf.Cos((Time.time- t0) * speed) * magnitude, 0);
	}
}
