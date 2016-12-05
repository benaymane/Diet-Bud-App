using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainScript : MonoBehaviour {
    public Text caloriesValue,
        fatValue,
        proteinValue,
        carbsValue;

    public Dropdown meals;

    public InputField serving;

    private DataHandler data = new DataHandler( );

	// Use this for initialization
	void Start () {
        caloriesValue.text = data.getCalories( ).ToString( );
        fatValue.text = data.getFat( ).ToString( );
        proteinValue.text = data.getProtein( ).ToString( );
        carbsValue.text = data.getCarbs( ).ToString( );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
