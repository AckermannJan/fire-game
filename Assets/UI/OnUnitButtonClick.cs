using UnityEngine.UI;
using UnityEngine;

public class OnUnitButtonClick : MonoBehaviour
{
    string parentName;
    // Start is called before the first frame update
    void Start() {
		gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        parentName = gameObject.transform.parent.name;
    }

	void TaskOnClick(){
        UnitSelections.Instance.DeselectAll();
        UnitSelections.Instance.SelectByName(parentName);
		Debug.Log ("You have clicked the button!");
	}
}
