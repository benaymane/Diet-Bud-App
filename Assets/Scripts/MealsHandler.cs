using UnityEngine;
using System;
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
        else if( !isNumeric( ) )
            sendError( errorCode.NO_NUM_INPUT );
        else if( !isPositive( ) )
            sendError( errorCode.NEGATIVE_INPUT );

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

    bool isNumeric( ) {
        int flush;
        return ( Int32.TryParse( calories.text, out flush ) && Int32.TryParse( fat.text, out flush )
            && Int32.TryParse( protein.text, out flush ) && Int32.TryParse( carbs.text, out flush ) );
    }

    bool isPositive( ) {
        return !( calories.text.Contains( "-" ) || fat.text.Contains( "-" ) ||
            protein.text.Contains( "-" ) || carbs.text.Contains( "-" ) );
    }

    void clearAll( ) {
        mealName.text =
            calories.text =
            fat.text =
            protein.text =
            carbs.text = "";
    }
}
