using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MealsHandler : MonoBehaviour {
    public InputField mealName,
        calories,
        fat,
        protein,
        carbs;

    public GameObject status;
    float sec = 3;

    void Update( ) {
        if( sec >= 3 )
            status.SetActive( false );
        else {
            sec += Time.deltaTime;
        }
    }

	public void addMeal( ) {
        string meal = "";

        if( mealName.text == "" ) {
            changeStatus( false );
            return;
        }

        meal += mealName.text + "-"
            + calories.text + "-"
            + fat.text + "-"
            + protein.text + "-"
            + carbs.text;

        DataHandler.addToDB( DataHandler.MEALS_FILE_NAME, meal );

        changeStatus( true );

        clearAll( );
    }

    void changeStatus( bool status ) {
        sec = 0;
        this.status.SetActive( true );

        if( status ) {
            this.status.GetComponent<Text>( ).text = mealName.text + " is added!";
            this.status.GetComponent<Text>( ).color = Color.green;
        } else {
            this.status.GetComponent<Text>( ).text = "Your meal wasn't added!";
            this.status.GetComponent<Text>( ).color = Color.red;
        }
    }

    void clearAll( ) {
        mealName.text =
            calories.text =
            fat.text =
            protein.text =
            carbs.text = "";
    }
}
