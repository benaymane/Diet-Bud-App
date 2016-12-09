using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MealsHandler : ErrorHandler {
    public InputField mealName,
        calories,
        fat,
        protein,
        carbs;    

	public void addMeal( ) {
        error = false;
        string meal = "";

        if( mealName.text == "" )
            sendError( errorCode.NAME_EMPTY );

        else if( mealName.text.Contains( "-" ) )
            sendError( errorCode.NAME_CONTAINS_BAR );

        else if( calories.text == "" )
            sendError( errorCode.CAL_EMPTY );

        else if( fat.text == "" )
            sendError( errorCode.FAT_EMPTY );

        else if( carbs.text == "" )
            sendError( errorCode.CARBS_EMPTY );

        if( error )
            return;

        meal += mealName.text + "-"
            + calories.text + "-"
            + fat.text + "-"
            + protein.text + "-"
            + carbs.text;

        DataHandler.addToDB( DataHandler.MEALS_FILE_NAME, meal );

        clearAll( );
        sendGood( mealName.text + " has been added!" );
    }

    void clearAll( ) {
        mealName.text =
            calories.text =
            fat.text =
            protein.text =
            carbs.text = "";
    }
}
