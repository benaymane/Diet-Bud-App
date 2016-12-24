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

    public AudioSource music;

    void Start( ) {
        music.mute = !GlobalVariables.soundOption;
    }

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

        else if( protein.text == "" )
            sendError( errorCode.PROTEIN_EMPTY );

        else if( carbs.text == "" )
            sendError( errorCode.CARBS_EMPTY );

        else if( !isNumeric( ) )
            sendError( errorCode.NO_NUM_INPUT );

        else if( !isPositive( ) )
            sendError( errorCode.NEGATIVE_INPUT );

        if( error )
            return;

        MainScript.newMeals.Add( new Meal( mealName.text, calories.text, fat.text, protein.text, carbs.text ) );
        //DataHandler.addToDB( DataHandler.MEALS_FILE_NAME, meal );

        clearAll( );
        sendGood( mealName.text + " has been added!" );
    }

    bool isNumeric( ) {
        double flush;

        return ( Double.TryParse( calories.text, out flush ) && Double.TryParse( fat.text, out flush )
            && Double.TryParse( protein.text, out flush ) && Double.TryParse( carbs.text, out flush ) );
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
